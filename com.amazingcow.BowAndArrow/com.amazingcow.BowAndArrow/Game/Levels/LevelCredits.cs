//----------------------------------------------------------------------------//
//               █      █                                                     //
//               ████████                                                     //
//             ██        ██                                                   //
//            ███  █  █  ███        LevelCredits.cs                           //
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
    public class LevelCredits : Level
    {
        #region Public Properties
        public override String PaperStringIntro
        { get { return kPaperIntroString; } }

        public override String PaperStringGameOver
        { get { return kPaperGameOverString; } }

        public override String LevelTitle
        { get { return kLevelTitle; } }

        public override String LevelDescription
        { get { return kLevelDescription; } }
        #endregion // Public Properties


        #region Init
        protected override void InitEnemies()
        {
            var pos     = new Vector2(PlayField.Center.X, PlayField.Center.Y);
            var balloon = new RedBalloon(pos);
            balloon.OnStateChangeDead  += OnEnemyStateChangeDead;
            balloon.OnStateChangeDying += OnEnemyStateChangeDying;

            Enemies.Add(balloon);

            AliveEnemies = 1;

            Player.ArrowsCount = 1;
        }
        #endregion //Init


        #region Helper Methods
        protected override void LevelCompleted()
        {
            GameManager.Instance.ChangeLevel(new Level1());
        }
        #endregion // Helper Methods


        #region Paper Strings
        //Intro
        const String kPaperIntroString = @"
We hope that you had
a great time playing this game.

We had making it :D

Dev: N2OMatt
<n2omatt@amazingcow.com>

Amazing Cow - 2016 - GPLv3

Dedicate to all people
fighting against cancer!

Thank you...

PS: You have only 1 arrow...
";

        const String kPaperGameOverString = @"
Oh noooooo....
Just now :/
";
        //Title
        const String kLevelTitle        = "Credits";
        const String kLevelDescription  = "Thank You <3";
        #endregion // Paper Strings

    }//class Level1
}//namespace com.amazingcow.BowAndArrow
