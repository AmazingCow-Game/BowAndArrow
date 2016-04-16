#region Usings
//System
using System;
using System.Collections.Generic;
//XNA
using Microsoft.Xna.Framework;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public abstract class Level
    {
        #region Enums / Constants
        public enum State
        {
            Intro,   //
            Playing, //
            Paused,  //
            GameOver //
        }

        private const int kEnemiesHintCount = 20;
        private const int kPapersHintCount  =  3;
        #endregion //Enums / Constants


        #region Public Properties
        public State CurrentState
        { get; protected set; }

        public List<Enemy> Enemies
        { get; protected set; }

        public Archer Player
        { get; protected set; }

        public List<Arrow> PlayerArrows
        { get; protected set; }

        public List<Paper> Papers
        { get; protected set; }
        #endregion //Public Properties


        #region CTOR
        public Level()
        {
            CurrentState = State.Intro;

            //Do the basic initialization.
            //So derived classes don't need to
            //init this basic stuff...
            Enemies      = new List<Enemy>(kEnemiesHintCount);
            PlayerArrows = new List<Arrow>(Archer.kMaxArrowsCount);
            Papers       = new List<Paper>(kPapersHintCount);
        }
        #endregion //CTOR


        #region Update / Draw
        public abstract void Update(GameTime gt);
        public abstract void Draw(GameTime gt);
        #endregion //Update / Draw


        #region Load / Unload
        public abstract void Load();
        public abstract void Unload();
        #endregion //Load / Unload

    }//class Level
}//namespace com.amazingcow.BowAndArrow

