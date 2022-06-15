using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopThroughColor : MonoBehaviour
{
    public Image targetImage;

    float H, S, V;
    void Update()
    {
        Color.RGBToHSV(targetImage.color, out H, out S, out V);
        H += 0.01f;
        if (H >= 1f)
        {
            H = 0.01f;
        }

        targetImage.color = Color.HSVToRGB(H,S,V);
    }


}
