using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMS.Core
{
    public static class ConversionHelper
    {
        public static int ToInt(this DayOfWeek weekday)
        {
            return (int)weekday + 1;
        }
    }
}