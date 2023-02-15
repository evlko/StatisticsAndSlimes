using System.Collections.Generic;
using UnityEngine;

namespace UserInterface
{
    public class ArenaTracker : MonoBehaviour
    {
        [SerializeField] private List<Transform> elements;
        private void Awake()
        {
            if (PlayerPrefs.GetInt("Arena") > 0)
            {
                foreach (var el in elements)
                {
                    el.gameObject.SetActive(false);
                }
            }
        }
    }
}
