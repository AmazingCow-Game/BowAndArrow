#region Usings
//System
using System;
//XNA
using Microsoft.Xna.Framework;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Level8 : Level
    {
        #region Constants
        //COWTODO: Check the correct values.
        const int kMaxVulturesCount = 15;
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
            int minVultureY = PlayField.Top    - Vulture.kVultureHeight;
            int maxVultureY = PlayField.Bottom - Vulture.kVultureHeight;
            //Makes the enemies came from right of screen.
            int minVultureX = PlayField.Right;
            int maxVultureX = 2 * PlayField.Right;

            for(int i = 0; i < kMaxVulturesCount; ++i)
            {
                var x = rndGen.Next(minVultureX, maxVultureX);
                var y = rndGen.Next(minVultureY, maxVultureY);

                var vulture = new Vulture(new Vector2(x, y));
                vulture.OnStateChangeDead  += OnEnemyStateChangeDead;
                vulture.OnStateChangeDying += OnEnemyStateChangeDying;

                Enemies.Add(vulture);
            }

            AliveEnemies = kMaxVulturesCount;
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
        const String kLevelTitle = "Level 8";
        #endregion

    }//class Level8
}//namespace com.amazingcow.BowAndArrow

