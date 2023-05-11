#!/usr/bin/env bash

source ./localci/local_find_unity.sh

set -x

export BUILD_NAME=${BUILD_NAME:-"KardiaPraxis"}

BUILD_TARGET=StandaloneWindows64 ./ci/build.sh
BUILD_TARGET=StandaloneLinux64 ./ci/build.sh
