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
        const int kMaxVulturesCount = 15;
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
            int minY = PlayField.Top    + Vulture.kHeight;
            int maxY = PlayField.Bottom - Vulture.kHeight;

            //Makes the enemies came from right of screen.
            int minX = PlayField.Right;
            int maxX = 3 * PlayField.Right;


            for(int i = 0; i < kMaxVulturesCount; ++i)
            {
                var x = rndGen.Next(minX, maxX);
                var y = rndGen.Next(minY, maxY);

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
            GameManager.Instance.ChangeLevel(new LevelCredits());
        }
        #endregion


        #region Paper Strings
        //Intro
        const String kPaperIntroString = @"
Your journey is ending...
Take this last challenge 
with care.
---

Don't forget, we believe
in you to make this a 
better world.

<3
";

        //Game Over
        const String kPaperGameOverString = @"
Game over.
";
        //Title
        const String kLevelTitle        = "Level 8";
        const String kLevelDescription  = "Vultures";
        #endregion

    }//class Level8
}//namespace com.amazingcow.BowAndArrow

