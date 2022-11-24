using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryTasks;

namespace GeometryPainting
{
    public static class SegmentExtensions
    {
        private static Dictionary<Segment, Color> _colorDictionary = new Dictionary<Segment, Color>();
        // Этой задаче следует быть в следующем модуле
        public static void SetColor(this Segment seg, Color color)
        {
            if (_colorDictionary.ContainsKey(seg))
                _colorDictionary[seg] = color;
            else
                _colorDictionary.Add(seg, color);
        }

        public static Color GetColor(this Segment seg)
        {
            if (_colorDictionary.ContainsKey(seg))
                return _colorDictionary[seg];
            else
                return Color.Black;
        }
    }
}
