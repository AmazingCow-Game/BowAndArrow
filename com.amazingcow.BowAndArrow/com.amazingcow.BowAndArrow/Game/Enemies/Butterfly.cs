#region Usings
//System
using System;
using System.Collections.Generic;
//Xna
using Microsoft.Xna.Framework;
#endregion //Usings


namespace cow.amazingcow.BowAndArrow
{
    public class Butterfly : Enemy
    {
        #region Constants
        public const int kSpeedMinButterfly = 35;
        public const int kSpeedMaxButterfly = 45;
        public const int kButterflyWidth    = 32;
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
            int ySpeed = GameManager.Instance.RandomNumGen.Next(kSpeedMinButterfly,
                                                                kSpeedMaxButterfly);
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
            Speed = new Vector2(-rndGen.Next(kSpeedMinButterfly, kSpeedMaxButterfly),
                                -rndGen.Next(kSpeedMinButterfly, kSpeedMaxButterfly));
        }
        #endregion //Public Methods


        #region Private Methods
        private void MoveDying(GameTime gt)
        {
            //Update the position.
            Position += (Speed * (gt.ElapsedGameTime.Milliseconds / 1000f));

            if(BoundingBox.Bottom <= 0)
                CurrentState = State.Dead;;
        }

        private void MoveAlive(GameTime gt)
        {
            //Update the position.
            Position += (Speed * (gt.ElapsedGameTime.Milliseconds / 1000f));

            var windowHeight = GameManager.Instance.GraphicsDevice.Viewport.Height;

            if(BoundingBox.Bottom <= 0)
                Position = new Vector2(Position.X, windowHeight);
        }
        #endregion //Private Methods

    }//Class Butterfly
}//namespace com.amazingcow.BowAndArrow

