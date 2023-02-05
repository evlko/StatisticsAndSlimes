using UnityEngine;

namespace Gameplay
{
    public abstract class InteractionObject : MonoBehaviour
    {
        protected abstract void Interact(Transform t);
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.transform.GetComponent<Dragging>()?.isDragging == false)
            {
                Interact(other.transform);
            }
        }
    }

    public enum InteractionTags
    {
        All,
        Slime,
        InteractionObject,
        None,
    }
}
