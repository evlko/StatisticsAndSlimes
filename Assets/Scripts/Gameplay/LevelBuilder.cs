using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public class LevelBuilder : MonoBehaviour
    {
        private List<Transform> _interactionObjectPositions;
        private System.Random _random;

        public void BuildLevel(List<InteractionObject> interactionObjects)
        {
            _interactionObjectPositions = GetComponentsInChildren<Transform>().Where(t => t != this.transform).ToList();
            _random = new System.Random();
            
            foreach (var interactionObject in interactionObjects)
            {
                InstantiateInteractionObject(interactionObject);
            }
        }

        private void InstantiateInteractionObject(InteractionObject interactionObject)
        {
            var index = _random.Next(_interactionObjectPositions.Count);
            Instantiate(interactionObject, _interactionObjectPositions[index].position, Quaternion.identity);
            _interactionObjectPositions.RemoveAt(index);
        }
    }
}
