using UnityEngine;

namespace Views
{
    public class View : MonoBehaviour
    {
        protected void ActivateView(bool active)
        {
            for (var i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(active);
            }
        }
    }
}
