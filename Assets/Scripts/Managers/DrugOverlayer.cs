using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrugOverlayer : MonoBehaviour
{
    public List<Texture> overlayedImages = new();
    public RawImage targetImage;

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
        overlaying = true;
        targetImage.gameObject.SetActive(true);
        StartCoroutine(overlay());

    }

     public void stopOverlaying()
    {
        targetImage.gameObject.SetActive(false);
        overlaying = false;

    }

    private int imageListCount;
    private int imageIndex;
    IEnumerator overlay()
    {
        
        while (overlaying)
        {
            yield return new WaitForSecondsRealtime(imageChangeFrequency);
            targetImage.texture = overlayedImages[imageIndex];
            imageIndex = Mathf.Clamp(imageIndex + 1, 0, imageListCount);
        }
    }
}
