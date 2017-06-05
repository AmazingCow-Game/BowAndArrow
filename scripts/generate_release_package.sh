#!/bin/bash

################################################################################
## Public Vars                                                                ##
################################################################################
## Names.
HOST=$(echo $(uname -o)_$(uname -m) | tr '/' '_');
GIT_TAG=$(git describe --tags --abbrev=0 | tr . _);
GAME_NAME="bow-and-arrow";
## Dirs.
BUILD_DIR="build";
RELEASE_FILES_DIR="release_files";
OUTPUT_DIR="release";
OUTPUT_TMP_DIR="release/$GAME_NAME";


################################################################################
## Functions                                                                  ##
################################################################################
gen_release_package()
{
    # Clean the output directory.
    rm    -rf "$OUTPUT_DIR";
    mkdir -p  "$OUTPUT_TMP_DIR";

    ## Copy all the files.
    cp -rv "$BUILD_DIR"/*         "$OUTPUT_TMP_DIR";
    cp -rv "$RELEASE_FILES_DIR"/* "$OUTPUT_TMP_DIR";

    ## Compress the output, to make it ready for distribution.
    COMPRESSED_FILENAME="./${GAME_NAME}_${GIT_TAG}_${HOST}";
    ZIP_FILENAME="$COMPRESSED_FILENAME.zip";
    TARGZ_FILENAME="$COMPRESSED_FILENAME.tar.gz";

    cd "$OUTPUT_DIR";
    zip -r    "$ZIP_FILENAME"   "./$GAME_NAME";
    tar -zcvf "$TARGZ_FILENAME" "./$GAME_NAME";
}


################################################################################
## Script                                                                     ##
################################################################################
## Check if we're on Project Base dir.
##   If not tell the user that the scripts is
##   supposed to run on that.
if [ $(basename $PWD) == "scripts" ]; then
    echo "Wrong directory - To generate release package run the scripts on the project's base path.";
    echo "  Ex: ./scripts/generate_release_package.sh";
    exit -1;
fi;

gen_release_package;
