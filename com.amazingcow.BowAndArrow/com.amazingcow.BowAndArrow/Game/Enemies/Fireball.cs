#region Usings
//System
using System;
//Xna
using Microsoft.Xna.Framework;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Fireball : Enemy
    {
        #region Constants
        //Public 
        public const int kHeight     = 49;
        public const int kWidth      = 39;
        public const int kScoreValue = 600;
        //Private
        const int kSpeedMin = 120;
        const int kSpeedMax = 180;       
        #endregion


        #region Public Properties 
        public override int ScoreValue { get { return kScoreValue; } }
        #endregion


        #region iVars
        Clock _dyingClock;
        #endregion //iVars


        #region CTOR
        public Fireball(Vector2 position)
            : base(position, Vector2.Zero, 100)
        {
            //Initialize the textures...
            var resMgr = ResourcesManager.Instance;
            AliveTexturesList.Add(resMgr.GetTexture("fire1"));
            AliveTexturesList.Add(resMgr.GetTexture("fire2"));
            DyingTexturesList.Add(resMgr.GetTexture("fire_dead"));

            //Init the Speed...
            int xSpeed = GameManager.Instance.RandomNumGen.Next(kSpeedMin,
                                                                kSpeedMax);
            Speed = new Vector2(-xSpeed, 0);

            //Init the timers...
            _dyingClock = new Clock(500, 1);
            _dyingClock.OnTick += OnDyingClockTick;
        }
        #endregion //CTOR


        #region Update / Draw
        public override void Update(GameTime gt)
        {
            //Fireball is already dead - Don't need to do anything else.
            if(CurrentState == State.Dead)
                return;
            
            base.Update(gt);

            _dyingClock.Update(gt.ElapsedGameTime.Milliseconds);

            //Just move in alive - Dying it will only glow.
            if(CurrentState == State.Alive)
                MoveAlive(gt);
        }
        #endregion //Update / Draw


        #region Public Methods
        public override void Kill()
        {
            //Already Dead - Don't do anything else...
            if(CurrentState != State.Alive)
                return;

            CurrentState  = State.Dying;
            _dyingClock.Start();

            Speed = Vector2.Zero;
        }

        public override bool CheckCollisionPlayer(Archer archer)
        {
            if(CurrentState != State.Alive)
                return false;

            return archer.BoundingBox.Intersects(this.BoundingBox);
        }
        #endregion //Public Methods


        #region Private Methods
        void MoveAlive(GameTime gt)
        {
            //Update the position.
            Position += (Speed * (gt.ElapsedGameTime.Milliseconds / 1000f));

            var bounds = GameManager.Instance.CurrentLevel.PlayField;
            if(BoundingBox.Right <  bounds.Left)
                ResetPosition();
        }
        #endregion //Private Methods


        #region Timers Callbacks
        void OnDyingClockTick(object sender, EventArgs e)
        {
            CurrentState = State.Dead;
        }
        #endregion //Timers Callbacks


        #region Helper Methods 
        void ResetPosition()
        {
            var bounds = GameManager.Instance.CurrentLevel.PlayField;
            var rnd    = GameManager.Instance.RandomNumGen;

            var x = rnd.Next(bounds.Right, bounds.Right * 2);
            var y = rnd.Next(bounds.Top    + BoundingBox.Height, 
                             bounds.Bottom - BoundingBox.Height);

            Position = new Vector2(x, y);
        }
        #endregion //Helper Methods 

    }//Class Fireball
}//namespace com.amazingcow.BowAndArrow

