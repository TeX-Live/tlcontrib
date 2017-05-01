#!/usr/bin/env perl

## getnonfreefonts
## Copyright 2006-2016 Reinhard Kotucha <reinhard.kotucha@web.de>
#
# This work may be distributed and/or modified under the
# conditions of the LaTeX Project Public License, either version 1.3
# of this license or (at your option) any later version.
# The latest version of this license is in
#   http://www.latex-project.org/lppl.txt
# 
# The current maintainer is Reinhard Kotucha.

my $TL_version='2016';  
my $revision='2016-11-08';

use Getopt::Long;
$Getopt::Long::autoabbrev=0;
Getopt::Long::Configure ("bundling");

$opt_lsfonts=0;
$opt_force=0;

sub usage {
    print <<'EOF';
Usage:
    getnonfreefonts[-sys] [-a|--all] [-d|--debug] [-f|--force]
        [-l|--lsfonts] [-r|--refreshmap] [-v|--verbose] [--version] 
        [-H|--http] [font1] [font2] ...

    getnonfreefonts installs fonts in $TEXMFHOME.
    getnonfreefonts-sys installs fonts in $TEXMFLOCAL.

    Options:
        -a|--all        Install all fonts.

        -d|--debug      Provide additional messages for debugging.

        -f|--force      Install fonts even if they are installed already.

        -h|--help       Print this message.

        -l|--lsfonts    List all fonts available.

        -r|--refreshmap Refresh map file.

        -v|--verbose    Be more verbose.

        -H|--http       Use http instead of ftp (see manual).

        --version       Print version number.

EOF
;
}

# GetOptions destroys @ARGV.
# We have to create a new array because assignments create references.

my @ARGS;
foreach my $arg (@ARGV) {
  push (@ARGS, $arg);
}

$done=GetOptions 
    "all|a",
    "debug|d",
    "force|f",
    "help|h",
    "http|H",
    "lsfonts|l",
    "refreshmap|r",
    "verbose|v",
    "version",
    "sys";

unless ($done) {
  print "\n"; usage; exit 1;
}

$^W=1 if $opt_debug;

my $pathsep;

sub win32 {
  if ($^O=~/^MSWin(32|64)/i) {
    $pathsep="\\";
    return 1;
  } else {
    $pathsep="/";
    return 0;
  }
}


# ANSI colors

my @supported_terminals=(
  'Eterm', 'ansi', 'color-xterm', 'con132x25', 'con132x30',
  'con132x43', 'con132x60', 'con80x25', 'con80x28', 'con80x30',
  'con80x43', 'con80x50', 'con80x60', 'cons25', 'console', 'cygwin',
  'dtterm', 'gnome', 'gnome-256color', 'konsole', 'kterm', 'linux',
  'linux-c', 'mach-color', 'mlterm', 'putty', 'rxvt', 'rxvt-cygwin',
  'rxvt-cygwin-native', 'rxvt-unicode', 'screen', 'screen-bce',
  'screen-w', 'screen.linux', 'vt100', 'xterm', 'xterm-256color',
  'xterm-color', 'xterm-debian'
    );

my $term_supported=0;

if ($ENV{'TERM'}) {
  foreach my $terminal (@supported_terminals) {
    if ($ENV{'TERM'} eq $terminal) {
      $term_supported=1;
    }
  }
}


sub colored {
  my $text=shift;
  my $color=shift;

  my $esc="\e[";
  my $restore="\e[0m";

  %colors=(
    red   => '00;31m',
    green => '00;32m'
      );

  if ($term_supported and -t 'STDOUT') {
    return "${esc}$colors{$color}${text}${restore}";
  } else {
    return $text;
  }
}


sub msg {
  my $message=shift;
  $msglen=length($message);
  print $message;
}


sub status {
  my $status=shift;
  my $color=shift;

  my $spaces=79-2-$msglen-length($status);
  $spaces=1 if ($spaces < 1);
  print ' ' x $spaces;
  print ('[' . colored($status, $color) . "]\n");
}


sub expand_var {
  my $var=shift;

  if (win32) {
    open KPSEWHICH, 'kpsewhich --expand-var=$'  . "$var |";
  } else {
    open KPSEWHICH, 'kpsewhich --expand-var=\$' . "$var |";
  }
  while (<KPSEWHICH>) {
    chop;
    return "$_";
  }
  close KPSEWHICH;
}

sub var_value {
  my $var=shift;
  my $ret;

  $ret=`kpsewhich --var-value=$var`;
  chomp($ret);
  return $ret;
}


@allpackages=@ARGV;

print "$TL_version\n" and exit 0 if $opt_version; 

if ($opt_help or !@ARGS) {
  print "\nThis is getnonfreefonts";
  print '-sys' if ($sys);
  print ", version $TL_version, revision $revision.\n\n";
  usage; 
}


sub message {
  my $message=shift;
  if ($message=~/^\[/) {
    print "$message\n";
  } else {
    printf "%-60s", $message;
  }
}


sub debug_msg {
  my $message=shift;
  if ($opt_debug) {
    print STDERR "DEBUG: $message\n";
  }
}


sub get_tmpdir {
  if ($opt_debug) {
    for my $var (qw(TMPDIR TEMP TMP)) {
      if (defined $ENV{$var}) {
        debug_msg "Environment variable $var='$ENV{$var}'.";
      } else {
        debug_msg "Environment variable $var not set.";
      }
    }
  }
  # get TMPDIR|TEMP|TMP environment variable

  my $SYSTMP=undef;
  $SYSTMP||=$ENV{'TMPDIR'};
  $SYSTMP||=$ENV{'TEMP'};
  $SYSTMP||=$ENV{'TMP'};
  $SYSTMP||='/tmp';
  return "$SYSTMP";
}

sub which {
  my $prog=shift;
  my @PATH;
  my $PATH=$ENV{'PATH'};
  if (&win32) {
    my @PATHEXT=split ';', $ENV{'PATHEXT'};
    push @PATHEXT, '';  # if argument contains an extension
    @PATH=split ';', $PATH;
    for my $dir (@PATH) {
	    $dir=~s/\\/\//g;
	    for my $ext (@PATHEXT) {
        if (-f "$dir/$prog$ext") {
          return "$dir/$prog$ext";
        }
	    }
    }
  } else {
    @PATH=split ':', $PATH;
    for my $dir (@PATH) {
	    if (-x "$dir/$prog") {
        return "$dir/$prog";
	    }
    }
  }
  return 0;
}

sub show_path {
  if ($opt_debug) {
    my @PATH;
    if (win32) {
      @PATH=split ';', $ENV{'PATH'};
    } else {
      @PATH=split ':', $ENV{'PATH'};
    }
    my $index=0;
    
    foreach my $dir (@PATH) {
      debug_msg "PATH\[$index\]: '$dir'.";
      ++$index;
    }
  }
}

sub signals {
  my @signals;
  my @common_signals=qw(INT ILL FPE SEGV TERM ABRT);
  my @signals_UNIX=qw(QUIT BUS PIPE);
  my @signals_Win32=qw(BREAK);

  if (win32) {
    @signals=(@common_signals, @signals_Win32);
  } else {
    @signals=(@common_signals, @signals_UNIX);
  }
  debug_msg "Supported signals: @signals.";
  return @signals;
}


sub getfont_url {
  my $getfont_url;
  my $HTTPS='https://www.tug.org/~kotucha/getnonfreefonts';
  debug_msg 'Download method: HTTPS.';
  $getfont_url="$HTTPS/getfont.pl";
  debug_msg "Using script '$getfont_url'.";
  return $getfont_url;
}


sub expand_braces {
  my $var=shift;
  my $pathsep;
  my $retstring;
  my @retlist;
  if (win32) {
    open KPSEWHICH, 'kpsewhich --expand-braces=$'  . "$var |";
    $pathsep=';';
  } else {
    open KPSEWHICH, 'kpsewhich --expand-braces=\$' . "$var |";
    $pathsep=':';
  }
  $retstring=(<KPSEWHICH>);
  close KPSEWHICH;
  chop $retstring;
  @retlist=split $pathsep, $retstring;
  if ($opt_debug) {
    my $index=0;
    foreach my $entry (@retlist) {
	    debug_msg "$var\[$index\]: '$entry'.";
	    ++$index;
    }
    debug_msg "Extracting the first element of the list ($var\[0\]):";
    debug_msg "$var\[0\]='$retlist[0]'.";
  }
  return "$retlist[0]";
}

sub check_tmpdir{
  my $SYSTMP=shift;
  die "! ERROR: The temporary directory '$SYSTMP' doesn't exist.\n"
      unless (-d "$SYSTMP");

  die "! ERROR: The temporary directory '$SYSTMP' is not writable.\n"
      unless (-w "$SYSTMP");
}

sub check_installroot {
  my ($INSTALLROOTNAME, $INSTALLROOT)=@_;
  mkdir "$INSTALLROOT" unless (-d "$INSTALLROOT");

  die "! ERROR: The variable $INSTALLROOTNAME is not set.\n"
      unless length ("$INSTALLROOT") > 0;
  
  die "! ERROR: The install directory '$INSTALLROOT' doesn't " .
      "exist.\n" .
      "         If this is the correct path, please create " .
      "this directory manually.\n" 
      unless (-d "$INSTALLROOT");
  
  die "! ERROR: The install directory '$INSTALLROOT' is not writable.\n"
      unless (-w "$INSTALLROOT");
}

sub check_binary {
  my $binary=shift;
  if ($opt_debug) {
    debug_msg "Search for $binary in PATH:";
    my $binary=which "$binary";
    debug_msg "Found '$binary'.";
  }
}

debug_msg "getnonfreefonts rev. $revision (TL $TL_version).";

debug_msg ("argv[0]: '$0'");
my $nargs=@ARGS+0;
for (0..$#ARGS) {
  my $i=$_;
  my $arg=$ARGS[$i];
  $i++;
  debug_msg ("argv[$i]: '$arg'");
}


my $sys=($0=~/-sys$/)? 1:0;
if ($sys==1) {
  debug_msg("sys=true, determined by filename");
} else {
  debug_msg("sys=false, determined by filename");
}

$sys=1 if $opt_sys;
debug_msg("sys=true, determined by option") if $opt_sys;

##$sys=1 if (defined $ENV{'TEX_SYS_PROG'});

if (win32) {
# ugly workaround for --sys detection in runscript wrapper.
  my $TEXMFVAR=var_value('TEXMFVAR');
  my $TEXMFSYSVAR=var_value('TEXMFSYSVAR');
  debug_msg("TEXMFVAR=$TEXMFVAR");
  debug_msg("TEXMFSYSVAR=$TEXMFSYSVAR");

  if ($TEXMFVAR eq $TEXMFSYSVAR) {
    $sys=1;
    debug_msg("sys=true, determined by kpathsea vars");
  }
}

# Determine the URL.
my $getfont_url=getfont_url;

my $SYSTMP=get_tmpdir;

debug_msg "Internal variable SYSTMP set to '$SYSTMP'.";

check_binary 'kpsewhich';

# Determine INSTALLROOT.

$INSTALLROOTNAME=($sys)? 'TEXMFLOCAL':'TEXMFHOME';

$INSTALLROOT=expand_braces "$INSTALLROOTNAME";

# Remove trailing exclamation marks and double slashes.

debug_msg "Removing leading \"!!\" and trailing \"//\" " . 
    "and set INSTALLROOT:";

$INSTALLROOT=~s/^!!//;
$INSTALLROOT=~s/\/\/$//;

debug_msg "INSTALLROOT='$INSTALLROOT'.";

($sys)? debug_msg "sys=true.":debug_msg "sys=false.";

if ($opt_help or !@ARGS) {
  print <<"ENDUSAGE";
  Directories:
       temporary: '$SYSTMP/getfont-<PID>'
       install:   '$INSTALLROOT'

ENDUSAGE
check_tmpdir $SYSTMP;
check_installroot "$INSTALLROOTNAME", "$INSTALLROOT";
exit 0;
}

check_tmpdir $SYSTMP;
check_installroot "$INSTALLROOTNAME", "$INSTALLROOT";

my $tmpdir="$SYSTMP" . $pathsep . "getfont-$$";
debug_msg "Internal variable tmpdir set to '$tmpdir'.";

mkdir "$tmpdir" or die "! ERROR: Can't mkdir '$tmpdir'.";
chdir "$tmpdir" or die "! ERROR: Can't cd '$tmpdir'.";

#install_signal_handlers $SYSTMP, $tmpdir;

sub remove_tmpdir {
  debug_msg "Executing Signal Handler:";
  chdir "$SYSTMP" or die "! ERROR: Can't cd '$SYSTMP'.\n";
  opendir TMPDIR, "$tmpdir" 
      or print "! ERROR: Can't read directory '$tmpdir'.\n";
  my @alltmps=readdir TMPDIR;
  closedir TMPDIR;
  foreach my $file (@alltmps) {
    next if $file=~/^\./;
    unlink "$tmpdir/$file";
    debug_msg "unlink '$tmpdir/$file'.";
  }
  rmdir "$tmpdir" or die 
      "! ERROR: Can't remove directory '$tmpdir'.\n";
  debug_msg "rmdir  '$tmpdir'.";
}

foreach my $signal (signals) {
  $SIG{"$signal"}=\&remove_tmpdir;
}
debug_msg 
    "Signal handlers installed. Don't expect too much on Windows.";

my $TLROOT=expand_var 'SELFAUTOPARENT';
my $BINDIR=expand_var 'SELFAUTOLOC';

my $has_wget=0;
my $WGET;

if (win32) {
  $has_wget=1; ## shipped with TL.
  if (-f "$TLROOT\\tlpkg\\installer\\wget\\wget.exe") {
	# TL-2008+
    $WGET="$TLROOT\\tlpkg\\installer\\wget\\wget.exe";
  } elsif (-f "$BINDIR\\wget.exe") {
	# TL-2005...TL-2007
    $WGET="$BINDIR\\wget.exe";
  } else {
    die "ERROR: No wget binary found.\n";
  }
} elsif (which "wget") {
  $WGET='wget';
  $has_wget=1; ## wget is in PATH.
}

show_path;

debug_msg "No wget binary found on your system, trying curl." 
    unless ($has_wget);

if ($has_wget) {
  debug_msg "Running '$WGET $getfont_url'.";
  system ("$WGET", "$getfont_url") == 0 
      or die "! Error: Can't execute wget.\n";
} else {
  debug_msg "Running 'curl -O $getfont_url'.";
  system ('curl', '-O', "$getfont_url") == 0 
      or die "! Error: Can't execute curl.\n";
}

# Download the fonts.

my @getfont=('perl', "./getfont.pl");
push @getfont, "--installroot=$INSTALLROOT";
push @getfont, '--lsfonts' if $opt_lsfonts;
push @getfont, '--force' if $opt_force;
push @getfont, '--debug' if $opt_debug;
push @getfont, '--verbose' if $opt_verbose;
push @getfont, '--sys' if $sys;
push @getfont, '--refreshmap' if $opt_refreshmap;
push @getfont, '--all' if $opt_all;
push @getfont, '--http' if $opt_http;
if ($has_wget) {
  push @getfont, "--wget_bin=$WGET";
} else {
  push @getfont, '--use_curl';
}
push @getfont, @allpackages;

my $getfont_cmd=join " ", @getfont;

debug_msg "Running '$getfont_cmd'.";

system @getfont;

# Evaluate the exit status.  It will be 2 if something had been
# installed and in this case we need to run texhash/updmap.

my $exit_code=$?;
my $exit_status=int($exit_code/256);

if ($sys) {	     
  debug_msg "Info: Execute updmap-sys if exit status is 2.";
} else {
  debug_msg "Info: Execute updmap if exit status is 2.";
}

debug_msg "Exit status of getfont.pl is $exit_status.";

if ($exit_status==2) {
  print "\n";
  msg "Running 'mktexlsr $INSTALLROOT'...";
  if (0) { 
    my $ret=system "mktexlsr $INSTALLROOT >NUL 2>NUL";
  } else {
    my $ret=system "mktexlsr $INSTALLROOT >/dev/null 2>/dev/null";
  }
  if ($ret) {
    status 'failed', 'red';
  } else {
    status 'done', 'green';
  }
  
  
  $updmap_command=($sys)? 'updmap-sys':'updmap';
  @updmap=("$updmap_command");
  push @updmap, '--quiet' unless $opt_verbose;
  print "\n";
  msg "Updating map files ($updmap_command). Be patient...";
  system @updmap;
  
  $? ? status('failed', 'red') : status('done', 'green');
}

remove_tmpdir;

__END__

#  Local Variables:
#    perl-indent-level: 2
#    tab-width: 2
#    indent-tabs-mode: nil
#  End:
#  vim:set tabstop=2 expandtab:
