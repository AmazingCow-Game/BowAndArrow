#region Usings
//System
using System;
//Xna
using Microsoft.Xna.Framework;
using System.Runtime.Remoting.Messaging;
using Microsoft.Xna.Framework.Input;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Archer : GameObject
    {
        #region Enums
        public enum BowState { Stand, Armed, Unarmed }
        #endregion //Enums

        #region Public Properties
        public BowState CurrentBowState { get; private set; }
        #endregion //Public Properties

        public Archer(Vector2 position) :
            base(position, Vector2.Zero, 0)
        {
            //Init the textures.
            var resMgr = ResourcesManager.Instance;

            AliveTexturesList.Add(resMgr.GetTexture("hero_stand"));
            AliveTexturesList.Add(resMgr.GetTexture("hero_armed"));
            AliveTexturesList.Add(resMgr.GetTexture("hero_without_arrow"));

            CurrentBowState = BowState.Stand;
        }


        #region Update / Draw
        public override void Update(GameTime gt)
        {
            //Arrow is already dead - Don't need to do anything else.
            if(CurrentState == State.Dead)
                return;

            var mouseState = Mouse.GetState();

            //
            if(mouseState.RightButton == ButtonState.Pressed &&
               CurrentBowState == BowState.Unarmed)
            {
                CurrentBowState = BowState.Stand;
            }

            else if(mouseState.LeftButton == ButtonState.Released &&
               CurrentBowState == BowState.Armed)
            {
                CurrentBowState = BowState.Unarmed;
            }

            else if(mouseState.LeftButton == ButtonState.Pressed &&
               CurrentBowState == BowState.Stand)
            {
                CurrentBowState = BowState.Armed;
            }

            CurrentTextureIndex = (int)CurrentBowState;

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

    }
}

