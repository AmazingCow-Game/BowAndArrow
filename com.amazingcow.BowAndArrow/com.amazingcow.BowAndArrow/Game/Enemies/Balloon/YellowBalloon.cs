#region Usings
//System
using System;
using System.Collections.Generic;
//Xna
using Microsoft.Xna.Framework;
using System.Security.Cryptography;


#endregion //Usings

namespace com.amazingcow.BowAndArrow
{
    public class YellowBalloon : Balloon
    {
        #region Constants
        public const int kSpeedMinYellonBalloon = 45;
        public const int kSpeedMaxYellowBallon  = 55;
        #endregion

        #region CTOR
        public YellowBalloon(Vector2 position) :
            base(position, Vector2.Zero)
        {
            //Initialize the textures...
            var resMgr = ResourcesManager.Instance;
            AliveTexturesList.Add(resMgr.GetTexture("ballon_yellow"));
            DyingTexturesList.Add(resMgr.GetTexture("ballon_yellow_dead"));

            //Init the Speed...
            int ySpeed = GameManager.Instance.RandomNumGem.Next(kSpeedMinYellonBalloon,
                                                                kSpeedMaxYellowBallon);
            Speed = new Vector2(0, -ySpeed);
        }
        #endregion //CTOR
    }
}

