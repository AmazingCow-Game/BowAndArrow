#region Usings
//System 
using System;
//XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public abstract class Enemy : GameObject
    {
        #region Public Methods 
        public abstract bool CheckCollisionPlayer(Archer archer);
        public abstract bool CheckCollisionArrow(Arrow arrow);
        #endregion //Public Methods 
    }
}

