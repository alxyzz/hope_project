using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{

    public GameObject creditsGO;
    public Transform creditsText;
    public float creditRollSpeed = 2f;
    public bool creditsHaveStarted = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (creditsHaveStarted)
        {
            creditsText.position = new Vector3(creditsText.position.x, creditsText.position.y + creditRollSpeed*Time.deltaTime);
            if (creditsText.position.y > 4500)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void RollCredits()
    {
        creditsGO.SetActive(true);
        creditsHaveStarted = true;
    }
}
