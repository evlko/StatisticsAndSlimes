using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    // TODO: it's better to use some kinda of a factory
    public class LevelBuilder : MonoBehaviour
    {
        [Header("Instantiate Positions")]
        public List<Transform> wellPositions;
        public List<Transform> puddlePositions;

        private System.Random _random;

        public void BuildLevel(List<InteractionObject> interactionObjects)
        {
            _random = new System.Random();
            
            foreach (var interactionObject in interactionObjects)
            {
                var availablePositions = new List<Transform>();
                switch (interactionObject)
                {
                    case Destroyer destroyer:
                        availablePositions = wellPositions;
                        break;
                    case Dyer dyer:
                        availablePositions = puddlePositions;
                        break;
                }
                InstantiateInteractionObject(interactionObject, availablePositions);
            }
        }

        private void InstantiateInteractionObject(InteractionObject interactionObject, List<Transform> positions)
        {
            var index = _random.Next(positions.Count);
            Instantiate(interactionObject, positions[index].position, Quaternion.identity);
            positions.RemoveAt(index);
        }
    }
}
