using System;
using Microsoft.Xna.Framework;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework.Input;

namespace com.amazingcow.BowAndArrow
{
    public class Level1 : Level
    {
        public Level1() : 
            base()
        {
            
            for(int i = 10; i < 20; ++i)
            {
                Enemies.Add(new RedBalloon() {
                        Position = new Vector2(i * 30, 100)
                });
            }

            for(int i = 20; i < 30; ++i)
            {
                Enemies.Add(new YellowBalloon() {
                        Position = new Vector2(i * 30, 100)
                });
            }
        }

        #region Update / Draw 
        public override void Update(GameTime gt)
        {
            var state = Mouse.GetState();
            var pressed = state.LeftButton == ButtonState.Pressed;
            var pos     = new Vector2(state.X, state.Y);

            if(pressed)
                PlayerArrows.Add(new Arrow() 
                {
                    Position = pos
                });

            //Enemies
            foreach(var enemy in Enemies) 
            {
                if(enemy.CurrentSprite.BoundingBox.Contains(pos))
                    enemy.Kill();

                enemy.Update(gt);
            }

            //Arrows
            foreach(var arrow in PlayerArrows)
                arrow.Update(gt);

        }
        public override void Draw(GameTime gt)
        {
            //Enemies
            foreach(var enemy in Enemies)
                enemy.Draw(gt);

            //Arrows
            foreach(var arrow in PlayerArrows)
                arrow.Draw(gt);

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

