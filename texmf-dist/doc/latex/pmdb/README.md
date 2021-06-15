The pmdb Package
Author: D. P. Story
Dated: 2021-06-07

This "poor man's database" (pmdb) package promotes a workflow 
for building exams, homework, and other content. The package 
supports the creation of a PDF document, which is a database of 
problems (or content) to be included in a target document. The 
package inserts checkboxes into the margins of the DB document; 
the instructor can select which which of the problems are to be 
included in the target document by checking one ore more of the 
checkboxes. By clicking the special \displayChoices control, the 
selected content is copied into the AR/AA console window in the 
form of \input statements (\input{prob1.tex}, eg), which is then 
be copied and pasted into the target document. By pressing 
Ctrl+Click while hovering over a checkbox, the associated 
content is loaded into the default application, possibly a LaTeX 
editor. For the Ctrl+Click feature to work, the file 
aeb-reader.js needs to be installed. As the filename suggests, 
this file will work for Adobe Acrobat Reader (DC), as well as 
for the magnificent Adobe Acrobat itself. 

What's New (2021-06-07) New command \editSourceOn, which inserts 
a pushbutton in the margin; pressing the button loads the source 
file in the default editor. Expanding \useEditLnk replaces the
marginal pushbutton with a link annotation. A new input mode
\InputProbs, allows inputting problems for a eqexam document.

Enjoy!

Now, I must get back to my retirement.


D. P. Story
www.acrotex.net
dpstory@uakron.edu
dpstory@acrotex.net


