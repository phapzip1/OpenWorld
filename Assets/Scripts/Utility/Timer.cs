using System;

namespace Phac.Utility
{
    public abstract class Timer
    {
        protected float InitialTime;
        protected float Time { get; set; }
        public bool IsRunning { get; protected set; }
        public float Progress => Time / InitialTime;
        public Action OnTimerStart = delegate {};
        public Action OnTimerStop = delegate {};

        protected Timer(float value)
        {
            InitialTime = value;
            IsRunning = false;
        }

        public void Start()
        {
            Time = InitialTime;
            if (!IsRunning)
            {
                IsRunning = true;
                OnTimerStart.Invoke();
            }
        }

        public void Resume() => IsRunning = true;

        public void Pause() => IsRunning = false;

        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                OnTimerStop.Invoke();
            }
        }

        public abstract void Tick(float deltaTime);
    }

    public class CountDownTimer : Timer
    {
        public CountDownTimer(float value) : base(value)
        {
        }

        public override void Tick(float deltaTime)
        {
            if (IsRunning)
            {
                Time -= deltaTime;
            }

            if (IsRunning && Time <= 0) {
                Stop();
            }
        }

        public void Reset() => Time = InitialTime;
        
        public bool IsFinished => Time <= 0;
    }

    public class StopwatchTimer : Timer
    {
        public StopwatchTimer(float value) : base(value)
        {
        }

        public override void Tick(float deltaTime)
        {
            if (IsRunning) {
                Time += deltaTime;
            }
        }

        public void Reset() => Time = 0;
        
        public float GetTime() => Time;
    }
}