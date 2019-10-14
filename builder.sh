#!/bin/bash

function usage()
{
cat << _EOT_

 builder
------------

Builder for unity application.

Usage:
  ./$(basename "$0") --outputApplicationPath /path/to/app [ --platform android --version 2019.2.6f1 ]

Required:
  -o, --outputApplicationPath /path/to/app : Output application path. ( the extension is not needed. )

Optional:
  -p, --platform android   : Platform. [ android | ios | mac | windows ]  [ default: android ]
  -v, --version 2019.2.6f1 : Unity version. [ default: 2019.2.6f1 ]

Helper options:
  --help, --debug

_EOT_
  [[ "${1+x}" != "" ]] && { exit "${1}"; }
  exit 1
}
function printColored() { local B="\033[0;"; local C=""; case "${1}" in "red") C="31m";; "green") C="32m";; "yellow") C="33m";; "blue") C="34m";; esac; printf "%b%b\033[0m" "${B}${C}" "${2}"; }



#------------------------------------------
# Preparation
#------------------------------------------
set -eu

# Parse parameters
for ARG in "$@"
do
    SHIFT="true"
    [[ "${ARG}" == "--debug" ]] && { shift 1; set -eux; SHIFT="false"; }
    { [[ "${ARG}" == "--outputApplicationPath" ]] || [[ "${ARG}" == "-o" ]]; } && { shift 1; OUTPUT_APPLICATION_PATH="${1}"; SHIFT="false"; }
    { [[ "${ARG}" == "--platform" ]] || [[ "${ARG}" == "-p" ]]; } && { shift 1; PLATFORM="${1}"; SHIFT="false"; }
    { [[ "${ARG}" == "--version" ]] || [[ "${ARG}" == "-v" ]]; } && { shift 1; VERSION="${1}"; SHIFT="false"; }
    { [[ "${ARG}" == "--help" ]] || [[ "${ARG}" == "-h" ]]; } && { shift 1; HELP="true"; SHIFT="false"; }
    { [[ "${SHIFT}" == "true" ]] && [[ "$#" -gt 0 ]]; } && { shift 1; }
done
[[ -n "${HELP+x}" ]] && { usage 0; }
# Check required parameters
[[ -z "${OUTPUT_APPLICATION_PATH+x}" ]] && { printColored yellow "[!] --outputApplicationPath is required.\n"; INVALID_STATE="true"; }
# Check invalid state and display usage
[[ -n "${INVALID_STATE+x}" ]] && { usage; }
# Initialize optional variables
[[ -z "${PLATFORM+x}" ]] && { PLATFORM="android"; }
[[ -z "${VERSION+x}" ]] && { VERSION="2019.2.6f1"; }



#------------------------------------------
# Main
#------------------------------------------

cat << __EOT__

[ Required parameters ]
outputApplicationPath: ${OUTPUT_APPLICATION_PATH}

[ Optional parameters ]
platform: ${PLATFORM}
version: ${VERSION}

__EOT__




SCRIPT_DIR="$(cd $(dirname "${BASH_SOURCE:-$0}") && pwd)"
APPLICATION_DIR="${SCRIPT_DIR}/unity-todo-app"
UNITY_CMD="/Applications/Unity/Hub/Editor/${VERSION}/Unity.app/Contents/MacOS/Unity"
BUILD_LOG="/tmp/build.log"

if [[ "${PLATFORM}" == "android" ]]; then
    ${UNITY_CMD} -batchmode -quit -logFile "${BUILD_LOG}" -projectPath "${APPLICATION_DIR}" -buildTarget android -executeMethod AppBuilder.buildForAndroid "${OUTPUT_APPLICATION_PATH}"
elif [[ "${PLATFORM}" == "ios" ]]; then
    ${UNITY_CMD} -batchmode -quit -logFile "${BUILD_LOG}" -projectPath "${APPLICATION_DIR}" -buildTarget ios -executeMethod AppBuilder.buildForIOS "${OUTPUT_APPLICATION_PATH}"
elif [[ "${PLATFORM}" == "mac" ]]; then
    ${UNITY_CMD} -batchmode -quit -logFile "${BUILD_LOG}" -projectPath "${APPLICATION_DIR}" -buildTarget mac -executeMethod AppBuilder.buildForMac "${OUTPUT_APPLICATION_PATH}"
elif [[ "${PLATFORM}" == "windows" ]]; then
    ${UNITY_CMD} -batchmode -quit -logFile "${BUILD_LOG}" -projectPath "${APPLICATION_DIR}" -buildTarget mac -executeMethod AppBuilder.buildForWindows "${OUTPUT_APPLICATION_PATH}"
else
    printColored yellow "${PLATFORM} is Unknown."
    exit 1
fi

# curl -sf ${STARTER_URL} |bash -s - \
#   -n builder \
#   -d "Builder for unity application."  \
#   -r outputApplicationPath,/path/to/app,"Output application path. ( the extension is not needed. )" \
#   -o platform,android,"Platform. [ android | ios | mac | windows ] ",android \
#   -o version,2019.2.6f1,"Unity version.",2019.2.6f1 \
#   -s > /tmp/test.sh; open /tmp/test.sh
