using UnityEngine;

namespace Gameplay
{
    public abstract class InteractionObject : MonoBehaviour
    {
        [Header("Interaction Object")]
        [SerializeField] protected string hintKey;
        private Hint _hint;
        
        protected virtual void Awake()
        {
            _hint = this.gameObject.AddComponent<Hint>();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.transform.GetComponent<Dragging>()?.isDragging == false)
            {
                Interact(other.transform);
            }
        }

        private void ShowHint(string key)
        {
            _hint.ShowHint(key);
        }

        private void HideHint()
        {
            _hint.HideHint();
        }

        private void OnMouseEnter()
        {
            ShowHint(hintKey);
        }
        
        private void OnMouseExit()
        {
            HideHint();
        }

        protected abstract void Interact(Transform t);
    }

    public enum InteractionTags
    {
        All,
        Slime,
        InteractionObject,
        None,
    }
}
