# Get-ExecutionPolicy
# Set-ExecutionPolicy RemoteSigned
# This script removes (deletes) PDFs from the $startLocation, but only if in a $baseName folder
$baseName="_Thor"
$classPath="C:\Users\dpstory\Desktop\Test Folder\target\myClass"
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
$startLocations=@($classPath)
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
$instrDest="$PWD\fromStudents"
#
# script starts here
#
$currentPath=Convert-Path .
foreach ($startLocation in $startLocations) {
  Set-Location $startLocation
  $numDel=0
  Write-Host "`nDeleting the following PDFs files from within a `"$baseName`" subfolder of `
`"$startLocation`""
  Get-ChildItem -Path . -Filter "*.pdf" -Recurse |
    Where-Object { $_.DirectoryName -match $baseName } -OutVariable fileName | 
	Remove-Item
  if ($fileName.Name -eq $null) {Write-Host "`nNo files to delete"}
  else {
    Write-Host "`nReport: deleting files,"
    $numDel=$fileName.Name.Count
    $fileName.Name
  }
  Write-Host @( Get-ChildItem $PWD ).Count "folders examined, $numDel files deleted"
   cd $currentPath
}
