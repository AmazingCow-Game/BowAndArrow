#region Usings
//System
using System;
//XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Level3 : Level
    {
        #region Constants
        //COWTODO: Check the correct values.
        private const int kMaxButterfliesCount = 15;
        #endregion //Constants

        #region CTOR
        public Level3()
            : base()
        {
        }
        #endregion //CTOR


        #region Init
        protected override void InitEnemies()
        {
            var viewport = GameManager.Instance.GraphicsDevice.Viewport;
            var rndGen   = GameManager.Instance.RandomNumGen;

            //Initialize the Enemies.
            int minButterflyX = viewport.Width  / 2;
            int maxButterflyX = viewport.Width - Butterfly.kButterflyWidth;
            //Makes the enemies came from bottom of screen.
            int minButterflyY = viewport.Height;
            int maxButterflyY = 2 * viewport.Height;

            for(int i = 0; i < kMaxButterfliesCount; ++i)
            {
                var x = rndGen.Next(minButterflyX, maxButterflyX);
                var y = rndGen.Next(minButterflyY, maxButterflyY);

                var butterfly = new Butterfly(new Vector2(x, y));
                butterfly.OnStateChangeDead  += OnEnemyStateChangeDead;
                butterfly.OnStateChangeDying += OnEnemyStateChangeDying;

                Enemies.Add(butterfly);
            }

            AliveEnemies = kMaxButterfliesCount;
        }

        protected override void InitPapers()
        {
            Papers.Add(new Paper("Intro", ""));
            Papers.Add(new Paper("Paused", ""));
            Papers.Add(new Paper("GameOver", ""));
        }
        #endregion //Init

        #region Helper Methods
        protected override void LevelCompleted()
        {
            GameManager.Instance.ChangeLevel(new Level1());
        }
        #endregion

    }//class Level2
}//namespace com.amazingcow.BowAndArrow

