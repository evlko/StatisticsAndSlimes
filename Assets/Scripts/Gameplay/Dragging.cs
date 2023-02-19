using Interfaces;
using UnityEngine;

namespace Gameplay
{
    public class Dragging : MonoBehaviour
    {
        private Collider2D _collider;
        private Animator _animator;
        private bool _isDragging = false;
        private static readonly int IsDragging = Animator.StringToHash("isDragging");

        public bool isDragging
        {
            get => _isDragging;
        }

        private void Awake()
        {
            _collider = this.gameObject.GetComponent<Collider2D>();
            _animator = this.gameObject.GetComponentInChildren<Animator>();
        }

        private void OnMouseDown()
        {
            _collider.isTrigger = true;
            _isDragging = true;
            _animator.SetBool(IsDragging, true);
        }

        private void OnMouseDrag()
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = (pos);
        }

        private void OnMouseUp()
        {
            _collider.isTrigger = false;
            _isDragging = false;
            _animator.SetBool(IsDragging, false);
        }
    }
}