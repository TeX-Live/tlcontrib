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

