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
    public class Archer : GameObject
    {
        #region Events
        public event EventHandler<EventArgs> OnArcherShootArrow;
        #endregion //Events


        #region Enums / Constants
        public enum BowState
        {
            Stand,  //Bow has an arrow but is not pulled.
            Armed,  //Bow has an arrow AND is pulled.
            Unarmed //Bow hasn't an arrow.
        }

        public const int kMaxArrowsCount = 20;

        private const int kChangeStateInterval = 200;
        #endregion //Enums


        #region Public Properties
        public int ArrowsCount
        { get; private set; }

        public BowState CurrentBowState
        { get; private set; }

        public Vector2 ArrowPosition
        {
            get {
                return new Vector2(this.Position.X + 47, //COWTODO: Remove magic number
                                   this.Position.Y + 41); //COWTODO: Remove magic number
            }
        }
        #endregion //Public Properties


        #region iVars
        private Clock _changeStateClock;
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
            CurrentBowState = BowState.Stand;
            ArrowsCount     = kMaxArrowsCount;

            //Init the timers.
            _changeStateClock = new Clock(kChangeStateInterval, 1);
            _changeStateClock.OnTick += OnChangeStateClockTick;
        }
        #endregion //CTOR


        #region Update / Draw
        public override void Update(GameTime gt)
        {
            //Arrow is already dead - Don't need to do anything else.
            if(CurrentState == State.Dead)
                return;

            //Update the timers.
            _changeStateClock.Update(gt.ElapsedGameTime.Milliseconds);

            var mouseState = InputHandler.Instance.CurrentMouseState;

            //Change the Bow State.
            if(mouseState.RightButton == ButtonState.Pressed)
                TryChangeBowState(BowState.Stand);
            else if(mouseState.LeftButton == ButtonState.Released)
                TryChangeBowState(BowState.Unarmed);
            else if(mouseState.LeftButton == ButtonState.Pressed)
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
        }
        #endregion //Public Methods


        #region Helper Methods
        private void TryChangeBowState(BowState targetBowState)
        {
            if(_changeStateClock.IsEnabled)
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


        private void Shoot()
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

            _changeStateClock.Start();
        }
        private void Arm()
        {
            CurrentBowState     = BowState.Armed;
            CurrentTextureIndex = (int)CurrentBowState;

            _changeStateClock.Start();
        }
        private void Stand()
        {
            CurrentBowState     = BowState.Stand;
            CurrentTextureIndex = (int)CurrentBowState;

            _changeStateClock.Start();
        }


        private void CalculateMovementSpeed()
        {
            var mouseState = InputHandler.Instance.CurrentMouseState;

            Speed = Vector2.Zero;

            //Archer only move when button is pressed.
            if(mouseState.LeftButton != ButtonState.Pressed)
                return;

            var mouseY      = mouseState.Y;
            var boundingBox = this.BoundingBox;

            //COWTODO: Remove the magic numbers.
            if(mouseY - 20 < boundingBox.Top)
                Speed = new Vector2(0, -100);
            if(mouseY + 20 > boundingBox.Bottom)
                Speed = new Vector2(0, 100);
        }
        #endregion //Helper Methods


        #region Timers Callbacks
        private void OnChangeStateClockTick(object sender, EventArgs e)
        {
            _changeStateClock.Stop();
        }
        #endregion //Timers Callbacks
    }
}

