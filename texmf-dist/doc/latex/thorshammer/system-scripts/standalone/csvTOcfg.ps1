# Make a complete cfg for thorshammer package from a csv list
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
}
get-content "$listName.csv" | foreach {
"\classMember{" + $_ `
+ "/_Thor}" `
-Replace ";", "}{" `
-Replace ",","}{" `
-Replace "{\*", "*{" } | Set-Content "convertedList.cfg"

# Store convertedList.cfg in a variable
$convertedListCfg = get-content .\convertedList.cfg -Raw

# function to replace umlauts
# https://www.datenteiler.de/powershell-umlaute-ersetzen/
function Replace-Umlaute ([string]$s) {
    $UmlautObject = New-Object PSObject | Add-Member -MemberType NoteProperty -Name Name -Value $s -PassThru

    # hash tables are by default case insensitive
    # we have to create a new hash table object for case sensitivity

    $characterMap = New-Object system.collections.hashtable
    $characterMap.ä = "ae"
    $characterMap.ö = "oe"
    $characterMap.ü = "ue"
    $characterMap.ß = "ss"
    $characterMap.Ä = "Ae"
    $characterMap.Ü = "Ue"
    $characterMap.Ö = "Oe"

    foreach ($property  in "Name") {
        foreach ($key in $characterMap.Keys) {
            $UmlautObject.$property = $UmlautObject.$property -creplace $key,$characterMap[$key]
        }
    }
    $UmlautObject
}

# replace umlauts in .cfg and make a correctedListCfg
$correctedListCfg = Replace-Umlaute "$convertedListCfg"
$correctedListCfg.Name

# Finally append correctedListCfg to desired
# cfg file, usually class.cfg
add-content "00-class.cfg" $correctedListCfg.Name
Remove-Item convertedList.cfg
