namespace System.Windows
{
    public static class PointExtensions
    {
        /// <summary>
        ///     Computes the cartesian distance between points.
        /// </summary>
        /// <param name="p1">The p 1.</param>
        /// <param name="p2">The p 2.</param>
        /// <returns>The <see cref="double" />.</returns>
        public static double GetDistanceTo(this Point p1, Point p2)
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }
    }
}