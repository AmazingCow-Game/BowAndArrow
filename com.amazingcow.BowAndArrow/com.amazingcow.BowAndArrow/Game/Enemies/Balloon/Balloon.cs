//----------------------------------------------------------------------------//
//               █      █                                                     //
//               ████████                                                     //
//             ██        ██                                                   //
//            ███  █  █  ███        Balloon.cs                                //
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
//Xna
using Microsoft.Xna.Framework;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public abstract class Balloon : Enemy
    {
        #region Constants
        //Public
        public const int kWidth  = 25;
        public const int kHeight = 39;
        //Private
        const int kStringHeight = 12;
        #endregion

        #region Public Properties
        public override Rectangle HitBox
        {
            get {
                return new Rectangle(BoundingBox.X,
                                     BoundingBox.Y,
                                     BoundingBox.Width,
                                     BoundingBox.Height - kStringHeight);
            }
        }
        #endregion //Public Properties


        #region CTOR
        protected Balloon(Vector2 position, Vector2 speed)
            : base(position, speed, 0)
        {
            //Empty...
        }
        #endregion //CTOR


        #region Update / Draw
        public override void Update(GameTime gt)
        {
            //Balloon is already dead - Don't need to do anything else.
            if(CurrentState == State.Dead)
                return;

            //Update the position.
            Position += (Speed * (gt.ElapsedGameTime.Milliseconds / 1000f));

            var lvl = GameManager.Instance.CurrentLevel;

            //On State.Alive -> If Balloon goes up of top of window
            //reset it to the bottom of the window.
            if(CurrentState == State.Alive &&
               BoundingBox.Bottom <= lvl.PlayField.Top)
            {
                Position = new Vector2(Position.X, lvl.PlayField.Bottom);
            }

            //On State.Dying -> If Balloon goes down of the window's
            //bottom, set the state to died.
            else if(CurrentState == State.Dying &&
                    Position.Y >= lvl.PlayField.Bottom)
            {
                Speed        = Vector2.Zero;
                CurrentState = State.Dead;
            }
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

            Speed *= -1.2f;
        }
        #endregion //Public Methods

    }//Class Balloon
}//namespace com.amazingcow.BowAndArrow

