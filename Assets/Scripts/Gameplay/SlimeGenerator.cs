using UnityEngine;

namespace Gameplay
{
    public class SlimeGenerator : MonoBehaviour
    {
        // TODO: meh, not cool property
        [SerializeField] private SlimePool slimePool;
        private void OnMouseDown()
        {
            slimePool.ActivateSlime();
        }
    }
}
