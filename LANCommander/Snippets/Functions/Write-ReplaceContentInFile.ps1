﻿# Use regex to replace text within a file. Quotes are escaped by double quoting ("")
Write-ReplaceContentInFile -Regex '^game.setPlayerName "(.+)"' -Replacement "game.setPlayerName ""$NewName""" -FilePath "$InstallDir\<File Path>"