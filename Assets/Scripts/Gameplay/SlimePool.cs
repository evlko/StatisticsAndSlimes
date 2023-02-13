using System;
using System.Collections.Generic;
using UnityEngine;
using Models;

namespace Gameplay
{
    public class SlimePool : MonoBehaviour
    {
        [Header("Slimes")]
        [SerializeField] private Slime slime;
        [SerializeField] private Transform slimePoolPosition;
        
        [Header("Spawn")]
        [SerializeField] private Vector2 xSpawnBorders;
        [SerializeField] private Vector2 ySpawnBorders;

        [Header("Hints Keys")] 
        [SerializeField] private string hintStoredIsEmpty;
        [SerializeField] private string hintRemoveRejected;

        private static List<Slime> _storedSlimes = new List<Slime>();
        private static List<Slime> _activeSlimes = new List<Slime>();

        public static Action SlimesPoolChanged;
        public static Action<string> ShowPoolHint;

        public static List<Slime> ActiveSlimes => _activeSlimes;
        public static List<Slime> StoredSlimes => _storedSlimes;

        public void BuildPool(List<SlimeData> storedSlimes, List<SlimeData> activeSlimes)
        {
            Debug.Log("Building pool...");
            
            _storedSlimes.Clear();
            _activeSlimes.Clear();

            foreach (var slimeData in storedSlimes)
            {
                var instantiatedSlime = InstantiateSlime(slimeData);
                _storedSlimes.Add(instantiatedSlime);
            }

            foreach (var slimeData in activeSlimes)
            {
                var instantiatedSlime = InstantiateSlime(slimeData);
                ActivateSlime(instantiatedSlime);
            }
        }

        private Slime InstantiateSlime(SlimeData slimeData)
        {
            var instantiatedSlime = Instantiate(this.slime, slimePoolPosition.position, Quaternion.identity);
            instantiatedSlime.Init(slimeData, this);

            return instantiatedSlime;
        }

        public void ActivateSlime(Slime pickedSlime = null)
        {
            if (pickedSlime == null)
            {
                if (_storedSlimes.Count <= 0)
                {
                    ShowPoolHint?.Invoke(hintStoredIsEmpty);
                    return;
                }
                pickedSlime = _storedSlimes[0];
            }

            if (_storedSlimes.Contains(pickedSlime))
            {
                _storedSlimes.Remove(pickedSlime);
            }
            
            _activeSlimes.Add(pickedSlime);

            pickedSlime.transform.position = new Vector2(UnityEngine.Random.Range(xSpawnBorders[0], xSpawnBorders[1]),
                UnityEngine.Random.Range(ySpawnBorders[0], ySpawnBorders[1]));

            SlimesPoolChanged?.Invoke();
        }

        public void RemoveSlime(Slime s)
        {
            if (s.SlimeData.QuantitativeFeatures[SlimeQuantitativeFeatures.Sweetness] > 60)
            { 
                ShowPoolHint?.Invoke(hintRemoveRejected);
                return;
            }
            
            _activeSlimes.Remove(s);
            _storedSlimes.Add(s);
            s.transform.position = slimePoolPosition.position;

            SlimesPoolChanged?.Invoke();
        }
    }
}