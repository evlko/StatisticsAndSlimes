using UnityEngine;
using Interfaces;
using Models;

namespace Gameplay
{
    public class Dyer : InteractionObject
    {
        [SerializeField] private ColorData color;
        private Color _color;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = this.GetComponent<SpriteRenderer>();
            _color = color.color;
            _spriteRenderer.color = _color;
        }
        
        protected override void Interact(Transform t)
        {
            t.GetComponent<IColorable>()?.Color(_color);
        }
    }
}
