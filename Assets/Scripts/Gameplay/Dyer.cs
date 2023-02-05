using UnityEngine;
using Interfaces;

namespace Gameplay
{
    public class Dyer : InteractionObject
    {
        [SerializeField] private Color color;

        protected override void Interact(Transform t)
        {
            t.GetComponent<IColorable>()?.Color(color);
        }
    }
}
