using UnityEngine;

namespace Plots
{
    public class BoxPlot : MonoBehaviour
    {
        [Header("Values")]
        [SerializeField] private float medianValue;
        [SerializeField] private float minValue;
        [SerializeField] private float maxValue;
        [SerializeField] private float firstQuantileValue;
        [SerializeField] private float thirdQuantileValue;

        [Header("Transforms")]
        [SerializeField] private Transform medianBox;
        [SerializeField] private Transform minBox;
        [SerializeField] private Transform maxBox;
        [SerializeField] private Transform firstQuantileBox;
        [SerializeField] private Transform thirdQuantileBox;

        [Header("Options")]
        [SerializeField] private float valueToSizeRatio;
        [SerializeField] private float valueInitialCenter;

        private void Update()
        {
            DrawPlot();
        }

        private void DrawPlot()
        {
            var centeredMedianValue = medianValue - valueInitialCenter;
            medianBox.transform.position = new Vector2(centeredMedianValue * valueToSizeRatio * 0.1f, 0);
            minBox.transform.localScale = new Vector2((medianValue - minValue) * valueToSizeRatio, 0.1f);
            maxBox.transform.localScale = new Vector2((maxValue - medianValue) * valueToSizeRatio, 0.1f);
            firstQuantileBox.transform.localScale = new Vector2((medianValue - firstQuantileValue) * valueToSizeRatio, 1f);
            thirdQuantileBox.transform.localScale = new Vector2((thirdQuantileValue - medianValue) * valueToSizeRatio, 1f);
        }
    }
}
