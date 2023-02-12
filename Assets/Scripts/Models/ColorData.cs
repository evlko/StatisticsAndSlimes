using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "Color", menuName = "ScriptableObjects/Color", order = 6)]
    public class ColorData : ScriptableObject
    {
        public Color color;
    }
}
