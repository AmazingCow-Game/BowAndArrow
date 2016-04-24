#region Usings
//Xna
using Microsoft.Xna.Framework;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class YellowBalloon : Balloon
    {
        #region Constants
        public const int kSpeedMinYellonBalloon = 45;
        public const int kSpeedMaxYellowBalloon = 55;
        #endregion


        #region CTOR
        public YellowBalloon(Vector2 position) :
            base(position, Vector2.Zero)
        {
            //Initialize the textures...
            var resMgr = ResourcesManager.Instance;
            AliveTexturesList.Add(resMgr.GetTexture("ballon_yellow"));
            DyingTexturesList.Add(resMgr.GetTexture("ballon_yellow_dead"));

            //Init the Speed...
            int ySpeed = GameManager.Instance.RandomNumGen.Next(kSpeedMinYellonBalloon,
                                                                kSpeedMaxYellowBalloon);
            Speed = new Vector2(0, -ySpeed);
        }
        #endregion //CTOR

    }//class YellowBalloon
}//namespace com.amazingcow.BowAndArrow

