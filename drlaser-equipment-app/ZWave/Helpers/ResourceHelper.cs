
namespace ZWave.Helpers
{
    public static class ResourceHelper
    {
        private static ResourceDictionary ColorResource = Application.Current.Resources.MergedDictionaries.ElementAt(0);
        private static ResourceDictionary DimensionResource = Application.Current.Resources.MergedDictionaries.ElementAt(2);
        private static ResourceDictionary StylesResource = Application.Current.Resources.MergedDictionaries.ElementAt(3);
        public static Color GetColor(string color)
        {
            if (ColorResource.TryGetValue(color, out var colorValue))
            {
                return (Color)colorValue;
            }

            throw new Exception($"Can not find Color {color} in Color.xaml");
        }

        /// <summary>
        /// Must cast value on received from this function to Double or Thickness based on defined dimension
        /// </summary>
        /// <param name="dimen"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public static object GetDimension(string dimen)
        {
            if (DimensionResource.TryGetValue(dimen, out var dimenValue))
            {
                return dimenValue;
            }

            throw new Exception($"Can not find Dimension {dimen} in Dimensions.xaml");
        }
        public static Style GetStyles(string style)
        {
            if (StylesResource.TryGetValue(style, out var styleValue))
            {
                return (Style)styleValue;
            }

            throw new Exception($"Can not find Dimension {style} in Styles.xaml");
        }
    }
}
