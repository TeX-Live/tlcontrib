#!/usr/bin/env perl

# $Id: $

# Copyright (C) 2006-2025 Reinhard Kotucha <reinhard.kotucha@gmx.de>
# 
# You may freely use, modify, and/or distribute this file.

my $revision='2025-01-14';

use Getopt::Long;
$Getopt::Long::autoabbrev=0;
Getopt::Long::Configure ("bundling");

my $getfont_url='https://www.tug.org/~kotucha/getnonfreefonts/getfont.pl';
my $banner="This is getnonfreefonts, revision $revision.\n\n";

# messages

my $usage = <<'ENDUSAGE';
Usage:
    getnonfreefonts --sys|--user [options] [font1] [font2] ...

    getnonfreefonts --sys installs fonts in $TEXMFLOCAL.
    getnonfreefonts --user installs fonts in $TEXMFHOME.

    In nearly all cases you should use getnonfreefonts --sys.
    For special cases see 

        http://tug.org/texlive/scripts-sys-user.html

    Options:

        -a|--all        Install all fonts.

        -d|--debug      Provide additional messages for debugging.

        -f|--force      Install fonts even if they are installed already.

        -h|--help       Print this message.

        -l|--lsfonts    List all fonts available.

        -r|--refreshmap Refresh map file.

        -v|--verbose    Be more verbose.

        --version       Print version number.

ENDUSAGE


my $sys_user_err = <<'EOF';
ERROR: You have to invoke getnonfreefonts with either --sys or --user.

In nearly all cases you should use getnonfreefonts --sys.
For special cases see 

   http://tug.org/texlive/scripts-sys-user.html

EOF


# system specific stuff

my %sys=(
  w32     => 0,
  dirsep  => '/',
  pathsep => ':',
  exe     => '',
  dollar  => '\$',
  nulldev => '>/dev/null 2>/dev/null' 
    );

if ($^O=~/^MSWin/i) {
  %sys=(
    w32     => 1,
    dirsep  => '\\',
    pathsep => ';',
    exe     => '.exe',
    dollar  => '$',
    nulldev => '>NUL 2>NUL' 
      );
}

# GetOptions removes optional arguments from @ARGV.
# We have to create a new array because assignments create references.

my @ARGS;
foreach my $arg (@ARGV) {
  push (@ARGS, $arg);
}

$opt_lsfonts=0;
$opt_force=0;
$opt_sys=0;
$opt_user=0;

$done=GetOptions 
    "all|a",
    "debug|d",
    "force|f",
    "help|h",
    "lsfonts|l",
    "refreshmap|r",
    "verbose|v",
    "version|V",
    "sys|s",
    "user|u";

@allpackages=@ARGV;


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

# messages

my $msglen=0;

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

sub debug_msg {
  my $message=shift;
  if ($opt_debug) {
    print STDERR "DEBUG: $message\n";
  }
}

# kpathsea stuff

sub var_value {
  my $var=shift;
  my $ret;

  $ret=`kpsewhich --var-value=$var`;
  chomp($ret);
  return $ret;
}

sub expand_var {
  my $var=shift;
  my $ret;
  my $dollar=$sys{'dollar'};
  $ret=`kpsewhich --expand-var=$dollar$var`;
  chomp($ret);
  return $ret;
}

sub expand_braces {
  my $var=shift;
  my $pathsep;
  my $retstring;
  my @retlist;
  if ($sys{'w32'}) {
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

sub get_tmpdir {
  if ($opt_debug) {
    for my $var (qw(TMPDIR TEMP)) {
      if (defined $ENV{$var}) {
        debug_msg "Environment variable $var='$ENV{$var}'.";
      } else {
        debug_msg "Environment variable $var not set.";
      }
    }
  }
  # get TMPDIR|TEMP environment variable, use /tmp as fallback.

  my $SYSTMP=undef;
  $SYSTMP||=$ENV{'TMPDIR'};
  $SYSTMP||=$ENV{'TEMP'};
  $SYSTMP||='/tmp';
  return "$SYSTMP";
}

sub which {
  my $prog=shift;
  my @PATH;
  my $PATH=$ENV{'PATH'};
  if ($sys{'w32'}) {
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
  my @PATH;
  @PATH=split($sys{'pathsep'}, $ENV{'PATH'});
  my $index=0;
  
  foreach my $dir (@PATH) {
    debug_msg "PATH\[$index\]: '$dir'.";
    ++$index;
  }
}

sub signals {
  # Signals supposed to be supported by Windows are derived from the
  # sources of the Microsoft C runtime library.  It's a matter of fact
  # that not everything described there really works.  Furthermore,
  # the behavior depends heavily on the version of Windows you are
  # using.  Don't expect too much.
  my @signals;
  my @common_signals=qw(INT ILL FPE SEGV TERM ABRT);
  my @signals_UNIX=qw(QUIT BUS PIPE);
  my @signals_Win32=qw(BREAK);

  if ($sys{'w32'}) {
    @signals=(@common_signals, @signals_Win32);
  } else {
    @signals=(@common_signals, @signals_UNIX);
  }
  return @signals;
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


### main ####

debug_msg "getnonfreefonts rev. $revision.";

debug_msg ("argv[0]: '$0'");
my $nargs=@ARGS+0;
for (0..$#ARGS) {
  my $i=$_;
  my $arg=$ARGS[$i];
  $i++;
  debug_msg ("argv[$i]: '$arg'");
}

debug_msg("opt_sys=$opt_sys");
debug_msg("opt_user=$opt_user");

show_path() if ($opt_debug);

check_binary 'kpsewhich';

# Determine INSTALLROOT.

$INSTALLROOTNAME=($opt_sys)? 'TEXMFLOCAL':'TEXMFHOME';

$INSTALLROOT=expand_braces "$INSTALLROOTNAME";

# Remove trailing exclamation marks and double slashes.

debug_msg "Removing leading \"!!\" and trailing \"//\" " . 
    "and set INSTALLROOT:";

$INSTALLROOT=~s/^!!//;
$INSTALLROOT=~s/\/\/$//;

debug_msg "INSTALLROOT='$INSTALLROOT'.";

my $SYSTMP=get_tmpdir;

debug_msg "Internal variable SYSTMP set to '$SYSTMP'.";

$^W=1 if $opt_debug;

unless ($done) {
  print STDERR "\n$banner$usage";
  exit 1;
}

if ($opt_version) {
  print "$revision\n";
  exit 0;
}

if ($opt_sys and $opt_user) {
  print STDERR "$sys_user_err";
  exit 1;
}

if ($opt_help) {
  print ($banner);
  print ($usage);

  if ($opt_sys or $opt_user) {
    print <<"ENDUSAGE";
    Directories:
       temporary: '$SYSTMP/getfont-<PID>'
       install:   '$INSTALLROOT'

ENDUSAGE
  } else {
    print "$sys_user_err";
  }
  exit 0;
}

  
unless ($opt_sys or $opt_user) {
  print "$sys_user_err";
  exit 1;
}

if ($opt_sys and $opt_user) {
  print "$sys_user_err";
  exit 1;
}


check_tmpdir $SYSTMP;
check_installroot "$INSTALLROOTNAME", "$INSTALLROOT";

my $tmpdir="$SYSTMP" . $sys{'dirsep'} . "getfont-$$";
debug_msg "Internal variable tmpdir set to '$tmpdir'.";

mkdir "$tmpdir" or die "ERROR: Can't mkdir '$tmpdir'.";
chdir "$tmpdir" or die "ERROR: Can't cd '$tmpdir'.";

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

my $TLROOT=expand_var 'SELFAUTOPARENT'; ### only used on Windows,
                                        ### hence no portability
                                        ### problem
my $BINDIR=expand_var 'SELFAUTOLOC';

my $has_wget=0;
my $WGET;

if ($sys{'w32'}) { ## wget is shipped with TL.
  $has_wget=1;
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
push @getfont, '--sys' if $opt_sys;
push @getfont, '--refreshmap' if $opt_refreshmap;
push @getfont, '--all' if $opt_all;

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

my $has_updmap_user=0;
if (-f var_value('SELFAUTOLOC').'/updmap-user'.$sys{'exe'}) {
  $has_updmap_user=1;
}

if ($opt_sys) {	     
  debug_msg "Info: Execute updmap-sys if exit status is 2.";
} else {
  if ($has_updmap_user) {
    debug_msg "Info: Execute updmap-user if exit status is 2.";
  } else {
    debug_msg "Info: Execute updmap if exit status is 2.";
  }
}

debug_msg "Exit status of getfont.pl is $exit_status.";

if ($exit_status==2) {
  print "\n";
  msg "Running 'mktexlsr $INSTALLROOT $sys{'nulldev'}' ...";
#  if ($sys{'w32'}) {
    my $ret=system "mktexlsr $INSTALLROOT $sys{'nulldev'}";
#  } else {
#    my $ret=system "mktexlsr $INSTALLROOT >/dev/null 2>/dev/null";
#  }
  
  $ret ? status 'failed', 'red' : status 'done', 'green';
   
  
  my $updmap_command;
  if ($has_updmap_user) {
    $updmap_command=($opt_sys)? 'updmap-sys':'updmap-user';
  } else {
    $updmap_command=($opt_sys)? 'updmap-sys':'updmap';
  }
  @updmap=("$updmap_command");
  push @updmap, '--quiet' unless $opt_verbose;
  print "\n";
  msg "Updating map files ($updmap_command). Be patient...";
  system @updmap;
  
  $? ? status('failed', 'red') : status('done', 'green');
}

remove_tmpdir;

__END__

# Local Variables:
#  mode: Perl
#  perl-indent-level: 2
#  indent-tabs-mode: nil
#  coding: utf-8-unix
# End:
# vim:set tabstop=2 expandtab:
