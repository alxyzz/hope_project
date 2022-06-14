using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TripManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int tripStatus = 0;
    public DrugTrip currentDrugState;
    SoberTrip soberTripState = new SoberTrip();
    FirstStageTrip firstStageTripState = new FirstStageTrip();
    SecondStageTrip secondStageTripState = new SecondStageTrip();
    ThirdStageTrip thirdStageTripState = new ThirdStageTrip();
    FourthStageTrip fourthStageTripState = new FourthStageTrip();
    FifthStageTrip fifthStageTripState = new FifthStageTrip();

    //so when the profile changes, it takes the profile associated to the drug trip stage and sets it as the current global volume profile
    

    public List<Light> allObjects = new List<Light>(); //so we can do fancy lightshows
    public int hallucinationStrength, chromaticAberrationVariationRate, wobbleStrength, colorChangeStrength, tripInSeconds = 30;
    public float timeLeft; //this will probably not be based on time, and more on events happening, so the player does not feel rushed
    [SerializeField]
    private VolumeProfile workingProfile, soberProfile, firstStageProfile, secondStageProfile, thirdStageTripProfile, fourthStageTripProfile, fifthStageTripProfile;
    [SerializeField]
    private Volume globalVolume;




    void Start()
    {
        SetupTrips();
        //InitializeOverrides();
        currentDrugState = soberTripState;




    }


    void SetupTrips()
    {
        soberTripState.NextStage = firstStageTripState;
        firstStageTripState.PreviousStage = soberTripState;

        firstStageTripState.PreviousStage = soberTripState;
        firstStageTripState.NextStage = secondStageTripState;

        secondStageTripState.PreviousStage = firstStageTripState;
        secondStageTripState.NextStage = thirdStageTripState;

        thirdStageTripState.PreviousStage = secondStageTripState;
        thirdStageTripState.NextStage = fourthStageTripState;

        fourthStageTripState.PreviousStage = thirdStageTripState;
        fourthStageTripState.NextStage = fifthStageTripState;

        fifthStageTripState.PreviousStage = fourthStageTripState;
        fifthStageTripState.NextStage = null; //death


        soberTripState.backgroundMusic = fifthBackgroundAudio;
        firstStageTripState.backgroundMusic = fifthBackgroundAudio;
        secondStageTripState.backgroundMusic = fifthBackgroundAudio;
        thirdStageTripState.backgroundMusic = fifthBackgroundAudio;
        fourthStageTripState.backgroundMusic = fifthBackgroundAudio;
        fifthStageTripState.backgroundMusic = fifthBackgroundAudio;


        //workingProfile, soberProfile, firstStageProfile, secondStageProfile, thirdStageTripProfile, fourthStageTripProfile, fifthStageTripProfile;
        soberTripState.associatedProfile = soberProfile;
        firstStageTripState.associatedProfile = firstStageProfile;
        secondStageTripState.associatedProfile = secondStageProfile;
        thirdStageTripState.associatedProfile = thirdStageTripProfile;
        fourthStageTripState.associatedProfile = fourthStageTripProfile;
        fifthStageTripState.associatedProfile = fifthStageTripProfile;
    }

    AudioClip soberBackgroundAudio, firstBackgroundAudio, secondBackgroundAudio, thirdBackgroundAudio, fourthBackgroundAudio, fifthBackgroundAudio;

    /*
0. Sober --> everything a bit shitty  ;(
1. Drug use/stage --> the player is forced in the "tutorial level" (no impact on maximum hope) ; everything beautiful and pastel coloured/light colours (we stay with this colouring for the other stages)
2. Drug use/stage --> Objects switch with more unrealistic objects (table might be a bathtub --> we can re-use objects!) and new objects and characters appear (just a few); 
3. Drug use/stage --> everything is getting a bit distorted (filters (?))and stops making sense;  character might become abstract/ lose colour and be white and black (we can basically do whatever we feel like and find funny in this stage :D)
4. Drug use/stage --> Nothing changes regarding the environment/characters, BUT the environment is turned upside down; the character is walking on the ceiling limiting  possible interactions to a minimum 
5. Drug use/stage --> player dies of overdose. A countdown and weird music starts (death is inevitable) ; countdown ends: ambulance siren and blackout 
     */


    public abstract class DrugTrip
    {
        public VolumeProfile associatedProfile;


        public virtual bool seeUnreality { get; } //see weird objects from stage 2 onwards;
        public virtual bool graphicVariance { get; } //see weird objects from stage 2 onwards;
        protected bool flipWorld { get; }
        public AudioClip backgroundMusic { get; set; }







        public DrugTrip PreviousStage { get; set; }
        public DrugTrip NextStage { get; set; }


        /// <summary>
        /// this runs every tick. use delays if you want something to be done periodically
        /// </summary>
        /// 
        public virtual void OnGet()
        {
            if (associatedProfile != null)
            {
                DataStorage.GameManagerComponent.TripManagerComponent.workingProfile = associatedProfile;
                if (backgroundMusic != null)
                {
                    DataStorage.GameManagerComponent.SoundManagerComponent.ChangeMusic(backgroundMusic);
                }
            }
            
        }
        public virtual void Cycle()
        {

        }

        public virtual void ComeDown()
        {//go back a level
            if (this.GetType() != typeof(SoberTrip))
            {
                DataStorage.GameManagerComponent.TripManagerComponent.currentDrugState = PreviousStage;
                DataStorage.GameManagerComponent.TripManagerComponent.currentDrugState.OnGet();
            }
        }

        public virtual void Intensify()
        {//go back a level
            if (this.GetType() != typeof(FifthStageTrip))
            {
                DataStorage.GameManagerComponent.TripManagerComponent.currentDrugState = NextStage;
                DataStorage.GameManagerComponent.TripManagerComponent.currentDrugState.OnGet();
            }
        }


    }

    class SoberTrip : DrugTrip
    {
        new VolumeProfile associatedProfile { get; set; }
        new public bool seeUnreality = false;
        new public bool graphicVariance = false;
        new public bool flipWorld = false;
        new public AudioClip backgroundMusic { get; set; }

        public new DrugTrip PreviousStage = null;
        public new DrugTrip NextStage { get; set; }

        public override void Cycle()
        {

        }

    }
    class FirstStageTrip : DrugTrip
    {
        new VolumeProfile associatedProfile { get; set; }
        new public bool seeUnreality = false;
        new public bool graphicVariance = false;
        new public bool flipWorld = false;
        new public AudioClip backgroundMusic { get; set; }
        public new DrugTrip PreviousStage { get; set; }
        public new DrugTrip NextStage { get; set; }

        public override void Cycle()
        {

        }

    }
    class SecondStageTrip : DrugTrip
    {
        new VolumeProfile associatedProfile { get; set; }
        new public bool seeUnreality = false;
        new public bool graphicVariance = false;
        new public bool flipWorld = false;
        new public AudioClip backgroundMusic { get; set; }
        public new DrugTrip PreviousStage { get; set; }
        public new DrugTrip NextStage { get; set; }

        public override void Cycle()
        {

        }
    }
    class ThirdStageTrip : DrugTrip
    {
        new VolumeProfile associatedProfile { get; set; }
        new public bool seeUnreality = false;
        new public bool graphicVariance = false;
        new public bool flipWorld = false;
        new public AudioClip backgroundMusic { get; set; }
        public new DrugTrip PreviousStage { get; set; }
        public new DrugTrip NextStage { get; set; }

        public override void Cycle()
        {

        }
    }
    class FourthStageTrip : DrugTrip
    {
        new VolumeProfile associatedProfile { get; set; }
        new public bool seeUnreality = false;
        new public bool graphicVariance = false;
        new public bool flipWorld = true;
        new public AudioClip backgroundMusic { get; set; }
        public new DrugTrip PreviousStage { get; set; }
        public new DrugTrip NextStage { get; set; }

        public override void Cycle()
        {

        }
    }
    class FifthStageTrip : DrugTrip
    {
        new VolumeProfile associatedProfile { get; set; }
        new public bool seeUnreality = false;
        new public bool graphicVariance = false;
        new public bool flipWorld = false;
        new public AudioClip backgroundMusic { get; set; }
        public new DrugTrip PreviousStage { get; set; }
        public new DrugTrip NextStage { get; set; }

        public override void Cycle()
        {

        }
    }

    ChromaticAberration profileChromaticAberration;
    SplitToning profileSplitToning;


    bool alternateCA;
    bool alternateST;


    /// <summary>
    /// refreshes the references to the current profile qualities , like chromatic aberration intensity, so we can modify them gradually
    /// </summary>
    void ReinitializeProfileQualities()
    {

        ChromaticAberration tmp;
        SplitToning tmp2;

        if (currentDrugState.associatedProfile.TryGet<ChromaticAberration>(out tmp))
        {
            profileChromaticAberration = tmp;
        }

        if (currentDrugState.associatedProfile.TryGet<SplitToning>(out tmp2))
        {
            profileSplitToning = tmp2;
        }

    }


    //void DoGoodTripCycleCA()
    //{
    //    if (alternateCA)
    //    {
    //        goodTripCA.intensity.value = Mathf.Clamp(goodTripCA.intensity.value + chromaticAberrationVariationRate * Time.deltaTime, goodTripCA.intensity.min, goodTripCA.intensity.max);
    //    }
    //    else
    //    {
    //        goodTripCA.intensity.value = Mathf.Clamp(goodTripCA.intensity.value - chromaticAberrationVariationRate * Time.deltaTime, goodTripCA.intensity.min, goodTripCA.intensity.max);
    //    }

    //    if (goodTripCA.intensity.value >= goodTripCA.intensity.max)
    //    {
    //        alternateCA = false;
    //    }
    //    else if (goodTripCA.intensity.value <= goodTripCA.intensity.min)
    //    {
    //        alternateCA = true;
    //    }
    //}
    //void DoGoodTripCycleST()
    //{
    //    if (alternateST)
    //    {
    //        goodTripST.balance.value += colorChangeStrength * Time.deltaTime;
    //    }
    //    else
    //    {
    //        goodTripST.balance.value -= colorChangeStrength * Time.deltaTime;
    //    }

    //    if (goodTripST.balance.value >= 100)
    //    {
    //        alternateST = false;
    //    }
    //    else if (goodTripST.balance.value <= -100)
    //    {
    //        alternateST = true;
    //    }
    //}

    //void DoBadTripCycleCA()
    //{
    //    if (alternateCA)
    //    {
    //        badTripCA.intensity.value = Mathf.Clamp(badTripCA.intensity.value + chromaticAberrationVariationRate * Time.deltaTime, badTripCA.intensity.min, badTripCA.intensity.max);
    //    }
    //    else
    //    {
    //        badTripCA.intensity.value = Mathf.Clamp(badTripCA.intensity.value - chromaticAberrationVariationRate * Time.deltaTime, badTripCA.intensity.min, badTripCA.intensity.max);
    //    }

    //    if (badTripCA.intensity.value >= badTripCA.intensity.max)
    //    {
    //        alternateCA = false;
    //    }
    //    else if (badTripCA.intensity.value <= badTripCA.intensity.min)
    //    {
    //        alternateCA = true;
    //    }
    //}
    //void DoBadTripCycleST()
    //{
    //    if (alternateST)
    //    {
    //        badTripST.balance.value -= colorChangeStrength * Time.deltaTime;
    //    }
    //    else
    //    {
    //        badTripST.balance.value -= colorChangeStrength * Time.deltaTime;
    //    }

    //    if (badTripST.balance.value >= 100)
    //    {
    //        alternateST = false;
    //    }
    //    else if (badTripST.balance.value <= -100)
    //    {
    //        alternateST = true;
    //    }
    //}




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
        //TripCycle();




    }


    //this also contains stuff we don't want to define in the drug trip's Cycle proc
    private void DrugCycle()
    {


        currentDrugState.Cycle();
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
        currentDrugState.ComeDown();
    }

    public void Trip()
    {
        Debug.Log("having a good time");
        currentDrugState.Intensify();
    }

    public void BadTrip()
    {
        Debug.Log("having a BAD time");
        //globalVolume.profile = badTripProfile;
        tripStatus = 3;
        timeLeft = 30;
    }


    public void Fadeout()
    {


    }



}
