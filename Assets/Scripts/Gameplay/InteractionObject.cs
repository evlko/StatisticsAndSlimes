using UnityEngine;

namespace Gameplay
{
    public abstract class InteractionObject : MonoBehaviour
    {
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.transform.GetComponent<Dragging>()?.isDragging == false)
            {
                Interact(other.transform);
            }
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
