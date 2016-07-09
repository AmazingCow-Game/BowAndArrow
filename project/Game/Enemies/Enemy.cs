//----------------------------------------------------------------------------//
//               █      █                                                     //
//               ████████                                                     //
//             ██        ██                                                   //
//            ███  █  █  ███        Enemy.cs                                  //
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
//XNA
using Microsoft.Xna.Framework;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public abstract class Enemy : GameObject
    {
        #region Public Properties
        public abstract int ScoreValue { get; }
        #endregion


        #region CTOR
        protected Enemy(Vector2 position, Vector2 speed,
                        float msToChangeTexture) :
            base(position, speed, msToChangeTexture)
        {
            //Empty...
        }
        #endregion //CTOR


        #region Public Methods
        public virtual bool CheckCollisionPlayer(Archer archer)
        {
            //Assumes that this Enemy cannot collide with player.
            return false;
        }

        public virtual bool CheckCollisionArrow(Arrow arrow)
        {
            //Arrow doesn't hit anything but alive ones.
            if(CurrentState != State.Alive)
                return false;

            return HitBox.Contains(arrow.HeadPoint);
        }
        #endregion //Public Methods

    }//class Enemy
}//namespace com.amazingcow.BowAndArrow

