#region Usings
//System
using System;
using System.IO;
//XNA
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class GameManager : Game
    {
        #region Constants 
        //Private
        readonly Color kBackgroundColor = new Color(0, 128, 0);
        #endregion

        #region iVars
        GraphicsDeviceManager _graphics;
        Color                 _clearColor;
        #endregion //iVars


        #region Public Properties
        public SpriteBatch CurrentSpriteBatch { get; private set; }
        public Level       CurrentLevel       { get; private set; }
        public Random      RandomNumGen       { get; private set; }
        public int         CurrentScore       { get; private set; }
        public int         HighScore          { get; private set; }
        #endregion //Public Properties


        #region Singleton
        static GameManager s_instance;
        public static GameManager Instance
        {
            get { 
                if(s_instance == null)
                    s_instance = new GameManager();
                return s_instance; 
            }
        }
        #endregion //Singleton


        #region CTOR
        GameManager()
        {
            //Init the iVars...
            _clearColor   = kBackgroundColor;
            _graphics     = new GraphicsDeviceManager(this);

            //Init the Properties...
            Content.RootDirectory = "Content";
            //COWTODO: In next version let the user pass the \
            //         seed from command line.
            RandomNumGen          = new Random(); 
            IsMouseVisible        = true;
            IsFixedTimeStep       = true;

            //Setup the graphics...
            //COWTODO: In the next version let the user select \
            //         the screen resolution.
            _graphics.PreferredBackBufferWidth  = 640;
            _graphics.PreferredBackBufferHeight = 480;

            LoadHighScore();
        }
        #endregion //CTOR


        #region Init / Load
        protected override void LoadContent()
        {
            CurrentSpriteBatch = new SpriteBatch(GraphicsDevice);
            NewGame();
        }
        protected override void OnExiting(Object sender, EventArgs args)
        {
            base.OnExiting(sender, args);

            SaveHighScore();
        }
        #endregion //Init / Load


        #region Update / Draw
        protected override void Update(GameTime gameTime)
        {            
            if(Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputHandler.Instance.Update();
            CurrentLevel.Update(gameTime);

            //To ease the development...
            #if DEBUG
                var k = InputHandler.Instance.CurrentKeyboardState;
                if(k.IsKeyDown(Keys.F1)) ChangeLevel(new Level1());
                if(k.IsKeyDown(Keys.F2)) ChangeLevel(new Level2());
                if(k.IsKeyDown(Keys.F3)) ChangeLevel(new Level3());
                if(k.IsKeyDown(Keys.F4)) ChangeLevel(new Level4());
                if(k.IsKeyDown(Keys.F5)) ChangeLevel(new Level5());
                if(k.IsKeyDown(Keys.F6)) ChangeLevel(new Level6());
                if(k.IsKeyDown(Keys.F7)) ChangeLevel(new Level7());
                if(k.IsKeyDown(Keys.F8)) ChangeLevel(new Level8());
                if(k.IsKeyDown(Keys.F9)) ChangeLevel(new LevelCredits());
            #endif

            //In development time we want visual clues that 
            //the game is slow.
            #if DEBUG
                _clearColor = (gameTime.IsRunningSlowly) 
                               ? Color.Red
                               : kBackgroundColor;
            #endif

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
        #endregion // Update / Draw


        #region Level Management
        public void NewGame()
        {
            CurrentScore = 0;
            ChangeLevel(new Level1());
        }

        public void ChangeLevel(Level level)
        {
            if(CurrentLevel != null)
                CurrentLevel.Unload();

            CurrentLevel = level;
            CurrentLevel.Load();
        }
        #endregion //Level Management


        #region Score
        void LoadHighScore()
        {
            try {
                var contents = File.ReadAllText(GetWriteableScorePath());
                HighScore = int.Parse(contents);
            } 
            catch (Exception) {
                HighScore = 0;
            }
        }
        void SaveHighScore()
        {
            try {
                File.WriteAllText(GetWriteableScorePath(), HighScore.ToString());
            }
            catch(Exception) {
                //Do nothing.
            }
        }

        public void IncrementScore(int value)
        {
            CurrentScore += value;
            if(CurrentScore > HighScore)
                HighScore = CurrentScore;
        }
        #endregion //Score


        #region Filesystem
        String GetWriteableScorePath()
        {
            var specialFolder = Environment.SpecialFolder.LocalApplicationData;
            var path          = Environment.GetFolderPath(specialFolder);

            var fullFolderPath = Path.Combine(path, "cow_bowandarrow");
            var fullFilePath   = Path.Combine(fullFolderPath, "score.txt");

            Directory.CreateDirectory(fullFolderPath);

            return fullFilePath;
        }
        #endregion //FileSystem
    }
}

