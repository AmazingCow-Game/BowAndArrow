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
        private string _title;
        private string _contents;
        #endregion //iVars

        #region CTOR
        public Paper(string title, string contents) :
            base(Vector2.Zero, Vector2.Zero, 0)
        {
            //Init the iVars...
            _title    = title;
            _contents = contents;

            //Init the Textures...
            var resMgr = ResourcesManager.Instance;
            AliveTexturesList.Add(resMgr.GetTexture("paper"));

            //Set the position to center of screen.
            var viewport = GameManager.Instance.GraphicsDevice.Viewport;

            var x = (viewport.Width / 2) - (BoundingBox.Width / 2);
            var y = (viewport.Height / 2) - (BoundingBox.Height / 2);

            Position = new Vector2(x, y);
        }
        #endregion


        public override void Kill()
        {
            //Do nothing...
        }

    }//class IntroPaper
}//namespace com.amazingcow.BowAndArrow

