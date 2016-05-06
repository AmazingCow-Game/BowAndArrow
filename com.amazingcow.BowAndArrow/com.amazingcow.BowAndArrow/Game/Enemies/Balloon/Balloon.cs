#region Usings
//System
using System;
//Xna
using Microsoft.Xna.Framework;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Balloon : Enemy
    {
        #region Constants
        //Public 
        public const int kWidth  = 25;
        public const int kHeight = 39;
        //Private
        const int kStringHeight = 12;
        #endregion

        #region Public Properties
        public override Rectangle HitBox
        {
            get {
                return new Rectangle(BoundingBox.X,
                                     BoundingBox.Y,
                                     BoundingBox.Width,
                                     BoundingBox.Height - kStringHeight);
            }
        }
        #endregion //Public Properties


        #region CTOR
        public Balloon(Vector2 position, Vector2 speed)
            : base(position, speed, 0)
        {
            //Empty...
        }
        #endregion //CTOR


        #region Update / Draw
        public override void Update(GameTime gt)
        {
            //Balloon is already dead - Don't need to do anything else.
            if(CurrentState == State.Dead)
                return;

            //Update the position.
            Position += (Speed * (gt.ElapsedGameTime.Milliseconds / 1000f));

            var windowHeight = GameManager.Instance.GraphicsDevice.Viewport.Height;

            //On State.Alive -> If Balloon goes up of top of window
            //reset it to the bottom of the window.
            if(CurrentState == State.Alive &&
               BoundingBox.Bottom <= 0)
            {
                Position = new Vector2(Position.X, windowHeight);
            }

            //On State.Dying -> If Balloon goes down of the window's
            //bottom, set the state to died.
            else if(CurrentState == State.Dying &&
                    Position.Y >= windowHeight)
            {
                Speed        = Vector2.Zero;
                CurrentState = State.Dead;
            }
        }
        #endregion //Update / Draw


        #region Public Methods
        public override void Kill()
        {
            //Already Dead - Don't do anything else...
            if(CurrentState != State.Alive)
                return;

            //Set the state and the sprite for dying
            //and make the balloon fall with more speed.
            CurrentState  = State.Dying;

            Speed *= -1.2f;
        }
        #endregion //Public Methods

    }//Class Balloon
}//namespace com.amazingcow.BowAndArrow

