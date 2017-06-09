#!/bin/bash
##----------------------------------------------------------------------------##
##               █      █                                                     ##
##               ████████                                                     ##
##             ██        ██                                                   ##
##            ███  █  █  ███        build.sh                                  ##
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
GAME_NAME="bow-and-arrow";
## Input dir.
PROJECT_DIR="./project"
PROJECT_DIR_BIN="$PROJECT_DIR/bin"
PROJECT_DIR_OBJ="$PROJECT_DIR/obj"
## Output dir.
BUILD_DIR="./build";
## Compilers
XBUILD="xbuild /p:Configuration=";
CXX="g++ -Ofast";

BUILD_TYPE="Release"; ## Default to release.


################################################################################
## Functions                                                                  ##
################################################################################
dev_build()
{
    echo "Build Type      : $BUILD_TYPE";
    echo "Project dir     : $PROJECT_DIR";
    echo "Project bin dir : $PROJECT_DIR_BIN";
    echo "Project obj dir : $PROJECT_DIR_OBJ";

    ## Clean up the build dirs.
    rm    -rf "$BUILD_DIR";
    rm    -rf "$PROJECT_DIR_BIN";
    rm    -rf "$PROJECT_DIR_OBJ";
    mkdir -p  "$BUILD_DIR";

    ## Compile the MonoGame project.
    ${XBUILD}${BUILD_TYPE} ./project/com.amazingcow.BowAndArrow.csproj

    ## Compile the bootstrap.
    $CXX "./$PROJECT_DIR/bootstrap.cpp" -o $PROJECT_DIR_BIN/$GAME_NAME;

    ## Copy everything to build folder.
    cp -r "$PROJECT_DIR_BIN"/* "$BUILD_DIR";
}


################################################################################
## Script                                                                     ##
################################################################################
## Check if we're on Project Base dir.
##   If not tell the user that the scripts is
##   supposed to run on that.
if [ $(basename $PWD) == "scripts" ]; then
    echo "Wrong directory - To build the game run the scripts on the project's base path.";
    echo "  Ex: ./scripts/build.sh";
    exit -1;
fi;

## Get from command line the build type.
if [ -n "$1" ]; then
    BUILD_TYPE="$1";
fi;

## Check if the build type is valid.
if [ "$BUILD_TYPE" = "Debug" ]; then
    BUILD_TYPE="Debug";
elif [ "$BUILD_TYPE" = "Release" ]; then
    BUILD_TYPE="Release";
else
    echo "Invalid build type: ($BUILD_TYPE)";
    exit -1;
fi;

## Build the game.
dev_build;

