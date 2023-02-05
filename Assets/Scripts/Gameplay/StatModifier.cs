using UnityEngine;
using Models;

namespace Gameplay
{
    public class StatModifier : InteractionObject
    {
        [SerializeField] protected SlimeQuantitativeFeatures modifiedQuantitativeFeature;
        [SerializeField] protected float value;

        protected override void Interact(Transform t)
        {
            t.GetComponent<Slime>()?.UpdateSlimeQuantitativeFeature(modifiedQuantitativeFeature, value);
        }
    }
}
