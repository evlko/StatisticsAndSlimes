using UnityEngine;
using Interfaces;

namespace Gameplay
{
    public class Dyer : InteractionObject
    {
        [SerializeField] private ColorsStorage.ColorNames color;
        private Color _color;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = this.GetComponent<SpriteRenderer>();
            _color = ColorsStorage.ColorMap[color.ToString()];
            _spriteRenderer.color = _color;
        }
        
        protected override void Interact(Transform t)
        {
            t.GetComponent<IColorable>()?.Color(_color);
        }
    }
}
