using UnityEngine;
using Interfaces;
using Models;

namespace Gameplay
{
    public class Dyer : InteractionObject
    {
        [Header("Dyer")]
        [SerializeField] private ColorData color;
        private Color _color;
        private SpriteRenderer _spriteRenderer;

        protected override void Awake()
        {
            base.Awake();
            
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
