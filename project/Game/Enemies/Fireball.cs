//----------------------------------------------------------------------------//
//               █      █                                                     //
//               ████████                                                     //
//             ██        ██                                                   //
//            ███  █  █  ███        Fireball.cs                               //
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
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Fireball : Enemy
    {
        #region Constants
        //Public
        public const int kHeight     = 49;
        public const int kWidth      = 39;
        public const int kScoreValue = 600;
        //Private
        const int kSpeedMin = 120;
        const int kSpeedMax = 180;
        #endregion


        #region Public Properties
        public override int ScoreValue { get { return kScoreValue; } }
        #endregion


        #region iVars
        Clock _dyingClock;
        #endregion //iVars


        #region CTOR
        public Fireball(Vector2 position)
            : base(position, Vector2.Zero, 100)
        {
            //Initialize the textures...
            var resMgr = ResourcesManager.Instance;
            AliveTexturesList.Add(resMgr.GetTexture("fire1"));
            AliveTexturesList.Add(resMgr.GetTexture("fire2"));
            DyingTexturesList.Add(resMgr.GetTexture("fire_dead"));

            //Init the Speed...
            int xSpeed = GameManager.Instance.RandomNumGen.Next(kSpeedMin,
                                                                kSpeedMax);
            Speed = new Vector2(-xSpeed, 0);

            //Init the timers...
            _dyingClock = new Clock(500, 1);
            _dyingClock.OnTick += OnDyingClockTick;
        }
        #endregion //CTOR


        #region Update / Draw
        public override void Update(GameTime gt)
        {
            //Fireball is already dead - Don't need to do anything else.
            if(CurrentState == State.Dead)
                return;

            base.Update(gt);

            _dyingClock.Update(gt.ElapsedGameTime.Milliseconds);

            //Just move in alive - Dying it will only glow.
            if(CurrentState == State.Alive)
                MoveAlive(gt);
        }
        #endregion //Update / Draw


        #region Public Methods
        public override void Kill()
        {
            //Already Dead - Don't do anything else...
            if(CurrentState != State.Alive)
                return;

            CurrentState  = State.Dying;
            _dyingClock.Start();

            Speed = Vector2.Zero;
        }

        public override bool CheckCollisionPlayer(Archer archer)
        {
            if(CurrentState != State.Alive)
                return false;

            return archer.BoundingBox.Intersects(this.BoundingBox);
        }
        #endregion //Public Methods


        #region Private Methods
        void MoveAlive(GameTime gt)
        {
            //Update the position.
            Position += (Speed * (gt.ElapsedGameTime.Milliseconds / 1000f));

            var bounds = GameManager.Instance.CurrentLevel.PlayField;
            if(BoundingBox.Right <  bounds.Left)
                ResetPosition();
        }
        #endregion //Private Methods


        #region Timers Callbacks
        void OnDyingClockTick(object sender, EventArgs e)
        {
            CurrentState = State.Dead;
        }
        #endregion //Timers Callbacks


        #region Helper Methods
        void ResetPosition()
        {
            var bounds = GameManager.Instance.CurrentLevel.PlayField;
            var rnd    = GameManager.Instance.RandomNumGen;

            var x = rnd.Next(bounds.Right, bounds.Right * 2);
            var y = rnd.Next(bounds.Top    + BoundingBox.Height,
                             bounds.Bottom - BoundingBox.Height);

            Position = new Vector2(x, y);
        }
        #endregion //Helper Methods

    }//Class Fireball
}//namespace com.amazingcow.BowAndArrow

