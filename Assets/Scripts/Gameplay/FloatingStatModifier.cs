using Interfaces;
using Models;
using UnityEngine;

namespace Gameplay
{
    public class FloatingStatModifier : StatModifier, IColorable, IDestroyable
    {
        private SpriteRenderer _spriteRenderer;
        private Transform _transform;
        private Vector2 _backPosition;

        private void Awake()
        {
            _spriteRenderer = this.GetComponent<SpriteRenderer>();
            _transform = transform.GetComponent<Transform>();
            _backPosition = _transform.position;
            this.gameObject.AddComponent<Dragging>();
        }

        public void Color(Color newColor)
        {
            _spriteRenderer.color = newColor;
            // TO-DO map new color with stats if there is no than default (current)
        }

        public void Color(float alpha)
        {
            var currentColor = _spriteRenderer.color;
            currentColor.a = alpha;
            _spriteRenderer.color = currentColor;
        }

        protected override void Interact(Transform t)
        {
            t.GetComponent<Slime>()?.UpdateSlimeQuantitativeFeature(modifiedQuantitativeFeature, value);
            Destroy();
        }
        
        public void Destroy()
        {
            _transform.position = _backPosition;
        }
    }
}
