$ErrorActionPreference = "Stop"
$utf8NoBom = New-Object System.Text.UTF8Encoding($false)

Write-Host "Replacing content in files..."
$files = Get-ChildItem -Path . -File -Recurse | Where-Object { $_.Extension -notmatch '\.(dll|exe|png|jpg|jpeg|gif|cache|pdb|sqlite|db|idx|pack|zip|ico|woff|woff2|ttf|eot)$' }

foreach ($f in $files) {
    try {
        $content = [System.IO.File]::ReadAllText($f.FullName)
        if ($content.Contains('SV22T1020775')) {
            $newContent = $content.Replace('SV22T1020775', 'SV22T1020775')
            [System.IO.File]::WriteAllText($f.FullName, $newContent, $utf8NoBom)
            Write-Host "Updated content in: $($f.FullName)"
        }
    } catch {
        Write-Host "Skipped file due to read error: $($f.FullName)"
    }
}

Write-Host "Renaming files..."
$filesToRename = Get-ChildItem -Path . -File -Recurse | Where-Object { $_.Name -match 'SV22T1020775' }
foreach ($f in $filesToRename) {
    if ($f.Exists) {
        $newName = $f.Name -replace 'SV22T1020775', 'SV22T1020775'
        Rename-Item -Path $f.FullName -NewName $newName
        Write-Host "Renamed file to: $newName"
    }
}

Write-Host "Renaming directories..."
$dirsToRename = Get-ChildItem -Path . -Directory -Recurse | Where-Object { $_.Name -match 'SV22T1020775' } | Sort-Object -Property @{Expression={$_.FullName.Length}; Descending=$true}
foreach ($d in $dirsToRename) {
    if ($d.Exists) {
        $newName = $d.Name -replace 'SV22T1020775', 'SV22T1020775'
        Rename-Item -Path $d.FullName -NewName $newName
        Write-Host "Renamed directory to: $newName"
    }
}

Write-Host "Done."
