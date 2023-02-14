using UnityEngine;

namespace UserInterface
{
    public class ExitButton : MonoBehaviour
    {
        public void Awake()
        {
            #if UNITY_WEBGL
                this.gameObject.SetActive(false);
            #endif
        }

        public void CloseApp()
        {
            Application.Quit();
        }
    }
}
