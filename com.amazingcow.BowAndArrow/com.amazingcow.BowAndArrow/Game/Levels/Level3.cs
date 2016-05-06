#region Usings
//System
using System;
//XNA
using Microsoft.Xna.Framework;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Level3 : Level
    {
        #region Constants
        const int kMaxButterfliesCount = 15;
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
            int minX = PlayField.Center.X - 100;
            int maxX = PlayField.Right - Butterfly.kWidth;
            //Makes the enemies came from bottom of screen.
            int minY = PlayField.Bottom;
            int maxY = 2 * PlayField.Bottom;

            for(int i = 0; i < kMaxButterfliesCount; ++i)
            {
                var x = rndGen.Next(minX, maxX);
                var y = rndGen.Next(minY, maxY);

                var butterfly = new Butterfly(new Vector2(x, y));
                butterfly.OnStateChangeDead  += OnEnemyStateChangeDead;
                butterfly.OnStateChangeDying += OnEnemyStateChangeDying;

                Enemies.Add(butterfly);
            }

            AliveEnemies = kMaxButterfliesCount;
        }
        #endregion //Init


        #region Helper Methods
        protected override void LevelCompleted()
        {
            GameManager.Instance.ChangeLevel(new Level4());
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
        const String kLevelTitle = "Level 3";
        #endregion

    }//class Level3
}//namespace com.amazingcow.BowAndArrow

