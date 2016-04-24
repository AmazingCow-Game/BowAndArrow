#region Usings
//System
using System;
using System.Diagnostics;
//Xna
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Level1 : Level
    {
        #region Constants
        //COWTODO: Check the correct values.
        private const int kMaxBalloonsCount = 15;
        #endregion //Constants


        #region CTOR
        public Level1() :
            base()
        {

        }
        #endregion //CTOR


        #region Init
        protected override void InitEnemies()
        {
            var viewport = GameManager.Instance.GraphicsDevice.Viewport;

            //Initialize the Enemies.
            int initialBalloonX = viewport.Width  / 2;
            int initialBalloonY = viewport.Height / 2;

            for(int i = 0; i < kMaxBalloonsCount; ++i)
            {
                var x = initialBalloonX + (Balloon.kBalloonWidth * i);

                var balloon = new RedBalloon(new Vector2(x, initialBalloonY));
                balloon.OnStateChangeDead  += OnEnemyStateChangeDead;
                balloon.OnStateChangeDying += OnEnemyStateChangeDying;

                Enemies.Add(balloon);
            }

            AliveEnemies = kMaxBalloonsCount;
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
            GameManager.Instance.ChangeLevel(new Level2());
        }
        #endregion

    }//class Level1
}//namespace com.amazingcow.BowAndArrow
