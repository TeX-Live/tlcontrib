The AcroTeX eDucation Bundle 
Author: D. P. Story 
Dated: 2020-03-14

AeB contains the following:

1) Web Package: Extensive support for page design.

2) Exerquiz Package: Support for creating online interactive exercises and
   quizzes.

3) eforms Package: Extensive support for Acrobat forms and links

4) dljslib package: A package of JavaScript functions that extends the
    capability of exerquiz.

5) taborder package: Supports the create of a tab order for form fields.

6) Documentation for AeB (AcroTeX eDucatation Bundle) and eForms
    (including insdljs and dljslib).

What's New (2020-03-14) Minor changes that support new features of eqexam.

What's New (2020-01-01) Replace use of \count0 with new counter \eqtmpcnta, 
in situation, the value of \count0 was leaking out giving incorrect page 
numbers. 

What's New (2019-12-17) Defined \InputExrSolnsLevel and \InputQzSolnsLevel to
make it easy to change the section-type for the solution pages.

What's New (2019-08-13) Use \protect when formatting a enhanced preview value.
Other minor changes and bug fixes.

What's New (2019-05-24) Added the enhanced preview feature. When in effect along with
ordinary preview, captions of buttons and initial values of other fields are viewable 
in non-conforming PDF readers.

What's New (2019/03/16) minor bug fixes; added \bParams/\eParams command pair to pass
arguments to JS code snippets declared within the defineJS environment. Fixed the spacing
problem when dvips is used to compile a doc containing the defineJS environment.

What's New (2018/12/13) More changes in exerquiz to support mi-solns; misc. bug fixes.

What's New (2028/12/05) Some changes in exerquiz to support mi-solns. Require aeb-comment
(version 3.2 of comment.sty). The newer versions of comment.sty are incompatible with
eqexam (which is supported by exerquiz) and introduces spurious spaces.

What's New (2018/11/27) Some changes to eforms package to support the new icon-appr
package.

What's New (2018/08/16) Changes to eforms and insdljs: several keys (eforms) added to
support aeb_mlink and annot_pro; switch add to insdljs to detect whether document JS
has been included.

What's New (2018/03/22) Changes in eforms to support features of aeb_mlink. Minor bug fixes

What's New (2018/02/13) Added commands to optionally group each solution when the appear
at the end of the file; this applies both exercises and quizzes.

What's New (2017/09/06) Suggested new problem type: "Correcting a math problem", see
http://www.acrotex.net/blog/?p=1335 for a demo. Better compatibility with luatex. Minor
bug fixes.

What's New (2017-08-08) Support for multi-letter variables, alternate appearances,
and interval repetition. The demo file for these features is
http://www.acrotex.net/blog/?p=1330

I maintain a web site featuring TeX/LaTeX/PDF stuff called
AcroTeX (www.math.uakron.edu/~dpstory/acrotex.html).  There you
will find the home web page of the AcroTeX eDucation Bundle.
(www.math.uakron.edu/~dpstory/webeq.html).

Examples for AeB have been moved to
http://www.math.uakron.edu/~dpstory/webeq_ex.html, another copy
of the examples are at
http://www.acrotex.net/blog/?cat=89

Additional examples are posted on the AcroTeX Blog page
http://www.acrotex.net/blog/

Now, I simply must get back to my retirement.

D. P. Story
www.acrotex.net
dpstory@uakron.edu
dpstory@acrotex.net
