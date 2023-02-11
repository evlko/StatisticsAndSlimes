using UnityEngine;

namespace Gameplay
{
    public class FloatingStatModifierPool : InteractionObject
    {
        [SerializeField] private FloatingStatModifier floatingStatModifier;
        private Transform _transform;

        private void Awake()
        {
            _transform = this.GetComponent<Transform>();
            var floatingStatModifierInstance = Instantiate(floatingStatModifier, _transform.position, Quaternion.identity);
            floatingStatModifierInstance.BackPosition = _transform;
        }

        protected override void Interact(Transform t)
        {
            throw new System.NotImplementedException();
        }
    }
}
