#region Usings
//System
using System;
//XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
#endregion //Usings

//COWTODO: Check if there is a need of implement a more efficient way \
//         of draw the strings...
namespace com.amazingcow.BowAndArrow
{
    public class Hud : GameObject
    {
        #region Constants
        const int kPaddingToBackground = 6;
        #endregion //Constants


        #region iVars
        Level      _lvl;
        SpriteFont _spriteFont;
        Texture2D  _littleArrowTexture;
        Texture2D  _hudBackgroundTexture;
        #endregion //iVars


        #region CTOR
        public Hud() :
            base(Vector2.Zero, Vector2.Zero, 0)
        {
            _lvl = GameManager.Instance.CurrentLevel;

            //Init the Textures.
            var resMgr = ResourcesManager.Instance;

            _hudBackgroundTexture = resMgr.GetTexture("hud");
            _littleArrowTexture   = resMgr.GetTexture("little_arrow");

            AliveTexturesList.Add(_hudBackgroundTexture);
                        
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

            var sb     = GameManager.Instance.CurrentSpriteBatch;
            var bounds = _hudBackgroundTexture.Bounds;

            DrawLevelTitle      (sb, bounds);
            DrawLevelDescription(sb, bounds);
            DrawScore           (sb, bounds);
            DrawHighScore       (sb, bounds);
            DrawArrowsCount     (sb, bounds);
            DrawArrowsIcons     (sb, bounds);
        }


        void DrawLevelTitle(SpriteBatch sb, Rectangle bounds)
        {
            var name = _lvl.LevelTitle;
            var size = _spriteFont.MeasureString(name);
            var pos  =  new Vector2(bounds.Center.X - (size.X / 2),
                                    bounds.Top + kPaddingToBackground);

            sb.DrawString(_spriteFont, name, pos, Color.Black);
        }

        void DrawLevelDescription(SpriteBatch sb, Rectangle bounds)
        {
            var desc = _lvl.LevelDescription;
            var size = _spriteFont.MeasureString(desc);
            var pos  =  new Vector2(bounds.Center.X - (size.X / 2),
                                    bounds.Bottom - size.Y - kPaddingToBackground);

            sb.DrawString(_spriteFont, desc, pos, Color.Black);
        }

        void DrawScore(SpriteBatch sb, Rectangle bounds)
        {
            var score = String.Format("Score: {0}",
                                      GameManager.Instance.HighScore);

            var pos   = new Vector2(bounds.Left + kPaddingToBackground, 
                                    bounds.Top  + kPaddingToBackground);

            sb.DrawString(_spriteFont, score, pos, Color.Black);
        }

        void DrawHighScore(SpriteBatch sb, Rectangle bounds)
        {
            var score = String.Format("High Score: {0}", 
                                      GameManager.Instance.HighScore);

            var size  = _spriteFont.MeasureString(score);
            var pos   = new Vector2(bounds.Left   + kPaddingToBackground, 
                                    bounds.Bottom - kPaddingToBackground - size.Y);

            sb.DrawString(_spriteFont, score, pos, Color.Black);
        }

        void DrawArrowsCount(SpriteBatch sb, Rectangle bounds)
        {
            var count = String.Format("Arrows: {0}", _lvl.Player.ArrowsCount);
            var size  = _spriteFont.MeasureString(count);
            var pos   = new Vector2(bounds.Right - size.X - kPaddingToBackground, 
                                    bounds.Top   + kPaddingToBackground);

            sb.DrawString(_spriteFont, count, pos, Color.Black);
        }

        void DrawArrowsIcons(SpriteBatch sb, Rectangle bounds)
        {
            var size = _littleArrowTexture.Bounds;

            for(int i = _lvl.Player.ArrowsCount; i > 0; --i)
            {
                var x = (bounds.Right - kPaddingToBackground) - (i * size.Width);
                var y = bounds.Bottom - kPaddingToBackground - size.Height;

                sb.Draw(_littleArrowTexture, new Vector2(x, y), Color.White);
            }
        }
        #endregion //Draw
              
    }//class Hud
}//namespace com.amazingcow.BowAndArrow

