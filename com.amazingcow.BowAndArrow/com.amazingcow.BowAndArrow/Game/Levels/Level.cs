using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using OpenTK.Platform.MacOS;

namespace com.amazingcow.BowAndArrow
{
    public abstract class Level
    {
        public enum State 
        {
            Intro,
            Playing,
            Paused,
            GameOver
        }


        #region Public Properties 
        public State        CurrentState { get; protected set; }
        public List<Enemy>  Enemies      { get; protected set; }
        public Archer       Player       { get; protected set; }
        public List<Arrow>  PlayerArrows { get; protected set; }
        #endregion //Public Properties 


        public Level()
        {
            CurrentState = State.Intro;
            Enemies      = new List<Enemy>();
            Player       = new Archer();
            PlayerArrows = new List<Arrow>();
        }


        #region Update / Draw 
        public abstract void Update(GameTime gt);
        public abstract void Draw(GameTime gt);
        #endregion //Update / Draw 


        #region Load / Unload
        public abstract void Load();
        public abstract void Unload();
        #endregion //Load / Unload
    }
}

