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
        //COWTODO: Check the correct values.
        const int kMaxWindsCount = 15;
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
            var rndGen = GameManager.Instance.RandomNumGen;

            //Initialize the Enemies.
            int minWindY = PlayField.Top    - Wind.kWindHeight;
            int maxWindY = PlayField.Bottom - Wind.kWindHeight;
            //Makes the enemies came from right of screen.
            int minWindX = PlayField.Right;
            int maxWindX = 2 * PlayField.Right;

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
            GameManager.Instance.ChangeLevel(new Level5());
        }
        #endregion


        #region Paper Strings
        //Intro
        const String kPaperIntroString = @"
You are a good guy
Free the butterflies!";

        //Game Over
        const String kPaperGameOverString = @"
Game over.
";
        //Title
        const String kLevelTitle = "Level 7";
        #endregion

    }//class Level7
}//namespace com.amazingcow.BowAndArrow

