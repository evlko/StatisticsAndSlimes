using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UserInterface
{
    public class SceneSelector : MonoBehaviour
    {
        [SerializeField] private List<string> additiveScenes;
        public void LoadSceneByName(string sceneName)
        {
            foreach (var scene in additiveScenes)
            {
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
            }
            SceneManager.LoadScene(sceneName);
        }
    }
}
