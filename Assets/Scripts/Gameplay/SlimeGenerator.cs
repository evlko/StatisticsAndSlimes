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
            SlimePool.SlimesPoolChanged += CheckPool;
        }

        private void Start()
        {
            CheckPool();
        }

        private void OnMouseDown()
        {
            slimePool.ActivateSlime();
        }

        private void CheckPool()
        {
            _spriteRenderer.sprite = SlimePool.StoredSlimes.Count == 0 ? emptyPool : notEmptyPool;
        }

        private void OnDestroy()
        {
            SlimePool.SlimesPoolChanged -= CheckPool;
        }
    }
}
