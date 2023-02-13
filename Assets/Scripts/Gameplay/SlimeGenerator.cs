using UnityEngine;

namespace Gameplay
{
    public class SlimeGenerator : MonoBehaviour
    {
        [SerializeField] private SlimePool slimePool;
        [Header("Visual")]
        [SerializeField] private Sprite emptyPool;
        [SerializeField] private Sprite notEmptyPool;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = this.GetComponent<SpriteRenderer>();
        }

        private void OnMouseDown()
        {
            slimePool.ActivateSlime();
            _spriteRenderer.sprite = SlimePool.StoredSlimes.Count == 0 ? emptyPool : notEmptyPool;
        }
    }
}
