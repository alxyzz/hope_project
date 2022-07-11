using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TripManager : MonoBehaviour
{

    [HideInInspector]
    public List<GameObject> soberOnlyGameObjects = new();
    [HideInInspector]
    public List<GameObject> highOnlyGameObjects = new();

    public DrugOverlayer Overlays;


    // Start is called before the first frame update
    [HideInInspector]
    public bool wasHigh = true; //for turning objects visible or visible based on drug state without looping thru all the objects every update
    public int tripStatus = 0;
    AudioClip soberBackgroundAudio, firstBackgroundAudio, secondBackgroundAudio, thirdBackgroundAudio, fourthBackgroundAudio, fifthBackgroundAudio;
    //so when the profile changes, it takes the profile associated to the drug trip stage and sets it as the current global volume profile


    public List<Light> allObjects = new List<Light>(); //so we can do fancy lightshows
    public int hallucinationStrength, chromaticAberrationVariationRate, wobbleStrength, colorChangeStrength, tripInSeconds = 30;
    public float timeLeft; //this will probably not be based on time, and more on events happening, so the player does not feel rushed
    [SerializeField]
    private VolumeProfile soberProfile, firstStageProfile, secondStageProfile, thirdStageProfile, fourthStageProfile, fifthStageProfile;
    [HideInInspector]
    private VolumeProfile workingProfile;

    [SerializeField]
    private Volume globalVolume;




    void Awake()
    {
        ReinitializeProfileQualities();
    }

    private bool seeUnreality; //see weird objects from stage 2 onwards;
    private bool graphicVariance; //see weird objects from stage 2 onwards;
    private bool flipWorld;
    public AudioClip backgroundMusic;


    /*
0. Sober --> everything a bit shitty  ;(
1. Drug use/stage --> the player is forced in the "tutorial level" (no impact on maximum hope) ; everything beautiful and pastel coloured/light colours (we stay with this colouring for the other stages)
2. Drug use/stage --> Objects switch with more unrealistic objects (table might be a bathtub --> we can re-use objects!) and new objects and characters appear (just a few); 
3. Drug use/stage --> everything is getting a bit distorted (filters (?))and stops making sense;  character might become abstract/ lose colour and be white and black (we can basically do whatever we feel like and find funny in this stage :D)
4. Drug use/stage --> Nothing changes regarding the environment/characters, BUT the environment is turned upside down; the character is walking on the ceiling limiting  possible interactions to a minimum 
5. Drug use/stage --> player dies of overdose. A countdown and weird music starts (death is inevitable) ; countdown ends: ambulance siren and blackout 
     */




    ChromaticAberration profileChromaticAberration;
    SplitToning profileSplitToning;


    private bool alternateCA;//not important - used to make the value go from one side of the range to another
    private bool alternateST;


    /// <summary>
    /// refreshes the references to the current profile qualities , like chromatic aberration intensity, so we can modify them gradually
    /// </summary>
    void ReinitializeProfileQualities()
    {
        if (workingProfile == null)
        {
            Debug.Log("working profile was null. Cancelling  ReinitializeProfileQualities()");
            return;
        }
        ChromaticAberration tmp;
        SplitToning tmp2;

        if (workingProfile.TryGet<ChromaticAberration>(out tmp))
        {
            profileChromaticAberration = tmp;
        }

        if (workingProfile.TryGet<SplitToning>(out tmp2))
        {
            profileSplitToning = tmp2;
        }

    }


    void CycleCA()
    {
        if (profileChromaticAberration == null)
        {
            ReinitializeProfileQualities();
        }
        if (alternateCA)
        {
            profileChromaticAberration.intensity.value = Mathf.Clamp(profileChromaticAberration.intensity.value + chromaticAberrationVariationRate * Time.deltaTime, profileChromaticAberration.intensity.min, profileChromaticAberration.intensity.max);
        }
        else
        {
            profileChromaticAberration.intensity.value = Mathf.Clamp(profileChromaticAberration.intensity.value - chromaticAberrationVariationRate * Time.deltaTime, profileChromaticAberration.intensity.min, profileChromaticAberration.intensity.max);
        }

        if (profileChromaticAberration.intensity.value >= profileChromaticAberration.intensity.max)
        {
            alternateCA = false;
        }
        else if (profileChromaticAberration.intensity.value <= profileChromaticAberration.intensity.min)
        {
            alternateCA = true;
        }
    }

    void CycleST()
    {
        if (profileSplitToning == null)
        {
            ReinitializeProfileQualities();
        }

        if (alternateST)
        {
            profileSplitToning.balance.value += colorChangeStrength * Time.deltaTime;
        }
        else
        {
            profileSplitToning.balance.value -= colorChangeStrength * Time.deltaTime;
        }

        if (profileSplitToning.balance.value >= 100)
        {
            alternateST = false;
        }
        else if (profileSplitToning.balance.value <= -100)
        {
            alternateST = true;
        }
    }




    ////void TripCycle()
    ////{
    ////    switch (tripStatus)
    ////    {
    ////        case 1://sober

    ////            break;
    ////        case 2://good trip
    ////            //DoGoodTripCycleCA();
    ////            //DoGoodTripCycleST();

    ////            timeLeft -= Time.deltaTime;
    ////            CheckEndTrip();
    ////            break;
    ////        case 3: //bad trip

    ////            //DoBadTripCycleCA();
    ////            //DoBadTripCycleST();
    ////            timeLeft -= Time.deltaTime;
    ////            CheckEndTrip();
    ////            break;
    ////    }
    ////}


    private void Update()
    {
        DrugCycle();




    }

    private void DrugCycle()
    {

        if (workingProfile != null)
        {
            if (tripStatus != 0)
            {
                CycleCA();
                CycleST();
            }

        }
        else
        {
            Debug.Log("current drug state was null - " + workingProfile);
            workingProfile = soberProfile;
        }

        //CheckEndTrip();
    }

    //private void CheckEndTrip()
    //{
    //    if (timeLeft <= 0)
    //    {
    //        currentDrugState.ComeDown();
    //    }
    //}

    void OnDrugStateChange()
    {
        Overlays.overlaying = false;

        ChangeHallucinatedObjectVisibilityStatus();
        Debug.Log("drug state changed");
        switch (tripStatus)
        {
            case 0:
                workingProfile = soberProfile;
                flipWorld = false;
                DataStorage.GameManagerComponent.SoundComponent.ChangeBGMusic("rain_ambient");
                break;


            case 1:
                workingProfile = firstStageProfile;
                flipWorld = false;
                break;


            case 2:
                workingProfile = secondStageProfile;
                flipWorld = false;
                DataStorage.GameManagerComponent.SoundComponent.ChangeBGMusic("stage2_bg_music");
                break;


            case 3:
                workingProfile = thirdStageProfile;
                flipWorld = false;
                break;


            case 4:
                workingProfile = fourthStageProfile;
                Overlays.startOverlaying();
                flipWorld = true;
                DataStorage.GameManagerComponent.SoundComponent.ChangeBGMusic("stage4_bg_music");
                break;

            case 5:
                workingProfile = fifthStageProfile;
                flipWorld = false;
                StartCoroutine(delayedDeath()); // I feel numb...
                break;

            default:
                tripStatus = 0;
                OnDrugStateChange();
                break;
        }

        globalVolume.profile = workingProfile;
        if (flipWorld) Camera.main.transform.rotation = Quaternion.Euler(9.282f, -180f, 180f);
        else Camera.main.transform.rotation = Quaternion.Euler(9.282f, -180f, 0f);
        ReinitializeProfileQualities();
    }

    public void ChangeHallucinatedObjectVisibilityStatus()
    {
        
        if (wasHigh || tripStatus == 0) 
        {
            //loop through all sober-only objects and characters and set them active
            //loop through all hallucinated objects and characters and set them inactive
            foreach (GameObject item in soberOnlyGameObjects)
            {
                item.SetActive(true);
            }
            foreach (GameObject item in highOnlyGameObjects)
            {
                item.SetActive(false);
            }
            Debug.Log("ChangeHallucinatedObjectVisibilityStatus() - we just got sober");

        }
        else if (!wasHigh || tripStatus > 0)
        {
            //loop through all hallucinated objects and characters and set them active
            //loop through all sober-only objects and characters and set them inactive
            foreach (GameObject item in soberOnlyGameObjects)
            {
                item.SetActive(false);
            }
            foreach (GameObject item in highOnlyGameObjects)
            {
                item.SetActive(true);
            }
            Debug.Log("ChangeHallucinatedObjectVisibilityStatus() - we just got high");
        }
    }

    public void GetLow()
    {
        if (tripStatus > 0)
        {
            wasHigh = true;
        }
        Debug.Log("got a bit more sober. trip level is " + tripStatus.ToString());
        tripStatus = Mathf.Clamp(tripStatus - 1, 0, 6);
        OnDrugStateChange();

    }


    public void GetHigh()
    {
        if (tripStatus == 0)
        {
            wasHigh = false;
        }
        else
        {
            wasHigh = true;
        }
        tripStatus = Mathf.Clamp(tripStatus + 1, 0, 6);
        Debug.Log("having a good time. trip is intensifying. trip level is " + tripStatus.ToString());
        OnDrugStateChange();


    }

    IEnumerator delayedDeath()
    {

        yield return new WaitForSecondsRealtime(10);
    }

    public void Fadeout()
    {
        //this should make the screen fade to black

    }



}
