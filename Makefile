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

################################################################################
## Public Vars                                                                ##
################################################################################
HOST="linux_x64"


################################################################################
## Private Vars                                                               ##
################################################################################
_GAME_NAME=bow_and_arrow


_COW_BIN=/usr/local/bin
_COW_SHARE=/usr/local/share/amazingcow_game_$(_GAME_NAME)
_GIT_TAG=`git describe --tags --abbrev=0 | tr . _`

_PROJECT_DIR=./project
_PROJECT_BIN_DIR=$(_PROJECT_DIR)/bin
_PROJECT_OBJ_DIR=$(_PROJECT_DIR)/obj

_CC=g++ -Ofast
_XBUILD=xbuild /p:Configuration=Release



################################################################################
## End user                                                                   ##
################################################################################
install:
	@ echo "---> Installing...".

	@ ## Deleting old stuff...
	@ rm -rf $(_COW_SHARE)
	@ rm -rf $(_COW_BIN)/$(_GAME_NAME)

	@ ## Install new stuff...
	@ mkdir -p $(_COW_SHARE)

	@ cp -rf ./build/* $(_COW_SHARE)
	@ ln -s  $(_COW_SHARE)/$(_GAME_NAME) $(_COW_BIN)/$(_GAME_NAME)

	@ echo "---> Done... We **really** hope that you have fun :D"


################################################################################
## Release                                                                    ##
################################################################################
gen-binary:
	mkdir -p ./bin/$(_GAME_NAME)

	cp -rf ./build/* ./bin/$(_GAME_NAME)
	cp AUTHORS.txt   \
	   CHANGELOG.txt \
	   COPYING.txt   \
	   README.md     \
	   TODO.txt      \
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
	rm -rf $(_PROJECT_BIN_DIR)
	rm -rf $(_PROJECT_OBJ_DIR)
	mkdir -p ./build

	$(_XBUILD) ./project/com.amazingcow.BowAndArrow.csproj

	## Copile the bootstrap
	$(_CC) ./project/bootstrap.cpp -o $(_PROJECT_BIN_DIR)/$(_GAME_NAME)

	## Copy everything to this directory level
	cp -r $(_PROJECT_BIN_DIR)/* ./build

