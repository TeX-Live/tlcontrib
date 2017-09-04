#!/bin/bash
# (c) 2016-2017 Norbert Preining
# License: GPLv3+
#
# USAGE:
# call this script with the two envvars below set to proper values
# eg:
#   TLCHECKOUT=/path/to/tl/svn/checkout TLNETDEST=/path/to/created/repo update.sh
# at the moment the generated repository in TLCHECKOUT is *not* signed
# due to the --no-sign option. You would need the TL distribtuion key
# to sign. But you can sign with a different key and tell the users to
# use tlmgr key add etc, see manual.

TLCHECKOUT=${TLCHECKOUT:-/home/norbert/Development/TeX/texlive.git}
TLNETDEST=${TLNETDEST:-/home/norbert/Domains/server/texlive.info/contrib/2017}
TLCATALOGUE=${TLCATALOGUE:-/home/norbert/Development/TeX/texcatalogue-svn}

# we don't do TeX Catalogue updates
#unset TEX_CATALOGUE

do_tlpdb=false
do_container=false
do_collection=false
if [ "$1" = "container" ] ; then
  do_container=true
elif [ "$1" = "tlpdb" ] ; then
  do_tlpdb=true
elif [ "$1" = "collection" ] ; then
  do_collection=true
else
  do_tlpdb=true
  do_container=true
  do_collection=true
fi

if $do_collection ; then
  col=tlpkg/tlpsrc/collection-contrib.tlpsrc
  echo "category Collection" > $col
  echo "shortdesc tlcontrib packages" >> $col
  echo "longdesc collections of all packages in contrib.texlive.info" >> $col
  for i in `ls tlpkg/tlpsrc/*.tlpsrc | sort` ; do
    bn=`basename $i .tlpsrc`
    if [ "$bn" = "00texlive.autopatterns" -o "$bn" = "00texlive.config" -o "$bn" = 00texlive.installation \
       -o "$bn" = collection-contrib ] ; then
      continue
    fi
    echo "depend $bn" >> $col
  done

  git add $col
  if ! git diff --cached --exit-code >/dev/null ; then 
    # something is staged
    echo "collection contrib is updated, diff is as following:"
    git diff --cached
    echo ""
    echo -n "Do you want to commit these changes (y/N): "
    read REPLY <&2
    case $REPLY in
      y*|Y*) git commit -m "collection-contrib updated" ;;
      *) echo "Ok, leave it for now!" ;;
    esac
  fi
fi

if $do_tlpdb ; then
  # update tlpdb
  $TLCHECKOUT/Master/tlpkg/bin/tl-update-tlpdb \
	-with-w32-pattern-warning -from-git \
	--catalogue=$TLCATALOGUE	\
	--master=`pwd`
fi

if $do_container ; then
  $TLCHECKOUT/Master/tlpkg/bin/tl-update-containers \
	-master `pwd` \
	-location $TLNETDEST	\
	-gpgcmd `pwd`/tl-sign-file \
	-all # sometimes we need -recreate

  grep ^name tlpkg/texlive.tlpdb | grep -v 00texlive | grep -v '\.' | awk '{print$2}' | sort > $TLNETDEST/packages.txt
fi

# sometimes -recreate might be necessary to fully rebuild!

