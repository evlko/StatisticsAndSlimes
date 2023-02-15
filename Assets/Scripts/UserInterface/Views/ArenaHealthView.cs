using System;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class ArenaHealthView : MonoBehaviour
    {
        [SerializeField] private List<Image> healthImages;

        private void Awake()
        {
            ArenaManager.ShowHealth += SetHealth;
        }

        private void SetHealth(int health)
        {
            for (int i = 0; i < health; i++)
            {
                healthImages[i].gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            ArenaManager.ShowHealth -= SetHealth;
        }
    }
}
