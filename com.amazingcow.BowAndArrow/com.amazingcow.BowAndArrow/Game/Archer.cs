#region Usings
//System
using System;
//Xna
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Archer : GameObject
    {
        #region Events
        public event EventHandler<EventArgs> OnArcherShootArrow;
        #endregion //Events


        #region Enums
        public enum BowState
        {
            Stand,  //Bow has an arrow but is not pulled.
            Armed,  //Bow has an arrow AND is pulled.
            Unarmed //Bow hasn't an arrow.
        }
        #endregion //Enums


        #region Public Constants
        public const int kMaxArrowsCount = 15;
        public const int kMaxEnemyHits   = 5;
        #endregion //Public Constants

        #region Private Constants
        //Timer
        const int kChangeBowStateInterval = 200;
        const int kHitGlowInterval        = 50;
        const int kHitGlowRepeatCount     = 8;
        //Arrow
        const int kArrowPositionOffsetX = 47;
        const int kArrowPositionOffsetY = 41;
        #endregion //Constants


        #region Public Properties
        public int ArrowsCount
        { get; private set; }

        public Vector2 ArrowPosition
        {
            get {
                return new Vector2(Position.X + kArrowPositionOffsetX, 
                                   Position.Y + kArrowPositionOffsetY);
            }
        }

        public BowState CurrentBowState
        { get; private set; }

        public int EnemyHits
        { get; private set; }
        #endregion //Public Properties


        #region iVars
        readonly Clock _changeBowStateClock;
        readonly Clock _hitGlowClock;
        #endregion //iVars


        #region CTOR
        public Archer(Vector2 position) :
            base(position, Vector2.Zero, 0)
        {
            //Init the textures.
            var resMgr = ResourcesManager.Instance;

            AliveTexturesList.Add(resMgr.GetTexture("hero_stand"));
            AliveTexturesList.Add(resMgr.GetTexture("hero_armed"));
            AliveTexturesList.Add(resMgr.GetTexture("hero_without_arrow"));

            DyingTexturesList = AliveTexturesList;

            //Init the Properties.
            ArrowsCount     = kMaxArrowsCount;
            CurrentBowState = BowState.Stand;
            EnemyHits       = 0;

            //Init the timers.
            _changeBowStateClock = new Clock(kChangeBowStateInterval, 1);
            _changeBowStateClock.OnTick += OnChangeStateClockTick;

            _hitGlowClock = new Clock(kHitGlowInterval, kHitGlowRepeatCount);
            _hitGlowClock.OnTick += OnHitGlowClockTick;
        }            
        #endregion //CTOR


        #region Update / Draw
        public override void Update(GameTime gt)
        {
            //Archer is already dead - Don't need to do anything else.
            if(CurrentState == State.Dead)
                return;

            //Update the timers.
            _changeBowStateClock.Update(gt.ElapsedGameTime.Milliseconds);
            _hitGlowClock.Update(gt.ElapsedGameTime.Milliseconds);

            var inputState = InputHandler.Instance.CurrentMouseState;

            //Change the Bow State.
            if(inputState.RightButton == ButtonState.Pressed)
                TryChangeBowState(BowState.Stand);
            else if(inputState.LeftButton == ButtonState.Released)
                TryChangeBowState(BowState.Unarmed);
            else if(inputState.LeftButton == ButtonState.Pressed)
                TryChangeBowState(BowState.Armed);

            //Try move...
            CalculateMovementSpeed();

            //Update the position.
            Position += (Speed * (gt.ElapsedGameTime.Milliseconds / 1000f));
        }
        #endregion //Update / Draw


        #region Public Methods
        public override void Kill()
        {
            //Already Dead - Don't do anything else...
            if(CurrentState != State.Alive)
                return;

            CurrentState = State.Dead;
        }

        public void Hit()
        {
            ++EnemyHits;

            //Reset the Glow Timer.
            _hitGlowClock.Stop();
            _hitGlowClock.Start();

            //Make glows imediatelly.
            OnHitGlowClockTick(null, null);

            if(EnemyHits == kMaxEnemyHits)
                Kill();
        }
        #endregion //Public Methods


        #region Helper Methods
        //BowState.
        void TryChangeBowState(BowState targetBowState)
        {
            if(_changeBowStateClock.IsEnabled)
                return;

            if(CurrentState == State.Dying)
                return;

            // Armed -> Unarmed
            if((CurrentBowState == BowState.Armed &&
                targetBowState  == BowState.Unarmed))
            {
                Shoot();
            }

            // Stand -> Armed
            else if(CurrentBowState == BowState.Stand &&
                    targetBowState  == BowState.Armed)
            {
                Arm();
            }

            // Unarmed -> Stand
            else if(CurrentBowState == BowState.Unarmed &&
                    targetBowState  == BowState.Stand)
            {
                Stand();
            }
        }

        //Bow Actions.
        void Shoot()
        {
            if(ArrowsCount > 0)
            {
                --ArrowsCount;
                OnArcherShootArrow(this, EventArgs.Empty);

                if(ArrowsCount <= 0)
                {
                    CurrentState = State.Dying;
                }
            }

            CurrentBowState     = BowState.Unarmed;
            CurrentTextureIndex = (int)CurrentBowState;

            _changeBowStateClock.Start();
        }
        void Arm()
        {
            CurrentBowState     = BowState.Armed;
            CurrentTextureIndex = (int)CurrentBowState;

            _changeBowStateClock.Start();
        }
        void Stand()
        {
            CurrentBowState     = BowState.Stand;
            CurrentTextureIndex = (int)CurrentBowState;

            _changeBowStateClock.Start();
        }

        //Movement.
        void CalculateMovementSpeed()
        {
            var mouseState = InputHandler.Instance.CurrentMouseState;

            Speed = Vector2.Zero;

            //Archer only move when button is pressed.
            if(mouseState.LeftButton != ButtonState.Pressed)
                return;

            var mouseY = mouseState.Y;
            var lvl    = GameManager.Instance.CurrentLevel;

            //COWTODO: Remove the magic numbers.
            if(mouseY - 20 < BoundingBox.Top &&
               BoundingBox.Top > lvl.PlayField.Top)
            {
                Speed = new Vector2(0, -200);
            }
            if(mouseY + 20 > BoundingBox.Bottom &&
               BoundingBox.Bottom < lvl.PlayField.Bottom)
            {
                Speed = new Vector2(0, 200);
            }
        }
        #endregion //Helper Methods


        #region Timers Callbacks
        void OnChangeStateClockTick(object sender, EventArgs e)
        {
            _changeBowStateClock.Stop();
        }

        void OnHitGlowClockTick(object sender, EventArgs e)
        {
            if(_hitGlowClock.TickCount == kHitGlowRepeatCount)
            {
                TintColor = Color.White;
                _hitGlowClock.Stop();
            }
            else
            {
                TintColor = (_hitGlowClock.TickCount % 2 == 0) 
                             ? Color.Red 
                             : Color.White;
            }    
        }
        #endregion //Timers Callbacks
    }
}

