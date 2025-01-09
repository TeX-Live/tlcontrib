$special{'acmtrans'}    = '&MAKEflatten';
$special{'adobecaslon'} = '&donormal';
delete $special{'comicsans'}; # need to delete to get -tds ready try
$special{'chemsym'}     = '&MAKEflatten';
$special{'imprintmtshadow'} = '&donormal';
$special{'lsabon'}      = '&donormal';
$special{'ptmsc'}       = '&donormal';
$special{'thsmc'}       = '&donormal';
$special{'verdana'}     = '&donormal';
$special{'lucidabr'}    = '&donormal';
$special{'lucold'}      = '&MAKEflatten';
$special{'lucida-otf'}  = '&MAKEflatten';
$specialdocfmt{'lucida-otf'} = "latex";
$specialtex{'lucida-otf'} = '^lucida-otf-kern\.tex|' . $standardtex;
$special{'futurans'}    = '&donormal';
$special{'pcarl'}       = '&donormal';
$special{'mathstone'}   = '&MAKEflatten';
$special{'spark-otf'}   = '&MAKEflatten';
$special{'opacity-pro'} = '&donormal';
$special{'notocjksc'}   = '&donormal';
$specialfoundry{'notocjksc'} = "google";
$special{'notocondensed'} = '&donormal';
$specialfoundry{'notocondensed'} = "google";
$special{'eq-save'}     = '&MAKEdps';
$special{'fetchbibpes'} = '&MAKEdps';
$special{'mkstmpdad'}   = '&MAKEdps';
$special{'dps'}         = '&MAKEdps';
$special{'fetchbibpes'} = '&MAKEdps';
$special{'datepicker-pro'} = '&MAKEdps';
$special{'aeb-pro'}     = '&MAKEdps';
$special{'annot-pro'}   = '&MAKEdps';
$special{'acrotex'}     = '&MAKEdps';
$special{'acrotex-js'}  = '&MAKEdps';
$special{'acromemory'}  = '&MAKEdps';
$special{'aeb-mlink'}   = '&MAKEdps';
$special{'aeb-minitoc'} = '&MAKEdps';
$special{'graphicxsp'}  = '&MAKEdps';
$special{'icon-appr'}   = '&MAKEdps';
$special{'eqexam'}      = '&MAKEdps';
$special{'bargraph-js'} = '&MAKEdps';
$special{'pmdb'}        = '&MAKEdps';
$special{'thorshammer'} = '&MAKEdps';
$special{'richtext'}    = '&MAKEdps';
$special{'thorshammer'} = '&MAKEdps';
$special{'fitr'}        = '&MAKEdps';
$special{'artthreads'}  = '&MAKEdps';
$special{'popupmenu'}   = '&MAKEdps';
$special{'ltx4yt'}      = '&MAKEdps';
$special{'rmannot'}     = '&MAKEdps';
$special{'eq-fetchbbl'} = '&MAKEdps';
$special{'docassembly'} = '&MAKEdps';
$special{'siam'}        = '&MAKEsiam';

$special{'fontsetup-nonfree'} = '&donormal';
$posthook{'fontsetup-nonfree'} = '&POST_onelevel';

delete($special{'garamondx'});
$posthook{'garamondx'} = '&POSTmove_subdirs';

sub MAKEdps {
  print "\t SPECIAL $package";

  # sort of flatten, but not completely (keep the 'examples' subdir)
  &xchdir($packagedir);
  &SYSTEM('if [ -d doc ] ; then mv doc/* . && rmdir doc ; fi');
  &SYSTEM('if [ -d docs ] ; then mv docs/* . && rmdir docs ; fi');

  # now do the normal thing
  &xchdir($RAW_DIR);
  &donormal();
}

sub POSTmove_subdirs {
  print "POSTmove_subdirs - just move directories around\n";
  &conditionally_rename_with_mkdir ("afm", "$DEST/fonts/afm/public/$package");
  &conditionally_rename_with_mkdir ("enc", "$DEST/fonts/enc/dvips/$package");
  &conditionally_rename_with_mkdir ("latex", "$DEST/tex/latex/$package");
  &conditionally_rename_with_mkdir ("tex", "$DEST/tex/latex/$package");
  &conditionally_rename_with_mkdir ("map/dvips", "$DEST/fonts/map/dvips/$package");
  &conditionally_rename_with_mkdir ("map/pdftex", "$DEST/fonts/map/pdftex/$package");
  &conditionally_rename_with_mkdir ("map", "$DEST/fonts/map/dvips/$package");
  &conditionally_rename_with_mkdir ("source", "$DEST/source/fonts/$package");
  &conditionally_rename_with_mkdir ("src", "$DEST/source/fonts/$package");
  &conditionally_rename_with_mkdir ("tfm", "$DEST/fonts/tfm/public/$package");
  &conditionally_rename_with_mkdir ("truetype", "$DEST/fonts/truetype/public/$package");
  &conditionally_rename_with_mkdir ("type1", "$DEST/fonts/type1/public/$package");
  &conditionally_rename_with_mkdir ("vf", "$DEST/fonts/vf/public/$package");
  if (-l "README") {
    &SYSTEM("rm README"); # symlink, should be removed already, but to be sure
  }
  &SYSTEM("$MV doc/* .");
  &SYSTEM("rmdir doc");
}

sub conditionally_rename_with_mkdir {
  my ($from,$to) = @_;
  if (! -e $from) { return }
  die "rename_with_mkdir needs exactly two args (got @_)" if @_ != 2;
  if (-e $to) {
    rmdir ($to); # ignore errors.
    die ("rename_with_mkdir destination exists: $to\n"
         . `ls $to`)
      if -e $to;
  }
  (my $parent = $to) =~ s,/[^/]*$,,;
  &xmkdir ($parent);
  &xsystem ("$MV $from $to");
}

