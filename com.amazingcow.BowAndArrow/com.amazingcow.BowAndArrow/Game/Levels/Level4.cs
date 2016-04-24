#region Usings
//System
using System;
//XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion //Usings


namespace cow.amazingcow.BowAndArrow
{
    public class Level4 : Level
    {
        #region Constants
        //COWTODO: Check the correct values.
        private const int kMaxSlimesCount = 15;
        #endregion //Constants

        #region CTOR
        public Level4()
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
            int minSlimeY = Slime.kSlimeHeight;
            int maxSlimeY = viewport.Height - Slime.kSlimeHeight;
            //Makes the enemies came from right of screen.
            int minSlimeX = viewport.Width;
            int maxSlimeX = 2 * viewport.Width;

            for(int i = 0; i < kMaxSlimesCount; ++i)
            {
                var x = rndGen.Next(minSlimeX, maxSlimeX);
                var y = rndGen.Next(minSlimeY, maxSlimeY);

                var slime = new Slime(new Vector2(x, y));
                slime.OnStateChangeDead  += OnEnemyStateChangeDead;
                slime.OnStateChangeDying += OnEnemyStateChangeDying;

                Enemies.Add(slime);
            }

            AliveEnemies = kMaxSlimesCount;
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

