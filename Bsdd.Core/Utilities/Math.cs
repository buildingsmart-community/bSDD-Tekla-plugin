using System;

namespace Bsdd.Core.Utilities
{
    public static class MathUtilities
    {
        // Conversion factors
        private const double DegreesToRadiansFactor = Math.PI / 180;
        private const double RadiansToDegreesFactor = 180 / Math.PI;

        /// <summary>
        /// Converts an angle from degrees to radians.
        /// </summary>
        /// <param name="degrees">Angle in degrees.</param>
        /// <returns>Angle in radians.</returns>
        public static double DegreesToRadians(double degrees)
        {
            return degrees * DegreesToRadiansFactor;
        }

        public static double AsRadians(this double degrees)
        {
            return DegreesToRadians(degrees);
        }


        /// <summary>
        /// Converts an angle from radians to degrees.
        /// </summary>
        /// <param name="radians">Angle in radians.</param>
        /// <returns>Angle in degrees.</returns>
        public static double RadiansToDegrees(double radians)
        {
            return radians * RadiansToDegreesFactor;
        }

        public static double AsDegrees(this double radians)
        {
            return RadiansToDegrees(radians);
        }

        /// <summary>
        /// Compares two double values to check if they are approximately equal within a specified tolerance (epsilon).
        /// </summary>
        /// <param name="one">The first double value.</param>
        /// <param name="two">The second double value.</param>
        /// <param name="epsilon">The tolerance value. Default is 0.05.</param>
        /// <returns>True if the two values are approximately equal, otherwise false.</returns>
        public static bool ApproximateEquals(double one, double two, double epsilon = 0.05)
        {
            return Math.Abs(one - two) < epsilon;
        }

        /// <summary>
        /// Normalizes an angle in degrees to the range [0, 360).
        /// </summary>
        /// <param name="degrees">Angle in degrees.</param>
        /// <returns>Normalized angle in degrees.</returns>
        public static double NormalizeDegrees(double degrees)
        {
            return (degrees % 360 + 360) % 360;
        }

        /// <summary>
        /// Normalizes an angle in radians to the range [0, 2π).
        /// </summary>
        /// <param name="radians">Angle in radians.</param>
        /// <returns>Normalized angle in radians.</returns>
        public static double NormalizeRadians(double radians)
        {
            return (radians % (2 * Math.PI) + (2 * Math.PI)) % (2 * Math.PI);
        }

        /// <summary>
        /// Clamps a value to the specified range [min, max].
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The minimum allowable value.</param>
        /// <param name="max">The maximum allowable value.</param>
        /// <returns>Clamped value within the specified range.</returns>
        public static double Clamp(double value, double min, double max)
        {
            return Math.Max(min, Math.Min(max, value));
        }

        /// <summary>
        /// Checks if a double value is approximately zero within a small tolerance.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="epsilon">The tolerance for considering the value as zero. Default is 1e-10.</param>
        /// <returns>True if the value is approximately zero, otherwise false.</returns>
        public static bool IsZero(double value, double epsilon = 1e-10)
        {
            return Math.Abs(value) < epsilon;
        }

        /// <summary>
        /// Calculates the smallest difference between two angles in degrees, considering circular wrap-around.
        /// </summary>
        /// <param name="angle1">First angle in degrees.</param>
        /// <param name="angle2">Second angle in degrees.</param>
        /// <returns>Smallest difference between the two angles in degrees.</returns>
        public static double AngleDifferenceDegrees(double angle1, double angle2)
        {
            double difference = NormalizeDegrees(angle2 - angle1);
            return difference > 180 ? difference - 360 : difference;
        }

        /// <summary>
        /// Calculates the smallest difference between two angles in radians, considering circular wrap-around.
        /// </summary>
        /// <param name="angle1">First angle in radians.</param>
        /// <param name="angle2">Second angle in radians.</param>
        /// <returns>Smallest difference between the two angles in radians.</returns>
        public static double AngleDifferenceRadians(double angle1, double angle2)
        {
            double difference = NormalizeRadians(angle2 - angle1);
            return difference > Math.PI ? difference - 2 * Math.PI : difference;
        }
    }
}