//----------------------------------------------------------------------------//
//               █      █                                                     //
//               ████████                                                     //
//             ██        ██                                                   //
//            ███  █  █  ███        Level.cs                                  //
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
using System.Collections.Generic;
//XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public abstract class Level
    {
        #region Enums / Constants
        //Public
        public enum State
        {
            Intro,   //
            Playing, //
            Paused,  //
            GameOver //
        }

        //Private
        const int kPaperIndexIntro    =  0;
        const int kPaperIndexGameOver =  1;

        const int kEnemiesHintCount = 20;
        const int kPapersHintCount  =  2;

        const int kInitialPlayerX = 10;
        #endregion //Enums / Constants


        #region Public Properties
        //Window and PlayField bounds
        public Rectangle PlayField
        { get; private set; }

        public Rectangle WindowRect
        {
            get { return GameManager.Instance.GraphicsDevice.Viewport.Bounds; }
        }

        //State
        public State CurrentState
        { get; protected set; }

        //Game Objects
        public List<Enemy> Enemies
        { get; protected set; }

        public Archer Player
        { get; protected set; }

        public List<Arrow> PlayerArrows
        { get; protected set; }

        public List<Paper> Papers
        { get; protected set; }

        public int AliveEnemies
        { get; protected set; }

        public int DyingEnemies
        { get; protected set; }

        public Hud TopHud { get; private set; }

        //Strings
        public abstract String PaperStringIntro    { get; }
        public abstract String PaperStringGameOver { get; }
        public abstract String LevelTitle          { get; }
        public abstract String LevelDescription    { get; }
        #endregion //Public Properties


        #region CTOR
        protected Level()
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


        #region Load / Unload
        public virtual void Load()
        {
            InitHud();

            PlayField = new Rectangle(
                WindowRect.Left,
                WindowRect.Top + TopHud.BoundingBox.Height,
                WindowRect.Right,
                WindowRect.Bottom - TopHud.BoundingBox.Height
            );


            InitPapers ();
            InitPlayer ();
            InitEnemies();
        }
        public virtual void Unload()
        {
            //COWHACK: We need to unsubscribe the events?
        }
        #endregion //Load / Unload


        #region Helper Methods
        protected abstract void LevelCompleted();

        void CheckGameOver()
        {
            if(Player.CurrentState == GameObject.State.Dying &&
               PlayerArrows.Count  == 0 &&
               DyingEnemies        == 0)
            {
                CurrentState = State.GameOver;
            }

            else if(Player.CurrentState == GameObject.State.Dead)
            {
                CurrentState = State.GameOver;
            }
        }
        void CheckVictory()
        {
            if(AliveEnemies == 0)
                LevelCompleted();
        }
        #endregion


        #region Init
        protected virtual void InitPlayer()
        {
            //Initialize the Player.
            int initialPlayerY = PlayField.Center.Y;

            Player = new Archer(new Vector2(kInitialPlayerX, initialPlayerY));
            Player.OnArcherShootArrow += OnPlayerShootArrow;
            Player.OnStateChangeDying += OnPlayerStateChangeDying;
            Player.OnStateChangeDead  += OnPlayerStateChangeDead;
        }

        protected abstract void InitEnemies();

        protected virtual void InitPapers()
        {
            Papers.Add(new Paper(PaperStringIntro));
            Papers.Add(new Paper(PaperStringGameOver));
        }

        protected virtual void InitHud()
        {
            TopHud = new Hud();
        }
        #endregion //Init


        #region Update
        public void Update(GameTime gt)
        {
            switch(CurrentState)
            {
                case State.Intro   : UpdateIntro   (gt); break;
                case State.Playing : UpdatePlaying (gt); break;
                case State.Paused  : UpdatePaused  (gt); break;
                case State.GameOver: UpdateGameOver(gt); break;
            }
        }

        protected virtual void UpdateIntro(GameTime gt)
        {
            //Just waits for a enter press to start the game.
            var prev = InputHandler.Instance.PreviousKeyboardState;
            var curr = InputHandler.Instance.CurrentKeyboardState;

            if(prev.IsKeyDown(Keys.Enter) && curr.IsKeyUp(Keys.Enter))
            {
                CurrentState = State.Playing;
            }
        }

        protected virtual void UpdatePlaying(GameTime gt)
        {
            //Handle the Pause...
            var prev = InputHandler.Instance.PreviousKeyboardState;
            var curr = InputHandler.Instance.CurrentKeyboardState;

            if(prev.IsKeyDown(Keys.Space) && curr.IsKeyUp(Keys.Space))
            {
                CurrentState = State.Paused;
                return;
            }


            //Enemies.
            for(int i = Enemies.Count - 1; i >= 0; --i)
            {
                var enemy = Enemies[i];
                if(enemy.CurrentState == GameObject.State.Dead)
                {
                    Enemies.RemoveAt(i);
                    continue;
                }

                enemy.Update(gt);
            }

            //Arrows.
            for(int i = PlayerArrows.Count - 1; i >= 0; --i)
            {
                var arrow = PlayerArrows[i];
                if(arrow.CurrentState == GameObject.State.Dead)
                {
                    PlayerArrows.RemoveAt(i);
                    continue;
                }

                arrow.Update(gt);
            }

            //Player.
            Player.Update(gt);


            //Check collisions.
            //Enemies -> Arrows
            foreach(var enemy in Enemies)
            {
                foreach(var arrow in PlayerArrows)
                {
                    if(enemy.CheckCollisionArrow(arrow))
                        enemy.Kill();
                }
            }

            //Enemies -> Player
            foreach(var enemy in Enemies)
            {
                if(enemy.CheckCollisionPlayer(Player))
                {
                    enemy.Kill();
                    Player.Hit();
                }
            }

            CheckGameOver();
            CheckVictory();
        }

        protected virtual void UpdatePaused(GameTime gt)
        {
            var prev = InputHandler.Instance.PreviousKeyboardState;
            var curr = InputHandler.Instance.CurrentKeyboardState;

            if(prev.IsKeyDown(Keys.Space) && curr.IsKeyUp(Keys.Space))
                CurrentState = State.Playing;
        }

        protected virtual void UpdateGameOver(GameTime gt)
        {
            var prev = InputHandler.Instance.PreviousKeyboardState;
            var curr = InputHandler.Instance.CurrentKeyboardState;

            if(prev.IsKeyDown(Keys.Enter) && curr.IsKeyUp(Keys.Enter))
            {
                GameManager.Instance.NewGame();
            }
        }
        #endregion //Update


        #region Draw
        public void Draw(GameTime gt)
        {
            switch(CurrentState)
            {
                case State.Intro   : DrawIntro   (gt); break;
                case State.Playing : DrawPlaying (gt); break;
                case State.Paused  : DrawPaused  (gt); break;
                case State.GameOver: DrawGameOver(gt); break;
            }

            DrawHud(gt);
        }

        protected virtual void DrawIntro(GameTime gt)
        {
            Papers[kPaperIndexIntro].Draw(gt);
        }

        protected virtual void DrawPlaying(GameTime gt)
        {
            //Player.
            Player.Draw(gt);

            //Enemies.
            foreach(var enemy in Enemies)
                enemy.Draw(gt);

            //Arrows.
            foreach(var arrow in PlayerArrows)
                arrow.Draw(gt);
        }

        protected virtual void DrawPaused(GameTime gt)
        {
            DrawPlaying(gt);
        }

        protected virtual void DrawGameOver(GameTime gt)
        {
            Papers[kPaperIndexGameOver].Draw(gt);
        }

        protected virtual void DrawHud(GameTime gt)
        {
            TopHud.Draw(gt);
        }
        #endregion // Draw


        #region Game Objects Callbacks
        //Player Callbacks.
        protected virtual void OnPlayerShootArrow(object sender, EventArgs e)
        {
            var arrow = new Arrow(Player.ArrowPosition);
            arrow.OnStateChangeDead += OnArrowStateChangedDead;

            PlayerArrows.Add(arrow);
        }

        protected virtual void OnPlayerStateChangeDying(object sender, EventArgs e)
        {
            Player.OnStateChangeDying -= OnPlayerStateChangeDying;
        }

        protected virtual void OnPlayerStateChangeDead(object sender, EventArgs e)
        {
            Player.OnStateChangeDying -= OnPlayerStateChangeDying;
        }


        //Enemy Callbacks.
        protected virtual void OnEnemyStateChangeDying(object sender, EventArgs e)
        {
            var gameObj = sender as Enemy;
            gameObj.OnStateChangeDying -= OnEnemyStateChangeDying;

            GameManager.Instance.IncrementScore(gameObj.ScoreValue);
            ++DyingEnemies;
        }
        protected virtual void OnEnemyStateChangeDead(object sender, EventArgs e)
        {
            var gameObj = sender as Enemy;
            gameObj.OnStateChangeDead -= OnEnemyStateChangeDead;

            --AliveEnemies;
            --DyingEnemies;
        }

        //Arrow Callbacks.
        protected virtual void OnArrowStateChangedDead(object sender, EventArgs e)
        {
            var arrow = sender as Arrow;
            arrow.OnStateChangeDead -= OnArrowStateChangedDead;
        }
        #endregion //Game Objects Callbacks

    }//class Level
}//namespace com.amazingcow.BowAndArrow

