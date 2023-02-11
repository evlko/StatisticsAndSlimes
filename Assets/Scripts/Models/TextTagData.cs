using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "TextTag", menuName = "ScriptableObjects/TextTag", order = 5)]
    public class TextTagData : ScriptableObject
    {
        public string tag;
        public Color textColor;
        public Color backgroundColor;
        public string fontName;
    }
}
