using System;

namespace Core.Runtime
{
    [Serializable]
    public class GameTime
    {
        public int Seconds { get; set; }
        public int Minutes { get; set; }
        public int Hours { get; set; }
        public int Days { get; set; }

        public int TotalSeconds => Days * 86400 + Hours * 3600 + Minutes * 60 + Seconds;
        public int TotalMinutes => Days * 1440 + Hours * 60 + Minutes;
        public int TotalHours => Days * 24 + Hours;
        public int TotalDays => Days;

        public GameTime()
        {
            Minutes = 0;
            Hours = 0;
            Days = 0;
        }

        public void Advance(int seconds)
        {
            Seconds += seconds;

            while (Seconds >= 60) { Seconds -= 60; Minutes++; }
            while (Minutes >= 60) { Minutes -= 60; Hours++; }
            while (Hours >= 24) { Hours -= 24; Days++; }
        }

        public int SnapTime()
        {
            return TotalSeconds;
        }
    }
}
