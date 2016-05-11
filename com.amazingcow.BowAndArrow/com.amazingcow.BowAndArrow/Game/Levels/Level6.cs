#region Usings
//System
using System;
//XNA
using Microsoft.Xna.Framework;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Level6 : Level
    {
        #region Constants
        const int kMaxFireballsCount = 15;
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
            int minFireballY = PlayField.Top    - Fireball.kHeight;
            int maxFireballY = PlayField.Bottom - Fireball.kHeight;

            //Makes the enemies came from right of screen.
            int minFireballX = PlayField.Right;
            int maxFireballX = 2 * PlayField.Right;


            for(int i = 0; i < kMaxFireballsCount; ++i)
            {
                var x = rndGen.Next(minFireballX, maxFireballX);
                var y = rndGen.Next(minFireballY, maxFireballY);

                var fireball = new Fireball(new Vector2(x, y));
                fireball.OnStateChangeDead  += OnEnemyStateChangeDead;
                fireball.OnStateChangeDying += OnEnemyStateChangeDying;

                Enemies.Add(fireball);
            }

            AliveEnemies = kMaxFireballsCount;
        }
        #endregion //Init


        #region Helper Methods
        protected override void LevelCompleted()
        {
            GameManager.Instance.ChangeLevel(new Level7());
        }
        #endregion


        #region Paper Strings
        //Intro 
        const String kPaperIntroString = @"
Let's heat this a bit..
Don't get burned.
--- 

We're sure that you like your hair.
We like ours too ;D

So let's share it with people
of Instituto Mario Penna.

They accept hair donation to
people that lost it in chemo!

See you at baber shop.
";

        //Game Over
        const String kPaperGameOverString = @"
Game over.
";
        //Title
        const String kLevelTitle        = @"Level 6";
        const String kLevelDescription  = "FIREBALLS";
        #endregion

    }//class Level6
}//namespace com.amazingcow.BowAndArrow

