using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ListeningDevice : MonoBehaviour
{
    public GameObject textPrompt;
    private bool inRange = false;
    public string requiredOutfit;

    
    public GameObject dressUpManager;
    [HideInInspector]
    public GameObject manager;

    public GameObject currentCam;
    public GameObject desiredCam;
    public Image blackFade;
    public GameObject noirFilter;
    public float currentCountdownValue;
    public List<string> npcStatements = new();
    public GameObject firstTextBox;
    public GameObject secondTextBox;
    public GameObject thirdTextBox;
    public GameObject fourthTextBox;
    public GameObject fifthTextBox;
    public GameObject sixthTextBox;

    public bool inLD = false;
    private int progress = 0;
    private int convoProgress = 0;
    private bool fadeComplete = false;

    
    public GameObject player;

    [HideInInspector]
    public GameObject pinboardManager;

    public EvidenceClass heardEvidence;
    private bool evidenceAlreadyCollected = false;

    public GameObject tutorialManager;

    public GameObject notebook;
    public int convoID;
    private bool speechReading = false;
    private bool ePressed = false;

    public GameObject popUpManager;
    public Image ldIcon;
    public GameObject ldText;

    // Start is called before the first frame update
    void Start()
    {
        dressUpManager = GameObject.FindGameObjectWithTag("Dress Up Manager");
        manager = GameObject.FindGameObjectWithTag("Manager");
        player = GameObject.FindGameObjectWithTag("Player");
        pinboardManager = GameObject.FindGameObjectWithTag("PinBoard Manager");
        progress = 0;
        convoProgress = 0;
        popUpManager = GameObject.FindGameObjectWithTag("PUManager");
        tutorialManager = GameObject.FindGameObjectWithTag("Tutorial Manager");
        
    }

    // Update is called once per frame
    void Update()
    {
       // if(inLD && currentCam.activeInHierarchy)
        //{
          //  print("Turning off cam");
            //currentCam.SetActive(false);
        //}
        
        if (inRange && Input.GetKeyDown(KeyCode.E) && !inLD && !ePressed)
        {
            ePressed = true;
            if (dressUpManager.GetComponent<DressUp>().activeOutfit != "Detective Outfit")
            {
                inLD = true;
                StartListening();
                popUpManager.GetComponent<PopUpManager>().FadeImage(ldIcon, ldText);
                print("Wrong Outfit");
            }
        }

        if (inLD && Input.GetKeyUp(KeyCode.Return) && fadeComplete)
        {
            
            switch(convoProgress)
            {
                case 0:
                    if (!speechReading)
                    {
                        speechReading = true;
                        secondTextBox.SetActive(true);
                        secondTextBox.GetComponentInChildren<TextMeshProUGUI>().text = npcStatements[progress];
                        convoProgress = 1;
                        progress++;
                        speechReading = false;
                    }
                    break;
                case 1:
                    if (!speechReading)
                    {
                        speechReading = true;
                        thirdTextBox.SetActive(true);
                        thirdTextBox.GetComponentInChildren<TextMeshProUGUI>().text = npcStatements[progress];
                        convoProgress = 2;
                        progress++;
                        speechReading = false;
                    }
                    break;
                case 2:
                    if (!speechReading)
                    {
                        speechReading = true;
                        if (npcStatements[progress] == "BREAK")
                        {
                            ClearDialogue();
                            StartCoroutine(BlackTransition(desiredCam, currentCam, false));
                            StartCoroutine(WaitForSeconds(false));
                            inLD = false;
                            foreach (EvidenceClass evidence in pinboardManager.GetComponent<PinboardManager>().discoveredEvidence)
                            {
                                if (evidence == heardEvidence)
                                {
                                    evidenceAlreadyCollected = true;
                                }
                            }
                            if (!heardEvidence.evidenceFound && !evidenceAlreadyCollected)// && !tutorialManager.GetComponent<Tutorials>().inTutorial)
                            {
                                pinboardManager.GetComponent<PinboardManager>().discoveredEvidence.Add(heardEvidence);
                                pinboardManager.GetComponent<PinboardManager>().UpdateEvidenceImages(heardEvidence);
                                heardEvidence.evidenceFound = true;
                            }
                            break;
                        }
                        firstTextBox.SetActive(false);
                        secondTextBox.SetActive(false);
                        thirdTextBox.SetActive(false);
                        fourthTextBox.SetActive(true);
                        fourthTextBox.GetComponentInChildren<TextMeshProUGUI>().text = npcStatements[progress];
                        convoProgress = 3;
                        progress++;
                        speechReading = false;
                    }
                    break;
                case 3:
                    if (!speechReading)
                    {
                        speechReading = true;
                        if (npcStatements[progress] == "BREAK")
                        {
                            ClearDialogue();
                            StartCoroutine(BlackTransition(desiredCam, currentCam, false));
                            StartCoroutine(WaitForSeconds(false));
                            inLD = false;
                            foreach (EvidenceClass evidence in pinboardManager.GetComponent<PinboardManager>().discoveredEvidence)
                            {
                                if (evidence == heardEvidence)
                                {
                                    evidenceAlreadyCollected = true;
                                }
                            }

                            if (tutorialManager.GetComponent<Tutorials>().inTutorial)
                            {
                                break;
                                
                            }
                            if (!heardEvidence.evidenceFound && !evidenceAlreadyCollected )//&& !tutorialManager.GetComponent<Tutorials>().inTutorial)
                                {
                                    
                                    pinboardManager.GetComponent<PinboardManager>().discoveredEvidence.Add(heardEvidence);
                                    pinboardManager.GetComponent<PinboardManager>().UpdateEvidenceImages(heardEvidence);
                                    heardEvidence.evidenceFound = true;
                                }
                            
                            break;
                        }
                        fifthTextBox.SetActive(true);
                        fifthTextBox.GetComponentInChildren<TextMeshProUGUI>().text = npcStatements[progress];
                        convoProgress = 4;
                        progress++;
                        speechReading = false;
                    }
                    break;
                case 4:
                    if (!speechReading)
                    {
                        speechReading = true;
                        if (npcStatements[progress] == "BREAK")
                        {
                            inLD = false;
                            ClearDialogue();
                            StartCoroutine(BlackTransition(desiredCam, currentCam, false));
                            StartCoroutine(WaitForSeconds(false));
                            foreach (EvidenceClass evidence in pinboardManager.GetComponent<PinboardManager>().discoveredEvidence)
                            {
                                if (evidence == heardEvidence)
                                {
                                    evidenceAlreadyCollected = true;
                                }
                            }
                            if (!heardEvidence.evidenceFound && !evidenceAlreadyCollected )//&& !tutorialManager.GetComponent<Tutorials>().inTutorial)
                            {
                                pinboardManager.GetComponent<PinboardManager>().discoveredEvidence.Add(heardEvidence);
                                pinboardManager.GetComponent<PinboardManager>().UpdateEvidenceImages(heardEvidence);
                                heardEvidence.evidenceFound = true;
                            }
                            break;
                        }
                        sixthTextBox.SetActive(true);
                        sixthTextBox.GetComponentInChildren<TextMeshProUGUI>().text = npcStatements[progress];
                        convoProgress = 5;
                        progress++;
                        speechReading = false;
                    }
                    break;
                case 5:
                    if (!speechReading)
                    {
                        speechReading = true;
                        if (npcStatements[progress] == "BREAK")
                        {
                            inLD = false;
                            ClearDialogue();
                            StartCoroutine(BlackTransition(desiredCam, currentCam, false));
                            StartCoroutine(WaitForSeconds(false));
                            foreach (EvidenceClass evidence in pinboardManager.GetComponent<PinboardManager>().discoveredEvidence)
                            {
                                if (evidence == heardEvidence)
                                {
                                    evidenceAlreadyCollected = true;
                                }
                            }
                            if (!heardEvidence.evidenceFound && !evidenceAlreadyCollected )//&& !tutorialManager.GetComponent<Tutorials>().inTutorial)
                            {
                                pinboardManager.GetComponent<PinboardManager>().discoveredEvidence.Add(heardEvidence);
                                pinboardManager.GetComponent<PinboardManager>().UpdateEvidenceImages(heardEvidence);
                                heardEvidence.evidenceFound = true;
                            }
                            break;
                        }
                        fourthTextBox.SetActive(false);
                        fifthTextBox.SetActive(false);
                        sixthTextBox.SetActive(false);
                        firstTextBox.SetActive(true);
                        firstTextBox.GetComponentInChildren<TextMeshProUGUI>().text = npcStatements[progress];
                        convoProgress = 6;
                        progress++;
                        speechReading = false;
                    }
                    break;
                case 6:
                    if (!speechReading)
                    {
                        inLD = false;
                        speechReading = true;
                        if (npcStatements[progress] == "BREAK")
                        {
                            inLD = false;
                            ClearDialogue();
                            StartCoroutine(BlackTransition(desiredCam, currentCam, false));
                            StartCoroutine(WaitForSeconds(false));
                            foreach (EvidenceClass evidence in pinboardManager.GetComponent<PinboardManager>().discoveredEvidence)
                            {
                                if (evidence == heardEvidence)
                                {
                                    evidenceAlreadyCollected = true;
                                }
                            }
                            if (!heardEvidence.evidenceFound && !evidenceAlreadyCollected )//&& !tutorialManager.GetComponent<Tutorials>().inTutorial)
                            {
                                pinboardManager.GetComponent<PinboardManager>().discoveredEvidence.Add(heardEvidence);
                                pinboardManager.GetComponent<PinboardManager>().UpdateEvidenceImages(heardEvidence);
                                heardEvidence.evidenceFound = true;
                            }
                            break;
                        }
                        speechReading = false;
                    }
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            
         //   if(tutorialManager.GetComponent<Tutorials>().firstLD)
         //   {
         //       Time.timeScale = 0;
         //       tutorialManager.GetComponent<Tutorials>().ActivateTutorial(tutorialManager.GetComponent<Tutorials>().ldTutorial);
         //       tutorialManager.GetComponent<Tutorials>().firstLD = false;
        //        Cursor.visible = true;
         //   }
            if (convoID == 0 && !notebook.GetComponent<Notebook>().chaseEddieComplete)
            {
               if (dressUpManager.GetComponent<DressUp>().activeOutfit == requiredOutfit)
               {
                    textPrompt.SetActive(true);
                }
                inRange = true;
            }
            if(convoID == 1 && !notebook.GetComponent<Notebook>().scarletEddieComplete)
            {
               if (dressUpManager.GetComponent<DressUp>().activeOutfit == requiredOutfit)
              {
                    textPrompt.SetActive(true);
                }
                inRange = true;
            }
            if (convoID == 2 && !notebook.GetComponent<Notebook>().juiceboxChaseComplete)
            {
                if (dressUpManager.GetComponent<DressUp>().activeOutfit == requiredOutfit)
                {
                    textPrompt.SetActive(true);
                }
                inRange = true;
            }
            if(convoID == 3 && !notebook.GetComponent<Notebook>().tutorialComplete)
            {
                textPrompt.SetActive(true);
                inRange = true;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textPrompt.SetActive(false);
            inRange = false;
        }
    }

    public void StartListening()
    {
        
        //player.GetComponent<PlayerMovement>().inLD = true;
        StartCoroutine(BlackTransition(currentCam, desiredCam, true));
        StartCoroutine(WaitForSeconds(true));
    }

    public IEnumerator BlackTransition(GameObject currentCam, GameObject desiredCam, bool intoLD, bool transitionToBlack = true, int timeToFade = 1)
    {
        textPrompt.SetActive(false);
        blackFade.gameObject.SetActive(true);
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

                    noirFilter.GetComponent<PostProcessingActivation>().TurnFilterOn(true);
                  //  if(intoLD)
                  //  {
                 //       player.SetActive(false);
                 //       
                  //  }
                  //  if (!intoLD)
                  //  {
                  //      player.SetActive(true);
                  //  }
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
                if (fadeProgress < 0.01f)
                {
                    
                    blackFade.gameObject.SetActive(false);
                    if (intoLD)
                    {
                        currentCam.SetActive(false);
                        speechReading = true;
                        firstTextBox.SetActive(true);
                        firstTextBox.GetComponentInChildren<TextMeshProUGUI>().text = npcStatements[0];
                        progress = 1;
                        speechReading = false;
                        fadeComplete = true;
                    }
                     if(!intoLD)
                    {
                        ePressed = false;
                    }
                    
                    yield return null;

                }
                yield return null;
            }
        }
    }

    private void ChangeCam(GameObject currentCam, GameObject desiredCam)
    {
        currentCam.SetActive(false);
        desiredCam.SetActive(true);
    }

    public IEnumerator WaitForSeconds(bool intoLD, float countdownValue = 2) //Waits for a specified period of time 
    {
        currentCountdownValue = countdownValue;
        while (currentCountdownValue > 0)
        {
            yield return new WaitForSeconds(1);
            currentCountdownValue--;
        }
        StartCoroutine(BlackTransition(currentCam, desiredCam, intoLD, false));
    }

    private void ClearDialogue()
    {
        switch(convoID)
        {
            case 0:
                 notebook.GetComponent<Notebook>().chaseEddieComplete = true;
                break;
            case 1:
                notebook.GetComponent<Notebook>().scarletEddieComplete = true;
                break;
            case 2: 
                notebook.GetComponent<Notebook>().juiceboxChaseComplete = true;
                break;
            case 3:
                notebook.GetComponent<Notebook>().tutorialComplete = true;
                manager.GetComponent<DialogueManager>().StartConversation(manager.GetComponent<DialogueManager>().grace.GetComponent<NPCDialogue>().dialogueTree[12], manager.GetComponent<DialogueManager>().grace, manager.GetComponent<DialogueManager>().graceCam3);
                break;
        }

        firstTextBox.SetActive(false);
        secondTextBox.SetActive(false);
        thirdTextBox.SetActive(false);
        fourthTextBox.SetActive(false);
        fifthTextBox.SetActive(false);
        sixthTextBox.SetActive(false);
        convoProgress = 0;
        progress = 0;
        speechReading = false;
        player.GetComponent<PlayerMovement>().inLD = false;
        fadeComplete = false;
        inRange = false;
        
    }
    
}
