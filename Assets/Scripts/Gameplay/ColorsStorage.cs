using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public static class ColorsStorage
    {
        public static readonly Dictionary<string, Color> ColorMap = new Dictionary<string, Color>
        {
            {"Red", new Color(255, 0, 0) },
            {"Green", new Color(0, 255, 0)},
        };

        public enum ColorNames
        {
            Red,
            Green,
        }
    }
}
