//----------------------------------------------------------------------------//
//               █      █                                                     //
//               ████████                                                     //
//             ██        ██                                                   //
//            ███  █  █  ███        GameObject.cs                             //
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
//XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public abstract class GameObject
    {
        #region Constants
        public const int kAliveTexturesListHintSize = 2;
        public const int kDyingTexturesListHintSize = 1;
        #endregion //Constants


        #region Events
        public event EventHandler<EventArgs> OnStateChangeDying;
        public event EventHandler<EventArgs> OnStateChangeDead;
        #endregion //Events


        #region Enums
        public enum State
        {
            Alive,
            Dying,
            Dead
        }
        #endregion //Enums


        #region iVars
        State _currentState;
        #endregion //iVars


        #region Public Properties
        public Vector2 Position
        { get; protected set; }

        public Vector2 Speed
        { get; protected set; }

        public virtual Rectangle BoundingBox
        {
            get {
                return new Rectangle((int)Position.X,
                                     (int)Position.Y,
                                     CurrentTexture.Width,
                                     CurrentTexture.Height);
            }
        }

        public virtual Rectangle HitBox
        {
            get { return BoundingBox; }
        }

        public State CurrentState
        {
            get { return _currentState; }
            protected set { ChangeState(value); }
        }

        public List<Texture2D> AliveTexturesList
        { get; protected set; }

        public List<Texture2D> DyingTexturesList
        { get; protected set; }

        public Texture2D CurrentTexture
        {
            get { return CurrentTexturesList[CurrentTextureIndex]; }
        }

        public List<Texture2D> CurrentTexturesList
        { get; protected set; }

        public int CurrentTextureIndex
        { get; protected set; }

        public Clock TextureAnimationTimer
        { get; protected set; }


        public Color TintColor
        { get; protected set; }
        #endregion //Public Properties


        #region CTOR
        protected GameObject(Vector2 position, Vector2 speed,
                             float msToChangeTexture)
        {
            //House keeping
            Position     = position;
            Speed        = speed;
            CurrentState = State.Alive;
            TintColor    = Color.White;

            //Textures
            AliveTexturesList     = new List<Texture2D>(kAliveTexturesListHintSize);
            DyingTexturesList     = new List<Texture2D>(kDyingTexturesListHintSize);
            CurrentTexturesList   = AliveTexturesList;
            CurrentTextureIndex   = 0;

            //Animation.
            TextureAnimationTimer = new Clock(msToChangeTexture,
                                              Clock.kRepeatForever);
            TextureAnimationTimer.OnTick += OnTextureAnimationTimerTick;
            TextureAnimationTimer.Start();
        }
        #endregion //CTOR


        #region Update / Draw
        public virtual void Update(GameTime gt)
        {
            TextureAnimationTimer.Update(gt.ElapsedGameTime.Milliseconds);
        }
        public virtual void Draw(GameTime gt)
        {
            if(CurrentState == State.Dead)
                return;

            GameManager.Instance.CurrentSpriteBatch.Draw(
                CurrentTexture,
                Position,
                TintColor);
        }
        #endregion //Update / Draw


        #region Public Methods
        public abstract void Kill();
        #endregion //Public Methods


        #region Private Methods
        void ChangeState(State state)
        {
            _currentState = state;

            if(_currentState == State.Dead)
            {
                OnStateChangeDead(this, EventArgs.Empty);
            }
            else if(_currentState == State.Dying)
            {
                OnStateChangeDying(this, EventArgs.Empty);
                CurrentTexturesList = DyingTexturesList;
                CurrentTextureIndex = 0;
            }
        }

        void OnTextureAnimationTimerTick(object sender, EventArgs e)
        {
            int count = CurrentTexturesList.Count;
            CurrentTextureIndex = (CurrentTextureIndex + 1) % count;
        }
        #endregion //Private Methods

    }//class GameObject
}//namespace com.amazingcow.BowAndArrow

