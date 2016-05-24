//----------------------------------------------------------------------------//
//               █      █                                                     //
//               ████████                                                     //
//             ██        ██                                                   //
//            ███  █  █  ███        ButterFly.cs                              //
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
    public class Butterfly : Enemy
    {
        #region Constants
        //Public
        public const int kWidth      = 32;
        public const int kScoreValue = 200;
        //Private
        const int kSpeedMin = 35;
        const int kSpeedMax = 45;
        #endregion


        #region Public Properties
        public override int ScoreValue { get { return kScoreValue; } }
        #endregion


        #region CTOR
        public Butterfly(Vector2 position)
            : base(position, Vector2.Zero, 0)
        {
            //Initialize the textures...
            var resMgr = ResourcesManager.Instance;
            AliveTexturesList.Add(resMgr.GetTexture("butterfly_bubled"));
            DyingTexturesList.Add(resMgr.GetTexture("butterfly"));

            //Init the Speed...
            int ySpeed = GameManager.Instance.RandomNumGen.Next(kSpeedMin,
                                                                kSpeedMax);
            Speed = new Vector2(0, -ySpeed);
        }
        #endregion //CTOR


        #region Update / Draw
        public override void Update(GameTime gt)
        {
            //Butterfly is already dead - Don't need to do anything else.
            if(CurrentState == State.Dead)
                return;

            if(CurrentState == State.Dying)
                MoveDying(gt);
            else
                MoveAlive(gt);
        }
        #endregion //Update / Draw


        #region Public Methods
        public override void Kill()
        {
            //Already Dead - Don't do anything else...
            if(CurrentState != State.Alive)
                return;

            //Set the state and the sprite for dying
            //and make the balloon fall with more speed.
            CurrentState  = State.Dying;

            var rndGen = GameManager.Instance.RandomNumGen;

            //Makes go towards the Top Left of screen.
            Speed = new Vector2(-rndGen.Next(kSpeedMin, kSpeedMax),
                                -rndGen.Next(kSpeedMin, kSpeedMax));
        }
        #endregion //Public Methods


        #region Private Methods
        private void MoveDying(GameTime gt)
        {
            //Update the position.
            Position += (Speed * (gt.ElapsedGameTime.Milliseconds / 1000f));

            //It got out of upper bound of screen.
            if(BoundingBox.Bottom <= 0)
                CurrentState = State.Dead;
        }

        void MoveAlive(GameTime gt)
        {
            //Update the position.
            Position += (Speed * (gt.ElapsedGameTime.Milliseconds / 1000f));

            var lvl = GameManager.Instance.CurrentLevel;
            if(BoundingBox.Bottom <= lvl.PlayField.Top)
                Position = new Vector2(Position.X, lvl.PlayField.Bottom);
        }
        #endregion //Private Methods

    }//Class Butterfly
}//namespace com.amazingcow.BowAndArrow

