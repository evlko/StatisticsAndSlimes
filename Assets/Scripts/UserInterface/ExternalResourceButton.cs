using UnityEngine;

namespace UserInterface
{
    public class ExternalResourceButton : MonoBehaviour
    {
        public void OpenResource(string source)
        {
            Application.ExternalEval("window.open('" + source + "', '_blank')");
        }
    }
}
