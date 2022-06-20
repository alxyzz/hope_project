using UnityEngine;

public class Flicker : MonoBehaviour
{

    /// <summary>
    /// the value the intensity is modified with, towards the next variable
    /// </summary>
    public float flickerIncrement;
    /// <summary>
    /// //the target intensity of light during a flicker. subtract it from the original intensity
    /// </summary>
    public float flickerTargetLightIntensityOffset;


    private float originalIntensity;

    public bool enableFlickering;
    private Light myLight;

    /*
     * How this works is basically every x seconds we flicker
     * if we are flickering we will randomly modify the light for a while
     * then we stop, and return to the initial intensity
     * 
     * 
     * 
     * 
     */


    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        originalIntensity = myLight.intensity;

    }


    private float temporaryIntensitySaveForFlickerPausing;
    /// <summary>
    /// used to pause flickering in case we need to do something with the light intensity, in particular. if you want to affect the color this is unecessary
    /// </summary>
    /// <param name="toggle"></param>
    public void ToggleFlicker(bool toggle)
    {
        if (toggle)
        {
            temporaryIntensitySaveForFlickerPausing = myLight.intensity;
            enableFlickering = false;
        }
        else
        {
            myLight.intensity = temporaryIntensitySaveForFlickerPausing;
            enableFlickering = true;
        }


    }


    // Update is called once per frame
    void Update()
    {
        if (enableFlickering == false)
        {//just dont act if enable flickering is off
            return;
        }


        //myLight.intensity = Random.Range(originalIntensity - flickerTargetLightIntensityOffset, originalIntensity);
        myLight.intensity = Mathf.Lerp(Random.Range(originalIntensity - flickerTargetLightIntensityOffset, originalIntensity), originalIntensity, flickerIncrement * Time.deltaTime);
        //////////THIS HANDLES WHAT FLICKERING DOES
        //if (Random.Range(0, 10) > 5) //50% of the time
        //{//randomly either increase
        //    myLight.intensity = Mathf.Lerp(myLight.intensity, originalIntensity, flickerIncrement * Time.deltaTime);
        //}
        //else
        //{// or decrease light intensity during the flickering period
        //    myLight.intensity = Mathf.Lerp(myLight.intensity, (originalIntensity - flickerTargetLightIntensityOffset), flickerIncrement * Time.deltaTime);
        //}
    }
}

