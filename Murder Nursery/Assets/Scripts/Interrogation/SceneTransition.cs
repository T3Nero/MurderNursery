using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Image blackFade; //Image used to fade the screen toblack 
    public bool toBlack; //Signal for screen fading 
    public bool screenBlack; //Signals that the screen is black
    private GameObject manager; //Stores the game manager 

    [Header("Cameras")]
    public GameObject mainCamera; //Main scene camera
    public GameObject interrogationCam; //Interrogation scene camera
    public GameObject currentCam; //Currently active cam 

    [Header("ST Variables")]
    public bool interrogationActive; //Signals that the interrogation is underway
    private float currentCountdownValue; //Used to determine the speed of the black fade
    public GameObject noirFilter; //The PP filter active in interrogation
    public GameObject activeInterrogant; //Stores the active interrogant 
    public GameObject interrogationManager; //Stores the interrogation manager 

    [Header("NPC Objects")]
    public GameObject femmeIntObject;//Stores an NPC object
    public GameObject juiceIntObject;//''
    public GameObject goonIntObject;//''
    public GameObject coolIntObject; //''
    public GameObject graceIntObject;

    public bool successfulInterrogation = false;
    public GameObject jbSummary;
    public GameObject scarletSummary;
    public GameObject eddieSummary;
    public GameObject chaseSummary;

    private bool jbSummaryViewed = false;
    private bool scarletSummaryViewed = false;
    private bool eddieSummaryViewed = false;
    private bool chaseSummaryViewed = false;

    public GameObject tutorialManager;
    public GameObject tutorialGrace;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager"); //Finds and stores manager object
        interrogationActive = false; //Signals that the player is not in an interrogation when they spawn in
        tutorialManager = GameObject.FindGameObjectWithTag("Tutorial Manager");
    }


    public void ChangeCam(GameObject currentCam, GameObject newCam) //Activates the desired cam and deactivates the previous cam
    {
        currentCam.SetActive(false);
        newCam.SetActive(true);
        currentCam = newCam;
    }

    public void ChangeToInterrogation(GameObject npc) //This method is called when the player is transitioned from dialogue to an interrogation
    {
        blackFade.gameObject.SetActive(true); //Activates the image which serves as our fade to black
        StartCoroutine(BlackTransitionToInterrogation(mainCamera, interrogationCam)); //Activates the gradual fade to black
        StartCoroutine(WaitForSeconds()); //Waits for a few seconds and activates the reverse fade
        if(npc.name == "Scarlet") //Checks which npc the player is talking to and moves them to the interrogation.
        {
            interrogationManager.GetComponent<Interrogation>().playerResponse1 = interrogationManager.GetComponent<Interrogation>().scarletPlayerResponseText;
            interrogationManager.GetComponent<Interrogation>().npcStatement = interrogationManager.GetComponent<Interrogation>().scarletStatementText;
            interrogationManager.GetComponent<Interrogation>().scarletPlayerResponseBox.SetActive(true);
            interrogationManager.GetComponent<Interrogation>().scarletStatementBox.SetActive(true);
            //interrogationManager.GetComponent<Interrogation>().playerResponse1.GetComponent<TextMeshProUGUI>().text = "Detective Drew:";
          //  interrogationManager.GetComponent<Interrogation>().npcStatement.GetComponent < TextMeshProUGUI>().text = "Scarlet: ";
            
            femmeIntObject.SetActive(true);
            activeInterrogant = femmeIntObject;
            interrogationManager.GetComponent<Interrogation>().StartInterrogation(activeInterrogant.GetComponent<NPCDialogue>().dialogueTree[0], activeInterrogant);
        }
        if(npc.name == "Chase")
        {
            interrogationManager.GetComponent<Interrogation>().playerResponse1 = interrogationManager.GetComponent<Interrogation>().chasePlayerResponseText;
            interrogationManager.GetComponent<Interrogation>().npcStatement = interrogationManager.GetComponent<Interrogation>().chaseStatementText;
            interrogationManager.GetComponent<Interrogation>().chasePlayerResponseBox.SetActive(true);
            interrogationManager.GetComponent<Interrogation>().chaseStatementBox.SetActive(true);
           // interrogationManager.GetComponent<Interrogation>().playerResponse1.GetComponent<TextMeshProUGUI>().text = "Detective Drew:";
          //  interrogationManager.GetComponent<Interrogation>().npcStatement.GetComponent<TextMeshProUGUI>().text = "Chase: ";
            coolIntObject.SetActive(true);
            activeInterrogant = coolIntObject;
            interrogationManager.GetComponent<Interrogation>().StartInterrogation(activeInterrogant.GetComponent<NPCDialogue>().dialogueTree[0], activeInterrogant);

        }
        if(npc.name == "Juice Box")
        {
            interrogationManager.GetComponent<Interrogation>().playerResponse1 = interrogationManager.GetComponent<Interrogation>().juiceBoxPlayerResponseText;
            interrogationManager.GetComponent<Interrogation>().npcStatement = interrogationManager.GetComponent<Interrogation>().juiceBoxStatementText;
            interrogationManager.GetComponent<Interrogation>().juiceBoxPlayerResponseBox.SetActive(true);
            interrogationManager.GetComponent<Interrogation>().juiceBoxStatementBox.SetActive(true);
          //  interrogationManager.GetComponent<Interrogation>().playerResponse1.GetComponent<TextMeshProUGUI>().text = "Detective Drew:";
          //  interrogationManager.GetComponent<Interrogation>().npcStatement.GetComponent<TextMeshProUGUI>().text = "Juice Box:";
            juiceIntObject.SetActive(true);
            activeInterrogant=juiceIntObject;
            
            interrogationManager.GetComponent<Interrogation>().StartInterrogation(activeInterrogant.GetComponent<NPCDialogue>().dialogueTree[0], activeInterrogant);
            
        }
        if(npc.name == "Eddie")
        {
            interrogationManager.GetComponent<Interrogation>().playerResponse1 = interrogationManager.GetComponent<Interrogation>().eddiePlayerResponseText;
            interrogationManager.GetComponent<Interrogation>().npcStatement = interrogationManager.GetComponent<Interrogation>().eddieStatementText;
            interrogationManager.GetComponent<Interrogation>().eddiePlayerResponseBox.SetActive(true);
            interrogationManager.GetComponent<Interrogation>().eddieStatementBox.SetActive(true);
          //  interrogationManager.GetComponent<Interrogation>().playerResponse1.GetComponent<TextMeshProUGUI>().text = "Detective Drew:";
          //  interrogationManager.GetComponent<Interrogation>().npcStatement.GetComponent<TextMeshProUGUI>().text = "Eddie: ";
            goonIntObject.SetActive(true);
            activeInterrogant = goonIntObject;
            interrogationManager.GetComponent<Interrogation>().StartInterrogation(activeInterrogant.GetComponent<NPCDialogue>().dialogueTree[0], activeInterrogant);
        }
        if(npc.name == "Grace")
        {
            interrogationManager.GetComponent<Interrogation>().playerResponse1 = interrogationManager.GetComponent<Interrogation>().gracePlayerResponseText;
            interrogationManager.GetComponent<Interrogation>().npcStatement = interrogationManager.GetComponent<Interrogation>().graceStatementText;
            interrogationManager.GetComponent<Interrogation>().gracePlayerResponseBox.SetActive(true);
            interrogationManager.GetComponent<Interrogation>().graceStatementBox.SetActive (true);
            graceIntObject.SetActive(true);
            activeInterrogant = graceIntObject;
            interrogationManager.GetComponent<Interrogation>().StartInterrogation(activeInterrogant.GetComponent<NPCDialogue>().dialogueTree[0], activeInterrogant);
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

    public IEnumerator BlackTransitionToMainArea(GameObject currentCam, GameObject desiredCam, bool transitionToBlack = true, int timeToFade = 1) //This method is similiar to the one above but is used for fading to main area
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
                    interrogationManager.GetComponent<Interrogation>().interrogationPanel.SetActive(false);
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
                    interrogationManager.GetComponent<Interrogation>().interrogationPanel.SetActive(false);
                    if(successfulInterrogation && interrogationManager.GetComponent<Interrogation>().jbCompleted &&!jbSummaryViewed)
                    {
                        jbSummary.SetActive(true);
                        successfulInterrogation = false;
                        jbSummaryViewed = true;
                    }
                    if(successfulInterrogation && interrogationManager.GetComponent<Interrogation>().scarletCompleted && !scarletSummaryViewed)
                    {
                        scarletSummary.SetActive(true);
                        successfulInterrogation = false;
                        scarletSummaryViewed = true;
                    }
                    if(successfulInterrogation && interrogationManager.GetComponent<Interrogation>().eddieCompleted && !eddieSummaryViewed)
                    {
                        eddieSummary.SetActive(true);
                        successfulInterrogation = false;
                        eddieSummaryViewed = true;
                    }
                    if(successfulInterrogation && interrogationManager.GetComponent<Interrogation>().chaseCompleted && !chaseSummaryViewed)
                    {
                        chaseSummary.SetActive(true);
                        successfulInterrogation = false;
                        chaseSummaryViewed = true;
                    }
                    if (tutorialManager.GetComponent<Tutorials>().inIPBTutorial)
                    {
                        manager.GetComponent<DialogueManager>().StartConversation(tutorialGrace.GetComponent<NPCDialogue>().dialogueTree[7], tutorialGrace, tutorialManager.GetComponent<Tutorials>().graceCam1);
                        tutorialManager.GetComponent<Tutorials>().inIPBTutorial = false;
                    }
                    yield return null;
                }
                yield return null;
            }
        }
    }

    public IEnumerator WaitForSeconds(float countdownValue = 2) //Waits for a specified period of time 
    {
        currentCountdownValue = countdownValue;
        while (currentCountdownValue > 0)
        {
            yield return new WaitForSeconds(1);
            currentCountdownValue--;
        }
        StartCoroutine(BlackTransitionToInterrogation(mainCamera, interrogationCam, false));
    }

    public IEnumerator WaitForSecondsMain(float countdownValue = 2) //''
    {
        currentCountdownValue = countdownValue;
        while (currentCountdownValue > 0)
        {
            yield return new WaitForSeconds(1);
            currentCountdownValue--;
        }
        StartCoroutine(BlackTransitionToMainArea(interrogationCam, mainCamera, false)); 

    }

    public void CloseInterrogationSummary(GameObject summary)
    {
        summary.SetActive(false);
    }
}
