##----------------------------------------------------------------------------##
##               █      █                                                     ##
##               ████████                                                     ##
##             ██        ██                                                   ##
##            ███  █  █  ███        Makefile                                  ##
##            █ █        █ █        Game_BowAndArrow                          ##
##             ████████████                                                   ##
##           █              █       Copyright (c) 2016                        ##
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

##COWTODO: We hard code the paths in bootstrap.cpp  \
##         While this matches the paths in Makefile \
##         it's fragile and we must change to       \
##         a more robust approach soon as possible.


################################################################################
## Public Vars                                                                ##
################################################################################
HOST=`uname -s`_`uname -m`


################################################################################
## Private Vars                                                               ##
################################################################################
_GAME_SAFE_NAME=bow_and_arrow
_GAME_NAME=bow-and-arrow
_DESKTOP_FILENAME=$(_GAME_SAFE_NAME).desktop

_INSTALL_DIR_BIN=/usr/local/bin
_INSTALL_DIR_SHARE=/usr/local/share/amazingcow_game_$(_GAME_SAFE_NAME)
_INSTALL_DIR_DESKTOP=/usr/share/applications

_PROJECT_DIR=./project
_PROJECT_DIR_BIN=$(_PROJECT_DIR)/bin
_PROJECT_DIR_OBJ=$(_PROJECT_DIR)/obj


_GIT_TAG=`git describe --tags --abbrev=0 | tr . _`
_CC=g++ -Ofast
_XBUILD=xbuild /p:Configuration=Release

SILENT=@


################################################################################
## End user                                                                   ##
################################################################################
install:
	$(SILENT) echo "---> Installing..."


	$(SILENT) ## Deleting old stuff...
	$(SILENT) rm -rf $(_INSTALL_DIR_SHARE)
	$(SILENT) rm -rf $(_INSTALL_DIR_BIN)/$(_GAME_NAME)
	$(SILENT) rm -rf $(_INSTALL_DIR_DESKTOP/$(_DESKTOP_FILENAME)

	$(SILENT) ## Create the dir if it doesn't exists...
	$(SILENT) mkdir -p $(_INSTALL_DIR_SHARE)

	$(SILENT) ## Copy the files to the share
	$(SILENT) cp -rf ./build/* $(_INSTALL_DIR_SHARE)

	$(SILENT) ## Copy the bootstrap
	$(SILENT) cp -rf $(_INSTALL_DIR_SHARE)/$(_GAME_NAME) $(_INSTALL_DIR_BIN)/$(_GAME_NAME)

	$(SILENT) ## Copy the desktop entry.
	$(SILENT) cp -f $(_DESKTOP_FILENAME) $(_INSTALL_DIR_DESKTOP)


	$(SILENT) echo "---> Done... We **really** hope that you have fun :D"


################################################################################
## Release                                                                    ##
################################################################################
gen-binary:
	mkdir -p ./bin/$(_GAME_NAME)

	cp -rf ./build/* ./bin/$(_GAME_NAME)
	cp AUTHORS.txt          \
	   CHANGELOG.txt        \
	   COPYING.txt          \
	   README.md            \
	   TODO.txt             \
	   $(_DESKTOP_FILENAME) \
	./bin/$(_GAME_NAME)

	cd ./bin && zip -r ./$(HOST)_$(_GIT_TAG).zip ./$(_GAME_NAME)
	rm -rf ./bin/$(_GAME_NAME)


gen-archive:
	rm -rf   ./archives
	mkdir -p ./archives

	git-archive-all ./archives/source_$(_GAME_NAME)_$(_GIT_TAG).zip
	git-archive-all  ./archives/source_$(_GAME_NAME)_$(_GIT_TAG).tar.gz


################################################################################
## Dev                                                                        ##
################################################################################
dev-build:
	## Compile the MonogGame Project
	rm -rf ./build
	rm -rf $(_PROJECT_DIR_BIN)
	rm -rf $(_PROJECT_DIR_OBJ)
	mkdir -p ./build

	$(_XBUILD) ./project/com.amazingcow.BowAndArrow.csproj

	## Copile the bootstrap
	$(_CC) ./project/bootstrap.cpp -o $(_PROJECT_DIR_BIN)/$(_GAME_NAME)

	## Copy everything to this directory level
	cp -r $(_PROJECT_DIR_BIN)/* ./build

