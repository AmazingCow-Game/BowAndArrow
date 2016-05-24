//----------------------------------------------------------------------------//
//               █      █                                                     //
//               ████████                                                     //
//             ██        ██                                                   //
//            ███  █  █  ███        InputHandler.cs                           //
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
using System;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;


namespace com.amazingcow.BowAndArrow
{
    public class InputHandler
    {
        #region Public Properties
        //Mouse
        public MouseState CurrentMouseState  { get; private set; }
        public MouseState PreviousMouseState { get; private set; }
        //Keyboard
        public KeyboardState CurrentKeyboardState  { get; private set; }
        public KeyboardState PreviousKeyboardState { get; private set; }
        #endregion //Public Properties


        #region Singleton
        static InputHandler s_instance = null;
        public static InputHandler Instance
        {
            get {
                if(s_instance == null)
                    s_instance = new InputHandler();
                return s_instance;
            }
        }
        #endregion //Singleton


        #region Private CTOR
        InputHandler()
        {
            CurrentMouseState    = Mouse.GetState();
            CurrentKeyboardState = Keyboard.GetState();
        }
        #endregion Private CTOR


        #region Update
        public void Update()
        {
            PreviousMouseState    = CurrentMouseState;
            PreviousKeyboardState = CurrentKeyboardState;

            CurrentMouseState    = Mouse.GetState();
            CurrentKeyboardState = Keyboard.GetState();
        }
        #endregion //Update

    }//class InputHandler
}//namespace com.amazingcow.BowAndArrow



