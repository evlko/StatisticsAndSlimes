using UnityEngine;

namespace Utils
{
    public class PlayerPrefsController : MonoBehaviour
    {
        public void ClearAllPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
