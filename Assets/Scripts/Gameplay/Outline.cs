using System;
using UnityEngine;

namespace Gameplay
{
    [DefaultExecutionOrder(100)]
    public class Outline : MonoBehaviour
    {
        [SerializeField] private Transform outline;
        [SerializeField] private bool isMouseTriggered;
        [SerializeField] private InteractionTags interactionTag;

        private Dragging _dragging;
        private bool _isDraggable;

        private void Awake()
        {
            _dragging = this.gameObject.GetComponent<Dragging>();
            _isDraggable = _dragging != null;
        }

        private void OnMouseEnter()
        {
            if (!isMouseTriggered) return;
            if (!_isDraggable)
            {
                outline.gameObject.SetActive(true);
            }
            else if (!_dragging.isDragging)
            {
                outline.gameObject.SetActive(true);
            }
        }

        private void OnMouseExit()
        {
            if (!isMouseTriggered) return;
            if (!_isDraggable)
            {
                outline.gameObject.SetActive(false);
            }
            else if (!_dragging.isDragging)
            {
                outline.gameObject.SetActive(false);
            }
        }

        private void OnMouseDown()
        {
            if (isMouseTriggered && _isDraggable)
            {
                outline.gameObject.SetActive(false);
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IgnoreWhileOnDrag()) return;
            outline.gameObject.SetActive(CheckTag(other.tag));
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (IgnoreWhileOnDrag()) return;
            outline.gameObject.SetActive(false);
        }

        private bool IgnoreWhileOnDrag()
        {
            if (!_isDraggable) return false;
            return _dragging.isDragging;
        }

        private bool CheckTag(string gameObjectTag)
        {
            switch (interactionTag)
            {
                case InteractionTags.All:
                case InteractionTags.Slime when gameObjectTag == "Slime":
                case InteractionTags.InteractionObject when gameObjectTag == "InteractionObject":
                    return true;
                case InteractionTags.None:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
