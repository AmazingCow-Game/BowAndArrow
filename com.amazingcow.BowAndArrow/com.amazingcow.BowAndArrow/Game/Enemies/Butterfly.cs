#region Usings
//System
using System;
//Xna
using Microsoft.Xna.Framework;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Butterfly : Enemy
    {
        #region Constants
        //Public
        public const int kWidth = 32;  
        //Private
        const int kSpeedMin = 35;
        const int kSpeedMax = 45;
        #endregion


        #region Public Properties
        #endregion //Public Properties


        #region CTOR
        public Butterfly(Vector2 position)
            : base(position, Vector2.Zero, 0)
        {
            //Initialize the textures...
            var resMgr = ResourcesManager.Instance;
            AliveTexturesList.Add(resMgr.GetTexture("butterfly_bubled"));
            DyingTexturesList.Add(resMgr.GetTexture("butterfly"));

            //Init the Speed...
            int ySpeed = GameManager.Instance.RandomNumGen.Next(kSpeedMin,
                                                                kSpeedMax);
            Speed = new Vector2(0, -ySpeed);
        }
        #endregion //CTOR


        #region Update / Draw
        public override void Update(GameTime gt)
        {
            //Butterfly is already dead - Don't need to do anything else.
            if(CurrentState == State.Dead)
                return;

            if(CurrentState == State.Dying)
                MoveDying(gt);
            else
                MoveAlive(gt);
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

            var rndGen = GameManager.Instance.RandomNumGen;

            //Makes go towards the Top Left of screen.
            Speed = new Vector2(-rndGen.Next(kSpeedMin, kSpeedMax),
                                -rndGen.Next(kSpeedMin, kSpeedMax));
        }
        #endregion //Public Methods


        #region Private Methods
        private void MoveDying(GameTime gt)
        {
            //Update the position.
            Position += (Speed * (gt.ElapsedGameTime.Milliseconds / 1000f));

            //It got out of upper bound of screen.
            if(BoundingBox.Bottom <= 0)
                CurrentState = State.Dead;
        }

        void MoveAlive(GameTime gt)
        {
            //Update the position.
            Position += (Speed * (gt.ElapsedGameTime.Milliseconds / 1000f));

            var lvl = GameManager.Instance.CurrentLevel;
            if(BoundingBox.Bottom <= lvl.PlayField.Top)
                Position = new Vector2(Position.X, lvl.PlayField.Bottom);
        }
        #endregion //Private Methods

    }//Class Butterfly
}//namespace com.amazingcow.BowAndArrow

