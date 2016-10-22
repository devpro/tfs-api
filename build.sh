#!/usr/bin/env bash

#exit if any command fails
set -e

artifactsFolder="./artifacts"

if [ -d $artifactsFolder ]; then
  rm -R $artifactsFolder
fi

dotnet restore

dotnet build ./src/Devpro.TfsApi -c Release -f net452 --no-incremental

revision=${TRAVIS_JOB_ID:=1}
revision=$(printf "beta%04d" $revision)
echo 'Revision is "'$revision'"'

dotnet pack ./src/Devpro.TfsApi.Dto -c Release -o ./artifacts --version-suffix=$revision
