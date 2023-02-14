using System;
using Models;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] protected SlimePool slimePool;
        [SerializeField] protected ChapterData chapterData;

        protected int CurrentLevel;
        private string _chapterName;
        
        public static Action<LevelData> ShowLevelData;
        public static Action LevelCompleted;

        protected void Awake()
        {
            _chapterName = chapterData.chapterName;
            
            if (!PlayerPrefs.HasKey(_chapterName))
            {
                PlayerPrefs.SetInt(_chapterName, 0);
            }

            CurrentLevel = PlayerPrefs.GetInt(_chapterName);

            if (CurrentLevel >= chapterData.levels.Count)
            {
                PlayerPrefs.SetInt(_chapterName, 0);
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                InitLevel();
            }
        }

        protected virtual void InitLevel()
        {
            
        }

        protected void CompleteLevel()
        {
            CurrentLevel += 1;
            PlayerPrefs.SetInt(_chapterName, CurrentLevel);
            LevelCompleted?.Invoke();
        }
    }
}
