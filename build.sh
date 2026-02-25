#!/usr/bin/env bash
# shellcheck disable=SC2086,SC2048
dotnet run --project ./CakeBuild/CakeBuild.csproj -- $*
exit $?
