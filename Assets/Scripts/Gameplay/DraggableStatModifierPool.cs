using UnityEngine;

namespace Gameplay
{
    public class DraggableStatModifierPool : InteractionObject
    {
        [SerializeField] private DraggableStatModifier draggableStatModifier;
        [SerializeField] private Transform poolPosition;

        private void Awake()
        {
            var floatingStatModifierInstance = Instantiate(draggableStatModifier, poolPosition.position, Quaternion.identity);
            floatingStatModifierInstance.BackPosition = poolPosition;
        }

        protected override void Interact(Transform t)
        {
            throw new System.NotImplementedException();
        }
    }
}
