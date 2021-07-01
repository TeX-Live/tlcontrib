# Version 1.4 changes
# =====================================================================================================
# small modifications of tex template
# remove some comments
# added partial support for cntdwn package, not fully working here to keep tex template small
# interested users should refer to Acrotex blog for further explanations
# anyway the template compiles without errors
# =====================================================================================================
# Version 1.3 changes
# =====================================================================================================
# copyka.ps1 allows a comment character (#)
# delka.ps1 allows a comment character (#)
# =====================================================================================================
# Version 1.2 changes
# =====================================================================================================
# removed moveka.ps1
# removed old version comments
# added template for altclasspath
# removed .ps1 from BAT helper file, that way one can easiear hit TAB for autocompletion in comamnd shell
# OPEN line 92: copyka.ps1 --> lines starting with # are *not* ignored at least here
# Workaround does not help: (get-content ./altclasspaths.txt) -notmatch '^#' WHY?

# =====================================================================================================
#
# This script makes a custom class folder for thorshammer with
# needed folders, adjusted path settings and templates for tex, cfg etc.
#
# Set path for thorshammer (local texmf dir} for Windows/Linux
# This has to be done only once
$pathThorshammerWin="C:\Users\$env:UserName\texmf\tex\latex\thorshammer" # Windows
$pathThorshammerWin="C:\Users\Public\Documents\My TeX Files\tex\latex\aeb\thorshammer"
$pathThorshammerLin="/home/manjothor/texmf/tex/latex/thorshammer" # Linux

# Store the system locale in a var. We need them later in the script for localization
$locale = Get-UICulture
$myLocale = "de"

# Remove everything to the left of a comment character, then trim
function Strip-and-Trim {
  param([string]$str)
  $pos=$str.IndexOf("#")
  If($pos -ne -1) {
    $str=$str.substring(0,$pos)
  }
  $str=$str.trim()
  return $str
}
Write-Host "There are" $args.Length "arguments";
$argList=@()
$listOfEight=$args[0]
if(Test-Path -Path ./$listOfEight) {
  get-content $args[0] | %{
    Write-Host "Line: $_"
    $argList+=$_
  }
  $class = Strip-and-Trim($argList[0])
  $number = Strip-and-Trim($argList[1])
  $subject = Strip-and-Trim($argList[2])
  $theme = Strip-and-Trim($argList[3])
  $instrName = Strip-and-Trim($argList[4])
  $date = Strip-and-Trim($argList[5])
  $time = Strip-and-Trim($argList[6])
  $classPath= Strip-and-Trim($argList[7])

# Remove whitespace in $class and $instrName because we don't need whitespace
# in this two vars.
  $class = $class.replace(' ','')
  $instrName = $instrName.replace(' ','')

############
# folders  #
############
  New-Item $class -ItemType directory
  cd $class
  New-Item $instrName -ItemType directory
  Set-Location "$instrName"
  New-Item backup -ItemType directory
  If ($locale -like "*$myLocale*") {
      New-Item vonSUS -ItemType directory
      New-Item anSUS -ItemType directory
  } else {
      New-Item fromStudents -ItemType directory
      New-Item toStudents -ItemType directory
  }
} else {
  Write-Host "The file" $listOfEight "cannot be found in the current folder," `
"check the spelling, include the extension."
  exit
}

##############
# copyscript #
##############
# Version 1.3
New-Item copyka.ps1 -ItemType file
Set-Content ./copyka.ps1 '# Get-ExecutionPolicy
# Set-ExecutionPolicy RemoteSigned
# This script copies PDFs from the $startLocation to $instrDest, but only
# if in a $instrName folder
#
# Strip-and-Trim function
# Remove everything to the left of a comment character, then trim
function Strip-and-Trim {
  param([string]$str)
  $pos=$str.IndexOf("#")
  If($pos -ne -1) {
    $str=$str.substring(0,$pos)
  }
  $str=$str.trim()
  return $str
}
# vars
$instrName="#instrName"
$startLocations=@("#classPath")
Write-Host ""
if(Test-Path -Path ./altclasspaths.txt) {
  get-content ./altclasspaths.txt | %{
    $str= Strip-and-Trim($_)
    if($str) {	  
      Write-Host "Reading alternate path: $str"
      $startLocations+=$str
    }
  }
}
$instrDest="$PWD\#from"
#
# script starts here
#
$currentPath=Convert-Path .
foreach ($startLocation in $startLocations) {
  Set-Location $startLocation
  $numCopied=0
  Write-Host "`nCopying any PDFs files from within a `"$instrName`" subfolder of `
`"$startLocation`" to `
`"$instrDest`""
  Get-ChildItem -Path . -Filter "*.pdf" -Recurse |
  Where-Object { $_.DirectoryName -match $instrName } -OutVariable fileName |
  Copy-Item -Destination $instrDest
  if ($fileName.Name -eq $null) {Write-Host "`nNo files to copy"}
  else {
    Write-Host "`nReport:"
    $numCopied=$fileName.Name.Count
    $fileName.Name
  }
  Write-Host @( Get-ChildItem $PWD ).Count "folders examined, $numCopied files copied"
  cd $currentPath
}'
# replace markers with content of vars: #instrName, #classPath
(Get-Content -Path .\copyka.ps1) | ForEach-Object {
	$_ -Replace '#instrName', $instrName `
	   -Replace '#classPath', $classPath } | Set-Content -Path .\copyka.ps1 -encoding UTF8

If ($locale -like "*$myLocale*") {
    (Get-Content -Path .\copyka.ps1) | ForEach-Object {
        $_ -Replace '#from', 'vonSUS' } | Set-Content -Path .\copyka.ps1 -encoding UTF8
} else {
    (Get-Content -Path .\copyka.ps1) | ForEach-Object {
        $_ -Replace '#from', 'fromStudents' } | Set-Content -Path .\copyka.ps1 -encoding UTF8
}

#################
# delete script #
#################
# Version 1.3
Set-Content ./delka.ps1 '# Get-ExecutionPolicy
# Set-ExecutionPolicy RemoteSigned
# This script removes (deletes) PDFs from the $startLocation, but only if in a $instrName folder
#
# Strip-and-Trim function
# Remove everything to the left of a comment character, then trim
function Strip-and-Trim {
  param([string]$str)
  $pos=$str.IndexOf("#")
  If($pos -ne -1) {
    $str=$str.substring(0,$pos)
  }
  $str=$str.trim()
  return $str
}# vars
#
$instrName="#instrName"
$startLocations=@("#classPath")
Write-Host ""
if(Test-Path -Path ./altclasspaths.txt) {
  get-content ./altclasspaths.txt | %{
    $str= Strip-and-Trim($_)
    if($str) {	  
      Write-Host "Reading alternate path: $str"
      $startLocations+=$str
    }
  }
}
$instrDest="$PWD\#from"
#
# script starts here
#
$currentPath=Convert-Path .
foreach ($startLocation in $startLocations) {
  Set-Location $startLocation
  $numDel=0
  Write-Host "`nDeleting the following PDFs files from within a `"$instrName`" subfolder of `
`"$startLocation`""
  Get-ChildItem -Path . -Filter "*.pdf" -Recurse |
    Where-Object { $_.DirectoryName -match $instrName } -OutVariable fileName | 
	Remove-Item
  if ($fileName.Name -eq $null) {Write-Host "`nNo files to delete"}
  else {
    Write-Host "`nReport: deleting files,"
    $numDel=$fileName.Name.Count
    $fileName.Name
  }
  Write-Host @( Get-ChildItem $PWD ).Count "folders examined, $numDel files deleted"
   cd $currentPath
}'
# replace markers with content of vars: #instrName, #classPath
(Get-Content -Path .\delka.ps1) | ForEach-Object {
	$_ -Replace '#instrName', $instrName `
	   -Replace '#classPath', $classPath } | Set-Content -Path .\delka.ps1 -encoding UTF8

If ($locale -like "*$myLocale*") {
    (Get-Content -Path .\delka.ps1) | ForEach-Object {
        $_ -Replace '#from', 'vonSUS' } | Set-Content -Path .\delka.ps1 -encoding UTF8
} else {
    (Get-Content -Path .\delka.ps1) | ForEach-Object {
        $_ -Replace '#from', 'fromStudents' } | Set-Content -Path .\delka.ps1 -encoding UTF8
}

##Begin Version 1.2
##########################
# altclasspaths template #
##########################
Set-Content ./altclasspaths.txt -encoding UTF8 '# C:\Users\IEUser\Desktop\TestClass1'

##End Version 1.2

#############################
# csv to cfg convert script #
#############################
Set-Content ./csvTOcfg.ps1 '# Make a complete cfg for thorshammer package from a csv list
# accepted list delimiters: , or ;. Spaces ar not allowed! Replace all german umlauts
# with ue, oe, ae, ss to avoid problems with thorshammer package
#
# Example list:
# First Name|Last Name|Folder
# Anton;Müller;AM29914M
#\classMember{Anton}{Mueller}{AM299/instructor-name}
# Anton;Müller;*C:/.../AM299
#\classMember{Anton}{Mueller}*{C:/.../AM299/instructor-name}
#
If ( $args.Length -eq 0 ) {
  Write-Host "A CSV file of the class members is required," `
    "see documentation"
  exit
} else {
  $listName=$args[0]
  if(Test-Path -Path ./$listName.csv) {
    } else {
      Write-Host "Cannot find the file `"$listName.csv`" in the current folder," `
        "check the spelling, do not include the extension."
      exit
  }
}'
######################
# Script starts here #
######################'
# Now append the script itself, this way we have to maintain only one script
# Remark: we must escape $ and ` with `$ and ``
Add-Content ./csvTOcfg.ps1 @"
get-content "`$listName.csv" | foreach {
"\classMember{" + `$_ ``
+ "/`#instrName}" ``
-Replace ";", "}{" ``
-Replace ",","}{" ``
-Replace "{\*", "*{" } | Set-Content "convertedList.cfg"

# Store convertedList.cfg in a variable
`$convertedListCfg = get-content .\convertedList.cfg -Raw

# function to replace umlauts
# https://www.datenteiler.de/powershell-umlaute-ersetzen/
function Replace-Umlaute ([string]`$s) {
    `$UmlautObject = New-Object PSObject | Add-Member -MemberType NoteProperty -Name Name -Value `$s -PassThru

    # hash tables are by default case insensitive
    # we have to create a new hash table object for case sensitivity

    `$characterMap = New-Object system.collections.hashtable
    `$characterMap.ä = "ae"
    `$characterMap.ö = "oe"
    `$characterMap.ü = "ue"
    `$characterMap.ß = "ss"
    `$characterMap.Ä = "Ae"
    `$characterMap.Ü = "Ue"
    `$characterMap.Ö = "Oe"

    foreach (`$property  in "Name") {
        foreach (`$key in `$characterMap.Keys) {
            `$UmlautObject.`$property = `$UmlautObject.`$property -creplace `$key,`$characterMap[`$key]
        }
    }
    `$UmlautObject
}

# replace umlauts in $convertedList.cfg and make a correctedListCfg
`$correctedListCfg = Replace-Umlaute "`$convertedListCfg"
`$correctedListCfg.Name

# Finally append correctedListCfg to desired
# cfg file, usually $class.cfg
add-content "..\`#class" `$correctedListCfg.Name
Remove-Item convertedList.cfg
"@

# Set correct class name
(Get-Content "csvTOcfg.ps1") | ForEach-Object {
    $_ -Replace '#class', "00-$class.cfg" `
      -Replace  '#instrName', $instrName } | Set-Content "csvTOcfg.ps1" -encoding UTF8

######################
# General helper BAT #
######################
New-Item "runps1.bat" -ItemType file
Set-Content "runps1.bat" `
'rem echo "%0" %1 %2 %3 %4 %5 %6 %7 %8 %9
PowerShell.exe -ExecutionPolicy Bypass -Command "& ''./%1'' %2"' ##Begin Version 1.2

##################
# class folders  #
##################
New-Item classFolders.ps1 -ItemType file
Set-Content ./classFolders.ps1 '# Create class folders
If ( $args.Length -eq 0 ) {
  Write-Host "A CVS file of the class members is required," `
    "see documentation"
  exit
} else {
  $listName=$args[0]
  if(Test-Path -Path ./$listName.csv) {
    } else {
      Write-Host "Cannot find the file `"$listName.csv`" in the current folder," `
        "check the spelling, do not include the extension."
      exit
  }
}
get-content "$listName.csv" | foreach {
$_ +">#instrName" -Replace ";","," } | Set-Content "commaList.csv"

$argList=@()
get-content "commaList.csv" | %{
# Write-Host "$_"
  $split=$_.split(",")
# Write-Host $split[2]
  $argList+=$split[2]
}
$currentPath=Convert-Path .
cd "#classPath"
Write-Host "Creating folder structure at `"#classPath`"," `
  "with some exceptions"
for ($i=0; $i -lt $argList.length; $i++) {
	$arg=$argList[$i]
	$splitTwo=$argList[$i].split(">")
	$firstName=$splitTwo[0]
	$secondName=$splitTwo[1]
  if ($firstName[0] -eq "*") {
    $arg=$arg.substring(1)
    $msg="Parsing the full path $arg" -Replace ">","/"
    Write-Host $msg
    Write-Host "Creating exceptional folders"
    $firstName=$firstName.substring(1)
#Write-Host "$firstName"
    Write-Host "  Creating folder: $firstName"
    New-Item $firstName -ErrorAction:Ignore -ItemType directory
    $secondName=$firstName+"/$secondName"
#Write-Host "$secondName"
    Write-Host "  Creating folder: $secondName"
    New-Item $secondName -ErrorAction:Ignore -ItemType directory
  } else {
  	$msg="Parsing the relative path $arg" -Replace ">","/"
  	Write-Host $msg
  	Write-Host "  Creating folder: $firstName"
  	New-Item $firstName -ErrorAction:Ignore -ItemType directory
  	cd $firstName
  	Write-Host "  Creating subfolder of $firstName named: $secondName"
  	New-Item $secondName -ErrorAction:Ignore -ItemType directory
  	cd ..
  }
}
cd $currentPath
Remove-Item commaList.csv'
#
# replace markers with content of vars: #instrName, #classPath
(Get-Content -Path .\classFolders.ps1) | ForEach-Object {
	$_ -Replace "#instrName", $instrName `
	   -Replace "#classPath", $classPath } | Set-Content -Path .\classFolders.ps1

############################
# Sample csv file          #
############################
New-Item sample-list.csv -ItemType file
Set-Content ./sample-list.csv `
'Mühle;Wäter;MW634B
Anton,Müller,AM256M
Laura,Vögt,LM356B'

###########################################
# copy container.pdf, terminate-batch.pdf #
###########################################
If (Test-Path $pathThorshammerWin) {
    Copy-Item "$pathThorshammerWin\container.pdf" -Destination "$PWD"
    Copy-Item "$pathThorshammerWin\terminate-batch.pdf" -Destination "$PWD"
}
If (Test-Path $pathThorshammerLin){
Copy-Item "$pathThorshammerLin/container.pdf" -Destination "$PWD"
Copy-Item "$pathThorshammerLin/terminate-batch.pdf" -Destination "$PWD"
}

################
# tex template #
################
cd ..
New-Item "tex-template.tex" -ItemType file

If ($locale -like "*$myLocale*") {
# German ######################################
Set-Content "tex-template.tex" '%% Customize template ! %%
\documentclass[fontsize=12pt]{scrartcl}
\usepackage[%
german,
pro,
usesf,
navibar,
forcolorpaper
%forpaper
]{web}
% Page layout
\usepackage[top=20mm,left=20mm,right=20mm,bottom=25mm]{geometry}
%\useFullWidthForPaper
\usepackage[usesumrytbls,allowrandomize]{exerquiz}
\usepackage{ran_toks}
\usepackage[%
% usebatch    % copy to class folder
% batchdistr  % only copy to instructors folder
testmode      % testing quizzes
]{thorshammer}
\hypersetup{pdfencoding=auto}          % include or remove hyperref options
%\usepackage{thorshammerConf}          % place your own sty file here
%\usepackage[autostyle=true]{csquotes}  % change quotes globally

\DeclareQuiz{q#number}
\setInitMag{fitwidth}
\hypersetup{pdfpagelayout=OneColumn}
\reversemarginpar
% It is important to freeze the seed to that (1) you can reproduce the exact
% same quiz at a later time; (2) allow content written to the AUX file to
% come up to date. This is important when using summary tables.
\useThisSeed{1344524586}

% Class Path
\InputClassData{00-#class}
\autoCopyOn
% \autoCopyOff

% Cover pages
%\DeclareCoverPage{0}

% Header settings
\thQzName{#theme} 
\thQzHeaderL{#class}
\thQzHeaderCQ{#subject: \thqzname}
\thQzHeaderCS{Lösungen: \thqzname}

% Title
\title{#class}
\subject{#subject}
\author{#instrName}
\keywords{#time}
\university{Freiherr-vom-Stein-Berufskolleg}
\version{#number}
\copyrightyears{#date}

% input web.cfg (again) to declare or define commands
\inputWebCfg

\begin{makeClassFiles}
\sadMultQuizzes
\end{makeClassFiles}

% Timer settings, this are personal settings for a timer in the quiz, if you want that, refer to
% http://www.acrotex.net/blog/?p=450
% with \usepackage[shortcount]{cntdwn}

% \newcommand{\thorTimer}[2]{%
% \setShortCntDwn{CntDwnTimer1}
% {%
%     length=#1*\minutes,         
%     notify1=#2*\minutes,        
%     event1=AllowEndQuiz1,
%     event2=NoAction,
%     event3=NoAction,
%     endEvent=EndTheQuiz1
%   }
% }
%\thorTimer{#time}{10} % original command \setShortCntDwn

\begin{document}

\makeinlinetitle

% Declare Quiz bodys
\declareQuizBody{qzbody1}
%\declareQuizBody{qzbody2}
%\declareQuizBody{qzbody3}
%\declareQuizBody{qzbody4}

\begin{qzbody1}
\bRTVToks{\currQuiz}
\thQuizHeader

% priorInitQuiz for the timer, see Timer settings
\begin{defineJS}[\makeesc\!]{\priorInitQuiz}
this.getField("endQuiz.!thisQuiz").display=display.hidden;
qtypesReadOnly("!thisQuiz",false);
\end{defineJS}

% postInitQuiz for timer, see Timer settings
\begin{defineJS}[\makeesc\!]{\postInitQuiz}
AllowEndQuiz1.arg="!currQuiz";
EndTheQuiz1.arg="!currQuiz";
sStartTimer(_oCntDwnTimer1);
\end{defineJS}

% Instructions here

\begin{quiz*}{\currQuiz}
% Bearbeiten Sie folgende Aufgaben.
\begin{questions}
\begin{rtVW}
\section{myTitle}
\item\PTs{2} my question text

\end{rtVW}

%% Change nothing here %%
\eRTVToks\displayListRandomly{\thisQuiz}
\end{questions}
\writeProListAux
\end{quiz*}\quad\thQuizTrailer
\end{qzbody1}
% Randomize questions Now we input the qzbody back in two times, though it can
% be more than that. The quiz name modified in each instance. Each instance of
% the quiz has a randomized order
%
\InputQuizBody{qzbody1}
%\InputQuizBody{qzbody1}
%\InputQuizBody{qzbody1}
%\InputQuizBody{qzbody2}
\end{document}'
###########################################
} else {
# English #################################
Set-Content "tex-template.tex" '%% Customize template ! %%
\documentclass{article}
\usepackage{amstext}
\usepackage{web}
\usepackage[usesumrytbls,allowrandomize]{exerquiz}
\usepackage{ran_toks}
\usepackage[% 
% usebatch    % copy to class folder
% batchdistr  % only copy to instructors folder
testmode      % testing quizzes
]{thorshammer}

\DeclareQuiz{q#number}

% It is important to freeze the seed to that (1) you can reproduce the exact
% same quiz at a later time; (2) allow content written to the AUX file to
% come up to date. This is important when using summary tables.
\useThisSeed{1344524586}

\setInitMag{fitwidth}
\hypersetup{pdfencoding=auto}
\reversemarginpar

\showCreditMarkup

\useBeginQuizButton[\CA{Begin}]
\useEndQuizButton[\CA{End}]
\PTsHook{($\eqPTs^{\text{pts}}$)}
\useMCCircles

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% For headers, title and other fine grained settings %
% refer to the thorshammer doku!                     %
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

% Class Path
\InputClassData{00-#class}
\autoCopyOn
% \autoCopyOff

% Cover pages
%\DeclareCoverPage{0}

% Header settings
\thQzName{#theme}
\thQzHeaderL{#class}
\thQzHeaderCQ{#subject: \thqzname}
\thQzHeaderCS{Solutions: \thqzname}

% Title
\title{#class}
\subject{#subject}
\author{#instrName}
\keywords{#time}
\university{AcroTeX.edu} % change to your institution
\version{#number}
\copyrightyears{#date}

% input web.cfg (again) to declare or define commands
\inputWebCfg

\begin{makeClassFiles}
\sadMultQuizzes
\end{makeClassFiles}

% Timer settings, this are personal settings
% for a timer in the quiz, if you want that, refer to %
% http://www.acrotex.net/blog/?p=450

% \newcommand{\thorTimer}[2]{%
% \setShortCntDwn{CntDwnTimer1}
% {%
%     length=#1*\minutes,         
%     notify1=#2*\minutes,        
%     event1=AllowEndQuiz1,
%     event2=NoAction,
%     event3=NoAction,
%     endEvent=EndTheQuiz1
%   }
% }
%\thorTimer{#time}{10} % original command \setShortCntDwn


\begin{document}

% Declare quiz bodies
\declareQuizBody{qzbody1}
%\declareQuizBody{qzbody2}
%\declareQuizBody{qzbody3}
%\declareQuizBody{qzbody4}

\begin{qzbody1}

\bRTVToks{\currQuiz}

\thQuizHeader

% priorInitQuiz for the timer, see Timer settings
\begin{defineJS}[\makeesc\!]{\priorInitQuiz}
this.getField("endQuiz.!thisQuiz").display=display.hidden;
qtypesReadOnly("!thisQuiz",false);
\end{defineJS}

% postInitQuiz for timer, see Timer settings
\begin{defineJS}[\makeesc\!]{\postInitQuiz}
AllowEndQuiz1.arg="!currQuiz";
EndTheQuiz1.arg="!currQuiz";
sStartTimer(_oCntDwnTimer1);
\end{defineJS}

% Instructions
\begin{quiz*}{\currQuiz}
Solve each of these problems, passing is 100\%.
\begin{questions}
\begin{rtVW}
  \item\PTs{3} Answer the question...

\end{rtVW}
%% Change nothing here %%
\eRTVToks
\displayListRandomly{\thisQuiz}
\end{questions}
\writeProListAux
\end{quiz*}\quad\thQuizTrailer
\end{qzbody1}
% Randomize questions Now we input the qzbody back in two times, though it can
% be more than that. The quiz name modified in each instance. Each instance of
% the quiz has a randomized order
%
\InputQuizBody{qzbody1}
%\InputQuizBody{qzbody1}
%\InputQuizBody{qzbody1}
%\InputQuizBody{qzbody2}
\end{document}'
}

###################################
# Generate TEX file from template #
###################################
New-Item "genquiz.ps1" -ItemType file
Set-Content "genquiz.ps1" `
'# Remove everything to the left of a comment character, then trim
function Strip-and-Trim {
  param([string]$str)
  $pos=$str.IndexOf("#")
  If($pos -ne -1) {
    $str=$str.substring(0,$pos)
  }
  $str=$str.trim()
  return $str
}
$argList=@()
get-content ../#varsList | %{
      Write-Host "Line: $_"
      $argList+=$_
  }
$class = Strip-and-Trim($argList[0])
$number = Strip-and-Trim($argList[1])
$subject = Strip-and-Trim($argList[2])
$theme = Strip-and-Trim($argList[3])
$instrName = Strip-and-Trim($argList[4])
$date = Strip-and-Trim($argList[5])
$time = Strip-and-Trim($argList[6])
$classPath= Strip-and-Trim($argList[7])

$templateName="$number-$class-$subject.tex"
Copy-Item ./tex-template.tex ./$templateName

# replace markers with vars
(Get-Content $templateName) | ForEach-Object {
	$_ -Replace ''#instrName'', $instrName `
	   -Replace ''#class'', $class `
	   -Replace ''#time'', $time `
	   -Replace ''#number'', $number `
	   -Replace ''#theme'', $theme `
	   -Replace ''#subject'', $subject `
	   -Replace ''#date'', $date } | Set-Content $templateName -encoding UTF8`
# Remove whitespace and umlauts from file name
Get-ChildItem *.tex | Rename-Item -NewName {
    $_.Name -replace '' '',''-'' `
      -replace ''ä'',''ae'' `
      -replace ''ö'',''oe'' `
      -replace ''ü'',''ue'' `
      -replace ''ß'',''ss'' }'
(Get-Content "genQuiz.ps1") | ForEach-Object {
	$_ -Replace '#varsList', $listOfEight } | Set-Content "genQuiz.ps1"
# Remove whitespace from tex template file
Get-ChildItem *.tex | Rename-Item -NewName { $_.Name -replace ' ','-' }

######################
# General helper BAT #
######################
New-Item "runps1.bat" -ItemType file
Set-Content "runps1.bat" `
'rem echo "%0" %1 %2 %3 %4 %5 %6 %7 %8 %9
PowerShell.exe -ExecutionPolicy Bypass -Command "& ''./%1'' %2"' ##Begin Version 1.2

####################
# web.cfg template #
####################
New-Item "web.cfg" -ItemType file
Set-Content "web.cfg" `
'%
% AeB Web Configuration file
%
\ExecuteOptions{dvips}
\bWebCustomize
% Insert redefinitions between these two marks
\eWebCustomize'

################
# cfg template #
################
New-Item "00-$class.cfg" -ItemType file
Set-Content "00-$class.cfg" '%reset the paths for \instrPath and \classPath for your system
% Use relative (*!) paths here for instructor according to dir, where tex file is located
\classPath{/#class}
\instrPath*{#instrName/backup}
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% Syntax:
% \classMember{firstname}{lastname}{rel-path}
% \classMember{firstname}{lastname}*{full-path}
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%'

# replace markers with content of vars
$pattern = "\\"
$classPath = $classPath -replace $pattern, "/"
(Get-Content "00-$class.cfg") | ForEach-Object {
    $_ -Replace '#instrName', $instrName `
      -Replace '#class', $classPath `
      -Replace ':', '' } | Set-Content "00-$class.cfg" -encoding UTF8
cd ..
