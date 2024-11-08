$SLN_FILE = $args[0]

$commands = @(
  "dotnet format $($SLN_FILE) style --verify-no-changes",
  "dotnet format $($SLN_FILE) whitespace --verify-no-changes",
  "dotnet format $($SLN_FILE) analyzers --verify-no-changes"
)

foreach ($command in $commands)
{
  $output = Invoke-Expression $command
  if ($output -ne "")
  {
    $fail = $true
  }
}
          
if ($fail)
{
  Write-Host "There's check"
  [Environment]::Exit(1)
}
