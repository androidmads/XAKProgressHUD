using Android.Content;

namespace KProgressHUD
{
    public class Helper
    {
        private static float scale;

        public static float DpToPixel(float dp, Context context)
        {
            if (scale == 0)
            {
                scale = context.Resources.DisplayMetrics.Density;
            }
            return (int)(dp * scale);
        }
    }
}