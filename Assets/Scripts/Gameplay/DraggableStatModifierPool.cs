using UnityEngine;

namespace Gameplay
{
    public class DraggableStatModifierPool : InteractionObject
    {
        [Header("Pool")]
        [SerializeField] private DraggableStatModifier draggableStatModifier;
        [SerializeField] private Transform poolPosition;

        protected override void Awake()
        {
            base.Awake();
            
            var floatingStatModifierInstance = Instantiate(draggableStatModifier, poolPosition.position, Quaternion.identity);
            floatingStatModifierInstance.BackPosition = poolPosition;
        }

        protected override void Interact(Transform t)
        {
            throw new System.NotImplementedException();
        }
    }
}
