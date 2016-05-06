﻿#region Usings
//System
using System;
//XNA
using Microsoft.Xna.Framework;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Level5 : Level
    {

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

            //Initialize the enemy.
            int startX = 200;//viewport.Width - 100;  //A bit of offset to left.
            int startY = (viewport.Height / 2) - BullsEye.kHeight / 2; //Center.

            var bullsEye = new BullsEye(new Vector2(startX, startY));
            bullsEye.OnStateChangeDead  += OnEnemyStateChangeDead;
            bullsEye.OnStateChangeDying += OnEnemyStateChangeDying;

            Enemies.Add(bullsEye);

            AliveEnemies = 1;
        }
        #endregion //Init


        #region Helper Methods
        protected override void LevelCompleted()
        {
            GameManager.Instance.ChangeLevel(new Level6());
        }
        #endregion


        #region Paper Strings
        //Intro 
        const String kPaperIntroString = @"
The slimes are comming!
Don't let their pass!";

        //Game Over
        const String kPaperGameOverString = @"
Game over.
";
        //Title
        const String kLevelTitle = @"Level 5";
        #endregion

    }//class Level2
}//namespace com.amazingcow.BowAndArrow

