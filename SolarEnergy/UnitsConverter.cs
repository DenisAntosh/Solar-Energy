using System;
using System.Collections.Generic;

namespace SolarEnergy
{
    public class UnitsConverter
    {
        private static Dictionary<DrawingUnits, double> _coeffsFromMM;

        static UnitsConverter()
        {
            _coeffsFromMM = new Dictionary<DrawingUnits, double>();
            _coeffsFromMM[DrawingUnits.Millimeters] = 1;
            _coeffsFromMM[DrawingUnits.Centimeters] = 0.1;
            _coeffsFromMM[DrawingUnits.Meters] = 0.001;
            _coeffsFromMM[DrawingUnits.Inches] = 0.0393700787;
            _coeffsFromMM[DrawingUnits.Feet] = 0.0032808399;
            _coeffsFromMM[DrawingUnits.Yards] = 0.0010936133;
        }

        public static int GetRound(DrawingUnits units)
        {
            switch (units)
            {
                case DrawingUnits.Millimeters:
                    return 0;
                case DrawingUnits.Centimeters:
                    return 1;
                case DrawingUnits.Meters:
                    return 3;
                case DrawingUnits.Inches:
                    return 3;
                case DrawingUnits.Feet:
                    return 3;
                case DrawingUnits.Yards:
                    return 3;
            }
            throw new NotSupportedException(units.ToString());
        }

        public static double Convert(double val, DrawingUnits inUnits, DrawingUnits outUnits, bool roundRes = true)
        {
            if (double.IsInfinity(val) || double.IsNaN(val))
            {
                return val;
            }
            if (inUnits == outUnits)
            {
                return val;
            }
            double coeffIn;
            if (!_coeffsFromMM.TryGetValue(inUnits, out coeffIn))
            {
                throw new NotSupportedException("Can't convert: " + inUnits.ToString() + "=>" + outUnits.ToString());
            }
            double coeffOut;
            if (!_coeffsFromMM.TryGetValue(outUnits, out coeffOut))
            {
                throw new NotSupportedException("Can't convert: " + inUnits.ToString() + "=>" + outUnits.ToString());
            }
            // from inUnit to MM
            val = val / coeffIn;
            // from MM to outUnit
            if (roundRes)
            {
                return Math.Round(val * coeffOut, 4);
            }
            else
            {
                return val * coeffOut;
            }
        }

        public static double ConvertArea(double val, DrawingUnits inUnits, DrawingUnits outUnits, bool roundRes = true)
        {
            if (double.IsInfinity(val) || double.IsNaN(val))
            {
                return val;
            }
            if (inUnits == outUnits)
            {
                return val;
            }
            double coeffIn;
            if (!_coeffsFromMM.TryGetValue(inUnits, out coeffIn))
            {
                throw new NotSupportedException("Can't convert: " + inUnits.ToString() + "=>" + outUnits.ToString());
            }
            double coeffOut;
            if (!_coeffsFromMM.TryGetValue(outUnits, out coeffOut))
            {
                throw new NotSupportedException("Can't convert: " + inUnits.ToString() + "=>" + outUnits.ToString());
            }

            coeffIn = coeffIn * coeffIn;
            coeffOut = coeffOut * coeffOut;

            // from inUnit to MM
            val = val / coeffIn;
            // from MM to outUnit
            if (roundRes)
            {
                return Math.Round(val * coeffOut, 4);
            }
            else
            {
                return val * coeffOut;
            }
        }


        public static double ConvertToMM(double val, DrawingUnits units, bool roundRes = true)
        {
            return Convert(val, units, DrawingUnits.Millimeters, roundRes);
        }

        //public static double ConvertToMM(double val)
        //{
        //    return ConvertToMM(val, DrawingUnitsManager.CurrentUnits, false);
        //}

        //public static double ConvertMM(double val, DrawingUnits units, bool roundRes = true)
        //{
        //    return Convert(val, DrawingUnits.Millimeters, units, roundRes);
        //}

        //public static double ConvertMM(double val)
        //{
        //    return ConvertMM(val, DrawingUnitsManager.CurrentUnits, false);
        //}
    }
}
