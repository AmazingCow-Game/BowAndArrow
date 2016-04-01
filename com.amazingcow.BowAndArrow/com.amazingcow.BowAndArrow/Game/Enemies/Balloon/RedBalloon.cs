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
        public RedBalloon(Vector2 position) :
            base(position, new Vector2(0, kSpeedRedBalloon))
        {
            //Initialize the textures...
            var resMgr = ResourcesManager.Instance;
            AliveTexturesList.Add(resMgr.GetTexture("ballon"));
            DyingTexturesList.Add(resMgr.GetTexture("ballon_dead"));
        }
        #endregion //CTOR
    }
}

