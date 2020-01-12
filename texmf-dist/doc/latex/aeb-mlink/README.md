Package: aeb_mlink 
Author: D. P. Story 
Dated: 2020-01-06

The aeb_mlink package defines new link commands to create multi-line links. 
The new commands are \mlhypertext, \mlhyperlink, \mlhyperref, \mlnameref, 
\mlNameref, \mlhref, and \mlurl. 

PDF Creators: Adobe Distiller or ps2pdf 

What's New (2020-01-06) This package reads web.cfg (web package, 
if available). The web.cfg has changed format slightly, this 
update now conforms to that format. Defined new option urlOpts to 
pass options to the url package. 

What's New (2018/08/18): Created \turnSyllbCntOn (\turnSyllbCntOff) to turn
on (resp., off) the viewing of syllable numbers. Reorganize core program to 
accommodate the use of \mlhypertext command within the program code of 
annot_pro. (This is to implement text markup annotations in that package.)

What's New (2018/04/26): Included a 'dummy' package named aeb-mlink. The 
aeb_mlink package is listed on CTAN as aeb-mlink, though there is no such 
package by that name. Well, now there is. 

What's New (2018/04/20): Previously, the links created by aeb_mlink consisted 
of a series of rectangles around each of the syllables of the hypertext or 
url; the little rectangles responded in unison when any one of them were 
clicked. In this version, Postscript procedures are used to combine the 
little rectangles into one rectangle per line of hypertext. Also the Rect 
entry has been changed to be the smallest rectange that encloses the 
hypertext (or url). The Postscript tries to detect problems with the links, 
and reports to the log (distiller or ps2pdf log). The PDF Specification does 
not support cross-page links, to resolve this issued, a method of 'cracking' 
a link apart, breaking it into two links. The second link is free to move to 
the next page. 

This package requires the latest version of AeB, in particular, the eforms 
package required is dated 2018/03/22 or later. See ctan.org/pkg/acrotex. 

In addition to the demo files distributed with the package, there are two new 
demo files available from the AcroTeX Blog webiste: 

  aeb_mlink: Fixing multi-line link boxes 
    (http://www.acrotex.net/blog/?p=1377)

  aeb_mlink: Crossing page boundaries with multi-line links 
    (http://www.acrotex.net/blog/?p=1383) 

Let me know if there are problems or suggested features.  e-mail
me at dpstory@uakron.edu or dpstory@acrotex.net

My other web site is http://blog.acrotex.net/ has the latest on my
ruminations on LaTeX and PDF.

Now, I simply must get back to my retirement.

D. P. Story
dpstory@uakron.edu

