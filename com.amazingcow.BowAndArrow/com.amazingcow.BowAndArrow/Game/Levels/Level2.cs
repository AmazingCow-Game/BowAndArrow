#region Usings
//System
using System;
//XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Level2 : Level
    {
        #region Constants
        //COWTODO: Check the correct values.
        private const int kMaxYellowBalloonsCount = 5;
        private const int kMaxRedBalloonsCount    = 15;
        #endregion //Constants

        #region CTOR
        public Level2()
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
            int minBalloonX = viewport.Width  / 2;
            int maxBalloonX = viewport.Width - Balloon.kBalloonWidth;
            //Makes the enemies came from bottom of screen.
            int minBalloonY = viewport.Height;
            int maxBalloonY = 2 * viewport.Height;

            //RedBalloons.
            for(int i = 0; i < kMaxRedBalloonsCount; ++i)
            {
                var x = rndGen.Next(minBalloonX, maxBalloonX);
                var y = rndGen.Next(minBalloonY, maxBalloonY);

                var balloon = new RedBalloon(new Vector2(x, y));
                balloon.OnStateChangeDead  += OnEnemyStateChangeDead;
                balloon.OnStateChangeDying += OnEnemyStateChangeDying;

                Enemies.Add(balloon);
            }

            ///YellowBalloons.
            for(int i = 0; i < kMaxRedBalloonsCount; ++i)
            {
                var x = rndGen.Next(minBalloonX, maxBalloonX);
                var y = rndGen.Next(minBalloonY, maxBalloonY);

                var balloon = new YellowBalloon(new Vector2(x, y));
                balloon.OnStateChangeDead  += OnEnemyStateChangeDead;
                balloon.OnStateChangeDying += OnEnemyStateChangeDying;

                Enemies.Add(balloon);
            }

            AliveEnemies = kMaxRedBalloonsCount;
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


        #region Game Objects Callbacks
        protected override void OnEnemyStateChangeDying(object sender, EventArgs e)
        {
            base.OnEnemyStateChangeDying(sender, e);

            //Level 2 is only about RedBalloons.
            if(sender is RedBalloon)
            --AliveEnemies;
        }
        #endregion //Game Objects Callbacks

    }//class Level2
}//namespace com.amazingcow.BowAndArrow

