$SLN_FILE = $args[0]

dotnet format $SLN_FILE --verbosity detailed --verify-no-changes --no-restore

if (! $?)
{
  Write-Error "Please reformat"
  [Environment]::Exit(1)
}
