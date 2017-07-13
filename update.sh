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

if [ "$1" = "container" ] ; then
  do_container=true
  do_tlpdb=false
fi
if [ "$1" = "tlpdb" ] ; then
  do_tlpdb=true
  do_container=false
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

