#region Usings
//System
using System;
//Xna
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class BullsEye : Enemy
    {
        #region Constants
        //Public
        public const int kHeight = 49;
        //Private
        static readonly Vector2 kSpeed = new Vector2(0, 50);
        #endregion


        #region Public Properties 
        public override Rectangle HitBox {
            get {
                return new Rectangle(BoundingBox.X, 
                                     BoundingBox.Y + 20,
                                     BoundingBox.Width, 
                                     BoundingBox.Height - 20);
            }
        }
        #endregion //Public Properties

        #region iVars
        Texture2D     _arrowTexture;
        List<Vector2> _arrowHitPoints;
        #endregion //iVars


        #region CTOR
        public BullsEye(Vector2 position)
            : base(position, kSpeed, 0)
        {
            //Initialize the textures...
            var resMgr = ResourcesManager.Instance;
            AliveTexturesList.Add(resMgr.GetTexture("bulls_eye"));


            _arrowTexture   = ResourcesManager.Instance.GetTexture("arrow");
            _arrowHitPoints = new List<Vector2>();
        }
        #endregion //CTOR


        #region Update / Draw
        public override void Update(GameTime gt)
        {
            //Update the position.
            Position += (Speed * (gt.ElapsedGameTime.Milliseconds / 1000f));

            var winHeight = GameManager.Instance.GraphicsDevice.Viewport.Height;

            //Just reverses the direction if reach the screen border.
            if(BoundingBox.Top <= 0 || BoundingBox.Bottom >= winHeight)
                Speed *= -1;
        }

        public override void Draw(GameTime gt)
        {
            base.Draw(gt);

            foreach(var hitpoint in _arrowHitPoints)
            {

                GameManager.Instance.CurrentSpriteBatch.Draw(
                    _arrowTexture, 
                    hitpoint + Position
                );
            }
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

        public override bool CheckCollisionArrow(Arrow arrow)
        {
            var hit = base.CheckCollisionArrow(arrow); 
            return false;
        }
        #endregion //Public Methods

    }//Class BullsEye
}//namespace com.amazingcow.BowAndArrow

