#!/bin/bash
##----------------------------------------------------------------------------##
##               █      █                                                     ##
##               ████████                                                     ##
##             ██        ██                                                   ##
##            ███  █  █  ███        generate_archive.sh                       ##
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
