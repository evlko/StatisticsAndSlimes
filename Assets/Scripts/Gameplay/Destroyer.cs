using Interfaces;
using UnityEngine;

namespace Gameplay
{
    public class Destroyer : InteractionObject
    {
        protected override void Interact(Transform t)
        {
            t.GetComponent<IDestroyable>().Destroy();
        }
    }
}
