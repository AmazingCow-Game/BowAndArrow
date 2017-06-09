//----------------------------------------------------------------------------//
//               █      █                                                     //
//               ████████                                                     //
//             ██        ██                                                   //
//            ███  █  █  ███        GameManager.cs                            //
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
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
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
        //Public
        //COWTODO: We need a better way to specify the version number \
        //         in the command line. Maybe a define...
        public const String kVersion   = "1.2.0";
        public const String kCopyright = "Copyright (c) Amazing Cow\n 2016, 2017";
        public const String kGameName  = "Bow & Arrow - Amazing Cow Labs";
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
            _clearColor           = kBackgroundColor;
            IsMouseVisible        = true;
            IsFixedTimeStep       = true;
            Content.RootDirectory = ResourcesManager.FindContentDirectoryPath();

            //COWTODO: In next version let the user pass the \
            //         seed from command line.
            RandomNumGen = new Random();

            //Setup the graphics...
            //COWTODO: In the next version let the user select \
            //         the screen resolution.
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth  = 640;
            _graphics.PreferredBackBufferHeight = 480;

            //Set the Window Caption.
            Window.Title = String.Format(
                "{0} - {1}",
                kGameName,
                kVersion
            );

            LoadHighScore();
        }
        #endregion //CTOR


        #region Init / Load
        protected override void LoadContent()
        {
            CurrentSpriteBatch = new SpriteBatch(GraphicsDevice);
            NewGame();
        }
        #endregion //Init / Load


        #region Update / Draw
        protected override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Escape))
                ExitGame();

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

        #region Exit
        private void ExitGame()
        {
            Environment.Exit(0);
        }

        protected override void OnExiting(Object sender, EventArgs args)
        {
            SaveHighScore();
            base.OnExiting(sender, args);

            ExitGame();
        }
        #endregion //Exit


        #region Score
        void LoadHighScore()
        {
            var path = GetWriteableScorePath();
            try {
                var contents = File.ReadAllText(path);
                HighScore = int.Parse(contents);
            }
            catch (Exception) {
                Debug.WriteLine("Cannot load highscore at:({0})", path);
                HighScore = 0;
            }
        }

        void SaveHighScore()
        {
            var path = GetWriteableScorePath();
            try {
                File.WriteAllText(path, HighScore.ToString());
            }
            catch(Exception) {
                Debug.WriteLine("Cannot save highscore at:({0})", path);
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

