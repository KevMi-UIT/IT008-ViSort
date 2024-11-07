#!/usr/bin/env bash

current_tag="v${1}"

tags=$(git tag | sed "s/^v//")

for tag in $tags; do
  if [[ "$tag" == "$current_tag" ]]; then
    echo "Current tag was created already."
    exit 0
  fi
done

git tag "$current_tag"
git push origin tag "$current_tag"

# vim: set fileformat=unix:
