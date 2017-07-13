The jj_game Class
Author: D. P. Story
Dated: 2016/11/24

JJ_game class is a Jeopardy-like game in which you compete for cyber money by 
answering questions composed by the game author. The questions can be 
multiple choice, math fill-in or text fill-in. 

Since the year 2000, many techniques have been developed, and this
version of jj_game has many enhancements an new features:
    (1) Added the ability to pose math and text fill-in questions

    (2) Enhanced control over the color design of the game

    (3) The distribution comes with 9 designs (color schemes)
        jeopardy, florida, iceland, hornet, qatar, norway, germany,
        bahamas and spain

    (4) Five general graphical backgrounds provided, and two additional
        ones that are used in a custom design

    (5) language option, currently english and german. Additional
        languages will be added as translators volunteer

The basic game can be constructed using dvips, pdftex, luatex, and xelatex.

Additionally, there is a pro option that requires the use of dvips/Distiller
workflow (Acrobat Pro 7.0 or later required). 

I have used the jj_game class in some of my classes for extra
credit; for this purpose, the following features were developed:

    (6) A forcredit option that forces the student---assuming the
        contestant is taking the game for credit---to enter his/her name.

    (7) With the pro option, layers are used to hide the questions
        from the contestant before he/she selects a question from
        the game board. When the contestant selects a question, the
        question is made visible.  The questions are in layers with
        a no print attribute, so the contestant cannot print out the
        game and distribute the questions to other contestants even if
        the questions are visible.

Documentation jjg_man.pdf contains all details of the game, and
wonderful demo files are also supplied.

What's New (2016/11/24): Brought jj_game class up to conformance to the 
modern exerquiz package, which has changed over the years. 

Comments and suggestions are always gratefully accepted and seriously
considered.

Hope you like the  new version, now, I simply must get back to my
retirement!

one dps
dpstory at uakron dot edu
dpstory at acrotex dot net
