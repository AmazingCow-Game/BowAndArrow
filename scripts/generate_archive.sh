#!/bin/bash

################################################################################
## Public Vars                                                                ##
################################################################################
GIT_TAG=$(git describe --tags --abbrev=0 | tr . _);
GAME_NAME="bow-and-arrow";
OUTPUT_DIR="archives";
OUTPUT_FILENAME="source_${GAME_NAME}_${GIT_TAG}";

################################################################################
## Script                                                                     ##
################################################################################
echo "--> Generating Archives for ($GAME_NAME) version ($GIT_TAG) in (./$OUTPUT_DIR).";

## Clean up the dir.
rm    -rf "$OUTPUT_DIR";
mkdir -p  "$OUTPUT_DIR";

## Archive.
if [ -z $(which git-archive-all) ]; then
    echo "[FATAL] Missing git-archive-all - Aborting.";
    exit 1;
fi;

git-archive-all $OUTPUT_DIR/$OUTPUT_FILENAME.zip
git-archive-all $OUTPUT_DIR/$OUTPUT_FILENAME.tar.gz

echo "--> Done...";
