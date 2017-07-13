%%
%% This is file `jj_game.cls',
%% generated with the docstrip utility.
%%
%% The original source files were:
%%
%% jj_game.dtx  (with options: `package')
%% 
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%% jj_game.cls class file,                              %%
%% Copyright (C) 2000-2016  D. P. Story                 %%
%%   dpstory@uakron.edu                                 %%
%%                                                      %%
%% This program can redistributed and/or modified under %%
%% the terms of the LaTeX Project Public License        %%
%% Distributed from CTAN archives in directory          %%
%% macros/latex/base/lppl.txt; either version 1 of the  %%
%% License, or (at your option) any later version.      %%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
\NeedsTeXFormat{LaTeX2e}[1997/12/01]
\ProvidesClass{jj_game}[2016/11/24 v3.1 jj_game Class (dps)]
\RequirePackage{xkeyval}
\RequirePackage{ifpdf}[2006/02/20]
\RequirePackage{ifxetex}[2006/08/21]
\newcounter{jjgdriver}
\DeclareOptionX{pdftex}{\setcounter{jjgdriver}{0}
    \def\jjgdriver{\@gobble}}
\DeclareOptionX{luatex}{\setcounter{jjgdriver}{0}
    \def\jjgdriver{\@gobble}}
\DeclareOptionX{dvips}{\setcounter{jjgdriver}{1}
    \def\jjgdriver{dvips}\PassOptionsToPackage{dvips}{web}}
\DeclareOptionX{dvipsone}{\setcounter{jjgdriver}{1}
    \def\jjgdriver{dvipsone}\PassOptionsToPackage{dvipsone}{web}}
\DeclareOptionX{dvipdfm}{\setcounter{jjgdriver}{2}
    \def\jjgdriver{dvipdfm}\PassOptionsToPackage{dvipdfm}{web}}
\DeclareOptionX{dvipdfmx}{\setcounter{jjgdriver}{2}
    \def\jjgdriver{dvipdfmx}\PassOptionsToPackage{dvipdfmx}{web}}
\DeclareOptionX{xetex}{\setcounter{jjgdriver}{2}
    \def\jjgdriver{\@gobble}}
\DeclareOptionX*{\PassOptionsToClass{\CurrentOption}{article}}
\newif\ifdebug \debugfalse
\DeclareOptionX{debug}{\debugtrue}
\newif\iffinal \finalfalse
\DeclareOptionX{final}{\finaltrue}
\newif\ifdouble \doublefalse
\def\scaleFactor{1}
\DeclareOptionX{double}{\doubletrue\def\scaleFactor{2}}
\newif\ifjjgtwoplayer\jjgtwoplayerfalse
\def\twoplayerGame{false}
\DeclareOptionX{twoplayer}{\jjgtwoplayertrue\def\twoplayerGame{true}}
\DeclareOptionX{pro}{\let\jjgInputProCode\jjg@InputProCode
    \ExecuteOptionsX{allowpeeking}
    \AtBeginDocument{\@ifundefined{xBld}{\PackageError{jj_game}
    {You have specified the `pro' option, but\MessageBreak
     the aeb_pro package is not loaded with its\MessageBreak
     `uselayers' option}{See jjg_pro_ca.tex as an example of
     the correct loading\MessageBreak
     of aeb_pro}}{}}}
\def\jjg@InputProCode{\InputIfFileExists{jjgpro.def}%
    {\ClassWarning{jj_game}{Inputting code for pro option}
      \ClassWarningNoLine{jj_game}{AeB Pro required with uselayers
      option}}{\ClassWarning{jjg_game}{Cannot find jjgpro.def.
      \MessageBreak Reinstall or refresh your file name database.}}}
\let\jjgInputProCode\relax
\AtEndOfClass{\jjgInputProCode}
\DeclareOptionX{forcredit}{%
    \let\jjImportForCreditCode\jj@ImportForCreditCode}
\DeclareOptionX{allowpeeking}{\jjnopeekingfalse}
\newif\ifjjnopeeking \jjnopeekingtrue
\let\jjImportForCreditCode\relax
\def\jj@ImportForCreditCode{\@ifpackageloaded{aeb_pro}
    {\InputIfFileExists{jjcredit.def}
    {\ClassWarning{jj_game}{Inputting forcredit code}
    \ClassWarningNoLine{jj_game}{Don't forget to use
    \string\contestantName\MessageBreak
    and \string\gameboardPrintButton}}
    {\ClassWarning{jj_game}{Cannot find jjcredit.def}}}
    {\ClassWarningNoLine{jj_game}
    {The forcredit option requires the aeb_pro package}}}
\let\jjg@madedesignselection0
\define@choicekey+{jj_game.cls}{design}{jeopardy,florida,iceland,%
    hornet,qatar,norway,germany,bahamas,spain,custom}{%
    \ifx\jjg@madedesignselection0\def\jj@design@choice{#1}%
        \AtEndOfClass{\jjInputDesignChoice}%
            \global\let\jjg@madedesignselection1
            \def\jjg@useddesign{design}\else
        \ClassWarning{jj_game}{Do not use both design
            and customdesign\MessageBreak
            at the same time.\MessageBreak
            Using the \jjg@useddesign\space selection}\fi}
{\ClassWarning{jj_game}{Bad choice for design, permissible values
   are jeopardy, florida, iceland, hornet, qatar, norway, %
   germany, bahamas, spain. Try again}}
\def\jjInputDesignChoice{%
    \InputIfFileExists{design_\jj@design@choice.def}%
    {\ClassWarning{jj_game}{Inputting design choice \jj@design@choice}}%
    {\ClassWarning{jj_game}{Cannot find the file
        design_\jj@design@choice.def}}%
}
\define@key{jj_game.cls}{customdesign}{%
    \ifx\jjg@madedesignselection0\def\jj@design@choice{#1}%
    \AtEndOfClass{\jjInputDesignChoice}%
    \global\let\jjg@madedesignselection1
    \def\jjg@useddesign{customdesign}\else
    \ClassWarning{jj_game}{Do not use both design and customdesign
    \MessageBreak at the same time.\MessageBreak
    Using the \jjg@useddesign\space selection}\fi}
\DeclareOptionX{nodesigngraphics}{\let\jjgdesigngraphics\endinput}
\let\jjgdesigngraphics\relax
\define@choicekey+{jj_game.cls}{lang}[\val\nr]{english,german}
{\xdef\jjLangOpt{\nr}}{\ClassWarning{jj_game}
    {Bad choice for lang, permissible values are english and german.
     Try again.}}
\def\jjLangOpt{0}
\ifpdf\ExecuteOptionsX{pdftex}\else
    \ifxetex\ExecuteOptionsX{xetex}\else
        \let\ExecuteOptions@SAVE\ExecuteOptions
        \let\ExecuteOptions\ExecuteOptionsX
        \InputIfFileExists{jj_game.cfg}{}
        {\@ifundefined{l@tex@@@@driver}{\ExecuteOptionsX{dvips}}
            {\ExecuteOptionsX{dvipsone}}}%
        \let\ExecuteOptions\ExecuteOptions@SAVE
    \fi\fi
\ProcessOptionsX
\LoadClass{article}[2014/09/29]
\edef\x{\noexpand
    \RequirePackage[\jjgdriver,usetemplates,dvipsnames]{web}}\x
\RequirePackage[execJS]{eforms}[2016/08/29]
\RequirePackage{verbatim}
\RequirePackage{comment}
\RequirePackage{calc}
\iffinal
    \def\JJinitSetup{%
        \hypersetup{%
            pdfmenubar=false,
            pdftoolbar=false,
            pdffitwindow=true,
            pdfcenterwindow=true,
            pdfwindowui=false%
        }
    }
\else
    \let\JJinitSetup\relax
\fi
\AtEndOfClass{\JJinitSetup}
\ifcase\jjLangOpt\relax
    \includecomment{JJLangEnglish}
    \excludecomment{JJLangGerman}
\or
    \includecomment{JJLangGerman}
    \excludecomment{JJLangEnglish}
    \AtEndOfClass{\germanLocalization}
\fi
\@ifundefined{jjImportForCreditCode}
    {\excludecomment{JJForCredit}}
    {\includecomment{JJForCredit}}
\def\germanLocalization{%
    \currencyHeading{\bfseries For~\EUR\theCurrencyAmt:}%
    \correctText{Richtig!}\incorrectText{Falsch!}%
    \def\jb@sepStyle{2}\def\jb@currStyle{0}%
    \def\APScore@Currency@def{"\string\\u20ac"}%
    \printButtonCaption{Drucken}%
    \printButtonLabel{Diese Seite drucken}%
    \contestantNameLabel{Student}%
    \timestampLabel{Zeitstempel}%
    \timeStampFormat{dd/mm/yy, H:MM:ss.}%
    \enterNamePlease{Bitte geben Sie Ihren Namen auf
        der ersten Seite ein, bevor Sie das Spiel beginnen!}
   \def\jb@Score{"Punkte "}
    \illegalAccessMsg{Sie haben gespickelt! Bitte w\string\344hlen
    Sie erst eine Frage vom Spielbrett, bevor Sie umbl\string\344ttern -
    diese Antwort wird nicht gewertet! \string\326ffnen Sie das
    PDF erneut und spielen Sie dieses Mal ohne zu spickeln.}
    \illegalAnswerTwiceMsg{\string\304ndern Ihrer Antwort
    ist nicht erlaubt!}

}
\definecolor{defaultfillCells}{rgb}{0.98,0.92,0.73}
\def\defaultfillCells{defaultfillCells}
\definecolor{defaultfillBanner}{rgb}{0.5,0,0.5}
\def\defaultfillBanner{defaultfillBanner}
\definecolor{defaulttextBanner}{rgb}{1,1,0} % yellow
\def\defaulttextBanner{defaulttextBanner}
\definecolor{defaulttextBoard}{rgb}{1,1,0} % yellow
\def\defaulttextBoard{defaulttextBoard}
\definecolor{defaultfillInstructions}{rgb}{0.98,0.92,0.73}
\def\defaultfillInstructions{defaultfillInstructions}
\definecolor{defaultfillGameBoard}{rgb}{1,0.8,0.6}
\def\defaultfillGameBoard{defaultfillGameBoard}
\definecolor{defaultfillQuestions}{rgb}{1,0.8,0.6}
\def\defaultfillQuestions{defaultfillQuestions}
\def\defaultDollarColorAmt{blue}
\def\defaultLinkColor{webgreen}
\definecolor{jj_banner_bg}{named}{defaultfillBanner}
\definecolor{jj_banner_txt}{named}{defaulttextBanner}
\def\jj@transparent{transparent}
\newcommand{\bannerTextFont}{\sffamily\huge}
\def\bannerTextColor{\ifx\textBanner\jj@transparent
    \else\color{\textBanner}\fi}
\def\titleBanner#1{\def\@titleBanner{#1}}
\titleBanner{You are the Winner!}
\def\insertJJTitleBanner{\newpage\jb@Banner{\@titleBanner}}
\newcommand{\includeFootBanner}{%
    \renewcommand{\@oddfoot}
    {%
        \ifnum\thepage=1\relax
        \colorbox{jj_banner_bg}{\makebox[\linewidth-2\fboxsep]{%
        \href{\jjgauthorURL}%
        {\textcolor{jj_banner_txt}{\webauthor\ \copyright\ \the\year}}%
        \hfill\textcolor{jj_banner_txt}{\webuniversity}}}\else\hfil\fi
    }%
}
\newcommand{\authorURL}[1]{\def\jjgauthorURL{#1}}
\authorURL{http://www.example.com}
\newdimen\jb@paperheight
\newdimen\jb@tmpdimb
\jb@paperheight=\paperheight
\newdimen\@Leftmargin   \@Leftmargin=0cm
\newdimen\@Rightmargin  \@Rightmargin=0cm
\newdimen\@Topmargin    \@Topmargin=0in
\newdimen\@Bottommargin \@Bottommargin=0in
\parindent0pt
\setlength\headheight{0pt}
\setlength\headsep   {0pt}
\setlength\footskip  {0pt}
\hoffset=0pt
\voffset=0pt
\pagestyle{empty}
\def\InitLayout{%
  \setlength{\textwidth}{\paperwidth}
  \addtolength{\textwidth}{-\@Leftmargin}
  \addtolength{\textwidth}{-\@Rightmargin}
  \setlength{\textheight}{\paperheight}
  \addtolength{\textheight}{-\@Topmargin}
  \addtolength{\textheight}{-\@Bottommargin}
  \addtolength{\textheight}{-\headheight}
  \addtolength{\textheight}{-\headsep}
  \addtolength{\textheight}{-\footskip}
  \setlength{\oddsidemargin}{\@Leftmargin}
  \addtolength{\oddsidemargin}{-1in}
  \setlength{\evensidemargin}{\@Leftmargin}
  \addtolength{\evensidemargin}{-1in}
  \setlength{\topmargin}{\@Topmargin}
  \addtolength{\topmargin}{-1in}
  \setlength{\textscreenwidth}{\paperwidth}
}
\def\screensize#1#2{%
    \paperheight=#1%
    \paperwidth=#2
    \web@set@paper
    \InitLayout
}
\def\marginsize#1#2#3#4{%
    \@Leftmargin=#1
    \@Rightmargin=#2
    \@Topmargin=#3
    \@Bottommargin=#4
    \InitLayout
}
\marginsize{.25in}{.25in}{.25in}{.25in}
\newcount\rownum \rownum=1 % These registers keep track of the row and
\newcount\colnum \colnum=0 % column of the quizboard
\newcount\corMsgnum \corMsgnum=0 % These registers keep track of the
\newcount\errMsgnum \errMsgnum=0 % error messages
\newcount\jb@n
\newcount\jb@m
\newtoks\jb@rowtoks
\newcounter{newletter}
\newdimen\cellHeight \cellHeight=1in
\newdimen\cellWidth  \cellWidth=.5in
\newdimen\tablewidth
\newdimen\tableheight
\newcount\numCategories \numCategories=5
\newcount\numQuestions  \numQuestions=5
\newdimen\rulewidth     \rulewidth=1bp
\newdimen\extraHeight   \extraHeight=.2in
\newdimen\extraWidth    \extraWidth=0pt
\def\calcPaperSize{%
    \tablewidth=\numCategories\cellWidth
    \advance\tablewidth by \rulewidth
    \advance\tablewidth by \numCategories\rulewidth
    \paperwidth=\tablewidth
    \advance\paperwidth by\@Leftmargin  % two times margin
    \advance\paperwidth by\@Rightmargin % two times margin
    \advance\paperwidth by2\extraWidth  % need more width
    \tableheight=\numQuestions\cellHeight
    \advance\tableheight by\cellHeight % banner
    \advance\tableheight by2\rulewidth % two rules for banner
    \advance\tableheight by\numQuestions\rulewidth
    \paperheight=\tableheight
    \advance\paperheight by\cellHeight % score
    \advance\paperheight by\@Topmargin
    \advance\paperheight by\@Bottommargin
    \advance\paperheight by\topskip
    \advance\paperheight by\extraHeight
    \screensize{\paperheight}{\paperwidth}
}
\def\verbatimwrite{\bgroup\catcode``=0 \@bsphack
  \let\do\@makeother\dospecials
  \catcode`\^^M\active \catcode`\^^I=12
  \def\verbatim@processline{%
    \immediate\write\verbatim@out
      {\the\verbatim@line}}%
  \verbatim@start}
\def\endverbatimwrite{\@esphack\egroup}
\def\jb@linkColor{\defaultLinkColor}
\def\jj@setChoice#1#2{% this choice, default
    \ifx#1\@empty#2\else#1\fi}
\def\jj@updateChoice#1#2{%  this choice, current choice
    \ifx#1\@empty\xdef#1{#2}\else\xdef#2{#1}\fi}
\newtoks\jb@tmptoks \jb@tmptoks={}
\def\jjgdummy{dummy}
\def\convertToSetKeys#1#2{%
    \def\thisxkvFamily{#1}\def\jjg@scratch{}\jb@tmptoks={}%
    \@convertColoniiEqual#2,dummy:dummy,\@nil
}
\def\@convertColoniiEqual#1:#2,#3\@nil{%
    \def\jj@rgii{#2}\ifx\jj@rgii\jjgdummy
    \edef\jj@next{\noexpand\setkeys{\thisxkvFamily}{\the\jb@tmptoks}}%
    \else\jb@tmptoks=\expandafter{\jjg@scratch,#1=#2}%
    \edef\jjg@scratch{\the\jb@tmptoks}%
    \def\jj@next{\@convertColoniiEqual#3\@nil}\fi\jj@next
}
\define@key{declcolors}{fillCells}[\defaultfillCells]%
    {\def\fillCells{#1}}
\define@key{declcolors}{fillBanner}[\defaultfillBanner]%
    {\def\fillBanner{#1}}
\define@key{declcolors}{textBanner}[\defaulttextBanner]%
    {\def\textBanner{#1}}
\define@key{declcolors}{textBoard}[\defaulttextBoard]%
    {\def\textBoard{#1}}
\define@key{declcolors}{fillInstructions}[\defaultfillInstructions]%
    {\def\fillInstructions{#1}}
\define@key{declcolors}{fillGameBoard}[\defaultfillGameBoard]%
    {\def\fillGameBoard{#1}}
\define@key{declcolors}{fillQuestions}[\defaultfillQuestions]%
    {\def\fillQuestions{#1}}
\define@key{declcolors}{dollarColor}[\defaultDollarColorAmt]%
    {\def\dollarColor{#1}}
\define@key{declcolors}{linkColor}[\defaultLinkColor]%
    {\def\@linkcolor{#1}}
\setkeys{declcolors}{fillCells,fillBanner,textBanner,textBoard,%
    fillInstructions,fillGameBoard,fillQuestions,dollarColor,linkColor}%
\def\DeclareColors#1{\convertToSetKeys{declcolors}{#1}}
\def\APDollar@Font@def{TiRo}\def\APDollar@Size@def{20}
\def\APDollar@TextColor@def{0 0 1}\def\APDollar@BorderColor@def{0 0 0}
\def\APDollar@FillColor@def{1 0.35 1}
\define@key{apdollar}{Font}[\APDollar@Font@def]{\def\APDollar@Font{#1}}
\define@key{apdollar}{Size}[\APDollar@Size@def]{\def\APDollar@Size{#1}}
\define@key{apdollar}{TextColor}[\APDollar@TextColor@def]%
    {\def\APDollar@TextColor{#1}}
\define@key{apdollar}{BorderColor}[\APDollar@BorderColor@def]%
    {\def\APDollar@BorderColor{#1}%
    \ifx\APDollar@BorderColor\jj@transparent\gdef\APD@llarBorder{}\else
    \xdef\APD@llarBorder{\APDollar@BorderColor}\fi
}
\define@key{apdollar}{FillColor}[\APDollar@FillColor@def]%
    {\def\APDollar@FillColor{#1}%
    \ifx\APDollar@FillColor\jj@transparent\gdef\APD@llarFill{}\else
    \xdef\APD@llarFill{\APDollar@FillColor}\fi
}
\def\APD@llar{/\APDollar@Font\space\APDollar@Size\space Tf
    \APDollar@TextColor\space rg}
\setkeys{apdollar}{Font,Size,TextColor,BorderColor,FillColor}
\def\APDollar#1{\convertToSetKeys{apdollar}{#1}}
\APDollar{}
\newcommand\EUR{%
{\sffamily C%
\makebox[0pt][l]{\kern-.70em\mbox{--}%
}%
\makebox[0pt][l]{\kern-.68em\raisebox{.25ex}{--}}}\,%
}
\def\APHidden@Font@def{TiRo}\def\APHidden@Size@def{20}
\def\APHidden@TextColor@def{0 0 1}\def\APHidden@BorderColor@def{0 0 0}
\def\APHidden@FillColor@def{0.98 0.92 0.73}
\def\Champion@def{You are a Champion!}
\define@key{aphidden}{Champion}[\Champion@def]{\def\jb@Champion{#1}}
\define@key{aphidden}{Font}[\APHidden@Font@def]{\def\APHidden@Font{#1}}
\define@key{aphidden}{Size}[\APHidden@Size@def]{\def\APHidden@Size{#1}}
\define@key{aphidden}{TextColor}[\APHidden@TextColor@def]%
    {\def\APHidden@TextColor{#1}}
\define@key{aphidden}{BorderColor}[\APHidden@BorderColor@def]%
    {\def\APHidden@BorderColor{#1}%
    \ifx\APHidden@BorderColor\jj@transparent\gdef\APHidd@nBorder{}\else
    \xdef\APHidd@nBorder{\APHidden@BorderColor}\fi
}
\define@key{aphidden}{FillColor}[\APHidden@FillColor@def]%
    {\def\APHidden@FillColor{#1}%
    \ifx\APHidden@FillColor\jj@transparent\gdef\APHidd@nFill{}\else
    \xdef\APHidd@nFill{\APHidden@FillColor}\fi
}
\def\APHidd@n{/\APHidden@Font\space\APHidden@Size\space Tf
    \APHidden@TextColor\space rg}
\setkeys{aphidden}{Champion,Font,Size,TextColor,BorderColor,FillColor}
\def\APHidden#1{\convertToSetKeys{aphidden}{#1}}
\APHidden{}
\def\APRight@Font@def{TiRo}\def\APRight@Size@def{20}
\def\APRight@TextColor@def{0 0 1}\def\APRRight@Msg@def{Right!}
\def\correctText#1{\gdef\@APRightMsg{#1}}\correctText{Right!}
\define@key{apright}{Font}[\APRight@Font@def]{\def\APRight@Font{#1}}
\define@key{apright}{Size}[\APRight@Size@def]{\def\APRight@Size{#1}}
\define@key{apright}{TextColor}[\APRight@TextColor@def]%
    {\def\@APRightTextColor{#1}}
\define@key{apright}{Message}[\APRRight@Msg@def]{\def\@APRightMsg{#1}}
\def\@APRight{/\APRight@Font\space\APRight@Size\space Tf
    \@APRightTextColor\space rg}%
\setkeys{apright}{Font,Size,TextColor,Message}
\def\APRight#1{\convertToSetKeys{apright}{#1}}
\APRight{}
\def\APWrong@Font@def{TiRo}\def\APWrong@Size@def{20}
\def\APWrong@TextColor@def{0 0 1}\def\APRWrong@Msg@def{Wrong!}
\def\incorrectText#1{\gdef\@APWrongMsg{#1}}\incorrectText{Wrong!}
\define@key{apwrong}{Font}[\APWrong@Font@def]{\def\APWrong@Font{#1}}
\define@key{apwrong}{Size}[\APWrong@Size@def]{\def\APWrong@Size{#1}}
\define@key{apwrong}{TextColor}[\APWrong@TextColor@def]%
    {\def\@APWrongTextColor{#1}}
\define@key{apwrong}{Message}[\APRWrong@Msg@def]{\def\@APWrongMsg{#1}}
\def\@APWrong{/\APWrong@Font\space\APWrong@Size\space Tf
    \@APWrongTextColor\space rg}
\setkeys{apwrong}{Font,Size,TextColor,Message}
\def\APWrong#1{\convertToSetKeys{apwrong}{#1}}
\APWrong{}
\define@key{gamedesign}{Cat}{\global\advance\numCategories by1
    \jb@Cat#1,}
\define@key{gamedesign}{CellWidth}[1in]{\global\cellWidth=#1\relax}
\define@key{gamedesign}{CellHeight}[.5in]{\global\cellHeight=#1\relax}
\define@key{gamedesign}{NumQuestions}[5]{\global\numQuestions=#1\relax}
\define@key{gamedesign}{Goal}[3500]{\def\jb@Goal{#1}}
\define@key{gamedesign}{GoalPercentage}[0]{\def\jb@GoalPercentage{#1}}
\define@key{gamedesign}{ExtraHeight}[.2in]{\global\extraHeight=#1\relax}
\define@key{gamedesign}{ExtraWidth}[0pt]{\global\extraWidth=#1\relax}
\define@key{gamedesign}{Champion}[\Champion@def]{\def\jb@Champion{#1}}
\setkeys{gamedesign}{CellWidth,CellHeight,NumQuestions,Goal,%
    GoalPercentage,ExtraHeight,ExtraWidth,Champion}
\def\GameDesign#1{\global\numCategories=0\relax
    \convertToSetKeys{gamedesign}{#1}%
    \ifdebug
        \typeout{^^JClass jj_game Info:}
        \typeout{Number of Categories: \the\numCategories}
        \typeout{Number of Questions per Category: \the\numQuestions^^J}
    \fi
        \jb@n = \numQuestions
        \advance\jb@n by 1
        \multiply\jb@n by\numQuestions
        \multiply\jb@n by50                 % total money/category
        \multiply\jb@n by\numCategories     % total money
        \ifdouble
            \multiply\jb@n by2              % total money, double jj
        \fi
        \ifdebug
            \typeout{Total Money: \the\jb@n}
        \fi
        % See if GoalPercentage differs from the default value
        \xdef\jb@tmp{\csname 0\endcsname}%
        \xdef\jb@temp{\csname\jb@GoalPercentage\endcsname}%
        \ifx\jb@tmp\jb@temp
        \else
            \ifdebug
                \typeout{Goal Percentage Specified: \jb@GoalPercentage}
            \fi
            \multiply\jb@n by\jb@GoalPercentage
            \divide\jb@n by100
            \def\jb@Goal{\the\jb@n}
        \fi
        \xdef\Goal{\jb@Goal}
        \ifdebug
            \typeout{Goal: \Goal^^J}
        \fi
    \calcPaperSize
    \jjCancelOutAeBProCatalog
    \jjImportForCreditCode
}
\def\jjCancelOutAeBProCatalog{\iffinal\let\aeb@SetCatalog\@empty\fi}
\def\jb@Cat{\@ifnextchar[{\jb@item}{\jb@item[] }}%
\gdef\jb@item[#1]#2,{%
    \expandafter\gdef\csname Category\the\numCategories\endcsname
        {\forpbx{#1#2}}%
    \expandafter\gdef\csname category\the\numCategories\endcsname{#2}%
}%
\def\jb@Score{""}
\def\jb@align@left{l}
\def\jb@align@center{c}
\def\jb@align@right{r}
\def\APScore@Font@def{TiRo}
\def\APScore@Size@def{20}
\def\APScore@TextColor@def{0 0 1}
\def\APScore@BorderColor@def{0 0 0}
\def\APScore@FillColor@def{0.98 0.92 0.73}
\def\APScore@CellHeight@def{\the\cellHeight}
\def\APScore@CellWidth@def{\the\cellWidth}
\def\APScore@AutoPlacement@def{true}
\def\APScore@Score@def{""}
\def\APScore@Currency@def{""}
\def\APScore@align@def{r}
\def\jb@align{r}
\def\APScore@tp@ScaledCellWidth@def{.5}
\let\APScore@tp@ScaledCellWidth\@empty
\def\APScore@tp@HighlightBorderColor@def{1 0 0}
\define@key{apscore}{Font}[\APScore@Font@def]{\def\APScore@Font{#1}}
\define@key{apscore}{Size}[\APScore@Size@def]{\def\APScore@Size{#1}}
\define@key{apscore}{TextColor}[\APScore@TextColor@def]%
    {\def\APScore@TextColor{#1}}
\define@key{apscore}{BorderColor}[\APScore@BorderColor@def]%
    {\def\APScore@BorderColor{#1}%
    \ifx\APScore@BorderColor\jj@transparent\gdef\@APScoreBorder{}\else
    \xdef\@APScoreBorder{\APScore@BorderColor}\fi
}
\define@key{apscore}{FillColor}[\APScore@FillColor@def]%
    {\def\APScore@FillColor{#1}%
    \ifx\APScore@FillColor\jj@transparent\gdef\@APScoreFill{}\else
    \xdef\@APScoreFill{\APScore@FillColor}\fi
}
\define@key{apscore}{CellHeight}[\APScore@CellHeight@def]%
    {\def\@APScorecellHeight{#1}}
\define@key{apscore}{CellWidth}[\APScore@CellWidth@def]%
    {\def\@APScorecellWidth{#1}}
\define@boolkey{apscore}{AutoPlacement}[true]{}
\define@key{apscore}{Score}[\APScore@Score@def]{\def\jb@Score{#1}}
\define@key{apscore}{Currency}[\APScore@Currency@def]%
    {\def\jb@Currency{#1}}
\define@key{apscore}{align}[\APScore@align@def]{\def\jb@align{#1}}
\define@key{apscore}{tpScaledCellWidth}%
    [\APScore@tp@ScaledCellWidth@def]%
    {\def\@APtpScaledCellWidth{#1}}
\define@key{apscore}{tpHighlightBorderColor}
    [\APScore@tp@HighlightBorderColor@def]%
    {\def\@APtpHighlightBorderColor{#1}}
\def\@APScore{/\APScore@Font\space\APScore@Size\space Tf
    \APScore@TextColor\space rg}%
\setkeys{apscore}{Font,Size,TextColor,BorderColor,FillColor,%
    CellHeight,CellWidth,AutoPlacement,Score,Currency,align,%
    tpScaledCellWidth,tpHighlightBorderColor}
\def\APScore#1{\convertToSetKeys{apscore}{#1}%
    \ifKV@apscore@AutoPlacement
        \gdef\@PlaceScoreBoard{\@@PlaceScoreBoard}
        \gdef\PlaceScoreBoard##1{%
        \message{^^J\expandafter\string\csname APScore\endcsname:
        `AutoPlacement: true', the use of \string\PlaceScoreBoard\space
        is not valid in this case!^^J^^J}}%
    \else
        \gdef\@PlaceScoreBoard{\@@@PlaceScoreBoard\@gobble}%
    \fi
    \gdef\jb@Quad{2}%
    \ifx\jb@align\jb@align@left\gdef\jb@Quad{0}%
    \else\ifx\jb@align\jb@align@center\gdef\jb@Quad{1}%
    \else\ifx\jb@align\jb@align@right\gdef\jb@Quad{2}%
    \fi\fi\fi
}
\APScore{}
\def\@@PlaceScoreBoard#1{#1}
\long\def\PlaceScoreBoard#1{\def\@@@PlaceScoreBoard{#1}}
\PlaceScoreBoard{{\centering\vbox{\ScoreBoard}}}
\define@key{twoplayerKeys}{player1}[Player 1]{\def\jjg@playeri{#1}}
\define@key{twoplayerKeys}{player2}[Player 2]{\def\jjg@playerii{#1}}
\define@key{twoplayerKeys}{playerFmt}[\sffamily\small]%
    {\def\jjg@playerFmt{#1}}
\define@boolkey{twoplayerKeys}{checkbox}[true]{}
\setkeys{twoplayerKeys}{player1,player2,playerFmt,checkbox}
\newcommand{\setTwoPlayerOptions}[1]{%
    \convertToSetKeys{twoplayerKeys}{#1}}
\def\Rect#1{\pdf@rect{\textcolor{\@linkcolor}{#1}}}
\def\({\noexpand\(}\def\){\noexpand\)}
\def\Bbox#1#2{%
    \vbox to#2{\hbox to#1{\hfill}\vfill}%
}
\def\hmark#1{\hypertarget{#1}{}}
\newcommand{\bannerTextControl}[1]{#1}
\def\jb@Banner#1{\vspace*{-\topskip}\hbox{\rlap{\ifx\fillBanner
    \jj@transparent\else\color{\fillBanner}\fi
    \setbox0=\hbox{\bannerTextFont\strut}\jb@tmpdimb=\ht0
    \advance\jb@tmpdimb by6pt \jb@tmpdima=\dp0 \advance\jb@tmpdima by4pt
    {\ifx\fillBanner\jj@transparent\else
    \vrule height\jb@tmpdimb depth\jb@tmpdima width\textwidth\fi}}%
    \vbox{\noindent\centering\bannerTextFont\bannerTextColor%
        \strut\bannerTextControl{#1}}}\bigskip}
\def\dollarAP{%
{\ifdouble\advance\rownum by\rownum\fi\xdef\@dollarAmt{\the\rownum00}}%
\rlap{\pushButton[%
    \H{P}\W{1}\S{S}\DA{\APD@llar}\BC{\APD@llarBorder}
    \BG{\APD@llarFill}\CA{\@dollarAmt}
    \A{/S/GoTo/D (Q\the\rownum\the\colnum) \jjAdditionalCellJSActions}
]{\the\rownum\the\colnum}{\cellWidth}{\cellHeight}}}
\def\rightAP{\rlap{\efKern{0pt}{1bp}\pushButton[%
    \F{\FHidden}\H{N}\S{S}\BG{}\DA{\@APRight}\CA{\@APRightMsg}
]{\the\rownum\the\colnum R}{\cellWidth}{\cellHeight}}}
\def\wrongAP{\rlap{\efKern{0pt}{2bp}\pushButton[%
    \F{\FHidden}\H{N}\S{S}\BG{}\DA{\@APWrong}\CA{\@APWrongMsg}
]{\the\rownum\the\colnum W}{\cellWidth}{\cellHeight}}}
\def\jb@nDec{0}
\def\jb@sepStyle{0}
\def\jb@negStyle{0}
\def\jb@currStyle{2}
\def\jb@Currency{""}
\def\jb@bCurrencyPrepend{true}
\newcommand{\prependCurrency}{\def\jb@bCurrencyPrepend{true}}
\newcommand{\appendCurrency}{\def\jb@bCurrencyPrepend{false}}
\def\AAVAction{event.target.textColor = event.value < 0 ?
  "RGB \@APWrongTextColor".split(" ") :
  "RGB \@APRightTextColor".split(" ");\r
  this.dirty=false;}
\def\AAFAction{AFNumber_Format\(\jb@nDec, \jb@sepStyle, \jb@negStyle,
    \jb@currStyle, \jb@Currency, \jb@bCurrencyPrepend\);
    event.value = \jb@Score\space + event.value;\r
    this.dirty=false;}
\let\jjgplayerihook\@empty
\let\jjgplayeriihook\@empty
\def\ScoreBoard{\ifjjgtwoplayer
\setlength{\@tempdima}{\@APScorecellWidth}%
\setlength{\@tempdima}{%
    (\@APScorecellWidth)*\ratio{\@APtpScaledCellWidth\space pt}{1pt}}%
\ifdim\@tempdima<\cellWidth
\@tempdima=\cellWidth\fi
    \edef\BboxWidth{\the\@tempdima}%
\setlength{\@tempdima}{\@APScorecellHeight/2-4bp}%
   \edef\BboxHeight{\the\@tempdima}%
\ifKV@twoplayerKeys@checkbox
\raisebox{\BboxHeight}{\checkBox[\Ff{\FfReadOnly}%
    \textColor{\@APtpHighlightBorderColor\space rg}\BC{}%
    \symbolchoice{circle}]{ckboxPlayer0}{10bp}{10bp}{On}}\fi\space
\parbox[t]{\BboxWidth\ifxetex+4bp\fi}{\jjgplayerihook
\textField[%
    \H{N}\Q{\jb@Quad}\W{3}\S{I}\BC{\@APScoreBorder}\BG{\@APScoreFill}%
    \DA{\@APScore}\autoCenter{n}\AA{\AAValidate{\AAVAction}%
    \AAFormat{\AAFAction}}]{Total0}{\BboxWidth\ifxetex-4bp\fi}%
    {\@APScorecellHeight\ifxetex-4bp\fi}\\[-3pt]
\makebox[\linewidth][c]{\jjg@playerFmt\jjg@playeri}}\efKern{3pt}{0bp}\space
\parbox[t]{\BboxWidth\ifxetex+4bp\fi}{%
\textField[%
    \H{N}\Q{\jb@Quad}\W{3}\S{I}\BC{\@APScoreBorder}\BG{\@APScoreFill}%
    \DA{\@APScore}\autoCenter{n}\AA{\AAValidate{\AAVAction}%
    \AAFormat{\AAFAction}}%
]{Total1}{\BboxWidth\ifxetex-4bp\fi}%
    {\@APScorecellHeight\ifxetex-4bp\fi}\jjgplayeriihook\\[-3pt]
\makebox[\linewidth][c]{\jjg@playerFmt\jjg@playerii}}
\ifKV@twoplayerKeys@checkbox
\raisebox{\BboxHeight}{\checkBox[\Ff{\FfReadOnly}%
    \textColor{\@APtpHighlightBorderColor\space rg}\BC{}%
    \symbolchoice{circle}]{ckboxPlayer1}{10bp}{10bp}{On}}\fi
\else\efKern{0pt}{-3bp}\textField[%
    \H{N}\Q{\jb@Quad}\W{3}\S{I}\BC{\@APScoreBorder}%
    \BG{\@APScoreFill}\DA{\@APScore}\autoCenter{n}%
    \AA{\AAValidate{\AAVAction}\AAFormat{\AAFAction}}%
]{Total}{\@APScorecellWidth\ifxetex-4bp\fi}%
    {\@APScorecellHeight\ifxetex-4bp\fi}%
\fi}
\def\cell{\global\advance\colnum1 \ifnum\colnum>\numCategories
    \global\colnum=1 \global\advance\rownum1 \fi
    \moveleft.5\cellWidth\hbox{\efKern{0pt}{-1bp}\dollarAP
        \efKern{0pt}{-1bp}\rightAP\efKern{0pt}{-1bp}\wrongAP}}%
\def\bgb{\ifxetex\rule{0pt}{\cellHeight+.6bp}\smash{\PoohBahBanner}\else
    \PoohBahBanner\fi\smash{\rlap{\ifx\fillBanner\jj@transparent\else
    \ifxetex\setlength{\tablewidth}{\tablewidth}%
    \setlength{\cellHeight}{\cellHeight\ifxetex+.4pt\fi}\fi
    \color{\fillBanner}{\vrule width\tablewidth height\cellHeight}\fi}}}
\def\bgc{\smash{\rlap{{\ifx\fillCells\jj@transparent\else
    \color{\fillCells}\vrule width\tablewidth height\cellHeight\fi}}}}
\def\PoohBahBanner{\rlap{\ifxetex\advance\tablewidth-6bp\else
    \advance\tablewidth-2bp\hskip1bp\fi
\textField[%
    \Q{1}\autoCenter{n}\F{\FHidden}\W{3}\S{I}\BC{\APHidd@nBorder}%
    \BG{\APHidd@nFill}\DA{\APHidd@n}%
    \DV{\jb@Champion}\V{\jb@Champion}%
]{GrandPoohbah}{\tablewidth}{\cellHeight}}}
\def\forpbx#1{\vbox to\cellHeight{%
  \hsize=\cellWidth\centering\vfill\strut\ifx\textBoard\jj@transparent
  \else\color{\textBoard}\fi#1\strut\vfill}}
\def\gameCategories{%
  \jb@n=0
  \jb@rowtoks={\bgb}%
  \loop\ifnum\jb@n<\numCategories\advance\jb@n by1
    \jb@tmptoks=\expandafter{%
        \expandafter&\csname Category\the\jb@n\endcsname&}%
    \edef\jb@temp{\the\jb@rowtoks\the\jb@tmptoks}%
    \jb@rowtoks=\expandafter{\jb@temp}%
  \repeat
  \edef\jb@temp{\the\jb@rowtoks}%
  \jb@rowtoks=\expandafter{\jb@temp\cr\\ }%
\the\jb@rowtoks}

\long\def\oloop #1\orepeat{%
  \def\oiterate{#1\relax  % Extra \relax
  \expandafter\oiterate\fi}%
  \oiterate
  \let\outeriterate\relax
}
\let\orepeat=\fi
\def\gamebody{%
  \jb@rowtoks={}%
  \jb@m=\numQuestions
  \edef\jb@temp{}%
  \oloop\advance\jb@m-1\ifnum\jb@m>0
    \getrow
  \orepeat
  \the\jb@rowtoks}
\def\getrow{%
  \jb@n=\numCategories
  \edef\jb@temp{\the\jb@rowtoks}%
  \jb@rowtoks=\expandafter{\jb@temp\bgc}%
  \loop\advance\jb@n-1\ifnum\jb@n>0
    \edef\jb@temp{\the\jb@rowtoks}%
    \jb@rowtoks=\expandafter{\jb@temp&\cell&}%
  \repeat
  \edef\jb@temp{\the\jb@rowtoks}%
  \jb@rowtoks=\expandafter{\jb@temp\cr\\ }%
}
\def\GameBoard{\vspace*{-\topskip}%
\begingroup
  % set up \halign environment
  \tabskip0pt\offinterlineskip\parskip0pt\parindent0pt
  \def\\{\noalign{\hrule height\rulewidth}}%
  \hbox to\textwidth\bgroup\hfill
    \vbox\bgroup\hsize=\tablewidth\hmark{GameBoard}%
      \halign{##\vrule width\rulewidth
        &&\hbox to\cellWidth{\hss\vbox to\cellHeight{\vss##\vss}\hss}%
        &\vrule width\rulewidth##\cr\noalign{\hrule height\rulewidth}%
        % Begin entering data into the different cells
        \gameCategories
        \gamebody
      }%
    \egroup\hfill
  \egroup
\endgroup\ifxetex\ifjjgtwoplayer\kern-1bp\else
    \kern2bp\fi\fi
\@PlaceScoreBoard{{\centering\vbox{\ScoreBoard}}}%
\par\JJ@afterGameBoardInsertion}
\let\JJ@afterGameBoardInsertion\@empty
\newcommand{\afterGameBoardInsertion}[1]{%
    \def\JJ@afterGameBoardInsertion{#1}}
\def\instructions
{%
    \instructionPageTemplate
    \ifx\fillInstructions\jj@transparent\else
        \textBgColor{\fillInstructions}\fi
    \jb@Banner{\@titleBanner}\par\parskip6pt
}
\def\endinstructions{\newpage}
\let\Question\relax
\def\Questions{\newpage\parskip0pt\relax\global\jb@n=0\relax
  \gameboardPageTemplate
  \ifx\fillGameBoard\jj@transparent\else
  \textBgColor{\fillGameBoard}\fi\GameBoard
  \newpage\ifx\fillQuestions\jj@transparent\else
  \textBgColor{\fillQuestions}\fi
  \rownum=0 \colnum=1 % initialize counters
  \questionPagesTemplate
}
\def\endQuestions{%
    \ifnum\jb@n=\numCategories\else
        \typeout{Number of categories is not correct!}
    \fi
    \newpage}
\def\Category#1{
    \def\currentCategory{#1}\global\advance\jb@n by1 \global\jb@m=0
    \ifdebug
    \typeout{^^JBegin Processing Category \the\jb@n: \currentCategory}
    \typeout{If '\currentCategory' is different from}
    \typeout{'\csname category\the\jb@n\endcsname',
            then your categories are out of order!}
    \fi}
\def\endCategory{
    \ifdebug
        \ifnum\jb@m=\numQuestions
        \else
            \typeout{In the '\currentCategory' category,
            the number of questions is not correct!}
        \fi
        \typeout{End Processing Category \the\jb@n:
        \the\jb@m\space of \the\numQuestions\space Questions}
    \fi
}
\let\jjAdditionalJSActions\@empty
\def\jjAdditionalCellJSActions{\Next{\JS{%
    var pushCell\the\rownum\the\colnum = true;\r
    makeNameReadOnly();}}}
\let\bJJGInsertLayer\@empty
\let\eJJGInsertLayer\@empty
\def\theCurrencyAmt{{\count0=\the\rownum00\relax
    \multiply\count0by\scaleFactor\relax\the\count0\relax}}
\def\aboveCurrencySkip#1{\def\jj@aboveCurrencySkip{#1}}
\aboveCurrencySkip{0pt}
\newcommand{\currencyHeading}[1]{\def\jj@currencyHeading{#1}}
\currencyHeading{\bfseries For~\$\theCurrencyAmt:}
\def\Question{\newpage\global\advance\jb@m by1
  \global\advance\rownum1 \ifnum\rownum>\numQuestions \global\rownum=1
  \global\advance\colnum1 \fi
  \global\advance\corMsgnum1 \ifnum\corMsgnum=8 \global\corMsgnum=1 \fi
  \edef\peekingOpenAction{\ifjjnopeeking/H true/S/Hide%
    /T (\the\rownum\the\colnum)\else\JS{;}\fi}%
  \ifcase\thejjgdriver
    \immediate\pdfobj{<< /O <<
        \peekingOpenAction\space\jjAdditionalJSActions >> >> }
    \edef\ThisPage{/AA \the\pdflastobj\space 0 R}
    \global\pdfpageattr=\expandafter{\ThisPage}%
  \or
    \literalps@out{%
         [ {ThisPage} << /AA << /O <<
            \peekingOpenAction\space\jjAdditionalJSActions >> >> >>
        /PUT pdfmark}%
  \or
    \@pdfm@mark{put @thispage << /AA << /O <<
        \peekingOpenAction\space\jjAdditionalJSActions >> >> >>}%
  \fi
  \jb@Banner{\csname category\the\colnum\endcsname}%
  \begingroup
  \vskip\jj@aboveCurrencySkip
  \parskip2pt
  \setcounter{newletter}{0}%
  \hmark{Q\the\rownum\the\colnum}\bJJGInsertLayer%
  \@ifnextchar[%
    {\let\Ans\Question@tabular\def\endQuestion{\end{tabular}%
     \eJJGInsertLayer\endgroup\newpage}\getBracArg}%
    {\let\Ans\Question@list\def\jb@numCols{1}\gdef\aeb@numCols{1}%
     \noindent\textcolor{\dollarColor}{\jj@currencyHeading}\space
     \ignorespaces%
     \def\endQuestion{\end{list}\eJJGInsertLayer\endgroup\newpage}}%
}
\def\getBracArg[#1]{\def\jb@numCols{#1}\gdef\aeb@numCols{#1}\noindent%
    \textcolor{\dollarColor}{\jj@currencyHeading}\space\ignorespaces}
\newlength{\jjgtmplength}
\newlength\jb@width \settowidth{\jb@width}{\normalfont(d)}
\newdimen\jb@tmpdima
\newlength\jb@tmplength
\providecommand\PBS[1]{\let\jb@temp=\\#1\let\\=\jb@temp}
\def\makelink#1{%
\setLink[\A{\JS{qbrespond(#1,\the\rownum,\the\colnum);}
        \Next{/S/GoTo/D (GameBoard)}}
    ]{\makebox[\jb@width]{\normalfont(\hfil\alph{newletter}\hfil)\hfill}}}
\def\eq@hspanner{\ }
\def\eq@hspanner@default{\ }
\def\eq@lw@l{\eq@l@l\eq@hspanner}
\def\setMClabelsep#1{\def\eq@hspanner{#1}}
\def\resetMClabelsep{\let\eq@hspanner\eq@hspanner@default}
\newskip\aboveanswersSkip
\setlength\aboveanswersSkip{3pt}
\def\Question@list{\vskip\aboveanswersSkip
\begin{list}{}%
{\usecounter{newletter}
   \let\Ans=\JJ@Ans@list
   \settowidth{\jb@tmplength}{\normalfont(d)}%
   \settowidth{\labelwidth}{\normalfont(d)\eq@hspanner}%
   \setlength{\parsep}{0pt}\setlength{\itemindent}{0pt}%
   \setlength{\listparindent}{\parindent}%
   \setlength{\leftmargin}{\labelwidth}%
   \setlength{\labelsep}{0pt}%
}\Ans}%
\def\JJ@Ans@list#1{\stepcounter{newletter}\item[\makelink{#1}\hfill]}%

\def\Question@tabular{%
   \vskip\aboveanswersSkip
   \setcounter{newletter}{0}%
   \let\Ans=\JJ@Ans@tabular
   \jb@tmpdima=\linewidth \divide\jb@tmpdima by\jb@numCols
   \vskip\partopsep
   \noindent\normalbaselines\tabcolsep=0pt
   \begin{tabular}{*{\jb@numCols}{p{\jb@tmpdima}}}%
\Ans}
\def\JJ@Ans@tabular#1{\leavevmode\stepcounter{newletter}%
  \PBS\raggedright
  \settowidth{\jb@tmplength}{\normalfont(d)\eq@hspanner}%
  \hangindent=\jb@tmplength\hangafter=1 %
  \makelink{#1}\eq@hspanner\ignorespaces
}
\newenvironment{oAnswer}
{%
    \gdef\oField{oQ\the\rownum\the\colnum}%
    \gdef\curr@quiz{oQ\the\rownum\the\colnum}%
    \def\currQuiz{oQ\the\rownum\the\colnum}%
    \let\@qzsolndest\@empty
    \stepcounter{questionno}%
    \let\eqQuizType=s\gdef\eqPTs{1}\global\let\eqQT=\eq@na
    \let\solution=\solution@sq
    \let\endsolution=\endsolution@sq
    \global\let\Ans=\Question@list
    \gdef\endQuestion{\endgroup\newpage}\sq@IDTxtField
}{}
\def\JJ@@RespBoxMathActions
{%
    \AA{\AAKeystroke{if(event.willCommit) {\r\t
            var retn = \processJSfunc(\rbArgs,\compareJSfunc);\r
            if ( retn != null ) {\r\t
                qbrespond(retn,\the\rownum,\the\colnum)\r\t
                this.gotoNamedDest("GameBoard");\r
            }
        }}%
        \AAFormat{\eqFormatArg}%
        \AAOnFocus{\JS{var retn = null;}}%
    }
}
\def\JJ@@RespBoxTxtActions
{%
    \AA{\AAKeystroke{if(event.willCommit) {\r\t
        retn = ProcRespTxt(\rbTxtAlt);\r
        if ( retn != null ) {\r\t
            qbrespond(retn,\the\rownum,\the\colnum)\r\t
            this.gotoNamedDest("GameBoard");\r
        }
    }}%
    \AAOnFocus{\JS{var retn = null;}}
    }%
}
\def\redefineRespBoxActions{%
    \let\@@RespBoxMathActions\JJ@@RespBoxMathActions
        \let\@@RespBoxTxtActions\JJ@@RespBoxTxtActions}
\newcommand{\printButtonCaption}[1]{\def\jj@printButtonCaption{#1}}
\printButtonCaption{Print}
\newcommand{\printButtonLabel}[1]{\def\jj@printButtonLabel{#1}}
\printButtonLabel{Print this page:}
\newcommand{\contestantNameLabel}[1]{\def\jjg@contestantNameLabel{#1}}
\contestantNameLabel{Student:}
\newcommand{\timestampLabel}[1]{\def\jjg@timestampLabel{#1}}
\timestampLabel{Time stamp:}
\newcommand{\printButtonEOL}[1]{\def\jjg@printButtonEOL{#1}}
\printButtonEOL{\\[1ex]}
\newcommand{\gameboardPrintButton}[1][]
{%
    \jj@printButtonLabel\space\pushButton[\CA{\jj@printButtonCaption}
    \A{\JS{%
        var pp = this.getPrintParams();\r
        pp.firstPage=this.pageNum;\r
        pp.lastPage=this.pageNum;\r
        pp.pageHandling = pp.constants.handling.shrink;\r
        var fv = pp.constants.flagValues;\r
        pp.flags |= (fv.suppressCenter | fv.suppressRotate);\r
        this.print(pp);}}#1]{printit}{}{11bp}\quad
        \jjg@contestantNameLabel\space\textField[\BC{}\Ff\FfReadOnly
            ]{jjContestantNameprint}{1.5in}{11bp}\jjg@printButtonEOL
        \jjg@timestampLabel\space\textField[\BC{}\Ff\FfReadOnly
            ]{timestamp}{1.5in}{11bp}%
}%
\newcommand{\contestantName}[3][\BC{}\AA{\AAKeystroke{%
    if ( event.willCommit ) {\r\t
        var f = this.getField("jjContestantNameprint");\r\t
        if ( f != null ) f.value = event.value;\r
    }
    }}]{\textField[#1]{jjContestantName}{#2}{#3}}
\newcommand{\timeStampFormat}[1]{\def\jjTimeStampFormat{#1}}
\timeStampFormat{mm-dd-yy, H:MM:ss.}
\AtBeginDocument{\redefineRespBoxActions}
\newcommand{\enterNamePlease}[1]{\def\jjEnterNamePlease{#1}}
\enterNamePlease{Please, enter your name on the first
    page before you begin the game!}
\let\jjInstructionPageBG\relax
\let\jjGameBoardPageBG\relax
\let\jjQuestionPagesBG\relax
\newcommand{\defineInstructionPageGraphic}[1]{%
    \def\jjInstructionPageBG{#1}}
\newcommand{\defineGameboardPageGraphic}[1]{\def\jjGameBoardPageBG{#1}}
\newcommand{\defineQuestionPagesGraphic}[1]{\def\jjQuestionPagesBG{#1}}
\newcommand{\instructionPageTemplate}{%
    \@ifundefined{jjInstructionPageBG}{}{%
    \edef\expand@templatename{\noexpand\template{\jjInstructionPageBG}}%
        \expand@templatename}}
\newcommand{\gameboardPageTemplate}{%
    \@ifundefined{jjGameBoardPageBG}{}{%
    \edef\expand@templatename{\noexpand\template{\jjGameBoardPageBG}}%
        \expand@templatename}}
\newcommand{\questionPagesTemplate}{%
    \@ifundefined{jjQuestionPagesBG}{}{%
    \edef\expand@templatename{\noexpand\template{\jjQuestionPagesBG}}%
        \expand@templatename}}
\newcommand{\illegalAccessMsg}[1]{\def\jjIllegalAccessMsg{#1}}
\illegalAccessMsg{You are peeking at this question.
    Choose a question from the game board first!
    You may want to start the game over now!}
\newcommand{\illegalAnswerTwiceMsg}[1]{\def\jjIllegalAnswerTwiceMsg{#1}}
\illegalAnswerTwiceMsg{Changing your answer is not allowed!}
\newcommand{\playeriWinnerMsg}[1]{\def\jjplayeriWinner{#1}}
\playeriWinnerMsg{\jjg@playeri\space is the winner!}
\newcommand{\playeriiWinnerMsg}[1]{\def\jjplayeriiWinner{#1}}
\playeriiWinnerMsg{\jjg@playerii\space is the winner!}
\newcommand{\gameTiedMsg}[1]{\def\jjgameTied{#1}}
\gameTiedMsg{It is all tied up! You are equally matched!}
\newcommand{\bothLosersMsg}[1]{\def\jjbothlosers{#1}}
\bothLosersMsg{You two either are not trying, or
    you do not know anything about the subject!}
\begin{insDLJS*}[qbrespond]{jjgdljs}
\begin{newsegment}{JJG: JavaScript to Control Jeopardy}
var _processLink=true;
var twoplayerGame=\twoplayerGame;
var whichPlayer=0;
if ( twoplayerGame ) {
    var reTrimSpaces=/^\s+|\s+$/g;
    var reInsertCommas=/\b\s+\b|\b\s+(?=\.)/g;
    var tpHighlightBorderColor="\@APtpHighlightBorderColor";
    var hightLightColor=%
    tpHighlightBorderColor.replace(reTrimSpaces,"").%
replace(reInsertCommas,",");
    var aHighlightColor = ("RGB,"+hightLightColor).split(",");
    this.getField("Total"+whichPlayer).strokeColor=%
aHighlightColor;
    var ckBx = this.getField("ckboxPlayer"+whichPlayer);
    if ( ckBx != null ) %
this.getField("ckboxPlayer"+whichPlayer).checkThisBox(0,true);
    this.dirty=false;
}
function qbrespond(ans,row,col)
{
   if (_processLink) {
       \ifjjnopeeking\else this.getField(""+row+col).hidden=true;\fi
        if ( eval("typeof pushCell"+row+col) == "undefined") {
            app.alert("\jjIllegalAccessMsg");
            return null;
        }
        if ( twoplayerGame )
            var Total = this.getField("Total"+whichPlayer);
        else
            var Total = this.getField("Total");
        var p = numQuestions*(col-1)+row-1;
        if (QAed.charAt(p)==0)
        {
            QAed = QAed.substring(0,p)+1+%
   QAed.substring(p+1,totalQuestions);
            if (ans==1)
            {
                this.getField(""+row+col+'R').hidden=false;
                Total.value += \scaleFactor*row*100;
                app.alert(aCM[nCM],3);
                if (++nCM > 6) nCM=0;
            }
            else
            {
                this.getField(""+row+col+'W').hidden=false;
                var f = this.getField(""+row+col)
                Total.value -= \scaleFactor*row*100;
                app.alert(aEM[nEM],3);
                if (++nEM > 6) nEM=0;
            }
            if ( typeof toggleSetThisLayer == "function" ) {
                toggleSetThisLayer("q"+row+col,false);
                eval("pushCell"+row+col+"=undefined");
            }
            if ( twoplayerGame ) {
                this.getField("Total"+whichPlayer).strokeColor=["G",0]
                var ckBx = this.getField("ckboxPlayer"+whichPlayer);
                if (ckBx != null ) %
this.getField("ckboxPlayer"+whichPlayer).checkThisBox(0,false);
                whichPlayer = ( whichPlayer+1) \% 2;
                this.getField("Total"+whichPlayer).strokeColor=%
aHighlightColor;
                ckBx = this.getField("ckboxPlayer"+whichPlayer);
                if (ckBx != null ) %
this.getField("ckboxPlayer"+whichPlayer).checkThisBox(0,true);

            }
            if ( ++NumQAed>=totalQuestions ) {
                if  (Total.value>=Goal)
                    this.getField("GrandPoohbah").hidden=false;
                if ( twoplayerGame ) {
                    var player0 = this.getField("Total0");
                    var player1 = this.getField("Total1");
                    var player0Score = Number(player0.value);
                    var player1Score = Number(player1.value);
                    if ( player0Score > player1Score ) {
                        if ( player0Score <= 0 ) {
                            player0.strokeColor = ["RGB", 1, 0, 0];
                            player1.strokeColor = ["RGB", 1, 0, 0];
                            var timer=app.setTimeOut(
                                'app.alert("\jjbothlosers")',250);
                        } else {
                            player0.strokeColor = ["RGB", 0, .6, 0];
                            player1.strokeColor = ["G",0];
                            var timer=app.setTimeOut(
                                'app.alert("\jjplayeriWinner")',250);
                        }
                    }
                    else if ( player1Score > player0Score ) {
                        if ( player1Score <= 0 ) {
                            player0.strokeColor = ["RGB", 1, 0, 0];
                            player1.strokeColor = ["RGB", 1, 0, 0];
                            var timer=app.setTimeOut(
                                'app.alert("\jjbothlosers")',250);
                        } else {
                            player1.strokeColor = ["RGB", 0, .6, 0];
                            player0.strokeColor = ["G",0];
                            var timer=app.setTimeOut(
                                'app.alert("\jjplayeriiWinner")',250);
                        }
                    }
                    else {
                        if ( player1Score <= 0 ) {
                            player0.strokeColor = ["RGB", 1, 0, 0];
                            player1.strokeColor = ["RGB", 1, 0, 0];
                            var timer=app.setTimeOut(
                                'app.alert("\jjbothlosers")',250);
                        } else {
                            player0.strokeColor = ["RGB", 0, 0, 1];
                            player1.strokeColor = ["RGB", 0, 0, 1];
                            var timer=app.setTimeOut(
                            'app.alert("\jjgameTied")',250);
                        }
                    }
                }
            }
        }
        else
            app.alert("\jjIllegalAnswerTwiceMsg");
   }
   this.dirty=false;
}
var tsfield = this.getField("timestamp");
if ( tsfield != null )
    tsfield.value = util.printd("\jjTimeStampFormat", new Date());
this.dirty=false;
function makeNameReadOnly()
{
    var f = this.getField("jjContestantName");
    if ( (f != null) && !f.readonly ) f.readonly=true;
    this.dirty=false;
}
\end{newsegment}
\begin{newsegment}{JJG: Global Variables}
var numQuestions = \the\numQuestions;
var numCategories = \the\numCategories;
var Goal = \Goal;
var totalQuestions = numQuestions * numCategories;
var QAed = "";
for (var i=0; i< totalQuestions; i++) QAed +="0";
var nEM = 0;
var nCM = 0;
var NumQAed = 0;
this.dirty=false;
\end{newsegment}
\begin{JJLangEnglish}
\begin{newsegment}{JJG: Arrays for Jeopardy for English}
var aCM = new Array
(
    "That's Right!  Way to go, Kiddo!",
    "Wonderfully Done! Congrats!",
    "Very Good! Keep up the good work!",
    "Good, good, good, good!",
    "Gee, you're smart! Try a harder one!",
    "You're pretty sharp! Continuez, s'il vous pla\356t!",
    "You're the tops! But wait...try another."
)
var aEM = new Array
(
    "That's wrong! Sorry. Try Again.",
    "Error again.  Don't guess, I'm watching!",
    "Please!  Please!  Work it out first!",
    "An error has insinuated itself into your calculations.\n"
   +"Recalibrate your thinking!",
    "Bah! Humbug!  You're haunted by Errors!",
    "Try again.  This time with order and method.",
    "Wrong again!  Use your little gray cells!"
)
this.dirty=false;
\end{newsegment}
\end{JJLangEnglish}
\begin{JJLangGerman}
\begin{newsegment}{JJG: Arrays for Jeopardy for German}
var aCM = new Array
(
    "Richtig!  So ist's recht!",
    "Gut gemacht! Gratuliere!",
    "Sehr gut! Weiter so!",
    "Gut, gut, gut, gut!",
    "Du bist sehr clever! Jetzt versuch mal eine Schwierigere!",
    "Jawoll! Continuez, s'il vous pla\356t!",
    "Spitze! Auf geht's, zu einer weiteren Aufgabe!"
)
var aEM = new Array
(
    "Leider falsch!",
    "Schon wieder falsch!  Nicht raten!",
    "Bitte denk erst nach bevor Du auf die Antwort klickst!",
    "Ein Fehler hat sich eingeschlichen. Erst denken, dann ...!",
    "Ohje! So ein Quatsch!",
    "Versuch's nochmal.  Es ist noch nicht aller Tage Abend.",
    "Schon wieder falsch!  Nutz Deine kleine grauen Zellen!"
)
this.dirty=false;
\end{newsegment}
\end{JJLangGerman}
\begin{JJForCredit}
\begin{newsegment}{JJG: JS to support forcredit}
var noNameError, fname;
function forCreditClosePage()
{
    fname = this.getField("jjContestantName");
    if ( fname != null ) {
        var JJjsCode = "if ( fname.value.replace(/\s*/g, '') == '' ) {"
            + " if ( typeof closingJJ == 'undefined' ) {"
            + " app.alert({cMsg: '\jjEnterNamePlease', %
cTitle: 'AcroTeX eDucation Bundle'});"
            + " this.pageNum = 0;"
            + " } } else { fname.readonly = true; }";
        noNameError = app.setTimeOut(JJjsCode, 15);
    }
    this.dirty=false;
}
\end{newsegment}
\end{JJForCredit}
\end{insDLJS*}
\AtEndOfClass{\thisPageAction{\JS{this.dirty=false;}}{}}
\endinput
%%
%% End of file `jj_game.cls'.
