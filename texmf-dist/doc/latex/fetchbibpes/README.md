The fetchbibpes Bundle
Author: D. P. Story
Dated: 2018/07/12

This bundle provides two packages, bibpes and fetchbibpes. This pair of 
packages was motivated by a friend, who was preparing Bible studies lessons 
using a combination of the application e-Sword (http://www.e-sword.net/) and 
LaTeX. He wanted a `database' of Bible packages from which he could simply 
`fetch' passages into the LaTeX source file.

    1.  The bibpes package is the `database' part of the problem. Use the 
        e-Sword application to copy and paste desired passages into an 
        (empty) TXT file. Use makebibpes.tex to convert the TXT file to a DEF 
        file formatted in a way that is usable by fetchbibpes. 

    2.  The fetchbibpes package is the `fetch' portion. Using the fetch 
        commands of fetchbibpes to reference the passages to be typeset into 
        the \LaTeX source. Two commands, \fetchverse and \fetchverses are 
        defined with numerous support options for formatting the verses. 
        A simple example is \fetchverses{Gen 1:1-10}, the command fetches
        the passage Genesis 1:10 from the `database' of DEF files input
        into the source document.

Unpack the distribution by latexing fetchbibpes.ins.

What's new (2018/07/12) Defined a new command \fetchversestxt; the command 
has the same arguments as \fetchverses, but it does not expand to typeset 
content. Rather, it defines two commands \versetxt and \passagetxt. These two 
are the passage reference and the passage for that verse but with all \LaTeX 
styling and font changes.  

What's new (2018/03/21): Implemented open ended ranges, for example, 
\fetchverses{Joh 3:27-} fetches all passages from John, chapter 3, starting 
with verse 27 until the end of the chapter. 

What's new (2016/09/24): Changed behavior of the alt key. Added an alt* key.


Enjoy.

D. P. Story
www.acrotex.net
dpstory@uakron.edu
dpstory@acrotex.net


