//----------------------------------------------------------------------------//
//               █      █                                                     //
//               ████████                                                     //
//             ██        ██                                                   //
//            ███  █  █  ███        Level2.cs                                 //
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
//XNA
using Microsoft.Xna.Framework;
using System.Diagnostics;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Level2 : Level
    {
        #region Constants
        //COWTODO: Check the correct values.
        const int kMaxYellowBalloonsCount = 5;
        const int kMaxRedBalloonsCount    = 15;
        #endregion //Constants


        #region Public Properties
        public override String PaperStringIntro
        { get { return kPaperIntroString; } }

        public override String PaperStringGameOver
        { get { return kPaperGameOverString; } }

        public override String LevelTitle
        { get { return kLevelTitle; } }

        public override String LevelDescription
        { get { return kLevelDescription; } }
        #endregion //Public Properties


        #region Init
        protected override void InitEnemies()
        {
            var rndGen = GameManager.Instance.RandomNumGen;

            //Initialize the Enemies.
            int minX = PlayField.Center.X;
            int maxX = PlayField.Right - Balloon.kWidth;
            //Makes the enemies came from bottom of screen.
            int minY = PlayField.Bottom;
            int maxY = 2 * PlayField.Bottom;

            //RedBalloons.
            for(int i = 0; i < kMaxRedBalloonsCount; ++i)
            {
                var x = rndGen.Next(minX, maxX);
                var y = rndGen.Next(minY, maxY);

                var balloon = new RedBalloon(new Vector2(x, y));
                balloon.OnStateChangeDead  += OnEnemyStateChangeDead;
                balloon.OnStateChangeDying += OnEnemyStateChangeDying;

                Enemies.Add(balloon);
            }

            //YellowBalloons.
            for(int i = 0; i < kMaxYellowBalloonsCount; ++i)
            {
                var x = rndGen.Next(minX, maxX);
                var y = rndGen.Next(minY, maxY);

                var balloon = new YellowBalloon(new Vector2(x, y));
                balloon.OnStateChangeDead  += OnEnemyStateChangeDead;
                balloon.OnStateChangeDying += OnEnemyStateChangeDying;

                Enemies.Add(balloon);
            }

            AliveEnemies = kMaxRedBalloonsCount;
        }
        #endregion //Init


        #region Helper Methods
        protected override void LevelCompleted()
        {
            GameManager.Instance.ChangeLevel(new Level3());
        }
        #endregion


        #region Game Objects Callbacks
        protected override void OnEnemyStateChangeDead(object sender, EventArgs e)
        {
            //Level 2 is only about RedBalloons.
            if(sender is RedBalloon)
                --AliveEnemies;

            --DyingEnemies;
        }
        #endregion //Game Objects Callbacks


        #region Paper Strings
        //Intro
        const String kPaperIntroString = @"
Ok, you got the basics
Now let's shot at more balloons
---

Did you know that
Instituto Mario Penna
helps people with cancer?

Why not save a few minutes to
check out their site?

http://www.mariopenna.org.br";

        //GameOver
        const String kPaperGameOverString = @"
Game over.
";
        //Level
        const String kLevelTitle        = "Level 2";
        const String kLevelDescription  = "More balloons";
        #endregion //Paper Strings

    }//class Level2
}//namespace com.amazingcow.BowAndArrow

