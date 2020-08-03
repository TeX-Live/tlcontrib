The fitr Package
Author: D. P. Story
Dated: 2020-07-09

This package is an implementation of the FitR view-type destination as
described in the PDF Reference. The package defines one new command
\jdRect. The command (optionally) sets a jump to and/or sets a destination
of a FitR (Rectangle). (Can you see where \jdRect comes from?).

The package requires eforms (part of the AeB) and collectbox (by Martin
Scharrer). Drivers supported are dvips and dvipsone (using Adobe Distiller
as the PDF creator); pdftex (which includes luatex); and dvipdfm,
dvipdfmx, and xetex.

The package was developed in response to a user of the AeB Bundle who was
interested in developing documents for students with low vision; the idea
is to magnify regions of the document so the student can read more
comfortably. Optional special effects are included (JavaScript functions)
to help focus one the rectangle as it is magnified, and as the previous
view is restored.

What's New for version dated 2020/07/09: The package is extended 
to support the PDF creator lualatex. All common workflows are 
now automatically detected: pdflatex, lualatex, and xelatex. 
Also, there are several new options, one of which is gonative; 
when this option is selected, no field or document JavaScript is 
used. For the gonative option, the workflow dvips->ps2pdf is 
also supported because there is no document JavaScript required. 
Of course, the dvips->distiller is also supported.

Now, I must get back to my retirement.


D. P. Story
www.acrotex.net
dpstory@uakron.edu
dpstory@acrotex.net


