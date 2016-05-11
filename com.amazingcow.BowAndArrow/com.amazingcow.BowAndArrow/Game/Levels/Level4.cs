#region Usings
//System
using System;
//XNA
using Microsoft.Xna.Framework;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Level4 : Level
    {
        #region Constants      
        const int kMaxSlimesCount = 15;
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
            int minY = PlayField.Top    + Slime.kHeight;
            int maxY = PlayField.Bottom - Slime.kHeight;
            //Makes the enemies came from right of screen.
            int minX = PlayField.Right;
            int maxX = 3 * PlayField.Right;

            for(int i = 0; i < kMaxSlimesCount; ++i)
            {
                var x = rndGen.Next(minX, maxX);
                var y = rndGen.Next(minY, maxY);

                var slime = new Slime(new Vector2(x, y));
                slime.OnStateChangeDead  += OnEnemyStateChangeDead;
                slime.OnStateChangeDying += OnEnemyStateChangeDying;

                Enemies.Add(slime);
            }

            AliveEnemies = kMaxSlimesCount;
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
The slimes are comming!
Don't let their pass!
---

You can enjoy freely this game.
This means that you can share,
study and hack it anytime and
way that you want.

Check our other free software
projects at:

opensource.amazingcow.com
";

        //Game Over
        const String kPaperGameOverString = @"
Game over.
";
        //Title
        const String kLevelTitle        = @"Level 4";
        const String kLevelDescription  = "SLIMED!";
        #endregion

    }//class Level4
}//namespace com.amazingcow.BowAndArrow

