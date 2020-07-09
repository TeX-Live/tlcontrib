The richtext Package
Author: D. P. Story 
Dated: 2020-07-02

The richtext package is used to create rich text strings that can, in turn,
be inserted into the RV (and V) keys of text field. Currently, only the eforms
package supports the RV key.

What's New 2020-07-02: dvips tends to break lines according to 
some algorithm known only to the developer. This version of richtext
has focused fixes to the dvips->distiller. It tries to prevent dvips
from breaking lines in the postscript file that will cause the rich
text field to fail. (Similar problems with JavaScript, by the way.)
After much testing, the fix seems to work.

Now, I simply must get back to my retirement.

D. P. Story
www.acrotex.net
dpstory@acrotex.net
dpstory@uakron.edu
