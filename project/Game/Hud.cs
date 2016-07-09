//----------------------------------------------------------------------------//
//               █      █                                                     //
//               ████████                                                     //
//             ██        ██                                                   //
//            ███  █  █  ███        Hud.cs                                    //
//            █ █        █ █        Game_BowAndArrow                          //
//             ████████████                                                   //
//           █              █       Copyright (c) 2016                        //
//          █     █    █     █      AmazingCow - www.AmazingCow.com           //
//          █     █    █     █                                                //
//           █              █       N2OMatt - n2omatt@amazingcow.com          //
//             ████████████         www.amazingcow.com/n2omatt                //
//                                                                            //
//                  This software is licensed as GPLv3                        //
//                 CHECK THE COPYING FILE TO MORE DETAILS                     //
//                                                                            //
//    Permission is granted to anyone to use this software for any purpose,   //
//   including commercial applications, and to alter it and redistribute it   //
//               freely, subject to the following restrictions:               //
//                                                                            //
//     0. You **CANNOT** change the type of the license.                      //
//     1. The origin of this software must not be misrepresented;             //
//        you must not claim that you wrote the original software.            //
//     2. If you use this software in a product, an acknowledgment in the     //
//        product IS HIGHLY APPRECIATED, both in source and binary forms.     //
//        (See opensource.AmazingCow.com/acknowledgment.html for details).    //
//        If you will not acknowledge, just send us a email. We'll be         //
//        *VERY* happy to see our work being used by other people. :)         //
//        The email is: acknowledgment_opensource@AmazingCow.com              //
//     3. Altered source versions must be plainly marked as such,             //
//        and must not be misrepresented as being the original software.      //
//     4. This notice may not be removed or altered from any source           //
//        distribution.                                                       //
//     5. Most important, you must have fun. ;)                               //
//                                                                            //
//      Visit opensource.amazingcow.com for more open-source projects.        //
//                                                                            //
//                                  Enjoy :)                                  //
//----------------------------------------------------------------------------//
#region Usings
//System
using System;
//XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion //Usings

//COWHACK: Check if there is a need of implement a more efficient way \
//         of draw the strings...
namespace com.amazingcow.BowAndArrow
{
    public class Hud : GameObject
    {
        #region Constants
        const int kPaddingToBackground = 6;
        const int kBackgroundHeight    = 46;
        #endregion //Constants


        #region Properties
        public override Rectangle BoundingBox
        {
            get {
                return new Rectangle((int)Position.X,
                                     (int)Position.Y,
                                     _lvl.WindowRect.Width,
                                     kBackgroundHeight);
            }
        }
        #endregion //Public Properties


        #region iVars
        Level      _lvl;
        SpriteFont _spriteFont;
        Texture2D  _littleArrowTexture;

        Texture2D _hudLeft;
        Texture2D _hudCenter;
        Texture2D _hudRight;
        #endregion //iVars


        #region CTOR
        public Hud() :
            base(Vector2.Zero, Vector2.Zero, 0)
        {
            _lvl = GameManager.Instance.CurrentLevel;

            //Init the Textures.
            var resMgr = ResourcesManager.Instance;

            _hudLeft   = resMgr.GetTexture("hud_left");
            _hudCenter = resMgr.GetTexture("hud_center");
            _hudRight  = resMgr.GetTexture("hud_right");

            _littleArrowTexture   = resMgr.GetTexture("little_arrow");

            //Init the Sprite Fonts.
            _spriteFont = resMgr.GetFont("arial");
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
            var sb = GameManager.Instance.CurrentSpriteBatch;

            DrawBackground(sb);
            DrawLevelTitle(sb);

            switch(_lvl.CurrentState)
            {
                case Level.State.Intro    : DrawIntroMessage    (sb); break;
                case Level.State.Playing  : DrawLevelDescription(sb); break;
                case Level.State.Paused   : DrawPauseMessage    (sb); break;
                case Level.State.GameOver : DrawGameOverMessage (sb); break;
            }

            DrawScore    (sb);
            DrawHighScore(sb);

            DrawArrowsCount(sb);
            DrawArrowsIcons(sb);
        }
        #endregion //Draw


        #region Draw Background
        void DrawBackground(SpriteBatch sb)
        {
            //Left
            sb.Draw(_hudLeft, new Vector2(BoundingBox.Left, BoundingBox.Top));
            //Center
            sb.Draw(_hudCenter,
                    new Vector2(_hudLeft.Width, BoundingBox.Top),
                    null,
                    null,
                    null,
                    0,
                    new Vector2(BoundingBox.Width - _hudRight.Width, 1),
                    null,
                    SpriteEffects.None,
                    0);
            //Right
            sb.Draw(_hudRight,
                     new Vector2(BoundingBox.Right - _hudRight.Width,
                                 BoundingBox.Top));
        }
        #endregion //Draw Background


        #region Draw Title / Description
        void DrawLevelTitle(SpriteBatch sb)
        {
            var name = _lvl.LevelTitle;
            var size = _spriteFont.MeasureString(name);
            var pos  =  new Vector2(BoundingBox.Center.X - (size.X / 2),
                                    BoundingBox.Top + kPaddingToBackground);

            sb.DrawString(_spriteFont, name, pos, Color.Black);
        }

        void DrawLevelDescription(SpriteBatch sb)
        {
            var desc = _lvl.LevelDescription;
            var size = _spriteFont.MeasureString(desc);
            var pos  =  new Vector2(BoundingBox.Center.X - (size.X / 2),
                                    BoundingBox.Bottom - size.Y - kPaddingToBackground);

            sb.DrawString(_spriteFont, desc, pos, Color.Black);
        }
        #endregion //Draw Title / Description


        #region Draw States Message
        void DrawIntroMessage(SpriteBatch sb)
        {
            var desc = "Press [Enter] to Play!";
            var size = _spriteFont.MeasureString(desc);
            var pos  =  new Vector2(BoundingBox.Center.X - (size.X / 2),
                                    BoundingBox.Bottom - size.Y - kPaddingToBackground);

            sb.DrawString(_spriteFont, desc, pos, Color.Black);
        }

        void DrawPauseMessage(SpriteBatch sb)
        {
            var desc = "PAUSED - Press [Space] to Resume!";
            var size = _spriteFont.MeasureString(desc);
            var pos  =  new Vector2(BoundingBox.Center.X - (size.X / 2),
                                    BoundingBox.Bottom - size.Y - kPaddingToBackground);

            sb.DrawString(_spriteFont, desc, pos, Color.Black);
        }

        void DrawGameOverMessage(SpriteBatch sb)
        {
            var desc = "GAME OVER - Press [Enter] to Start Again!";
            var size = _spriteFont.MeasureString(desc);
            var pos  =  new Vector2(BoundingBox.Center.X - (size.X / 2),
                                    BoundingBox.Bottom - size.Y - kPaddingToBackground);

            sb.DrawString(_spriteFont, desc, pos, Color.Black);
        }
        #endregion //Draw States Message


        #region Draw Score / High Score
        void DrawScore(SpriteBatch sb)
        {
            var score = String.Format("Score: {0}",
                                      GameManager.Instance.CurrentScore);

            var pos   = new Vector2(BoundingBox.Left + kPaddingToBackground,
                                    BoundingBox.Top  + kPaddingToBackground);

            sb.DrawString(_spriteFont, score, pos, Color.Black);
        }

        void DrawHighScore(SpriteBatch sb)
        {
            var score = String.Format("High Score: {0}",
                                      GameManager.Instance.HighScore);

            var size  = _spriteFont.MeasureString(score);
            var pos   = new Vector2(BoundingBox.Left   + kPaddingToBackground,
                                    BoundingBox.Bottom - kPaddingToBackground - size.Y);

            sb.DrawString(_spriteFont, score, pos, Color.Black);
        }
        #endregion //Draw Score / High Score


        #region Draw Arrows Info
        void DrawArrowsCount(SpriteBatch sb)
        {
            var count = String.Format("Arrows: {0}", _lvl.Player.ArrowsCount);
            var size  = _spriteFont.MeasureString(count);
            var pos   = new Vector2(BoundingBox.Right - size.X - kPaddingToBackground,
                                    BoundingBox.Top   + kPaddingToBackground);

            sb.DrawString(_spriteFont, count, pos, Color.Black);
        }

        void DrawArrowsIcons(SpriteBatch sb)
        {
            var size = _littleArrowTexture.Bounds;

            for(int i = _lvl.Player.ArrowsCount; i > 0; --i)
            {
                var x = (BoundingBox.Right - kPaddingToBackground) - (i * size.Width);
                var y = BoundingBox.Bottom - kPaddingToBackground - size.Height;

                sb.Draw(_littleArrowTexture, new Vector2(x, y), Color.White);
            }
        }
        #endregion //Draw Arrows Info

    }//class Hud
}//namespace com.amazingcow.BowAndArrow

