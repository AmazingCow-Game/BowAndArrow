#region Usings
//System
using System;
//XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Paper : GameObject
    {
        #region iVars
        readonly string     _contents;
        readonly SpriteFont _spriteFont;
        #endregion //iVars

        #region CTOR
        public Paper(string contents) :
            base(Vector2.Zero, Vector2.Zero, 0)
        {
            //Init the iVars.
            _contents = contents;

            //Init the Textures.
            var resMgr = ResourcesManager.Instance;
            AliveTexturesList.Add(resMgr.GetTexture("paper"));
                      
            //Init the Sprite Fonts.
            _spriteFont = resMgr.GetFont("arial");

            var lvl = GameManager.Instance.CurrentLevel;

            var x = lvl.PlayField.Center.X - BoundingBox.Center.X;
            var y = lvl.PlayField.Center.Y - BoundingBox.Center.Y;

            Position = new Vector2(x, y);
        }
        #endregion


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
        
            var sbatch       = GameManager.Instance.CurrentSpriteBatch;
            var paperCenterX = BoundingBox.Center.X;
            var paperTopY    = BoundingBox.Top + 20;

            var splitedString = _contents.Split('\n');
            for(int i = 1; i < splitedString.Length; i++)
            {
                var currStr = splitedString[i];
                var strSize = _spriteFont.MeasureString(currStr);
    
                var pos = new Vector2(paperCenterX - (strSize.X / 2),
                                      paperTopY);

                pos.Y += strSize.Y * i;
                sbatch.DrawString(_spriteFont, currStr, pos, Color.Black);
            }
        }
        #endregion //Draw
              
    }//class Paper
}//namespace com.amazingcow.BowAndArrow

