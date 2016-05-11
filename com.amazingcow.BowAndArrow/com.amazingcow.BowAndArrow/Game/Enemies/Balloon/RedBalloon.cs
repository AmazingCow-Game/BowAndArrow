#region Usings
//Xna
using Microsoft.Xna.Framework;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class RedBalloon : Balloon
    {
        #region Constants
        public const int kSpeedRedBalloon = -40;
        public const int kScoreValue      = 100;
        #endregion


        #region Public Properties 
        public override int ScoreValue { get { return kScoreValue; } }
        #endregion


        #region CTOR
        public RedBalloon(Vector2 position) :
            base(position, new Vector2(0, kSpeedRedBalloon))
        {
            //Initialize the textures...
            var resMgr = ResourcesManager.Instance;
            AliveTexturesList.Add(resMgr.GetTexture("ballon"));
            DyingTexturesList.Add(resMgr.GetTexture("ballon_dead"));
        }
        #endregion //CTOR

    }//class RedBalloon
}//namespace com.amazingcow.BowAndArrow

