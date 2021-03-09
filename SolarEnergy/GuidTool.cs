using System;

namespace SolarEnergy
{
    public class GuidTool
    {
        public static string GenGuid()
        {
            return Guid.NewGuid().ToString("B").ToUpper();
        }
    }
}
