using UnityEngine;

namespace Gameplay
{
    public class SlimeGenerator : MonoBehaviour
    {
        public SlimePool slimePool;
        private void OnMouseDown()
        {
            slimePool.ActivateSlime();
        }
    }
}
