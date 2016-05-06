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
        #endregion //Constants


        #region Public Properties 
        public override String PaperStringIntro 
        { get { return kPaperIntroString; } }

        public override String PaperStringGameOver
        { get { return kPaperGameOverString; } }

        public override String LevelTitle        
        { get { return kLevelTitle; } }
        #endregion //Public Properties 


        #region Init
        protected override void InitEnemies()
        {
            var viewport = GameManager.Instance.GraphicsDevice.Viewport;

            //Initialize the Enemies.
            int startX = viewport.Width - (Balloon.kWidth) - 20; //Just little to left.
            int startY = viewport.Height + 10; //Just little off of screen

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
        const String kPaperIntroString = @"
- Bow and Arrow -
Amazing Cow Remake

This a remake of the old
Windows 3.1 Bow & Arrow game.

It was originally published by
John di Troia.

This version is free sofware (GPLv3)
You can study, hack and share it ;D

Amazing Cow - 2016
www.amazingcow.com

Enjoy...";

        //GameOver
        const String kPaperGameOverString = @"
GameOver
";

        //Title
        const String kLevelTitle = "Level 1 - Training";
        #endregion //Paper Strings

    }//class Level1
}//namespace com.amazingcow.BowAndArrow
