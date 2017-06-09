#!/bin/bash
##----------------------------------------------------------------------------##
##               █      █                                                     ##
##               ████████                                                     ##
##             ██        ██                                                   ##
##            ███  █  █  ███        install.sh                                ##
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
BIN_DIR="/usr/local/bin";
GAME_DATA_DIR="/usr/local/share/amazingcow_game_bow_and_arrow";
DESKTOP_SHARE_DIR="/usr/local/share/applications";

################################################################################
## Script                                                                     ##
################################################################################
BASEDIR=$(dirname "$0")
cd "$BASEDIR";

## We need super user access since we are touching
## some paths that belongs to system.
if [ "$(id -u)" != "0" ]; then
   echo "This script must be run as root" 1>&2
   exit 1
fi


## Some systems the .desktop files resides in
## other places.
if [ ! -e $DESKTOP_SHARE_DIR ]; then
    DESKTOP_SHARE_DIR="/usr/share/applications";
fi;


echo " ----------------------------------------- ";
echo " - Bow & Arrow - Amazing Cow - Installer - ";
echo " -    Copyright (c) 2017 - AmazingCow    - ";
echo " -      GPLv3 - www.amazingcow.com       - ";
echo " ----------------------------------------- ";
echo "";

## Bootstrap executable
echo "--> Copying executable to ($BIN_DIR)";
cp bow-and-arrow $BIN_DIR/;

## Desktop entry.
echo "--> Copying desktop entry to ($DESKTOP_SHARE_DIR)";
cp bow_and_arrow.desktop $DESKTOP_SHARE_DIR/;

## Game Files
echo "--> Copying game data to ($GAME_DATA_DIR)";
mkdir -p "$GAME_DATA_DIR";

cp    com.amazingcow.BowAndArrow.exe  $GAME_DATA_DIR/;
cp -r Content                         $GAME_DATA_DIR/;
cp    MonoGame.Framework.dll          $GAME_DATA_DIR/;
cp    NVorbis.dll                     $GAME_DATA_DIR/;
cp    OpenTK.dll                      $GAME_DATA_DIR/;
cp    OpenTK.dll.config               $GAME_DATA_DIR/;

echo "--> Done...";

echo "";
echo "Thank you for playing Bow & Arrow.";
echo "This game is FREE SOFTWARE (GPLv3), so you can";
echo "study, hack and share it!";
echo "";
echo "Go to http://amazingcow.com for more Free Software!";
echo "";
echo "Press enter to continue...";
read
