using System;
using System.Collections.Generic;
using UnityEngine;
using Models;

namespace Gameplay
{
    public class SlimePool : MonoBehaviour
    {
        [Header("Slimes")]
        // TODO: rename this property -> slime
        [SerializeField] private Slime slimeView;
        [SerializeField] private Transform slimePoolPosition;

        [Header("Spawn")]
        [SerializeField] private Vector2 xSpawnBorders;
        [SerializeField] private Vector2 ySpawnBorders;
        
        private static List<Slime> _storedSlimes = new List<Slime>();
        private static List<Slime> _activeSlimes = new List<Slime>();
        private readonly System.Random _random = new System.Random();
        
        public static Action SlimesPoolChanged;

        public static List<Slime> ActiveSlimes => _activeSlimes;

        // TODO: duplicates are not fun :(
        public void BuildPool(List<SlimeData> storedSlimes, List<SlimeData> activeSlimes)
        {
            Debug.Log("Building pool...");
            
            _storedSlimes.Clear();
            _activeSlimes.Clear();
            
            foreach (var t in storedSlimes)
            {
                var slime = Instantiate(slimeView, slimePoolPosition.position, Quaternion.identity);
                slime.Init(t, this);
                slime.transform.parent = this.transform;
                _storedSlimes.Add(slime);
            }

            foreach (var t in activeSlimes)
            {
                var slime = Instantiate(slimeView, slimePoolPosition.position, Quaternion.identity);
                slime.Init(t, this);
                slime.transform.parent = this.transform;
                ActivateSlime(slime);
            }
        }

        public void ActivateSlime(Slime pickedSlime = null)
        {
            Debug.Log("Activate slime...");

            if (pickedSlime == null)
            {
                if (_storedSlimes.Count <= 0) return;
                var slimeIndex = 0;
                pickedSlime = _storedSlimes[slimeIndex];
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

        public void RemoveSlime(Slime slime)
        {
            _activeSlimes.Remove(slime);
            _storedSlimes.Add(slime);
            slime.transform.position = slimePoolPosition.position;
            
            SlimesPoolChanged?.Invoke();
        }
    }
}