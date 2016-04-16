#region Usings
//System
using System;
using System.Diagnostics;
//Xna
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion //Usings

namespace com.amazingcow.BowAndArrow
{
    public class Level1 : Level
    {
        #region Constants
        //COWTODO: Check the correct values.
        private const int kMaxBalloonsCount   = 15;
        private const int kMaxArrowsCount     = 15;
        private const int kPaperIndexIntro    =  0;
        private const int kPaperIndexPaused   =  1;
        private const int kPaperIndexGameOver =  2;
        #endregion //Constants

        Action mouseLeftClick;

        #region CTOR
        public Level1() :
            base()
        {
            ActionStep step = new ActionStep();
            step.Button =1;
            step.State = ButtonState.Pressed;

            ActionStep step2 = new ActionStep();
            step2.Button =1;
            step2.State = ButtonState.Released;

            mouseLeftClick = new Action();
            mouseLeftClick.Steps.Add(step);
            mouseLeftClick.Steps.Add(step2);
            mouseLeftClick.OnTrigger += MouseLeftClick_OnTrigger;


            InputHandler.Instance.Actions.Add(mouseLeftClick);
        }

        void MouseLeftClick_OnTrigger (object sender, EventArgs e)
        {
            Debug.WriteLine("Mouse 1 clicked");
        }

        #endregion //CTOR


        #region Update
        public override void Update(GameTime gt)
        {
            switch(CurrentState)
            {
                case State.Intro   : UpdateIntro   (gt); break;
                case State.Playing : UpdatePlaying (gt); break;
                case State.Paused  : UpdatePaused  (gt); break;
                case State.GameOver: UpdateGameOver(gt); break;
            }
        }

        private void UpdateIntro(GameTime gt)
        {
            //COWTODO: Make the mouse click.
            var mouseState = Mouse.GetState();
            if(mouseState.LeftButton == ButtonState.Pressed)
                CurrentState = State.Playing;
        }

        private void UpdatePlaying(GameTime gt)
        {
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


            //Check collision.
            foreach(var enemy in Enemies)
            {
                foreach(var arrow in PlayerArrows)
                {
                    if(enemy.CheckCollisionArrow(arrow))
                        enemy.Kill();
                }
            }

            //Player.
            Player.Update(gt);
        }

        private void UpdatePaused(GameTime gt)
        {

        }

        private void UpdateGameOver(GameTime gt)
        {

        }
        #endregion //Update


        #region Draw
        public override void Draw(GameTime gt)
        {
            switch(CurrentState)
            {
                case State.Intro   : DrawIntro   (gt); break;
                case State.Playing : DrawPlaying (gt); break;
                case State.Paused  : DrawPaused  (gt); break;
                case State.GameOver: DrawGameOver(gt); break;
            }
        }

        private void DrawIntro(GameTime gt)
        {
            Papers[kPaperIndexIntro].Draw(gt);
        }

        private void DrawPlaying(GameTime gt)
        {
            //Player.
            Player.Draw(gt);

            //Enemies
            foreach(var enemy in Enemies)
                enemy.Draw(gt);

            //Arrows
            foreach(var arrow in PlayerArrows)
                arrow.Draw(gt);
        }

        private void DrawPaused(GameTime gt)
        {
            DrawPlaying(gt);
            Papers[kPaperIndexPaused].Draw(gt);
        }

        private void DrawGameOver(GameTime gt)
        {
            Papers[kPaperIndexGameOver].Draw(gt);
        }
        #endregion // Draw


        #region Game Objects Callbacks
        void OnPlayerShootArrow(object sender, EventArgs e)
        {
            var arrow = new Arrow(Player.ArrowPosition);
            arrow.OnStateChangeDead += OnArrowStateChangedDead;

            PlayerArrows.Add(arrow);

            //Debug.WriteLine("Arrows Count: {0}", Player.ArrowsCount);
        }
        void OnPlayerStateChangeDying (object sender, EventArgs e)
        {
            Player.OnStateChangeDying -= OnPlayerStateChangeDying;
        }


        void OnEnemyStateChangeDying(object sender, EventArgs e)
        {
            var gameObj = sender as Enemy;
            gameObj.OnStateChangeDying -= OnEnemyStateChangeDying;
        }

        void OnEnemyStateChangeDead(object sender, EventArgs e)
        {
            var gameObj = sender as Enemy;
            gameObj.OnStateChangeDead -= OnEnemyStateChangeDead;
        }

        void OnArrowStateChangedDead(object sender, EventArgs e)
        {
            var arrow = sender as Arrow;
            arrow.OnStateChangeDead -= OnArrowStateChangedDead;
        }
        #endregion //Game Objects Callbacks


        #region Load / Unload
        public override void Load()
        {
            InitEnemies();
            InitPlayer ();
            InitPapers ();
        }
        public override void Unload()
        {

        }
        #endregion //Load / Unload


        #region Init
        private void InitEnemies()
        {
            var viewport = GameManager.Instance.GraphicsDevice.Viewport;

            //Initialize the Enemies.
            int initialBalloonX = viewport.Width  / 2;
            int initialBalloonY = viewport.Height / 2;

            for(int i = 0; i < kMaxBalloonsCount; ++i)
            {
                var x = initialBalloonX + (Balloon.kBalloonWidth * i);

                var balloon = new RedBalloon(new Vector2(x, initialBalloonY));
                balloon.OnStateChangeDead  += OnEnemyStateChangeDead;
                balloon.OnStateChangeDying += OnEnemyStateChangeDying;

                Enemies.Add(balloon);
            }
        }

        private void InitPlayer()
        {
            var viewport = GameManager.Instance.GraphicsDevice.Viewport;

            //Initialize the Player.
            int initialPlayerX = 100;
            int initialPlayerY = viewport.Height / 2;

            Player = new Archer(new Vector2(initialPlayerX, initialPlayerY));
            Player.OnArcherShootArrow += OnPlayerShootArrow;
            Player.OnStateChangeDying += OnPlayerStateChangeDying;
        }

        private void InitPapers()
        {
            Papers.Add(new Paper("Intro", ""));
            Papers.Add(new Paper("Paused", ""));
            Papers.Add(new Paper("GameOver", ""));
        }

        #endregion //Init


    }
}

