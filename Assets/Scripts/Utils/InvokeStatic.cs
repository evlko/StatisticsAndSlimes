using UnityEngine;

namespace Utils
{
    [DefaultExecutionOrder(100)]
    public class InvokeStatic : MonoBehaviour
    {
        private void Awake()
        {
            Localization.Localization.LoadLocalized?.Invoke();
        }
    }
}
