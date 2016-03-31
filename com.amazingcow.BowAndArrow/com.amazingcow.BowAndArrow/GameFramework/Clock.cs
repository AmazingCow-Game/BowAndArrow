using System;

namespace com.amazingcow.BowAndArrow
{
    public class Clock
    {
        #region Events 
        public event EventHandler<EventArgs> OnTick;
        #endregion //Events 


        #region Constants 
        public const double kRepeatForever = double.MaxValue;
        #endregion //Constants


        #region Public Properties 
        public float Interval     { get; set;         }
        public int   RepeatCount  { get; set;         }
        public int   TickCount    { get; private set; }
        public bool  IsEnabled    { get; private set; }
        #endregion //Public Properties


        #region iVars 
        private float _updateTime;
        #endregion //iVars 


        #region CTOR
        public Clock(float interval, int repeat)
        {
            Interval    = interval;
            RepeatCount = repeat;
            TickCount   = 0;
        }
        #endregion //CTOR


        #region Update
        public void Update(float dt)
        {
            if(!IsEnabled)
                return;

            if(TickCount >= RepeatCount)
                return;

            _updateTime -= dt;
            if(_updateTime <= 0)
            {
                _updateTime = Interval;
                ++TickCount;

                if(OnTick != null)
                    OnTick(this, EventArgs.Empty);
            }
        }       
        #endregion //Update


        #region Start / Stop 
        public void Start()
        {
            TickCount   = 0;
            IsEnabled   = true;

            _updateTime = Interval;
        }

        public void Stop()
        {
            IsEnabled = false;
        }
        #endregion //Start / Stop 
    }
}

