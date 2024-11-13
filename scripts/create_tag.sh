#!/usr/bin/env bash

[[ $# -lt 1 ]] && {
  echo "No version found in arg"
  exit 1
}

version="$1"

[[ $version == '0.1.0' ]] && {
  echo "The init version, no create tag"
  exit 0
}

current_tag="v${version}"

tags=$(git tag)

for tag in $tags; do
  if [[ "$tag" == "$current_tag" ]]; then
    echo "Current tag was created already."
    exit 0
  fi
done

git config --global user.email "kevinnitro@duck.com"
git config --global user.name "KevinNitroG"

git tag "$current_tag"
git push origin tag "$current_tag"

# vim: set fileformat=unix:
