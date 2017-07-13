The fitr Package
Author: D. P. Story
Version: v1.2d
Dated: 2016/04/04

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

Unpack the distribution by latexing fitr.ins.

Enjoy!

Now, I must get back to my retirement.


D. P. Story
www.acrotex.net
dpstory@uakron.edu
dpstory@acrotex.net


