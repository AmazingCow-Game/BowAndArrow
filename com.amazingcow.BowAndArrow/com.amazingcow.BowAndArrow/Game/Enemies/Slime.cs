#region Usings
//System
using System;
//Xna
using Microsoft.Xna.Framework;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Slime : Enemy
    {
        #region Constants
        //Public
        public const int kHeight = 49;
        //Private
        const int kSpeedMin    =  80;
        const int kSpeedMax    = 150;
        const int kWidth       =  39;
        const int kTimeToDieMs = 500;
        #endregion


        #region iVars
        private Clock _dyingClock;
        #endregion //iVars


        #region CTOR
        public Slime(Vector2 position)
            : base(position, Vector2.Zero, 0)
        {
            //Initialize the textures...
            var resMgr = ResourcesManager.Instance;
            AliveTexturesList.Add(resMgr.GetTexture("slime"));
            DyingTexturesList.Add(resMgr.GetTexture("slime_dead"));

            //Init the Speed...
            int xSpeed = GameManager.Instance.RandomNumGen.Next(kSpeedMin,
                                                                kSpeedMax);
            Speed = new Vector2(-xSpeed, 0);

            //Init the timers...
            _dyingClock = new Clock(kTimeToDieMs, 1);
            _dyingClock.OnTick += OnDyingClockTick;
        }
        #endregion //CTOR


        #region Update / Draw
        public override void Update(GameTime gt)
        {
            //Slime is already dead - Don't need to do anything else.
            if(CurrentState == State.Dead)
                return;
            
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

            //Star the clock and stop the slime.
            CurrentState = State.Dying;
            _dyingClock.Start();

            Speed = Vector2.Zero;
        }

        public override bool CheckCollisionPlayer(Archer archer)
        {
            if(CurrentState != State.Alive)
                return false;

            return archer.HitBox.Intersects(this.BoundingBox);
        }
        #endregion //Public Methods


        #region Private Methods
        private void MoveAlive(GameTime gt)
        {
            //Update the position.
            Position += (Speed * (gt.ElapsedGameTime.Milliseconds / 1000f));

            //Goes off the screen - Kill it.
            if(BoundingBox.Right <= 0)
                CurrentState = State.Dead;
        }
        #endregion //Private Methods


        #region Timers Callbacks
        void OnDyingClockTick(object sender, EventArgs e)
        {
            //Glow for enough time already.
            CurrentState = State.Dead;
        }
        #endregion //Timers Callbacks

    }//Class Slime
}//namespace com.amazingcow.BowAndArrow

