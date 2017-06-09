//----------------------------------------------------------------------------//
//               █      █                                                     //
//               ████████                                                     //
//             ██        ██                                                   //
//            ███  █  █  ███        Level1.cs                                 //
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
    public class Level1 : Level
    {
        #region Constants
        const int kMaxBalloonsCount = 15;
        #endregion // Constants


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
            //Initialize the Enemies.
            int startX = PlayField.Right - (Balloon.kWidth) - 20;
            int startY = PlayField.Bottom + 10;

            //Constructs the balloons from right to left.
            for(int i = 0; i < kMaxBalloonsCount; ++i)
            {
                var x = startX - (Balloon.kWidth * i) - (2 * i); //Litle offset between them.

                var balloon = new RedBalloon(new Vector2(x, startY));
                balloon.OnStateChangeDead  += OnEnemyStateChangeDead;
                balloon.OnStateChangeDying += OnEnemyStateChangeDying;

                Enemies.Add(balloon);
            }

            AliveEnemies = kMaxBalloonsCount;
        }
        #endregion //Init


        #region Helper Methods
        protected override void LevelCompleted()
        {
            GameManager.Instance.ChangeLevel(new Level2());
        }
        #endregion // Helper Methods


        #region Paper Strings
        //Intro
        static readonly String kPaperIntroString = String.Format(@"
- Bow and Arrow -
Amazing Cow Labs' Remake

This a remake of the old
Windows 3.1 Bow & Arrow game.

It was originally published by
John di Troia.

This version is free sofware (GPLv3)
You can study, hack and share it ;D

{0}
www.amazingcow.com

Enjoy...", GameManager.kCopyright);

        //GameOver
        const String kPaperGameOverString = @"
GameOver
";

        //Title
        const String kLevelTitle        = "Level 1";
        const String kLevelDescription  = "Training";
        #endregion // Paper Strings

    }//class Level1
}//namespace com.amazingcow.BowAndArrow
