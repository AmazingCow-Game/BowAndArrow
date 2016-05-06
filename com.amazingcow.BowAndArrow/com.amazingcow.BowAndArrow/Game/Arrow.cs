#region Usings
//System
using System;
//Xna
using Microsoft.Xna.Framework;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Arrow : GameObject
    {
        #region Public Properties
        public Vector2 HeadPoint
        {
            get
            {
                return new Vector2(BoundingBox.Right,
                                   Position.Y);
            }
        }
        #endregion //Public Properties



        public Arrow(Vector2 position) :
            base(position,
                 new Vector2(150, 0), //COWTODO: Change the magic numbers.
                 0)
        {
            //Init the textures.
            AliveTexturesList.Add(ResourcesManager.Instance.GetTexture("arrow"));
        }


        #region Update / Draw
        public override void Update(GameTime gt)
        {
            //Arrow is already dead - Don't need to do anything else.
            if(CurrentState == State.Dead)
                return;

            //Update the position.
            Position += (Speed * (gt.ElapsedGameTime.Milliseconds / 1000f));

            //Check if Arrow went out of screen.
            var windowWidth = GameManager.Instance.GraphicsDevice.Viewport.Width;
            if(Position.X >= windowWidth)
                CurrentState = State.Dead;
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

    }
}

