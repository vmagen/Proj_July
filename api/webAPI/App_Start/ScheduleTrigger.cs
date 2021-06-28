using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace webAPI
{
    internal class ScheduleTrigger
    {
        internal event Action OnTimeTriggered;

        readonly TimeSpan triggerHour;
        readonly TimeSpan repetingSpan; //daily, weekly, every 10 minutes etc...
        internal ScheduleTrigger(TimeSpan repeatingSpan, int hour=0, int minute = 0, int second = 0)
        {
            this.triggerHour = new TimeSpan(hour, minute, second);
            this.repetingSpan = repeatingSpan;
            InitiateAsync();
        }

       
        async void InitiateAsync()
        {
            while (true)
            {
                TimeSpan triggerTime = new DateTime().TimeOfDay;
                triggerTime = triggerTime.Add(repetingSpan);

                await Task.Delay(triggerTime);
                OnTimeTriggered?.Invoke();
            }
        }
    }
}