//----------------------------------------------------------------------------//
//               █      █                                                     //
//               ████████                                                     //
//             ██        ██                                                   //
//            ███  █  █  ███        BullsEye.cs                               //
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
//Xna
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class BullsEye : Enemy
    {
        #region Constants
        //Public
        public const int kHeight     = 49;
        public const int kScoreValue = 500;
        //Private
        static readonly Vector2 kSpeed = new Vector2(0, 50);
        #endregion


        #region Public Properties
        public override int ScoreValue { get { return kScoreValue; } }

        public override Rectangle HitBox {
            get {
                return new Rectangle(BoundingBox.X,
                                     BoundingBox.Y + 10,
                                     BoundingBox.Width,
                                     14);
            }
        }
        #endregion //Public Properties

        #region iVars
        Texture2D   _arrowTexture;
        List<float> _arrowHitPoints;
        #endregion //iVars


        #region CTOR
        public BullsEye(Vector2 position)
            : base(position, kSpeed, 0)
        {
            //Initialize the textures...
            var resMgr = ResourcesManager.Instance;
            AliveTexturesList.Add(resMgr.GetTexture("bulls_eye"));

            _arrowTexture   = ResourcesManager.Instance.GetTexture("arrow");
            _arrowHitPoints = new List<float>();
        }
        #endregion //CTOR


        #region Update / Draw
        public override void Update(GameTime gt)
        {
            //Update the position.
            Position += (Speed * (gt.ElapsedGameTime.Milliseconds / 1000f));

            var lvl = GameManager.Instance.CurrentLevel;

            //Just reverses the direction if reach the screen border.
            if(BoundingBox.Top <= lvl.PlayField.Top ||
               BoundingBox.Bottom >= lvl.PlayField.Bottom)
            {
                Speed *= -1;
            }
        }

        public override void Draw(GameTime gt)
        {
            base.Draw(gt);

            foreach(var hitpoint in _arrowHitPoints)
            {
                GameManager.Instance.CurrentSpriteBatch.Draw(
                    _arrowTexture,
                    new Vector2(BoundingBox.Left - _arrowTexture.Bounds.Width + 5,
                                BoundingBox.Top + hitpoint)
                );
            }
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

        public override bool CheckCollisionArrow(Arrow arrow)
        {
            if(BoundingBox.Contains(arrow.HeadPoint))
            {
                var hitY = arrow.HeadPoint.Y - BoundingBox.Top;
                _arrowHitPoints.Add(hitY);

                if(HitBox.Contains(arrow.HeadPoint))
                    return true;

                arrow.Kill();
            }
            return false;
        }
        #endregion //Public Methods

    }//Class BullsEye
}//namespace com.amazingcow.BowAndArrow

