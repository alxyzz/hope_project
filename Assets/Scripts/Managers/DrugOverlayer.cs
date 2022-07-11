using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrugOverlayer : MonoBehaviour
{
    public List<Sprite> overlayedImages = new();
    public Image targetImage;

    public float imageChangeFrequency;

    [HideInInspector]
    public bool overlaying;
    public bool epileptic;

    private void Start()
    {
        imageListCount = overlayedImages.Count;
    }

    public void startOverlaying()
    {
        if (epileptic || imageListCount == 0)
        {
            return;
        }

        StartCoroutine(overlay());

    }
    private int imageListCount;
    private int imageIndex;
    IEnumerator overlay()
    {
        
        while (overlaying)
        {
            yield return new WaitForSecondsRealtime(imageChangeFrequency);
            targetImage.texture = overlayedImages[imageIndex];
            imageIndex++;
            if (imageIndex >= imageListCount && imageListCount != 0)
            {
                imageIndex = 0;
            }
        }
    }
}
