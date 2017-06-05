#!/bin/bash

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
