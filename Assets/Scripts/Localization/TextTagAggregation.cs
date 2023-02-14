using System.Collections.Generic;
using System.Linq;
using Models;
using UnityEngine;

namespace Localization
{
    public static class TextTagAggregation
    {
        private static List<TextTagData> _textTags;
        
        static TextTagAggregation()
        {
            _textTags = Resources.LoadAll("TextTags", typeof(TextTagData)).Cast<TextTagData>().ToList();
        }

        public static string SetTagValue(string text)
        {
            foreach (var t in _textTags)
            {
                var tag = "<" + t.tag + ">";
                if (!text.Contains(tag)) continue;
                text = text.Replace(tag, GenerateTagValues(t));
                tag = "</" + t.tag + ">";
                text = text.Replace(tag, GenerateClosedTag());
            }

            return text;
        }
        
        private static string GenerateTagValues(TextTagData tag)
        {
            var result = "";
            result += "<font=\"" + tag.fontName + "\">";
            result += "<color=#" + ColorUtility.ToHtmlStringRGBA(tag.textColor) + ">";
            result += "<mark=#" + ColorUtility.ToHtmlStringRGBA(tag.backgroundColor) + ">";

            return result;
        }

        private static string GenerateClosedTag()
        {
            var result = "</mark></color></font>";
            return result;
        }
    }
}
