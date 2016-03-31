#region Usings
//System
using System;
using System.Collections.Generic;
//Xna
using Microsoft.Xna.Framework;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Balloon : Enemy
    {
        #region Public Properties 
        public override Vector2 Position
        {
            get { return CurrentSprite.Position;  }
            set { CurrentSprite.Position = value; }
        }
        #endregion //Public Properties 


        #region Constants 
        protected const int kSpriteIndexAlive = 0;
        protected const int kSpriteIndexDying = 1;
        protected const int kSpriteIndexSize  = 2;
        #endregion


        #region iVars 
        protected List<Sprite> _spriteList;
        #endregion //iVars


        #region CTOR
        public Balloon()
        {
            //Initialize the sprites.
            _spriteList = new List<Sprite>(kSpriteIndexSize);
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
               Position.Y + CurrentSprite.BoundingBox.Height <= 0) 
            {
                Position = new Vector2(Position.X, windowHeight);
            }

            //On State.Dying -> If Balloon goes down of the window's
            //bottom, set the state to died.
            else if(CurrentState == State.Dying &&
                    Position.Y >= windowHeight)
            {
                Speed = Vector2.Zero;
                CurrentState = State.Dead;
            }
        }

        public override void Draw(GameTime gt)
        {
            if(CurrentState == State.Dead)
                return;

            CurrentSprite.Draw(gt);
        }
        #endregion //Update / Draw 


        #region Public Methods 
        public override bool CheckCollisionPlayer(Archer archer)
        {
            return false; //Never gets collided with the Player.
        }

        public override bool CheckCollisionArrow(Arrow arrow)
        {
            return false;
        }

        public override void Kill()
        {
            //Already Dead - Don't do anything else...
            if(CurrentState != State.Alive)
                return;

            var pos = CurrentSprite.Position;

            //Set the state and the sprite for dying 
            //and make the balloon fall with more speed.
            CurrentState  = State.Dying;           
            CurrentSprite = _spriteList[kSpriteIndexDying];
            CurrentSprite.Position = pos;

            Speed *= -1.2f;
        }
        #endregion //Public Methods 

    }
}

