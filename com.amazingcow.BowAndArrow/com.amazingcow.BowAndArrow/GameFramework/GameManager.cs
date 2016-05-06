#region Usings
//System
using System;
using System.Diagnostics;
//XNA
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class GameManager : Game
    {
        #region iVars
        GraphicsDeviceManager _graphics;
        Color                 _clearColor;
        #endregion //iVars


        #region Public Properties
        public SpriteBatch CurrentSpriteBatch { get; private set; }
        public Level       CurrentLevel       { get; private set; }
        public Random      RandomNumGen       { get; private set; }
        #endregion //Public Properties


        #region Singleton
        static GameManager s_instance;
        public static GameManager Instance
        {
            get { return s_instance; }
        }
        #endregion //Singleton


        #region CTOR
        public GameManager()
        {
            //Init the iVars...
            _clearColor   = Color.CornflowerBlue;
            _graphics     = new GraphicsDeviceManager(this);

            //Init the Properties...
            Content.RootDirectory = "Content";
            RandomNumGen          = new Random(); //COWTODO:
            IsMouseVisible        = true;
            IsFixedTimeStep       = true;

            //COWTODO: Make a "real" Singleton.
            s_instance = this;

            //Setup the graphics...
            _graphics.PreferredBackBufferWidth  = 640; //COWTODO:
            _graphics.PreferredBackBufferHeight = 480; //COWTODO:
        }
        #endregion //CTOR


        #region Init / Load
        protected override void LoadContent()
        {
            CurrentSpriteBatch = new SpriteBatch(GraphicsDevice);
            ChangeLevel(new Level1());
        }
        #endregion //Init / Load


        #region Update / Draw
        protected override void Update(GameTime gameTime)
        {
            //COWTODO: Implement the correctly handling...
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            InputHandler.Instance.Update();

            CurrentLevel.Update(gameTime);

            //COWTODO: Implement the correctly handling...
            if(gameTime.IsRunningSlowly)
                _clearColor = new Color(128, 0, 128);
            else
                _clearColor = new Color(0, 128, 0);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(_clearColor);

            CurrentSpriteBatch.Begin(SpriteSortMode.Deferred);
                CurrentLevel.Draw(gameTime);
            CurrentSpriteBatch.End();

            base.Draw(gameTime);

        }
        #endregion // Update/Draw


        #region Level Management
        public void ChangeLevel(Level level)
        {
            if(CurrentLevel != null)
                CurrentLevel.Unload();

            CurrentLevel = level;
            CurrentLevel.Load();
        }
        #endregion //Level Management
    }
}

