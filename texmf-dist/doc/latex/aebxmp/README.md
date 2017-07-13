The aebxmp Package
Author: D. P. Story
Dated: 2017-02-17

This is a package that requies the document author to have the full Acrobat
application. In this case the dvips/Distiller, pdflatex, or xelatex workflow
may be use to create the PDF.

The package provides commands for populating certain additional metadata,
beyond that already provided by hyperref.

1.  Commands to set the copyright status, notice, and url fields (as seen in the
    Additional Metadata dialog accessed from the Document Properties >
    Description tab.

2.  (v2.0) Added support for two other fields found in the Additional Metadata
    dialog box: for populating Author Title and Description Writer.

3.  (v2.0) aebxmp also sets the value of Created as seen at the bottom of the
    Additional Metadata dialog box

4.  (v2.0) aebxmp defines the \Authors command for setting multiple authors,
    the authors are accessible separately using Doc.info.Authors.

5.  (v2.0) Finally, aebxmp defines a command for setting custom document
    properties, this is seen on the Custom tab of the Document Properties
    dialog box.

6.  (v2.2) Added a \Keywords command that takes a comma-delimited list
    of keywords, and creates an array of keywords. These keywords can be
    accessed individually using a special document-level JavaScript
    function.

7.  (v2.3) Rewrote some of the code so that now the XMP package is set
    using only E4X; removed all literal elements.

8.  (v2.3d) Added access functions getCopyrightStatus(),
    getCopyrightInfoURL(), getAuthorTitle(), and getDescriptionWriter(). 

9.  (v2.5) Extended aebxmp to include a non-Distiller workflow as long as
    the document author has the Acrobat application.

10. (v2.5a) Require insdljs dated 2016/07/31 to make colon syntax available.
    Values of customProperties can use the colon notation.

My other web site is http://www.acrotex.net/, follow my articles at
http://blog.acrotex.net.

Now, I simply must get back to my retirement.

D. P. Story
dpstory at acrotex dot net

