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



