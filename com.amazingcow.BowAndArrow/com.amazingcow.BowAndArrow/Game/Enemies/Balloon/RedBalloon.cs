#region Usings
//System
using System;
using System.Collections.Generic;
//Xna
using Microsoft.Xna.Framework;
#endregion //Usings

namespace com.amazingcow.BowAndArrow
{
    public class RedBalloon : Balloon
    {
        #region Constants 
        public const int kSpeedRedBalloon = -40;
        #endregion


        #region CTOR
        public RedBalloon() : 
            base()
        {
            //Init the Sprites.
            _spriteList.Add(new Sprite("ballon"));
            _spriteList.Add(new Sprite("ballon_dead"));

            CurrentSprite = _spriteList[kSpriteIndexAlive];

            //Init the Speed.
            Speed = new Vector2(0, kSpeedRedBalloon);
        }
        #endregion //CTOR
    }
}

