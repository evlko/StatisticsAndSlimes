using Interfaces;
using UnityEngine;

namespace Gameplay
{
    public class DraggableStatModifier : StatModifier, IColorable, IDestroyable
    {
        private SpriteRenderer _spriteRenderer;
        private Dragging _dragging;
        private Transform _transform;
        private Transform _backPosition;

        public Transform BackPosition
        {
            set => _backPosition = value;
        }

        protected override void Awake()
        {
            base.Awake();
            
            _spriteRenderer = this.GetComponent<SpriteRenderer>();
            _transform = transform.GetComponent<Transform>();
            _dragging = this.gameObject.AddComponent<Dragging>();
        }

        public void Color(Color newColor)
        {
            _spriteRenderer.color = newColor;
            // TODO: map new color with stats if there is no then default (current)
        }

        protected override void Interact(Transform t)
        {
            if (_dragging.isDragging) return;
            var slime = t.GetComponent<Slime>();
            if (slime == null) return;
            slime.UpdateSlimeQuantitativeFeature(modifiedQuantitativeFeature, value);
            Destroy();
        }
        
        public void Destroy()
        {
            if (_backPosition != null)
            {
                _transform.position = _backPosition.position;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
