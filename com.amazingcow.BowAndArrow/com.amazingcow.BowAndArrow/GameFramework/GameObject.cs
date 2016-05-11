#region Usings
//System
using System;
using System.Collections.Generic;
//XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public abstract class GameObject
    {
        #region Constants
        public const int kAliveTexturesListHintSize = 2;
        public const int kDyingTexturesListHintSize = 1;
        #endregion //Constants


        #region Events
        public event EventHandler<EventArgs> OnStateChangeDying;
        public event EventHandler<EventArgs> OnStateChangeDead;
        #endregion //Events


        #region Enums
        public enum State
        {
            Alive,
            Dying,
            Dead
        }
        #endregion //Enums


        #region iVars
        State _currentState;
        #endregion //iVars


        #region Public Properties
        public Vector2 Position
        { get; protected set; }

        public Vector2 Speed
        { get; protected set; }

        public Rectangle BoundingBox
        {
            get {
                return new Rectangle((int)Position.X,
                                     (int)Position.Y,
                                     CurrentTexture.Width,
                                     CurrentTexture.Height);
            }
        }

        public virtual Rectangle HitBox
        {
            get { return BoundingBox; }
        }

        public State CurrentState
        {
            get { return _currentState; }
            protected set { ChangeState(value); }
        }

        public List<Texture2D> AliveTexturesList
        { get; protected set; }

        public List<Texture2D> DyingTexturesList
        { get; protected set; }

        public Texture2D CurrentTexture
        {
            get { return CurrentTexturesList[CurrentTextureIndex]; }
        }

        public List<Texture2D> CurrentTexturesList
        { get; protected set; }

        public int CurrentTextureIndex
        { get; protected set; }

        public Clock TextureAnimationTimer
        { get; protected set; }


        public Color TintColor
        { get; protected set; }
        #endregion //Public Properties


        #region CTOR
        protected GameObject(Vector2 position, Vector2 speed,
                             float msToChangeTexture)
        {
            //House keeping
            Position     = position;
            Speed        = speed;
            CurrentState = State.Alive;
            TintColor    = Color.White;

            //Textures
            AliveTexturesList     = new List<Texture2D>(kAliveTexturesListHintSize);
            DyingTexturesList     = new List<Texture2D>(kDyingTexturesListHintSize);
            CurrentTexturesList   = AliveTexturesList;
            CurrentTextureIndex   = 0;

            //Animation.
            TextureAnimationTimer = new Clock(msToChangeTexture,
                                              Clock.kRepeatForever);
            TextureAnimationTimer.OnTick += OnTextureAnimationTimerTick;
            TextureAnimationTimer.Start();
        }
        #endregion //CTOR


        #region Update / Draw
        public virtual void Update(GameTime gt)
        {
            TextureAnimationTimer.Update(gt.ElapsedGameTime.Milliseconds);
        }
        public virtual void Draw(GameTime gt)
        {
            if(CurrentState == State.Dead)
                return;

            GameManager.Instance.CurrentSpriteBatch.Draw(
                CurrentTexture, 
                Position, 
                TintColor);
        }
        #endregion //Update / Draw


        #region Public Methods
        public abstract void Kill();
        #endregion //Public Methods


        #region Private Methods
        void ChangeState(State state)
        {
            _currentState = state;

            if(_currentState == State.Dead)
            {
                OnStateChangeDead(this, EventArgs.Empty);
            }
            else if(_currentState == State.Dying)
            {
                OnStateChangeDying(this, EventArgs.Empty);
                CurrentTexturesList = DyingTexturesList;
                CurrentTextureIndex = 0;
            }
        }
            
        void OnTextureAnimationTimerTick(object sender, EventArgs e)
        {
            int count = CurrentTexturesList.Count;
            CurrentTextureIndex = (CurrentTextureIndex + 1) % count;
        }
        #endregion //Private Methods

    }//class GameObject
}//namespace com.amazingcow.BowAndArrow

