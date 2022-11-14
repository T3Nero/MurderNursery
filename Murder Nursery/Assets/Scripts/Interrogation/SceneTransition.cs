using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Image blackFade;
    public bool toBlack;
    public bool screenBlack;
    private GameObject manager;
    public GameObject mainCamera;
    public GameObject interrogationCam;
    public GameObject currentCam;
    public bool interrogationActive;
    private float currentCountdownValue;
    public GameObject noirFilter;
    public GameObject femmeIntObject;
    public GameObject juiceIntObject;
    public GameObject goonIntObject;
    public GameObject coolIntObject;
    public GameObject activeInterrogant;
    public GameObject interrogationManager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager"); //Finds and stores manager object
        interrogationActive = false; //Signals that the player is not in an interrogation when they spawn in
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.X)) //Testing purposes only
        {
            ChangeToMainArea();
        }

    }

    private void ChangeCam(GameObject currentCam, GameObject newCam) //Activates the desired cam and deactivates the previous cam
    {
        currentCam.SetActive(false);
        newCam.SetActive(true);
        currentCam = newCam;
    }

    public void ChangeToInterrogation(GameObject npc) //Check here for detective outfit //This method is called when the player is transitioned from dialogue to an interrogation
    {
        blackFade.gameObject.SetActive(true); //Activates the image which serves as our fade to black
        StartCoroutine(BlackTransitionToInterrogation(mainCamera, interrogationCam)); //Activates the gradual fade to black
        StartCoroutine(WaitForSeconds()); //Waits for a few seconds and activates the reverse fade
        if(npc.name == "FemmeFatale") //Checks which npc the player is talking to and moves them to the interrogation.
        {
            femmeIntObject.SetActive(true);
            activeInterrogant = femmeIntObject;
            interrogationManager.GetComponent<Interrogation>().StartInterrogation(activeInterrogant.GetComponent<NPCDialogue>().dialogueTree[0]);
        }
        if(npc.name == "CoolGangstaKid")
        {
            coolIntObject.SetActive(true);
            activeInterrogant = coolIntObject;
        }
        if(npc.name == "JuiceBox")
        {
            juiceIntObject.SetActive(true);
            activeInterrogant=juiceIntObject;
        }
        if(npc.name == "GoonGuy")
        {
            goonIntObject.SetActive(true);
            activeInterrogant = goonIntObject;
        }
    }
    
    public void ChangeToMainArea() //This method is called when the player is being transitioned back to the main play area
    {
        blackFade.gameObject.SetActive(true);
        StartCoroutine(BlackTransitionToMainArea(interrogationCam, mainCamera));
        StartCoroutine(WaitForSecondsMain());
    }

    public IEnumerator BlackTransitionToInterrogation(GameObject currentCam, GameObject desiredCam, bool transitionToBlack = true, int timeToFade = 1) //Gradually fades the screen to black or white depending on current screen status
    {
        Color screenColour = blackFade.color;
        float fadeProgress;
        if (transitionToBlack)
        {           
            while (blackFade.color.a < 1)
            {
                fadeProgress = screenColour.a  + (timeToFade * Time.deltaTime);
                screenColour = new Color(screenColour.r, screenColour.g, screenColour.b, fadeProgress);
                blackFade.color = screenColour;
                if(fadeProgress > 0.95f)
                {
                    ChangeCam(currentCam, desiredCam);
                    noirFilter.GetComponent<PostProcessingActivation>().TurnFilterOn(true);
                    interrogationActive = true;
                }
                
                yield return null;
            }
        }
        else
        {
            while(blackFade.color.a > 0)
            {
                fadeProgress = screenColour.a - (timeToFade * Time.deltaTime);
                screenColour = new Color(screenColour.r, screenColour.g, screenColour.b, fadeProgress);
                blackFade.color = screenColour;
                if(fadeProgress < 0.01f)
                {
                    interrogationManager.GetComponent<Interrogation>().interrogationPanel.SetActive(true);
                    blackFade.gameObject.SetActive(false);
                    yield return null;
                }
                yield return null;
            }
        }
    }

    public IEnumerator BlackTransitionToMainArea(GameObject currentCam, GameObject desiredCam, bool transitionToBlack = true, int timeToFade = 1)
    {
        Color screenColour = blackFade.color;
        float fadeProgress;
        if (transitionToBlack)
        {
            while (blackFade.color.a < 1)
            {
                fadeProgress = screenColour.a + (timeToFade * Time.deltaTime);
                screenColour = new Color(screenColour.r, screenColour.g, screenColour.b, fadeProgress);
                blackFade.color = screenColour;
                if (fadeProgress > 0.95f)
                {
                    ChangeCam(currentCam, desiredCam);
                    noirFilter.GetComponent<PostProcessingActivation>().TurnFilterOn(false);
                    activeInterrogant.SetActive(false);
                    interrogationActive = false;
                }

                yield return null;
            }
        }
        else
        {
            while (blackFade.color.a > 0)
            {
                fadeProgress = screenColour.a - (timeToFade * Time.deltaTime);
                screenColour = new Color(screenColour.r, screenColour.g, screenColour.b, fadeProgress);
                blackFade.color = screenColour;
                if(fadeProgress < 0.01f)
                {
                    blackFade.gameObject.SetActive(false);
                    yield return null;
                }
                yield return null;
            }
        }
    }

    public IEnumerator WaitForSeconds(float countdownValue = 2)
    {
        currentCountdownValue = countdownValue;
        while (currentCountdownValue > 0)
        {
            yield return new WaitForSeconds(1);
            currentCountdownValue--;
        }
        StartCoroutine(BlackTransitionToInterrogation(mainCamera, interrogationCam, false));
    }

    public IEnumerator WaitForSecondsMain(float countdownValue = 2)
    {
        currentCountdownValue = countdownValue;
        while (currentCountdownValue > 0)
        {
            yield return new WaitForSeconds(1);
            currentCountdownValue--;
        }
        StartCoroutine(BlackTransitionToInterrogation(interrogationCam, mainCamera, false)); 

    }

}
