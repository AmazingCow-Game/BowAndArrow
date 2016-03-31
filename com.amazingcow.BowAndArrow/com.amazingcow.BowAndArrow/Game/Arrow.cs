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
        public override Vector2 Position
        {
            get { return CurrentSprite.Position;  }
            set { CurrentSprite.Position = value; }
        }
        #endregion //Public Properties 


                                 
        public Arrow()
        {
            CurrentSprite = new Sprite("arrow");
            Speed = new Vector2(50, 0);
        }


        #region Update / Draw
        public override void Update(GameTime gt)
        {
            //Balloon is already dead - Don't need to do anything else.
            if(CurrentState == State.Dead)
                return;

            //Update the position.
            Position += (Speed * (gt.ElapsedGameTime.Milliseconds / 1000f));
        }

        public override void Draw(GameTime gt)
        {
            if(CurrentState == State.Dead)
                return;

            CurrentSprite.Draw(gt);
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

