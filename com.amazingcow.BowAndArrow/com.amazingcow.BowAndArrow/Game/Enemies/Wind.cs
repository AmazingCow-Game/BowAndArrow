#region Usings
//System
using System;
//Xna
using Microsoft.Xna.Framework;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Wind : Enemy
    {
        #region Constants
        public const int kSpeedMinWind = 120;
        public const int kSpeedMaxWind = 250;
        public const int kWindHeight   = 49;
        public const int kWindWidth    = 39;
        #endregion


        #region iVars
        private Clock _dyingClock;
        #endregion //iVars


        #region CTOR
        public Wind(Vector2 position)
            : base(position, Vector2.Zero, 100)
        {
            //Initialize the textures...
            var resMgr = ResourcesManager.Instance;
            AliveTexturesList.Add(resMgr.GetTexture("wind1"));
            AliveTexturesList.Add(resMgr.GetTexture("wind2"));
            DyingTexturesList.Add(resMgr.GetTexture("wind_dead"));

            //Init the Speed...
            int xSpeed = GameManager.Instance.RandomNumGen.Next(kSpeedMinWind,
                                                                kSpeedMaxWind);
            Speed = new Vector2(-xSpeed, 0);

            //Init the timers...
            _dyingClock = new Clock(500, 1);
            _dyingClock.OnTick += OnDyingClockTick;
        }
        #endregion //CTOR


        #region Update / Draw
        public override void Update(GameTime gt)
        {
            //Wind is already dead - Don't need to do anything else.
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
        private void MoveAlive(GameTime gt)
        {
            //Update the position.
            Position += (Speed * (gt.ElapsedGameTime.Milliseconds / 1000f));

            if(BoundingBox.Right <= 0)
                CurrentState = State.Dead;
        }
        #endregion //Private Methods


        #region Timers Callbacks
        void OnDyingClockTick(object sender, EventArgs e)
        {
            CurrentState = State.Dead;
        }
        #endregion //Timers Callbacks

    }//Class Wind
}//namespace com.amazingcow.BowAndArrow

