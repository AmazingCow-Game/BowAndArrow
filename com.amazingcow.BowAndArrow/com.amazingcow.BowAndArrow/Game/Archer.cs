//----------------------------------------------------------------------------//
//               █      █                                                     //
//               ████████                                                     //
//             ██        ██                                                   //
//            ███  █  █  ███        Archer.cs                                 //
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
//Xna
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Archer : GameObject
    {
        #region Events
        public event EventHandler<EventArgs> OnArcherShootArrow;
        #endregion //Events


        #region Enums
        public enum BowState
        {
            Stand,  //Bow has an arrow but is not pulled.
            Armed,  //Bow has an arrow AND is pulled.
            Unarmed //Bow hasn't an arrow.
        }
        #endregion //Enums


        #region Public Constants
        //Public
        public const int kMaxArrowsCount = 15;
        public const int kMaxEnemyHits   = 5;
        //Private
        const int kSpeed          = 200;
        const int kMouseThreshold = 20;
        #endregion //Public Constants


        #region Private Constants
        //Timer
        const int kChangeBowStateInterval = 200;
        const int kHitGlowInterval        = 50;
        const int kHitGlowRepeatCount     = 8;
        //Arrow
        const int kArrowPositionOffsetX = 47;
        const int kArrowPositionOffsetY = 41;
        #endregion //Constants


        #region Public Properties
        public int ArrowsCount
        { get; set; }

        public Vector2 ArrowPosition
        {
            get {
                return new Vector2(Position.X + kArrowPositionOffsetX,
                                   Position.Y + kArrowPositionOffsetY);
            }
        }

        public BowState CurrentBowState
        { get; private set; }

        public int EnemyHits
        { get; private set; }
        #endregion //Public Properties


        #region iVars
        readonly Clock _changeBowStateClock;
        readonly Clock _hitGlowClock;
        #endregion //iVars


        #region CTOR
        public Archer(Vector2 position) :
            base(position, Vector2.Zero, 0)
        {
            //Init the textures.
            var resMgr = ResourcesManager.Instance;

            AliveTexturesList.Add(resMgr.GetTexture("hero_stand"));
            AliveTexturesList.Add(resMgr.GetTexture("hero_armed"));
            AliveTexturesList.Add(resMgr.GetTexture("hero_without_arrow"));

            DyingTexturesList = AliveTexturesList;

            //Init the Properties.
            ArrowsCount     = kMaxArrowsCount;
            CurrentBowState = BowState.Stand;
            EnemyHits       = 0;

            //Init the timers.
            _changeBowStateClock = new Clock(kChangeBowStateInterval, 1);
            _changeBowStateClock.OnTick += OnChangeStateClockTick;

            _hitGlowClock = new Clock(kHitGlowInterval, kHitGlowRepeatCount);
            _hitGlowClock.OnTick += OnHitGlowClockTick;
        }
        #endregion //CTOR


        #region Update / Draw
        public override void Update(GameTime gt)
        {
            //Archer is already dead - Don't need to do anything else.
            if(CurrentState == State.Dead)
                return;

            //Update the timers.
            _changeBowStateClock.Update(gt.ElapsedGameTime.Milliseconds);
            _hitGlowClock.Update(gt.ElapsedGameTime.Milliseconds);

            CheckTryChangeBowState();
            CheckMovementSpeed();

            //Update the position.
            Position += (Speed * (gt.ElapsedGameTime.Milliseconds / 1000f));
        }
        #endregion //Update / Draw


        #region Public Methods
        public override void Kill()
        {
            //Already Dead - Don't do anything else...
            if(CurrentState != State.Alive)
                return;

            CurrentState = State.Dead;
        }

        public void Hit()
        {
            ++EnemyHits;

            //Reset the Glow Timer.
            _hitGlowClock.Stop();
            _hitGlowClock.Start();

            //Make glows imediatelly.
            OnHitGlowClockTick(null, null);

            if(EnemyHits == kMaxEnemyHits)
                Kill();
        }
        #endregion //Public Methods


        #region Helper Methods
        //BowState.
        void CheckTryChangeBowState()
        {
            if(!CheckTryChangeBowStateKeyboard())
                CheckTryChangeBowStateMouse();
        }
        bool CheckTryChangeBowStateKeyboard ()
        {
            var curr = InputHandler.Instance.CurrentKeyboardState;
            var prev = InputHandler.Instance.PreviousKeyboardState;
            var usedKeyboard = false;

            //Change the Bow State.
            if(curr.IsKeyDown(Keys.LeftAlt) && prev.IsKeyUp(Keys.LeftAlt))
            {
                TryChangeBowState(BowState.Stand);
                usedKeyboard = true;
            }
            else if(curr.IsKeyUp(Keys.LeftControl) && prev.IsKeyDown(Keys.LeftControl))
            {
                TryChangeBowState(BowState.Unarmed);
                usedKeyboard = true;
            }
            else if(curr.IsKeyDown(Keys.LeftControl))
            {
                TryChangeBowState(BowState.Armed);
                usedKeyboard = true;
            }

            return usedKeyboard;
        }
        void CheckTryChangeBowStateMouse()
        {
            var mouseState = InputHandler.Instance.CurrentMouseState;

            //Change the Bow State.
            if(mouseState.RightButton == ButtonState.Pressed)
                TryChangeBowState(BowState.Stand);
            else if(mouseState.LeftButton == ButtonState.Released)
                TryChangeBowState(BowState.Unarmed);
            else if(mouseState.LeftButton == ButtonState.Pressed)
                TryChangeBowState(BowState.Armed);
        }
        void TryChangeBowState(BowState targetBowState)
        {
            if(_changeBowStateClock.IsEnabled)
                return;

            if(CurrentState == State.Dying)
                return;

            // Armed -> Unarmed
            if((CurrentBowState == BowState.Armed &&
                targetBowState  == BowState.Unarmed))
            {
                Shoot();
            }

            // Stand -> Armed
            else if(CurrentBowState == BowState.Stand &&
                    targetBowState  == BowState.Armed)
            {
                Arm();
            }

            // Unarmed -> Stand
            else if(CurrentBowState == BowState.Unarmed &&
                    targetBowState  == BowState.Stand)
            {
                Stand();
            }
        }

        //Bow Actions.
        void Shoot()
        {
            if(ArrowsCount > 0)
            {
                --ArrowsCount;
                OnArcherShootArrow(this, EventArgs.Empty);

                if(ArrowsCount <= 0)
                {
                    CurrentState = State.Dying;
                }
            }

            CurrentBowState     = BowState.Unarmed;
            CurrentTextureIndex = (int)CurrentBowState;

            _changeBowStateClock.Start();
        }
        void Arm()
        {
            CurrentBowState     = BowState.Armed;
            CurrentTextureIndex = (int)CurrentBowState;

            _changeBowStateClock.Start();
        }
        void Stand()
        {
            CurrentBowState     = BowState.Stand;
            CurrentTextureIndex = (int)CurrentBowState;

            _changeBowStateClock.Start();
        }


        //Movement.
        void CheckMovementSpeed()
        {
            Speed = Vector2.Zero;
            if(!CheckMovementSpeedKeyboard())
                CheckMovementSpeedMouse();
        }
        bool CheckMovementSpeedKeyboard()
        {
            var keyboardState = InputHandler.Instance.CurrentKeyboardState;
            var lvl           = GameManager.Instance.CurrentLevel;
            var usedKeyboard  = false;

            if(keyboardState.IsKeyDown(Keys.Up) &&
               BoundingBox.Top > lvl.PlayField.Top)
            {
                Speed = new Vector2(0, -kSpeed);
                usedKeyboard = true;
            }
            if(keyboardState.IsKeyDown(Keys.Down) &&
               BoundingBox.Bottom < lvl.PlayField.Bottom)
            {
                Speed = new Vector2(0, kSpeed);
                usedKeyboard = true;
            }

            return usedKeyboard;
        }
        void CheckMovementSpeedMouse()
        {
            var mouseState = InputHandler.Instance.CurrentMouseState;

            //Archer only move when button is pressed.
            if(mouseState.LeftButton != ButtonState.Pressed)
                return;

            var mouseY = mouseState.Y;
            var lvl    = GameManager.Instance.CurrentLevel;

            if(mouseY - kMouseThreshold < BoundingBox.Top &&
               BoundingBox.Top > lvl.PlayField.Top)
            {
                Speed = new Vector2(0, -kSpeed);
            }
            if(mouseY + kMouseThreshold > BoundingBox.Bottom &&
               BoundingBox.Bottom < lvl.PlayField.Bottom)
            {
                Speed = new Vector2(0, kSpeed);
            }
        }
        #endregion //Helper Methods


        #region Timers Callbacks
        void OnChangeStateClockTick(object sender, EventArgs e)
        {
            _changeBowStateClock.Stop();
        }

        void OnHitGlowClockTick(object sender, EventArgs e)
        {
            if(_hitGlowClock.TickCount == kHitGlowRepeatCount)
            {
                TintColor = Color.White;
                _hitGlowClock.Stop();
            }
            else
            {
                TintColor = (_hitGlowClock.TickCount % 2 == 0)
                             ? Color.Red
                             : Color.White;
            }
        }
        #endregion //Timers Callbacks
    }
}

