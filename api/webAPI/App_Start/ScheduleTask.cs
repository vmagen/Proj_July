using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAPI
{
    public static class ScheduleTask
    {
        public static void Trigger(Action action, TimeSpan repeatingDays, int hour = 0, int minute = 0, int second = 0)
        {
            var trigger = new ScheduleTrigger(repeatingDays, hour, minute, second);
            trigger.OnTimeTriggered += action;
        }
    }
}