using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TripManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int tripStatus = 1;
    public List<Light> allObjects = new List<Light>(); //so we can do fancy lightshows
    public int hallucinationStrength, chromaticAberrationVariationRate, wobbleStrength, colorChangeStrength, tripInSeconds = 30;
    public float timeLeft; //this will probably not be based on time, and more on events happening, so the player does not feel rushed
    [SerializeField]
    private VolumeProfile badTripProfile, goodTripProfile, soberProfile;
    [SerializeField]
    private Volume globalVolume;


    ChromaticAberration badTripCA;
    ChromaticAberration goodTripCA;

    SplitToning badTripST;
    SplitToning goodTripST;
    bool alternateCA;
    bool alternateST;
    void Start()
    {
        InitializeOverrides();





    }

    void InitializeOverrides()
    {

        ChromaticAberration tmp;
        SplitToning tmp2;

        Volume volume = gameObject.GetComponent<Volume>();

        if (badTripProfile.TryGet<ChromaticAberration>(out tmp))
        {
            badTripCA = tmp;
        }

        if (goodTripProfile.TryGet<ChromaticAberration>(out tmp))
        {
            goodTripCA = tmp;
        }

        if (badTripProfile.TryGet<SplitToning>(out tmp2))
        {
            badTripST = tmp2;
        }

        if (goodTripProfile.TryGet<SplitToning>(out tmp2))
        {
            goodTripST = tmp2;
        }
    }


    void DoGoodTripCycleCA()
    {
        if (alternateCA)
        {
            goodTripCA.intensity.value = Mathf.Clamp(goodTripCA.intensity.value + chromaticAberrationVariationRate * Time.deltaTime, goodTripCA.intensity.min, goodTripCA.intensity.max);
        }
        else
        {
            goodTripCA.intensity.value = Mathf.Clamp(goodTripCA.intensity.value - chromaticAberrationVariationRate * Time.deltaTime, goodTripCA.intensity.min, goodTripCA.intensity.max);
        }

        if (goodTripCA.intensity.value >= goodTripCA.intensity.max)
        {
            alternateCA = false;
        }
        else if (goodTripCA.intensity.value <= goodTripCA.intensity.min)
        {
            alternateCA = true;
        }
    }
    void DoGoodTripCycleST()
    {
        if (alternateST)
        {
            goodTripST.balance.value += colorChangeStrength * Time.deltaTime;
        }
        else
        {
            goodTripST.balance.value -= colorChangeStrength * Time.deltaTime;
        }

        if (goodTripST.balance.value >= 100)
        {
            alternateST = false;
        }
        else if (goodTripST.balance.value <= -100)
        {
            alternateST = true;
        }
    }

    void DoBadTripCycleCA()
    {
        if (alternateCA)
        {
            badTripCA.intensity.value = Mathf.Clamp(badTripCA.intensity.value + chromaticAberrationVariationRate * Time.deltaTime, badTripCA.intensity.min, badTripCA.intensity.max);
        }
        else
        {
            badTripCA.intensity.value = Mathf.Clamp(badTripCA.intensity.value - chromaticAberrationVariationRate * Time.deltaTime, badTripCA.intensity.min, badTripCA.intensity.max);
        }

        if (badTripCA.intensity.value >= badTripCA.intensity.max)
        {
            alternateCA = false;
        }
        else if (badTripCA.intensity.value <= badTripCA.intensity.min)
        {
            alternateCA = true;
        }
    }
    void DoBadTripCycleST()
    {
        if (alternateST)
        {
            badTripST.balance.value -= colorChangeStrength * Time.deltaTime;
        }
        else
        {
            badTripST.balance.value -= colorChangeStrength * Time.deltaTime;
        }

        if (badTripST.balance.value >= 100)
        {
            alternateST = false;
        }
        else if (badTripST.balance.value <= -100)
        {
            alternateST = true;
        }
    }




    void TripCycle()
    {
        switch (tripStatus)
        {
            case 1://sober

                break;
            case 2://good trip
                //DoGoodTripCycleCA();
                DoGoodTripCycleST();

                timeLeft -= Time.deltaTime;
                CheckEndTrip();
                break;
            case 3: //bad trip

                DoBadTripCycleCA();
                DoBadTripCycleST();
                timeLeft -= Time.deltaTime;
                CheckEndTrip();
                break;
        }
    }


    private void Update()
    {
        TripCycle();

    }


    private void CheckEndTrip()
    {
        if (timeLeft <= 0)
        {
            GetSober();
        }
    }



    public void GetSober()
    {
        globalVolume.profile = soberProfile;
        tripStatus = 1;
    }

    public void Trip()
    {
        Debug.Log("having a good time");
        globalVolume.profile = goodTripProfile;
        tripStatus = 2;
        timeLeft = 30;
    }

    public void BadTrip()
    {
        Debug.Log("having a BAD time");
        globalVolume.profile = badTripProfile;
        tripStatus = 3;
        timeLeft = 30;
    }


    public void Fadeout()
    {


    }



}
