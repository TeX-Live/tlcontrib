#!/bin/sh
#
# KETCindy starter script
#
# (C) 2017 Norbert Preining
# Licensed under the same license terms as ketpic itself, that is GPLv3+
#

BinaryName=Cinderella2
TemplateFile=template.cdy

if [ "$1" = "-c" ]
then
  shift
  cinderella="$1"
else
  cinderella=`which $BinaryName 2>/dev/null`
fi

if [ -z "$cinderella" ]
then
  echo "Cannot find $BinaryName!" >&2
  exit 1
fi

if [ ! -x "$cinderella" ] ; then
  echo "Program $cinderella is not executable!" >&2
  exit 1
fi
# find real path
realcind=`realpath "$cinderella"`
cinddir=`dirname "$realcind"`
plugindir="$cinddir/Plugins"
plugin="$plugindir/KetCindyPlugin.jar"

# find Jar
KetCdyJar=`kpsewhich -format=texmfscripts KetCindyPlugin.jar`
# search for template.cdy
TempCdy=`kpsewhich -format=texmfscripts $TemplateFile`

if [ -z "$TempCdy" -o -z "$KetCdyJar" ]
then
  echo "Cannot find $TemplateFile via kpsewhich, is ketpic installed?" >&2
  exit 1
fi


if [ ! -r "$plugin" ] ; then
  echo "Cinderella is *NOT* set up for KETCindy!"
  echo "You need to copy"
  echo "   $KetCdyJar"
  echo "into"
  echo "   $plugindir"
  echo ""
  exit 1
fi

mkdir -p ~/ketcindy

exec "$cinderella" "$TempCdy"

