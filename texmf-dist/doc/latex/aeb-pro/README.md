The AeB Pro Package
Author: D. P. Story
Dated: 2021-06-20

The AeB Pro Package complements and extends AcroTeX eDucation Bundle.

AeB Pro implements a number of new features:
    (1) AeB Central (this can be used by non-distiller users)
    (2) Complete support for set up the initial view of the document
    (3) Extensive support for document actions: document level JS,
        set document actions (willSave, didSave, etc.) and open
        document actions. (4) Complete support for page actions (5)
        Complete support for full screen mode, and all the current
        transition effects through version 8.
    (6) Methods to easily attach documents
    (7) document assembly methods, methods used immediately following
        PDF creation.
    (8) Methods for linking to attachments and launching attachments
    (9) Support for creating PDF Packages.
   (10) Initializing fields using unicode.
   (11) Basic support for layers, rollovers and animations.
   (12) In this version (v2.1 or later), the package is opened up to 
        non-Distiller workflows. Use the dvips/Distiller workflow to obtain all 
        features, (1)--(11) below, of this package. For authors that have Acrobat 
        but prefer a non-Distiller workflow, use the useacrobat option for 
        features (1)--(10) below. For authors who do not have Acrobat, use the 
        nopro option to access features (1)--(3), 

Extensive examples illustrate all these features.

Installation Instructions: Contained in the documentation
aebpro_man.pdf and in install_jsfiles.pdf.

Let me know if there are problems or suggested features.  E-mail
me at dpstory@uakron.edu or dpstory@acrotex.net

The AcroTeX Blog (http://blog.acrotex.net/) lists the distribution files at
http://www.acrotex.net/blog/?page_id=835, all demo files that use AeB Pro
are listed at http://www.acrotex.net/blog/?tag=aeb-pro.

What's New (2021-06-20) Now require acrotex-js (https://www.ctan.org/pkg/acrotex-js)
  to provide the folder JavaScript support and instructions for installation
  and testing.

What's New (2021-04-27) Added aebCertifyInvisibleSign() to aeb_pro.js 
(Version 1.5) This JS function supports http://www.acrotex.net/blog/?p=1274,
an article titled Certify Invisible Signing using AeB Pro.

What's New (2021-02-07) Fix a long time bug of page events.

What's New (2021-02-04) Updated documentation to reflect new security restriction
by Adobe Acrobat DC (purchased or updated after December 2020). Authored the document
acrobat-in-workflow.pdf to explain the procedure to configure Acrobat DC. Creating
an altnernative name of aeb-pro, to conform to CTAN naming.

What's New (2019/03/21) This new version requires aeb-comment 
(version 3.2 of the comment package). Changed the order of 
loading of the insdljs package. 

What's New (2018-02-17) Added a star-option to \texHelp, added demo files to 
illustrate rollover animation techniques (http://www.acrotex.net/blog/?tag=rollovers). 

Now, I simply must get back to my retirement.

D. P. Story
dpstory@uakron.edu
