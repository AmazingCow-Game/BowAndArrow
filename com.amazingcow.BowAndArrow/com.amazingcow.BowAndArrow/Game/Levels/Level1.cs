#region Usings
//System
using System;
using System.Diagnostics;
//Xma
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class Level1 : Level
    {
        public Level1() :
            base()
        {
            for(int i = 10; i < 20; ++i)
            {
                var balloon = new RedBalloon(new Vector2(i * 30, 100));
                balloon.OnStateChangeDead  += OnEnemyStateChangeDead;
                balloon.OnStateChangeDying += OnEnemyStateChangeDying;

                Enemies.Add(balloon);
            }

            Player = new Archer(new Vector2(100, 100));
        }

        void OnEnemyStateChangeDying(object sender, EventArgs e)
        {
            var gameObj = sender as Enemy;
            gameObj.OnStateChangeDying -= OnEnemyStateChangeDying;

            Debug.WriteLine("Enemy ChangeState - Dying");
        }

        void OnEnemyStateChangeDead(object sender, EventArgs e)
        {
            var gameObj = sender as Enemy;
            gameObj.OnStateChangeDead -= OnEnemyStateChangeDead;

            Debug.WriteLine("Enemy ChangeState - Dead");
        }

        void OnArrowStateChangedDead(object sender, EventArgs e)
        {
            var arrow = sender as Arrow;
            arrow.OnStateChangeDead -= OnArrowStateChangedDead;

            Debug.WriteLine("Arrow ChangeState - Dead - {0}", PlayerArrows.Count);
        }


        #region Update / Draw
        public override void Update(GameTime gt)
        {
            var state = Mouse.GetState();
            var pressed = state.LeftButton == ButtonState.Pressed;
            var pos = new Vector2(state.X, state.Y);

//            if(pressed)
//            {
//                var arrow = new Arrow(pos);
//                arrow.OnStateChangeDead += OnArrowStateChangedDead;
//                PlayerArrows.Add(arrow);
//            }

            //Enemies
            for(int i = Enemies.Count - 1; i >= 0; --i)
            {
                var enemy = Enemies[i];
                if(enemy.CurrentState == GameObject.State.Dead)
                {
                    Enemies.RemoveAt(i);
                    break;
                }

                enemy.Update(gt);
            }

            //Arrows
            for(int i = PlayerArrows.Count - 1; i >= 0; --i)
            {
                var arrow = PlayerArrows[i];
                if(arrow.CurrentState == GameObject.State.Dead)
                {
                    PlayerArrows.RemoveAt(i);
                    break;
                }

                arrow.Update(gt);
            }

            foreach(var enemy in Enemies)
            {
                foreach(var arrow in PlayerArrows)
                {
                    if(enemy.CheckCollisionArrow(arrow))
                        enemy.Kill();
                }

                //COWTODO: Player hit.
            }

            Player.Update(gt);
        }
        public override void Draw(GameTime gt)
        {
            //Enemies
            foreach(var enemy in Enemies)
                enemy.Draw(gt);

            //Arrows
            foreach(var arrow in PlayerArrows)
                arrow.Draw(gt);

            //Player.
            Player.Draw(gt);
        }
        #endregion //Update / Draw


        #region Load / Unload
        public override void Load()
        {

        }
        public override void Unload()
        {

        }
        #endregion //Load / Unload
    }
}

