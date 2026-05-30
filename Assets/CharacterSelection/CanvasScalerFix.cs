using UnityEngine;
using UnityEngine.UI;

public class CanvasScalerFix : MonoBehaviour
{
    void Start()
    {
        CanvasScaler scaler = GetComponent<CanvasScaler>();

        if (scaler.uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize)
        {
            float aspect = (float)Screen.width / Screen.height;

            if (aspect < 1.4f)
            {
                // iPads or square screens → favor height
                scaler.matchWidthOrHeight = 1f;
            }
            else
            {
                // phones or widescreens → balanced or width
                scaler.matchWidthOrHeight = 0.5f;
            }
        }
    }
}
