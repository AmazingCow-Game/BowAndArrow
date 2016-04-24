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
        public enum State
        {
            Intro,   //
            Playing, //
            Paused,  //
            GameOver //
        }

        protected const int kPaperIndexIntro    =  0;
        protected const int kPaperIndexPaused   =  1;
        protected const int kPaperIndexGameOver =  2;

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

        public int AliveEnemies
        { get; protected set; }
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
            InitEnemies();
            InitPlayer ();
            InitPapers ();
        }
        public virtual void Unload()
        {
            //COWTODO: We need to unsubscribe the events?
        }
        #endregion //Load / Unload


        #region Helper Methods
        protected abstract void LevelCompleted();

        private void CheckGameOver()
        {
            if(Player.CurrentState == GameObject.State.Dying &&
               Player.ArrowsCount  == 0 &&
               PlayerArrows.Count  == 0)
            {
                CurrentState = State.GameOver;
            }
            else if(Player.CurrentState == GameObject.State.Dead)
            {
                CurrentState = State.GameOver;
            }
        }
        private void CheckVictory()
        {
            if(AliveEnemies == 0)
                LevelCompleted();
        }
        #endregion


        #region Init
        protected virtual void  InitPlayer()
        {
            var viewport = GameManager.Instance.GraphicsDevice.Viewport;

            //Initialize the Player.
            int initialPlayerX = 100; //COWTODO: Remove the magic constants.
            int initialPlayerY = viewport.Height / 2;

            Player = new Archer(new Vector2(initialPlayerX, initialPlayerY));
            Player.OnArcherShootArrow += OnPlayerShootArrow;
            Player.OnStateChangeDying += OnPlayerStateChangeDying;
        }

        protected abstract void InitEnemies();
        protected abstract void InitPapers ();
        #endregion //Init


        #region Update
        public void Update(GameTime gt)
        {
            //COWTODO: Remove this
            if(InputHandler.Instance.CurrentKeyboardState.IsKeyDown(Keys.Enter))
                AliveEnemies = 0;

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
            //Just waits for a space press to start the game.
            var prev = InputHandler.Instance.PreviousMouseState;
            var curr = InputHandler.Instance.CurrentMouseState;

            if(prev.LeftButton == ButtonState.Pressed &&
               curr.LeftButton == ButtonState.Pressed)
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
            Papers[kPaperIndexPaused].Draw(gt);
        }

        protected virtual void DrawGameOver(GameTime gt)
        {
            Papers[kPaperIndexGameOver].Draw(gt);
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

        //Enemy Callbacks.
        protected virtual void OnEnemyStateChangeDying(object sender, EventArgs e)
        {
            var gameObj = sender as Enemy;
            gameObj.OnStateChangeDying -= OnEnemyStateChangeDying;
        }
        protected virtual void OnEnemyStateChangeDead(object sender, EventArgs e)
        {
            var gameObj = sender as Enemy;
            gameObj.OnStateChangeDead -= OnEnemyStateChangeDead;

            --AliveEnemies;
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

