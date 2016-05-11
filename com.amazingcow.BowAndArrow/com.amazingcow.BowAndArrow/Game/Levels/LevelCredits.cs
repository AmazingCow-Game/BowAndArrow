#region Usings
//System
using System;
//Xna
using Microsoft.Xna.Framework;
#endregion //Usings


namespace com.amazingcow.BowAndArrow
{
    public class LevelCredits : Level
    {
        #region Public Properties 
        public override String PaperStringIntro 
        { get { return kPaperIntroString; } }

        public override String PaperStringGameOver
        { get { return kPaperGameOverString; } }

        public override String LevelTitle        
        { get { return kLevelTitle; } }

        public override String LevelDescription        
        { get { return kLevelDescription; } }
        #endregion // Public Properties 


        #region Init
        protected override void InitEnemies()
        {                        
            var pos     = new Vector2(PlayField.Center.X, PlayField.Center.Y);
            var balloon = new RedBalloon(pos);
            balloon.OnStateChangeDead  += OnEnemyStateChangeDead;
            balloon.OnStateChangeDying += OnEnemyStateChangeDying;

            Enemies.Add(balloon);

            AliveEnemies = 1;

            Player.ArrowsCount = 1;
        }
        #endregion //Init


        #region Helper Methods
        protected override void LevelCompleted()
        {
            GameManager.Instance.ChangeLevel(new Level1());
        }
        #endregion // Helper Methods 


        #region Paper Strings
        //Intro 
        const String kPaperIntroString = @"
We hope that you had
a great time playing this game.

We had making it :D

Dev: N2OMatt
<n2omatt@amazingcow.com>

Amazing Cow - 2016 - GPLv3

Dedicate to all people 
fighting against cancer!

Thank you...

PS: You have only 1 arrow...
";

        const String kPaperGameOverString = @"
Oh noooooo....
Just now :/
";
        //Title
        const String kLevelTitle        = "Credits";
        const String kLevelDescription  = "Thank You <3";
        #endregion // Paper Strings

    }//class Level1
}//namespace com.amazingcow.BowAndArrow
