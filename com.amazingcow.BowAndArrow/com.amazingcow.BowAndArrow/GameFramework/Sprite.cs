#region Usings
//System
using System;
//XNA
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Sprite
    {
        #region Public Properties
        public Texture2D     Texture     { get; set; }
        public Vector2       Position    { get; set; }
        public Vector2       Origin      { get; set; }
        public float         Rotation    { get; set; }
        public Vector2       Scale       { get; set; }
        public Color         TintColor   { get; set; }
        public SpriteEffects Effects     { get; set; }
        public int           ZIndex      { get; set; }
        public Rectangle     BoundingBox
        {
            get {
                return new Rectangle((int)Position.X,
                                     (int)Position.Y,
                                     Texture.Bounds.Width,
                                     Texture.Bounds.Height);
            }
        }
        #endregion //Public Properties


        #region
        public Sprite(String name)
        {
            Texture    = ResourcesManager.Instance.GetTexture(name);
            Position   = Vector2.Zero;
            Origin     = Vector2.Zero;
            Rotation   = 0;
            Scale      = Vector2.One;
            TintColor  = Color.White;
            Effects    = SpriteEffects.None;
            ZIndex     = 0;
        }
        #endregion


        #region IDrawable
        public void Draw(GameTime gameTime)
        {
            GameManager.Instance.CurrentSpriteBatch.Draw(
                Texture,
                Position,
                null,
                null,
                Origin,
                Rotation,
                Scale,
                TintColor,
                Effects,
                ZIndex
            );
        }
        #endregion
    }
}

