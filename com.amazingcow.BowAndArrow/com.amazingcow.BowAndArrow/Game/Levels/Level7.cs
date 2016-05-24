//----------------------------------------------------------------------------//
//               █      █                                                     //
//               ████████                                                     //
//             ██        ██                                                   //
//            ███  █  █  ███        Level7.cs                                 //
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
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Level7 : Level
    {
        #region Constants
        const int kMaxWindsCount = 15;
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
            int minWindY = PlayField.Top    - Wind.kHeight;
            int maxWindY = PlayField.Bottom - Wind.kHeight;

            //Makes the enemies came from right of screen.
            int minWindX = PlayField.Right;
            int maxWindX = 3 * PlayField.Right;

            for(int i = 0; i < kMaxWindsCount; ++i)
            {
                var x = rndGen.Next(minWindX, maxWindX);
                var y = rndGen.Next(minWindY, maxWindY);

                var wind = new Wind(new Vector2(x, y));
                wind.OnStateChangeDead  += OnEnemyStateChangeDead;
                wind.OnStateChangeDying += OnEnemyStateChangeDying;

                Enemies.Add(wind);
            }

            AliveEnemies = kMaxWindsCount;
        }
        #endregion //Init


        #region Helper Methods
        protected override void LevelCompleted()
        {
            GameManager.Instance.ChangeLevel(new Level8());
        }
        #endregion


        #region Paper Strings
        //Intro
        const String kPaperIntroString = @"
Beware with the wind!
It could cut your head off!
---

Instituto Mario Penna accepts
medice donation too.

That's nice hun?

Check out ther site ;D

http://www.mariopenna.org.br
";

        //Game Over
        const String kPaperGameOverString = @"
Game over.
";
        //Title
        const String kLevelTitle        = "Level 7";
        const String kLevelDescription  = "Wind";
        #endregion

    }//class Level7
}//namespace com.amazingcow.BowAndArrow

