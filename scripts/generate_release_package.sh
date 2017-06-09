#!/bin/bash
##----------------------------------------------------------------------------##
##               █      █                                                     ##
##               ████████                                                     ##
##             ██        ██                                                   ##
##            ███  █  █  ███        generate_release_package.sh               ##
##            █ █        █ █        Game_BowAndArrow                          ##
##             ████████████                                                   ##
##           █              █       Copyright (c) 2017                        ##
##          █     █    █     █      AmazingCow - www.AmazingCow.com           ##
##          █     █    █     █                                                ##
##           █              █       N2OMatt - n2omatt@amazingcow.com          ##
##             ████████████         www.amazingcow.com/n2omatt                ##
##                                                                            ##
##                  This software is licensed as GPLv3                        ##
##                 CHECK THE COPYING FILE TO MORE DETAILS                     ##
##                                                                            ##
##    Permission is granted to anyone to use this software for any purpose,   ##
##   including commercial applications, and to alter it and redistribute it   ##
##               freely, subject to the following restrictions:               ##
##                                                                            ##
##     0. You **CANNOT** change the type of the license.                      ##
##     1. The origin of this software must not be misrepresented;             ##
##        you must not claim that you wrote the original software.            ##
##     2. If you use this software in a product, an acknowledgment in the     ##
##        product IS HIGHLY APPRECIATED, both in source and binary forms.     ##
##        (See opensource.AmazingCow.com/acknowledgment.html for details).    ##
##        If you will not acknowledge, just send us a email. We'll be         ##
##        *VERY* happy to see our work being used by other people. :)         ##
##        The email is: acknowledgment_opensource@AmazingCow.com              ##
##     3. Altered source versions must be plainly marked as such,             ##
##        and must not be misrepresented as being the original software.      ##
##     4. This notice may not be removed or altered from any source           ##
##        distribution.                                                       ##
##     5. Most important, you must have fun. ;)                               ##
##                                                                            ##
##      Visit opensource.amazingcow.com for more open-source projects.        ##
##                                                                            ##
##                                  Enjoy :)                                  ##
##----------------------------------------------------------------------------##

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
