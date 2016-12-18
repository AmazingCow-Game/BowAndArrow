//----------------------------------------------------------------------------//
//               █      █                                                     //
//               ████████                                                     //
//             ██        ██                                                   //
//            ███  █  █  ███        ResourcesManager.cs                       //
//            █ █        █ █        Game_BowAndArrow                          //
//             ████████████                                                   //
//           █              █       Copyright (c) 2016                        //
//          █     █    █     █      AmazingCow - www.AmazingCow.com           //
//          █     █    █     █                                                //
//           █              █       N2OMatt - n2omatt@amazingcow.com          //
//             ████████████         www.amazingcow.com/n2omatt                //
//                                                                            //
//                  This software is licensed as GPLv3                        //
//                 CHECK THE COPYING FILE TO MORE DETAILS                     //
//                                                                            //
//    Permission is granted to anyone to use this software for any purpose,   //
//   including commercial applications, and to alter it and redistribute it   //
//               freely, subject to the following restrictions:               //
//                                                                            //
//     0. You **CANNOT** change the type of the license.                      //
//     1. The origin of this software must not be misrepresented;             //
//        you must not claim that you wrote the original software.            //
//     2. If you use this software in a product, an acknowledgment in the     //
//        product IS HIGHLY APPRECIATED, both in source and binary forms.     //
//        (See opensource.AmazingCow.com/acknowledgment.html for details).    //
//        If you will not acknowledge, just send us a email. We'll be         //
//        *VERY* happy to see our work being used by other people. :)         //
//        The email is: acknowledgment_opensource@AmazingCow.com              //
//     3. Altered source versions must be plainly marked as such,             //
//        and must not be misrepresented as being the original software.      //
//     4. This notice may not be removed or altered from any source           //
//        distribution.                                                       //
//     5. Most important, you must have fun. ;)                               //
//                                                                            //
//      Visit opensource.amazingcow.com for more open-source projects.        //
//                                                                            //
//                                  Enjoy :)                                  //
//----------------------------------------------------------------------------//
#region Usings
//System
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
//XNA
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class ResourcesManager
    {
        #region iVars
        ContentManager _contentManager;

        Dictionary<String, Texture2D>  _texturesDict;
        Dictionary<String, SpriteFont> _fontsDict;
        #endregion


        #region Singleton
        private static ResourcesManager s_instance;
        public static ResourcesManager Instance
        {
            get
            {
                if(s_instance == null)
                    s_instance = new ResourcesManager();
                return s_instance;
            }
        }
        #endregion //Singleton


        #region Static Methods 
        public static String FindContentDirectoryPath()
        {
            //Init the search paths.
            var pathsList = new List<String> () {
                Path.Combine(Environment.CurrentDirectory, "Content"),
                "/usr/local/share/amazingcow_game_bow_and_arrow/Content"
            };

            //Select the first avaiable path.
            String selectedSearchPath = null;
            foreach(var searchPath in pathsList)
            {
                if(Directory.Exists(searchPath))
                {
                    selectedSearchPath = searchPath;
                    Debug.WriteLine("Select Asset Search Path: " + searchPath);

                    break;
                }
            }

            //COWTODO: This is NOT portable, but i guess     \
            //that we aren't target the mobile right now, so \
            //this is the easy fix :S
            if(selectedSearchPath == null)
            {
                System.Windows.Forms.MessageBox.Show(
                    "Cannot find the assets folder - Sorry :(",
                    GameManager.kGameName
                );

                Environment.Exit(1);
            }

            return selectedSearchPath;
        }
        #endregion //Static Methods 


        #region CTOR
        private ResourcesManager()
        {
            //Empty...
            _contentManager = GameManager.Instance.Content;

            _texturesDict = new Dictionary<String, Texture2D >();
            _fontsDict    = new Dictionary<String, SpriteFont>();
        }
        #endregion //CTOR


        #region Public Methods
        public Texture2D GetTexture(String name)
        {
            if(_texturesDict.ContainsKey(name))
                return _texturesDict[name];

            var texture = _contentManager.Load<Texture2D>("sprites/" + name);
            _texturesDict.Add(name, texture);

            return texture;
        }

        public SpriteFont GetFont(String name)
        {
            if(_fontsDict.ContainsKey(name))
                return _fontsDict[name];

            var font = _contentManager.Load<SpriteFont>("fonts/" + name);
            _fontsDict.Add(name, font);

            return font;
        }

        #endregion //Public Methods
    }
}

