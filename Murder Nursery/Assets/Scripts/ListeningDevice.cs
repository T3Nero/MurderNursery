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
    
    public GameObject manager;

    public GameObject currentCam;
    public GameObject desiredCam;
    public Image blackFade;
    public GameObject noirFilter;
    public float currentCountdownValue;
    public float cooldownCDValue;
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
    private bool tutorialTransition = false;
    public GameObject inventoryManager;
    private bool enterCooldown = false;
    public bool ensureCamOff = false;
    public GameObject ppLayer;


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
        inventoryManager = GameObject.FindGameObjectWithTag("Inventory");
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
        if (inRange && Input.GetKeyDown(KeyCode.E) && !inLD && !ePressed && !inventoryManager.GetComponent<ToggleUIVisibility>().inventoryOpen)
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

        if(textPrompt.activeInHierarchy && inventoryManager.GetComponent<ToggleUIVisibility>().inventoryOpen && inRange)
        {
            textPrompt.SetActive(false);
        }
        if(!textPrompt.activeInHierarchy && !inventoryManager.GetComponent<ToggleUIVisibility>().inventoryOpen && inRange && !inLD)
        {
            textPrompt.SetActive(true);
        }

        if(inLD && currentCam.activeInHierarchy && ensureCamOff)
        {
            currentCam.SetActive(false);
        }

        if (inLD && Input.GetKeyUp(KeyCode.Return) && fadeComplete && !speechReading)
        {
            if (!enterCooldown)
            {
                switch (convoProgress)
                {
                    case 0:
                        if (!speechReading)
                        {
                            speechReading = true;
                            secondTextBox.SetActive(true);
                            secondTextBox.GetComponentInChildren<TextMeshProUGUI>().text = npcStatements[progress];
                            progress++;
                            convoProgress = 1;

                            speechReading = false;
                            StartCoroutine(WaitForSecondsCooldown());
                        }
                        break;
                    case 1:
                        if (!speechReading)
                        {
                            speechReading = true;
                            thirdTextBox.SetActive(true);
                            thirdTextBox.GetComponentInChildren<TextMeshProUGUI>().text = npcStatements[progress];
                            progress++;
                            convoProgress = 2;

                            speechReading = false;
                            StartCoroutine(WaitForSecondsCooldown());
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
                                if (!tutorialManager.GetComponent<Tutorials>().inTutorial)
                                {
                                    foreach (EvidenceClass evidence in pinboardManager.GetComponent<PinboardManager>().discoveredEvidence)
                                    {
                                        if (evidence == heardEvidence)
                                        {
                                            evidenceAlreadyCollected = true;
                                        }
                                    }
                                    if (!heardEvidence.evidenceFound && !evidenceAlreadyCollected)
                                    {
                                        pinboardManager.GetComponent<PinboardManager>().discoveredEvidence.Add(heardEvidence);
                                        pinboardManager.GetComponent<PinboardManager>().UpdateEvidenceImages(heardEvidence);
                                        heardEvidence.evidenceFound = true;
                                    }
                                    break;
                                }
                            }
                            firstTextBox.SetActive(false);
                            secondTextBox.SetActive(false);
                            thirdTextBox.SetActive(false);
                            fourthTextBox.SetActive(true);
                            fourthTextBox.GetComponentInChildren<TextMeshProUGUI>().text = npcStatements[progress];
                            progress++;
                            convoProgress = 3;

                            speechReading = false;
                            StartCoroutine(WaitForSecondsCooldown());
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
                                if (!tutorialManager.GetComponent<Tutorials>().inTutorial)
                                {
                                    foreach (EvidenceClass evidence in pinboardManager.GetComponent<PinboardManager>().discoveredEvidence)
                                    {
                                        if (evidence == heardEvidence)
                                        {
                                            evidenceAlreadyCollected = true;
                                        }
                                    }

                                    
                                    if (!heardEvidence.evidenceFound && !evidenceAlreadyCollected)//&& !tutorialManager.GetComponent<Tutorials>().inTutorial)
                                    {

                                        pinboardManager.GetComponent<PinboardManager>().discoveredEvidence.Add(heardEvidence);
                                        pinboardManager.GetComponent<PinboardManager>().UpdateEvidenceImages(heardEvidence);
                                        heardEvidence.evidenceFound = true;
                                    }

                                    break;
                                }
                            }
                            if (tutorialManager.GetComponent<Tutorials>().inTutorial)
                            {
                                break;

                            }
                            fifthTextBox.SetActive(true);
                            fifthTextBox.GetComponentInChildren<TextMeshProUGUI>().text = npcStatements[progress];
                            progress++;
                            convoProgress = 4;

                            speechReading = false;
                            StartCoroutine(WaitForSecondsCooldown());
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
                                if (!tutorialManager.GetComponent<Tutorials>().inTutorial)
                                {
                                    foreach (EvidenceClass evidence in pinboardManager.GetComponent<PinboardManager>().discoveredEvidence)
                                    {
                                        if (evidence == heardEvidence)
                                        {
                                            evidenceAlreadyCollected = true;
                                        }
                                    }
                                    if (!heardEvidence.evidenceFound && !evidenceAlreadyCollected)//&& !tutorialManager.GetComponent<Tutorials>().inTutorial)
                                    {
                                        pinboardManager.GetComponent<PinboardManager>().discoveredEvidence.Add(heardEvidence);
                                        pinboardManager.GetComponent<PinboardManager>().UpdateEvidenceImages(heardEvidence);
                                        heardEvidence.evidenceFound = true;
                                    }
                                    break;
                                }
                            }
                            sixthTextBox.SetActive(true);
                            sixthTextBox.GetComponentInChildren<TextMeshProUGUI>().text = npcStatements[progress];
                            progress++;
                            convoProgress = 5;

                            speechReading = false;
                            StartCoroutine(WaitForSecondsCooldown());
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
                                if (!tutorialManager.GetComponent<Tutorials>().inTutorial)
                                {
                                    foreach (EvidenceClass evidence in pinboardManager.GetComponent<PinboardManager>().discoveredEvidence)
                                    {
                                        if (evidence == heardEvidence)
                                        {
                                            evidenceAlreadyCollected = true;
                                        }
                                    }
                                    if (!heardEvidence.evidenceFound && !evidenceAlreadyCollected)//&& !tutorialManager.GetComponent<Tutorials>().inTutorial)
                                    {
                                        pinboardManager.GetComponent<PinboardManager>().discoveredEvidence.Add(heardEvidence);
                                        pinboardManager.GetComponent<PinboardManager>().UpdateEvidenceImages(heardEvidence);
                                        heardEvidence.evidenceFound = true;
                                    }
                                    break;
                                }
                            }
                            fourthTextBox.SetActive(false);
                            fifthTextBox.SetActive(false);
                            sixthTextBox.SetActive(false);
                            firstTextBox.SetActive(true);
                            firstTextBox.GetComponentInChildren<TextMeshProUGUI>().text = npcStatements[progress];
                            progress++;
                            convoProgress = 6;

                            speechReading = false;
                            StartCoroutine(WaitForSecondsCooldown());
                        }
                        break;
                    case 6:
                        if (!speechReading)
                        {

                            speechReading = true;
                            if (npcStatements[progress] == "BREAK")
                            {
                                inLD = false;
                                ClearDialogue();
                                StartCoroutine(BlackTransition(desiredCam, currentCam, false));
                                StartCoroutine(WaitForSeconds(false));
                                if (!tutorialManager.GetComponent<Tutorials>().inTutorial)
                                {
                                    foreach (EvidenceClass evidence in pinboardManager.GetComponent<PinboardManager>().discoveredEvidence)
                                    {
                                        if (evidence == heardEvidence)
                                        {
                                            evidenceAlreadyCollected = true;
                                        }
                                    }
                                    if (!heardEvidence.evidenceFound && !evidenceAlreadyCollected)//&& !tutorialManager.GetComponent<Tutorials>().inTutorial)
                                    {
                                        pinboardManager.GetComponent<PinboardManager>().discoveredEvidence.Add(heardEvidence);
                                        pinboardManager.GetComponent<PinboardManager>().UpdateEvidenceImages(heardEvidence);
                                        heardEvidence.evidenceFound = true;
                                    }
                                    break;
                                }
                            }
                            speechReading = false;
                            StartCoroutine(WaitForSecondsCooldown());
                        }
                        break;

                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && dressUpManager.GetComponent<DressUp>().activeOutfit != "Detective Outfit")
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
                    player = other.gameObject;
                }
                inRange = true;
            }
            if(convoID == 1 && !notebook.GetComponent<Notebook>().scarletEddieComplete)
            {
               if (dressUpManager.GetComponent<DressUp>().activeOutfit == requiredOutfit)
              {
                    textPrompt.SetActive(true);
                    player = other.gameObject;
                }
                inRange = true;
            }
            if (convoID == 2 && !notebook.GetComponent<Notebook>().juiceboxChaseComplete)
            {
                if (dressUpManager.GetComponent<DressUp>().activeOutfit == requiredOutfit)
                {
                    textPrompt.SetActive(true);
                    player = other.gameObject;
                }
                inRange = true;
            }
            if(convoID == 3 && !notebook.GetComponent<Notebook>().tutorialComplete)
            {
                textPrompt.SetActive(true);
                player = other.gameObject;
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
       // ppLayer.SetActive(true);
        tutorialManager.GetComponent<Tutorials>().lockMovementMainGame = true; ;
        player.GetComponent<PlayerMovement>().inLD = true;
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
                    ensureCamOff = true;

                   // noirFilter.GetComponent<PostProcessingActivation>().TurnFilterOn(true);
                    if(intoLD)
                   {
                        player.SetActive(false);
                        

                    }
                    if (!intoLD)
                    {
                        player.SetActive(true);
                        ensureCamOff = false;

                    }
                }

                yield return null;
            }
        }
        else 
        {
            while (blackFade.color.a > 0)
            {
                if(tutorialTransition)
                {
                    tutorialManager.GetComponent<Tutorials>().lockMovement = true;
                    
                }
                fadeProgress = screenColour.a - (timeToFade * Time.deltaTime);
                screenColour = new Color(screenColour.r, screenColour.g, screenColour.b, fadeProgress);
                blackFade.color = screenColour;
                if (fadeProgress < 0.05f)
                {
                    
                    blackFade.gameObject.SetActive(false);
                    if (intoLD)
                    {
                       // currentCam.SetActive(false);
                        speechReading = true;
                        firstTextBox.SetActive(true);
                        firstTextBox.GetComponentInChildren<TextMeshProUGUI>().text = npcStatements[0];
                        progress = 1;
                        speechReading = false;
                        StartCoroutine(WaitForSecondsCooldown());
                        fadeComplete = true;
                        currentCam.SetActive(false);
                        
                            
                        
                    }
                     if(!intoLD)
                    {
                        ePressed = false;
                        tutorialManager.GetComponent<Tutorials>().lockMovementMainGame = false;
                        if (tutorialTransition)
                        {
                            //ppLayer.SetActive(false);
                            tutorialManager.GetComponent<Tutorials>().inDUTutorial2 = false;
                            manager.GetComponent<DialogueManager>().StartConversation(manager.GetComponent<DialogueManager>().grace.GetComponent<NPCDialogue>().dialogueTree[12], manager.GetComponent<DialogueManager>().grace, manager.GetComponent<DialogueManager>().graceCam3);
                            tutorialTransition = false;
                            
                        }
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

    public IEnumerator WaitForSecondsCooldown(float countdownValue = 1)
    {
        enterCooldown = true;
        cooldownCDValue = countdownValue;
        while (cooldownCDValue > 0)
        {
            
            yield return new WaitForSeconds(1);
            cooldownCDValue--;
        }
        enterCooldown = false;
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
        if(tutorialManager.GetComponent<Tutorials>().inLDTutorial)
        {
            tutorialTransition = true;
        }
        
    }
    
}
