using UnityEngine;

namespace Utils
{
    public class PlayerPrefsController : MonoBehaviour
    {
        public void SetPlayerPrefValue(string prefName)
        {
            PlayerPrefs.SetInt(prefName, 0);
        }
    }
}
