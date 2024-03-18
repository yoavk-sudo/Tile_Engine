using System.Text.RegularExpressions;

namespace Tile_Engine
{
    public static class ExtensionMethods
    {
        public static string TrimAndDecapitalize(this string str)
        {
            return Regex.Replace(str.ToLower(), @"\s+", "");
        }

        public static bool IsNotNullAndBiggerThanZero<T>(this T[] arr)
        {
            return arr != null && arr.Length > 0;
        }

        public static bool IsWithinBounds<T>(this T[,] arr, int x, int y)
        {
            return x < arr.GetLength(0) && y < arr.GetLength(1);
        }
    }
}
