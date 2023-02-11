using UnityEngine;

namespace Gameplay
{
    public class DraggableStatModifierPool : InteractionObject
    {
        [SerializeField] private DraggableStatModifier draggableStatModifier;
        private Transform _transform;

        private void Awake()
        {
            _transform = this.GetComponent<Transform>();
            var floatingStatModifierInstance = Instantiate(draggableStatModifier, _transform.position, Quaternion.identity);
            floatingStatModifierInstance.BackPosition = _transform;
        }

        protected override void Interact(Transform t)
        {
            throw new System.NotImplementedException();
        }
    }
}
