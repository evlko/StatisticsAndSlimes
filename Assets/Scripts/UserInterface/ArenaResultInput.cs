using System;
using TMPro;
using UnityEngine;

namespace UserInterface
{
    public class ArenaResultInput : MonoBehaviour
    {
        private TMP_InputField _inputField;
        public static Action<float> ConditionChecked;

        private void Awake()
        {
            _inputField = this.GetComponent<TMP_InputField>();
        }
        
        public void AnswerChanged()
        {
            ConditionChecked?.Invoke(float.Parse(_inputField.text));
        }
    }
}
