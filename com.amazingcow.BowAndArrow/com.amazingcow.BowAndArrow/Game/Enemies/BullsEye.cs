#region Usings
//System
using System;
using System.Collections.Generic;
//Xna
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class BullsEye : Enemy
    {
        #region Constants
        //Public
        public const int kHeight     = 49;
        public const int kScoreValue = 500;
        //Private
        static readonly Vector2 kSpeed = new Vector2(0, 50);
        #endregion


        #region Public Properties 
        public override int ScoreValue { get { return kScoreValue; } }

        public override Rectangle HitBox {
            get {
                return new Rectangle(BoundingBox.X, 
                                     BoundingBox.Y + 10,
                                     BoundingBox.Width, 
                                     14);
            }
        }
        #endregion //Public Properties

        #region iVars
        Texture2D   _arrowTexture;
        List<float> _arrowHitPoints;
        #endregion //iVars


        #region CTOR
        public BullsEye(Vector2 position)
            : base(position, kSpeed, 0)
        {
            //Initialize the textures...
            var resMgr = ResourcesManager.Instance;
            AliveTexturesList.Add(resMgr.GetTexture("bulls_eye"));

            _arrowTexture   = ResourcesManager.Instance.GetTexture("arrow");
            _arrowHitPoints = new List<float>();
        }
        #endregion //CTOR


        #region Update / Draw
        public override void Update(GameTime gt)
        {
            //Update the position.
            Position += (Speed * (gt.ElapsedGameTime.Milliseconds / 1000f));

            var lvl = GameManager.Instance.CurrentLevel;

            //Just reverses the direction if reach the screen border.
            if(BoundingBox.Top <= lvl.PlayField.Top || 
               BoundingBox.Bottom >= lvl.PlayField.Bottom)
            {
                Speed *= -1;
            }
        }

        public override void Draw(GameTime gt)
        {
            base.Draw(gt);

            foreach(var hitpoint in _arrowHitPoints)
            {
                GameManager.Instance.CurrentSpriteBatch.Draw(
                    _arrowTexture, 
                    new Vector2(BoundingBox.Left - _arrowTexture.Bounds.Width + 5,
                                BoundingBox.Top + hitpoint)
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
            if(BoundingBox.Contains(arrow.HeadPoint))
            {
                var hitY = arrow.HeadPoint.Y - BoundingBox.Top;
                _arrowHitPoints.Add(hitY);

                if(HitBox.Contains(arrow.HeadPoint))
                    return true;                

                arrow.Kill();
            }
            return false;
        }
        #endregion //Public Methods

    }//Class BullsEye
}//namespace com.amazingcow.BowAndArrow

