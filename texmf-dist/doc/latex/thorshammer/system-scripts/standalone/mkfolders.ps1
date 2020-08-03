$classPath="C:\Users\dpstory\Desktop\Test Folder\target\myClass"
# Create class folders
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
$_ +">_Thor" -Replace ";","," } | Set-Content "commaList.csv"

$argList=@()
get-content "commaList.csv" | %{
# Write-Host "$_"
  $split=$_.split(",")
# Write-Host $split[2]
  $argList+=$split[2]
}
$currentPath=Convert-Path .
cd $classPath
Write-Host "Creating folder structure at `$classPath`"," `
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
Remove-Item commaList.csv
