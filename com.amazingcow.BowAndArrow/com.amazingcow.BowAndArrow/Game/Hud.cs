#region Usings
//System
using System;
//XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Hud : GameObject
    {
        #region iVars
        SpriteFont _spriteFont;
        #endregion //iVars

        #region CTOR
        public Hud() :
            base(Vector2.Zero, Vector2.Zero, 0)
        {
            //Init the Textures.
            var resMgr = ResourcesManager.Instance;
            AliveTexturesList.Add(resMgr.GetTexture("hud"));
                      
            //Init the Sprite Fonts.
            _spriteFont = resMgr.GetFont("coolvetica");
        }
        #endregion //CTOR


        #region Public Methods 
        public override void Kill()
        {
            //Do nothing...
        }
        #endregion //Public Methods


        #region Draw
        public override void Draw(GameTime gt)
        {
            base.Draw(gt);        
        }
        #endregion //Draw
    
    }//class Hud
}//namespace com.amazingcow.BowAndArrow

