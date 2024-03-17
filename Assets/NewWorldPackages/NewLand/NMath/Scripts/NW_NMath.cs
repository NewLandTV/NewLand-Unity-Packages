namespace NewLandPackages
{
    public struct NMath
    {
        #region Properties

        public const float PI = 3.14159274f;
        public const float Infinity = float.PositiveInfinity;
        public const float NegativeInfinity = float.NegativeInfinity;
        public const float Deg2Rad = 0.0174532924f;
        public const float Rad2Deg = 57.29578f;

        #endregion
        #region Method

        #region Abs

        public static int Abs(int value)
        {
            return value > 0 ? value : value * -1;
        }

        public static float Abs(float value)
        {
            return value > 0f ? value : value * -1f;
        }

        public static double Abs(double value)
        {
            return value > 0d ? value : value * -1d;
        }

        public static long Abs(long value)
        {
            return value > 0 ? value : value * -1;
        }

        #endregion
        #region Max

        public static int Max(int a, int b)
        {
            return a > b ? a : b;
        }

        public static float Max(float a, float b)
        {
            return a > b ? a : b;
        }

        public static double Max(double a, double b)
        {
            return a > b ? a : b;
        }

        public static long Max(long a, long b)
        {
            return a > b ? a : b;
        }

        #endregion
        #region Min

        public static int Min(int a, int b)
        {
            return a < b ? a : b;
        }

        public static float Min(float a, float b)
        {
            return a < b ? a : b;
        }

        public static double Min(double a, double b)
        {
            return a < b ? a : b;
        }

        public static long Min(long a, long b)
        {
            return a < b ? a : b;
        }

        #endregion

        #endregion
    }
}
