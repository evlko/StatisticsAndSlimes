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
    }
}
