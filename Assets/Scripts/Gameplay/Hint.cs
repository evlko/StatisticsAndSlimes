using System;
using UnityEngine;

namespace Gameplay
{
    public class Hint: MonoBehaviour
    {
        public static Action<string, float> HintShowed;
        public static Action HintHidden;

        public void ShowHint(string key, float time = 0)
        {
            HintShowed?.Invoke(key, time);
        }

        public void HideHint()
        {
            HintHidden?.Invoke();
        }
    }
}
