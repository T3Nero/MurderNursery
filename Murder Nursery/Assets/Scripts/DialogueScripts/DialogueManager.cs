using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("General UI Objects")]
    public GameObject dialogueZone; //Canvas object containing all dialogue UI elements
    public GameObject briberyOption; //UI object for the briber button
    public GameObject briberyPanel; //Canvas object containing all bribery UI elements
    public GameObject dresserBox; // check active outfit
    public Image repGainSprite; //Sprite displayed when reputation is gained
    public Image repLossSprite; //Sprite displayed when reputation is lost
    //public Image item1; //Image for first possible bribe
    //public Image item2; //Image for second possible bribe

    public GameObject bribe; // Image used when storing bribery items

    [HideInInspector]
    public GameObject bribeItem; // item to remove when bribe is successful

    public Transform briberyContent; // transform to hold bribery items in the UI


    [Header("Summary Panel")]
    public GameObject summaryPanel;
    public GameObject tick1;
    public GameObject tick2;
    public GameObject tick3;  
    public GameObject tick4;
    public GameObject tick5;
    public Image npcSprite;
    public GameObject npcName;
    public GameObject npcDescription;

    [Header("Player Dialogue Objects")]
    public GameObject playerFirstResponse; //Text containing player's response
    public GameObject playerSecondResponse;//''
    public GameObject playerThirdResponse;//''
    public GameObject playerFirstResponseBox;//Panel containing text for player's response
    public GameObject playerSecondResponseBox; //''
    public GameObject playerThirdResponseBox;//''
    public GameObject playerResponse1; //UI element used to display previous player responses
    public GameObject playerResponse2;//''
    public GameObject playerResponse3;//''
    public GameObject playerResponse4;//''


    [Header("NPC Dialogue Objects")]
    public GameObject npcNameArea; //UI element displayed current active npc name
    public Image npcSprite1;//Sprite used to display active npc in dialogue zone
    public Image npcSprite2;//''
    public Image npcSprite3;//''
    public Image npcSprite4;//''
    public GameObject npcStatement; //UI element used to store previous NPC statement
    public GameObject npcStatement2; //''
    public GameObject npcStatement3;//''
    public GameObject npcStatement4;//''

    [Header("Characters")]
    public GameObject scarlet; //Scarlet's game object
    public GameObject juiceBox; //Juice Box's game object
    public GameObject eddie; //Eddie's game object
    public GameObject chase; //Chase's game object
    public GameObject grace;

    [Header("Audio")]
    public AudioSource playerAudio; //Audio source for the player
    public AudioClip repLossSound; //Sound played upon repuation loss
    public AudioClip repGainSound; //Sound played upon reputation gain
    public AudioClip selectOptionSound; //Sound played when selecting options
    public AudioClip changeOptionSound;//Sound played when navigating options
    public AudioClip passOutfitCheckSound; //Sound played upon passing an outfit check

    [Header("Reputation Variables")]
    public int relationship = 0; //Default value for player relationships
    public bool gainingRep = false; //Signals that the player is gaining reputation
    public bool losingRep = false; //Signals that the player is losing reputation
    public GameObject repLockResponse1; //Response issued when the player does not have enough reputation to proceed
    public GameObject repLockResponse2; //''
    public GameObject repLockResponse3; //''

    public bool inConvo = false; //Signals that the player is speaking to an NPC
    
    public DialogueNode activeNode; //Stores the currently displayed node
    
    public GameObject activeNPC; //Stores the currently active NPC

    //Position vectors for player response UI elements
    private Vector3 response1Position;
    private Vector3 response2Position;
    private Vector3 response3Position;

    //Conclusion checkers
    [HideInInspector]
    public bool trueEndingReached = false;
    [HideInInspector]
    public bool goodEndingReached = false;
    [HideInInspector]
    public bool badEndingReached = false;

    [Header("Game Objects and Cameras")]
    public GameObject dressUpBox; //Stores the dress up box game object
    public Camera currentNPCCam; //The current NPC camera being used
    public Camera playerCam; //The player's camera
    public GameObject player; //The player game object
    public GameObject inventoryManager; //The inventory manager game object

    [Header("Colours")]
    public Color unselectedColour; //Colours used for option selection
    public Color selectedColour;

    //Dialogue nodes for saving progress
    private DialogueNode chaseLastNode = null;
    private DialogueNode eddieLastNode = null;
    private DialogueNode scarletLastNode = null;
    private DialogueNode juiceBoxLastNode = null;
    private bool chaseMidConvo = false;
    private bool eddieMidConvo = false;
    private bool scarletMidConvo = false;
    private bool juiceBoxMidConvo = false;

    //Variables for recording player responses
    private int responseCount = 0; 
    //private bool lastResponsePlayer = false;
    private int lastResponseID;
    private bool firstNode = true;
    private string lastResponse;
    private string lastResponse2;
    private string npcLastResponse1;
    private string npcLastResponse2;
    private int playerChoice;
    private string nonAnimatedText = null;

    //Manager Objects
    [HideInInspector]
    public GameObject manager;
    [SerializeField]
    GameObject repManager;
    public GameObject pinBoardManager;
    ReputationManager RM;

    private bool firstBribe = true;
    private bool firstInterrogation = true;
    public GameObject tutorialManager;

    public GameObject interrogationManager;
    public GameObject interrogateButton;

    [Header("Summary Panel UI")]
    public Sprite chaseSummary;
    public Sprite eddieSummary;
    public Sprite scarletSummary;
    public Sprite jbSummary;
    public Sprite graceSummary;
    public GameObject qMark1;
    public GameObject qMark2;
    public GameObject qMark3;
    public GameObject qMark4;
    public GameObject qMark5;
    public GameObject summaryLeaveButton;
    public GameObject beginInterrogationButton;

    public GameObject leaveButton;
    public GameObject graceCam2;
    public Camera graceCam3;
    public Camera graceCam4;

    [Header("Name Tags")]
    public GameObject chaseTag;
    public GameObject eddieTag;
    public GameObject scarletTag;
    public GameObject jbTag;
    public GameObject graceTag;
    public GameObject currentTag;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager"); //Assigns the game manager
        RM = repManager.GetComponent<ReputationManager>(); //Assigns the rep manager
        response1Position = new Vector3(1472.978759765625f, 247.28692626953126f, 0.0f); //Sets the player response UI position
        response2Position = new Vector3 (1472.9764404296875f,174.4442138671875f, 0.0f);//''
        response3Position = new Vector3(1472.9814453125f, 102.63192749023438f, 0.0f); //''
        

    } 

    // Update is called once per frame
    void Update()
    {
        if(inConvo) //Checks if the player is currently speaking to NPC
        {
            
            Cursor.visible = true; //CURSOR STUFF
            Cursor.lockState = CursorLockMode.None;
        }

        if(!beginInterrogationButton.activeInHierarchy && tutorialManager.GetComponent<Tutorials>().inISTutorial4)
        {
            beginInterrogationButton.SetActive(true);
            tutorialManager.GetComponent<Tutorials>().inISTutorial4 = false;

        }

        if(!tutorialManager.GetComponent<Tutorials>().inTutorial)
        {
            beginInterrogationButton.SetActive(true);
            leaveButton.SetActive(true);
            summaryLeaveButton.SetActive(true);
        }

        //if(gainingRep) //Checks if the player is gaining rep and displays the rep increase sprite
      //  {
        //    for (int i = 0; i < 1; i++)
         //   {
                
         //       StartCoroutine(RepFader(repGainSprite, true)); 
         //       StartCoroutine(WaitForSeconds(gainingRep, repGainSprite, 1.5f));
         //   }
      //  }
      //  if(losingRep) //Checks if the player is losing rep and displays the rep decrease sprite
      //  {
      //      for(int i = 0; i < 1; i++)
      //      {
       //         StartCoroutine(RepFader(repLossSprite, true));
       //         StartCoroutine(WaitForSeconds(losingRep, repLossSprite, 1.5f));
      //      }
      //  }
        if (activeNode != null) 
        {
            if (activeNode.exitNode) //Checks if there is an exit node present 
            {
                
                if (activeNode.triggerTrueEnd) //Triggers the true end if applicable
                {
                    trueEndingReached = true;
                    manager.GetComponent<Conclusion>().blackFade.gameObject.SetActive(true);
                    StartCoroutine(manager.GetComponent<Conclusion>().BlackTransition());
                }
                if(activeNode.triggerGoodEnd) //Triggers the good end if applicable
                {
                    goodEndingReached = true;
                    manager.GetComponent<Conclusion>().blackFade.gameObject.SetActive(true);
                    StartCoroutine(manager.GetComponent<Conclusion>().BlackTransition());

                }
                if(activeNode.triggerBadEnd) //Triggers the bad end if applicable
                {
                    badEndingReached = true;
                    manager.GetComponent<Conclusion>().blackFade.gameObject.SetActive(true);
                    StartCoroutine(manager.GetComponent<Conclusion>().BlackTransition());
                }
                
                
                if(activeNPC == chase)
                {
                    chaseLastNode = null;
                    chaseMidConvo = false;
                }
                if(activeNPC == eddie)
                {
                    eddieLastNode = null;
                    eddieMidConvo = false;
                }
                if(activeNPC == scarlet)
                {
                    scarletLastNode = null;
                    scarletMidConvo = false;
                }
                if(activeNPC == juiceBox)
                {
                    juiceBoxLastNode = null;
                    juiceBoxMidConvo = false;
                }
                ExitConversation(); //Exits the conversation
            }
        }
    }

    public void StartConversation(DialogueNode startNode, GameObject npc, Camera npcCam) //Begins the dialogue interaction with chosen NPC
    {
        interrogateButton.SetActive(true);
       if(tutorialManager.GetComponent<Tutorials>().inTutorial)
        {
            interrogateButton.SetActive(false);
            leaveButton.SetActive(false);
        }
        Cursor.visible = true; //CURSOR STUFF
        player.SetActive(false); //Deactivates the player object for camera reasons
        currentNPCCam = npcCam; 
        currentNPCCam.gameObject.SetActive(true); //Changes camera to NPC cam
        playerCam.gameObject.SetActive(false);
        activeNPC = npc;//Updates the active NPC
        if(activeNPC == chase)
        {
            chaseTag.SetActive(true);
            currentTag = chaseTag;
        }
        if(activeNPC == eddie)
        {
            eddieTag.SetActive(true);
            currentTag = eddieTag;
        }
        if(activeNPC == scarlet)
        {
            scarletTag.SetActive(true);
            currentTag = scarletTag;
        }
        if(activeNPC == juiceBox)
        {
            jbTag.SetActive(true);
            currentTag = jbTag;
        }
        if(activeNPC == grace)
        {
            graceTag.SetActive(true);
            currentTag = graceTag;
        }
        if(activeNPC == chase && interrogationManager.GetComponent<Interrogation>().chaseCompleted)
        {
            interrogateButton.SetActive(false);
        }
        if(activeNPC == juiceBox && interrogationManager.GetComponent<Interrogation>().jbCompleted)
        {
            interrogateButton.SetActive(false);
        }
        if(activeNPC == scarlet && interrogationManager.GetComponent<Interrogation>().scarletCompleted)
        {
            interrogateButton.SetActive(false);
        }
        if(activeNPC == eddie && interrogationManager.GetComponent<Interrogation>().eddieCompleted)
        {
            interrogateButton.SetActive(true);
        }
        if (activeNPC == chase && chaseLastNode != null)
        {
            if (chaseLastNode != activeNPC.GetComponent<NPCDialogue>().interrogationNode)
            {
                activeNode = chaseLastNode;
                LoadNodeInfo(chaseLastNode);

              //  LoadBribeDialogue(startNode);
            }
            else if(chaseLastNode == activeNPC.GetComponent<NPCDialogue>().interrogationNode)
            {
                activeNode = activeNPC.GetComponent<NPCDialogue>().introNode;
                LoadNodeInfo(activeNPC.GetComponent<NPCDialogue>().introNode);
            }
        }
        else if (activeNPC == chase && !chaseMidConvo)
        {
            activeNode = startNode; //Updates the active node to the start node of the conversation
            LoadNodeInfo(startNode); //Loads the node information to the UI elements
        }

        if (activeNPC == eddie && eddieLastNode != null)
        {
            if (eddieLastNode != activeNPC.GetComponent<NPCDialogue>().interrogationNode)
            {
                activeNode = eddieLastNode;
                LoadNodeInfo(eddieLastNode);
            }
            else if(eddieLastNode == activeNPC.GetComponent<NPCDialogue>().interrogationNode)
            {
                activeNode = activeNPC.GetComponent<NPCDialogue>().introNode;
                LoadNodeInfo(activeNPC.GetComponent<NPCDialogue>().introNode);
            }
            }

           // LoadBribeDialogue(startNode);
        
        else if (activeNPC == eddie && !eddieMidConvo)
        {
            activeNode = startNode; //Updates the active node to the start node of the conversation
            LoadNodeInfo(startNode); //Loads the node information to the UI elements
        }
        if(activeNPC == scarlet && scarletLastNode != null)
        {
            if (scarletLastNode != activeNPC.GetComponent<NPCDialogue>().interrogationNode)
            {
                activeNode = scarletLastNode;
                LoadNodeInfo(scarletLastNode);
            }
            else if(scarletLastNode == activeNPC.GetComponent<NPCDialogue>().interrogationNode)
            {
                activeNode = activeNPC.GetComponent<NPCDialogue>().introNode;
                LoadNodeInfo(activeNPC.GetComponent<NPCDialogue>().introNode);
            }
           // LoadBribeDialogue(startNode);
        }
        else if (activeNPC == scarlet && !scarletMidConvo)
        {
            activeNode = startNode; //Updates the active node to the start node of the conversation
            LoadNodeInfo(startNode); //Loads the node information to the UI elements
        }
        if (activeNPC == juiceBox && juiceBoxLastNode != null)
        {
            if (juiceBoxLastNode != activeNPC.GetComponent<NPCDialogue>().interrogationNode)
            {
                activeNode = juiceBoxLastNode;
                LoadNodeInfo(juiceBoxLastNode);
            }
            else if(juiceBoxLastNode == activeNPC.GetComponent<NPCDialogue>().interrogationNode)
            {
                activeNode = activeNPC.GetComponent<NPCDialogue>().introNode;
                LoadNodeInfo(activeNPC.GetComponent<NPCDialogue>().introNode);
            }
            //LoadBribeDialogue(startNode);
        }
        else if (activeNPC == juiceBox )//&& !juiceBoxMidConvo)
        {
           //juiceBoxLastNode = startNode;
            activeNode = startNode; //Updates the active node to the start node of the conversation
            LoadNodeInfo(startNode); //Loads the node information to the UI elements
        }

        if(activeNPC == grace)
        {
            activeNode = startNode;
            LoadNodeInfo(activeNode);
        }
        
        npcStatement.SetActive(true);
        npcStatement.GetComponent<TextMeshProUGUI>().text = activeNode.speech;
        npcLastResponse1 = activeNode.speech;
        dialogueZone.SetActive(true); //Activates the dialogue zone
        
        inConvo = true; //Signals that the player is in a conversation
        npcSprite1.sprite = npc.GetComponent<NPCDialogue>().sprite; //Updates the UI sprite boxes to that of the active NPC
        npcSprite2.sprite = npc.GetComponent<NPCDialogue>().sprite;//''
        npcSprite3.sprite = npc.GetComponent<NPCDialogue>().sprite;//''
        npcSprite4.sprite = npc.GetComponent<NPCDialogue>().sprite;//''

        if (!tutorialManager.GetComponent<Tutorials>().inTutorial)
        {
            if (dresserBox.GetComponent<DressUp>().activeOutfit != "Detective Outfit")
            {
                interrogateButton.SetActive(false);
            }
            
        }
    }

    // Called after attempting to bribe an NPC (StartConversation)
    public void LoadBribeDialogue(DialogueNode bribeNode)
    {
        npcStatement.GetComponent<TextMeshProUGUI>().text = bribeNode.speech;
            activeNode = bribeNode;
            LoadNodeInfo(bribeNode);
       // UpdateRollingText();
        //}

        if (bribeNode == activeNPC.GetComponent<NPCDialogue>().bribeFailPath) // load bribe failed dialogue
        {
            npcStatement.GetComponent <TextMeshProUGUI>().text = bribeNode.speech;
            activeNode = bribeNode;
            LoadNodeInfo(bribeNode);
        }
    }

    public void ExitConversation() //Is called when the conversation is exited
    {       
        player.SetActive(true);

        if(!tutorialManager.GetComponent<Tutorials>().mGlassTutorial1)
        {
            activeNPC.GetComponent<NPCDialogue>().ToggleConversation();
        }

        //activeNPC.GetComponent<NPCDialogue>().inConversation = false;
        if (!tutorialManager.GetComponent<Tutorials>().inTutorial)
        {
            playerCam.gameObject.SetActive(true);
            currentNPCCam.gameObject.SetActive(false);
        }

        if(tutorialManager.GetComponent<Tutorials>().inDUTutorial)
        {
            playerCam.gameObject.SetActive(true);
            currentNPCCam.gameObject.SetActive(false);
            grace.GetComponent<AIScript>().SetDestination(grace.GetComponent<AIScript>().patrolPoints[0].transform.position);
        }

        if(tutorialManager.GetComponent<Tutorials>().nbTutorial && !tutorialManager.GetComponent<Tutorials>().inDUTutorial2)
        {
            tutorialManager.GetComponent<Tutorials>().lockMovement = false;
            playerCam.gameObject.SetActive(true);
            graceCam3.gameObject.SetActive(false);
            tutorialManager.GetComponent<Tutorials>().inLDTutorial = false;
            grace.GetComponent<AIScript>().graceLD.SetActive(false);
            grace.GetComponent<AIScript>().SetDestination(grace.GetComponent<AIScript>().patrolPoints[2].transform.position);
        }

        if (tutorialManager.GetComponent<Tutorials>().inLDTutorial)
        {
            playerCam.gameObject.SetActive(true);
            graceCam3.gameObject.SetActive(false);
            tutorialManager.GetComponent<Tutorials>().inLDTutorial = false;
            tutorialManager.GetComponent<Tutorials>().nbTutorial = true;
            grace.GetComponent<AIScript>().graceDU.SetActive(false);
            grace.GetComponent<AIScript>().SetDestination(grace.GetComponent<AIScript>().patrolPoints[1].transform.position);
        }

        if (tutorialManager.GetComponent<Tutorials>().inDUTutorial2)
        {
            playerCam.gameObject.SetActive(true);
            graceCam2.SetActive(false);
            tutorialManager.GetComponent<Tutorials>().inLDTutorial = true;
        }

        if (tutorialManager.GetComponent<Tutorials>().mGlassTutorial1)
        {
            playerCam.gameObject.SetActive(true);
            graceCam4.gameObject.SetActive(false);
        }



        currentTag.SetActive(false);
        

        dialogueZone.SetActive(false);
        inConvo = false;
        ClearDialogue();
        activeNPC = null;
        activeNode = null;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadNodeInfo(DialogueNode newNode) //Loads the information of the new node
    {
        if(newNode.tutorialChosen)
        {
            pinBoardManager.GetComponent<PinboardManager>().UpdateEvidenceImages(pinBoardManager.GetComponent<PinboardManager>().tutorialEvidence);
            tutorialManager.GetComponent<Tutorials>().tutorialChosen = true;
        }
        if(newNode.exitTutorial)
        {
            ExitConversation();
            tutorialManager.GetComponent<Tutorials>().EndTutorial();
            return;
        }    
        if(newNode.pinboardTutorial)
        {
            ExitConversation();
            tutorialManager.GetComponent<Tutorials>().PinboardTutorial();
            return;
        }
        if(newNode.interroSummaryTutorial)
        {
            interrogateButton.SetActive(true);
        }
        if(newNode.dressUpTutorial)
        {
            tutorialManager.GetComponent<Tutorials>().inDUTutorial = true;
            
        }
        if(newNode.dressUpTutorial2)
        {
            dresserBox.GetComponent<DressUp>().EnterDressUp();
            ExitConversation();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            return;
        }
        if(summaryPanel.activeInHierarchy)
        {
            summaryPanel.SetActive(false);
        }
      //  repLockResponse1.SetActive(false); //REPUTATION STUFF// NOT IN CURRENT BUILD
      //  repLockResponse2.SetActive(false);
      //  repLockResponse3.SetActive(false);

       // if (newNode.repLevelOption1 > 0) //Checks if a response is locked behind a reputation check
       // {
       //     repLockResponse1.SetActive(true);
       // }
       // if (newNode.repLevelOption2 > 0) //''
       // {
       //     repLockResponse2.SetActive(true);
      //  }
      //  if (newNode.repLevelOption3 > 0) //''
      //  {
      //      repLockResponse3.SetActive(true);
      //  }

        if (activeNode != null)
        {
            activeNode.nodeActive = false; //Sets the previous node as inactive

        }

        if (newNode.fitCheck) //Checks if there is an outfit check on the new node
        {
            if (dressUpBox.GetComponent<DressUp>().CheckOutfit(newNode.requiredOutfit))
            {
                playerAudio.PlayOneShot(passOutfitCheckSound, 0.5f);
                activeNode = newNode.dressUpNode;
                if (activeNPC == chase)
                {
                    chaseLastNode = activeNode;
                    chaseMidConvo = true;
                }
                if (activeNPC == eddie)
                {
                    eddieLastNode = activeNode;
                    eddieMidConvo = true;
                }
                if(activeNPC == scarlet)
                {
                    scarletLastNode = activeNode;
                    scarletMidConvo = true;
                }
                if(activeNPC == juiceBox)
                {
                    juiceBoxLastNode = activeNode;
                    juiceBoxMidConvo = true;
                }
            }
            else activeNode = newNode;
            if (activeNPC == chase)
            {
                chaseLastNode = activeNode;
                chaseMidConvo = true;
            }
            if (activeNPC == eddie)
            {
                eddieLastNode = activeNode;
                eddieMidConvo = true;
            }
            if (activeNPC == scarlet)
            {
                scarletLastNode = activeNode;
                scarletMidConvo = true;
            }
            if (activeNPC == juiceBox)
            {
                juiceBoxLastNode = activeNode;
                juiceBoxMidConvo = true;
            }

        }
        else if (!newNode.fitCheck || !dressUpBox.GetComponent<DressUp>().CheckOutfit(activeNode.requiredOutfit)) //Activates the default node if an outfit check is not present or not passed
        {
            activeNode = newNode;
            if (activeNPC == chase)
            {
                chaseLastNode = activeNode;
                chaseMidConvo = true;
            }
            if(activeNPC == eddie)
            {
                eddieLastNode = activeNode;
                eddieMidConvo = true;
            }
            if(activeNPC == scarlet)
            {
                scarletLastNode = activeNode;
                scarletMidConvo = true;
            }
            if (activeNPC == juiceBox)
            {
                juiceBoxLastNode = activeNode;
                juiceBoxMidConvo = true;
            }

        }
        if (firstNode) //Checks if the node being loaded is the first node of the conversation
        {
            npcLastResponse1 = activeNode.speech;
            nonAnimatedText = activeNode.nonAnimatedSpeech;
        }
        if (!firstNode)
        {
            npcLastResponse2 = nonAnimatedText;
            npcLastResponse1 = activeNode.speech;
            nonAnimatedText = activeNode.nonAnimatedSpeech;

            UpdateRollingText(); //Updates the UI to create the text rolling effect

        }
        SwitchEmotion(); //Updates the displayed emotion of the NPC

        if (activeNode.evidenceToDiscover != null && !activeNode.nodeVisited && !activeNode.evidenceToDiscover.evidenceFound) //Checks if the node has evidence to be discoverd and if so adds the evidence to the player's list
        {
            pinBoardManager.GetComponent<PinboardManager>().discoveredEvidence.Add(activeNode.evidenceToDiscover);
            //pinBoardManager.GetComponent<PinboardManager>().UpdateEvidenceListUI(activeNode.evidenceToDiscover);
            pinBoardManager.GetComponent<PinboardManager>().UpdateEvidenceImages(activeNode.evidenceToDiscover);

            activeNode.evidenceToDiscover.evidenceFound = true;
        }
        activeNode.nodeActive = true;
        //npcStatement.GetComponent<TextMeshProUGUI>().text = activeNode.speech;

        if (!activeNode.firstPathLocked) //Checks if a path is locked and updates the UI
        {
            playerFirstResponseBox.SetActive(true);
        }
        if (!activeNode.secondPathLocked) //''
        {
            playerSecondResponseBox.SetActive(true);
        }
        if (!activeNode.thirdPathLocked) //''
        {
            playerThirdResponseBox.SetActive(true);
        }

        switch (activeNode.responses.Length) //Updates the UI depending on the number of responses available to the player 
        {
            case 0:
                playerFirstResponseBox.SetActive(false);
                playerSecondResponseBox.SetActive(false);
                playerThirdResponseBox.SetActive(false);
                break;
            case 1:
                playerFirstResponseBox.SetActive(true);
                playerSecondResponseBox.SetActive(false);
                playerThirdResponseBox.SetActive(false);
                playerFirstResponse.GetComponent<TextMeshProUGUI>().text = activeNode.responses[0].ToString();
                break;
            case 2:
                playerFirstResponseBox.SetActive(true);
                playerThirdResponseBox.SetActive(false);
                playerSecondResponseBox.SetActive(true);
                playerFirstResponse.GetComponent<TextMeshProUGUI>().text = activeNode.responses[0].ToString();
                playerSecondResponse.GetComponent<TextMeshProUGUI>().text = activeNode.responses[1].ToString();
                break;
            case 3:
                playerFirstResponseBox.SetActive(true);
                playerFirstResponse.GetComponent<TextMeshProUGUI>().text = activeNode.responses[0].ToString();
                playerSecondResponseBox.SetActive(true);
                playerSecondResponse.GetComponent<TextMeshProUGUI>().text = activeNode.responses[1].ToString();
                playerThirdResponseBox.SetActive(true);
                playerThirdResponse.GetComponent<TextMeshProUGUI>().text = activeNode.responses[2].ToString();
                break;
        }

        if(activeNode.lockingNode) //Checks if the current node locks responses and updates the UI depending on which nodes are locked
        {
            if (activeNode.nodeVisited)
            {
                if (activeNode.firstPathLocked)
                {
                    playerFirstResponseBox.SetActive(false);
                }
                
                if (activeNode.secondPathLocked)
                {
                    playerSecondResponseBox.SetActive(false);
                }

                if(activeNode.thirdPathLocked)
                {
                    playerThirdResponseBox.SetActive(false);
                }

                
            }
        }
       // MoveOptions(); //Alters the UI to fit the number of responses available
        firstNode = false; 
       if (activeNode.briberyAvailable && !activeNode.bribeGiven) //Activates bribery option if applicable
        {
           briberyOption.SetActive(true);
        }
        //else 
        //{
          //  briberyOption.SetActive(false);
        //}
        
        

    }

    void UpdateRep(int repGain) //Called when the reputation levels require updating // CURRENTLY REMOVED FROM GAME
    {
        if (activeNPC == eddie) //Updates rep for Eddie
        {
            if (repGain > 0)
            {
                playerAudio.PlayOneShot(repGainSound, 0.5f);
                gainingRep = true;
            }
            if(repGain < 0)
            {
                playerAudio.PlayOneShot(repLossSound, 0.5f);
                losingRep = true;
            }
            RM.UpdateReputation(RM.goonPoints += repGain);
            RM.UpdateGoon();
        }
        if (activeNPC == scarlet) //Updates rep for Scarlet
        {
            if (repGain > 0)
            {
                playerAudio.PlayOneShot(repGainSound, 0.5f);
                gainingRep = true;
            }
            if (repGain < 0)
            {
                playerAudio.PlayOneShot(repLossSound, 0.5f);
                losingRep = true;
            }
            RM.UpdateReputation(RM.femmePoints += repGain);
            RM.UpdateFemme();
        }
        if (activeNPC == juiceBox) //Updates rep for JuiceBox
        {
            if (repGain > 0)
            {
                playerAudio.PlayOneShot(repGainSound, 0.5f);
                gainingRep = true;
            }
            if (repGain < 0)
            {
                playerAudio.PlayOneShot(repLossSound, 0.5f);
                losingRep = true;

            }
            RM.UpdateReputation(RM.juiceBoxPoints += repGain);
            RM.UpdateJuiceBox();

        }
        if (activeNPC == chase) //Updates rep for Chase
        {
            if (repGain > 0)
            {
                playerAudio.PlayOneShot(repGainSound, 0.5f);
                gainingRep = true;
            }
            if (repGain < 0)
            {
                playerAudio.PlayOneShot(repLossSound, 0.5f);
                losingRep = true;
            }
            RM.UpdateReputation(RM.coolGuyPoints += repGain);
            RM.UpdateCoolGuy();
        }
    }

    public void ContinueConversation() //Is called while the player is mid conversation
    {

        if (playerChoice == 0 && activeNode.lockingFirstPath) //Updates the locked status of a node after visiting
        {
            activeNode.firstPathLocked = true;
        }
        if(playerChoice == 1 && activeNode.lockingSecondPath) //''
        {
            activeNode.secondPathLocked = true;
        }
        if (playerChoice == 2 && activeNode.lockingThirdPath) //''
        {
            activeNode.thirdPathLocked = true;
        }
        if(activeNode.interrogationNode) //Checks if node triggers an interrogation and enters one if so 
        {
            EnterInterrogation();
            inConvo = false;
        }
        //if(playerChoice == 0 && activeNode.repGainResponse1 != 0) //Triggers reputation update if required
        //{
          //  UpdateRep(activeNode.repGainResponse1);

//        }
       // if(playerChoice == 1 && activeNode.repGainResponse2 != 0) //''
        //{
        //    UpdateRep(activeNode.repGainResponse2);
        //}
       // if(playerChoice == 2 && activeNode.repGainResponse3 != 0)//''
       /// {
       //     UpdateRep(activeNode.repGainResponse3);
       // }

        if(playerChoice == 0|| playerChoice == 1 || playerChoice == 2) // Checks if the player has chosen a response
        {
            if(!interrogationManager.GetComponent<Interrogation>().inInterrogation)
            {
                if (activeNode.children.Length > 0)
                {
                    activeNode.nodeVisited = true; //Marks that the node has been visited
                    playerAudio.PlayOneShot(selectOptionSound, 0.5f);
                    if (responseCount >= 1)
                    {
                        lastResponse2 = lastResponse;
                    }
                    switch (playerChoice) //Stores the player's last response for UI purposes
                    {
                        case 0:
                            lastResponse = activeNode.responses[0];
                            break;
                        case 1:
                            lastResponse = activeNode.responses[1];
                            break;
                        case 2:
                            lastResponse = activeNode.responses[2];
                            break;
                    }

                    responseCount++;
                    LoadNodeInfo(activeNode.children[playerChoice]); //Loads the next relevant node
                }
                

            }
        }
    }

    public void EnterInterrogation() //Enters an interrogation 
    {
        GameObject interrogatedNPC = activeNPC;
        if(interrogatedNPC == grace)
        {
            tutorialManager.GetComponent<Tutorials>().inIPBTutorial = true;
        }
        interrogationManager.GetComponent<Interrogation>().inInterrogation = true;
        ExitConversation();
        tutorialManager.GetComponent<Tutorials>().isTextObject.SetActive(false);
        manager.GetComponent<SceneTransition>().ChangeToInterrogation(interrogatedNPC);
        summaryPanel.SetActive(false);
        
    }


    public void Bribery() // Activates the bribery panel and updates it depending on what items the player possesses
    {
        if (!activeNPC.GetComponent<NPCDialogue>().bribeGiven)
        {
            //dialogueZone.SetActive(false);
            //if (firstBribe)
          //  {
          //      Time.timeScale = 0;
          //      tutorialManager.GetComponent<Tutorials>().ActivateTutorial(tutorialManager.GetComponent<Tutorials>().bribeTutorial);
         //       firstBribe = false;
         //   }
            briberyPanel.SetActive(true);
            //bribe.gameObject.SetActive(false);
            //item1.gameObject.SetActive(false);
            //item2.gameObject.SetActive(false);

            // loop through all items in inventory & check if they are a bribery item, if so: add to bribery items panel
            for (int i = 0; i < inventoryManager.GetComponent<InventoryManager>().items.Count; i++)
            {
                if (inventoryManager.GetComponent<InventoryManager>().items[i].itemType == Item.ItemType.Bribery)
                {
                    if (!briberyContent.Find(inventoryManager.GetComponent<InventoryManager>().items[i].itemName))
                    {
                        bribeItem = Instantiate(bribe, briberyContent);
                        bribeItem.GetComponent<Image>().sprite = inventoryManager.GetComponent<InventoryManager>().items[i].icon;
                        bribeItem.name = inventoryManager.GetComponent<InventoryManager>().items[i].itemName;
                        bribeItem.GetComponent<Bribing>().StoreInfo(inventoryManager.GetComponent<InventoryManager>().items[i]);
                        bribeItem.SetActive(true);
                    }
                }
            }
        }

        if(activeNPC.GetComponent<NPCDialogue>().bribeGiven)
        {
            LoadBribeDialogue(activeNPC.GetComponent<NPCDialogue>().bribePath);
        }
        
        //if(inventoryManager.GetComponent<InventoryManager>().items.Count == 4)
        //{
        //    inventoryManager.GetComponent<Bribing>().StoreInfo(inventoryManager.GetComponent<InventoryManager>().items[2]);
        //    inventoryManager.GetComponent<Bribing>().StoreInfo(inventoryManager.GetComponent<InventoryManager>().items[3]);
        //    item1.gameObject.SetActive(true);
        //    item2.gameObject.SetActive(true);
        //    item1.sprite = inventoryManager.GetComponent<InventoryManager>().items[2].icon;
        //    item2.sprite = inventoryManager.GetComponent<InventoryManager>().items[3].icon;
        //}
    }

    public IEnumerator RepFader(Image repSymbol, bool fadeIn, float fadeSpeed = 1f) //Fades in or out the desired rep symbol
    {
        repSymbol.gameObject.SetActive(true);
        Color repColour = repSymbol.color;
        float fadeProgress;
        if(fadeIn)
        {
            
            while(repSymbol.color.a < 1)
            {
                fadeProgress = repColour.a + (fadeSpeed * Time.deltaTime) ;
                repColour = new Color(repColour.r, repColour.g, repColour.b, fadeProgress);
                repSymbol.color = repColour;
                yield return null;
            }

        }
        else
        {
            while(repSymbol.color.a > 0)
            {
                fadeProgress = repColour.a - (fadeSpeed * Time.deltaTime);
                repColour = new Color(repColour.r, repColour.g, repColour.b, fadeProgress);
                repSymbol.color = repColour;
                if(fadeProgress < 0.01f)
                {
                    repSymbol.gameObject.SetActive(false);
                }
                yield return null;
            }
            
        }
    }

    public IEnumerator WaitForSeconds(bool signal, Image sprite, float countdownValue = 2) //Used to wait for a set period of time
    {
        float currentCountdownValue = countdownValue;
        while (currentCountdownValue > 0)
        {
            yield return new WaitForSeconds(1);
            currentCountdownValue--;
        }
        //StartCoroutine(RepFader(sprite, false));
        signal = false;
    }

    public void MoveOptions() //Updates the UI components depending on which paths are locked and/or present
    {
        if(!activeNode.nodeVisited)
        {
            playerFirstResponseBox.transform.position = response1Position;
            playerSecondResponseBox.transform.position = response2Position;
            playerThirdResponseBox.transform.position = response3Position;
        }
        else
        if(activeNode.firstPathLocked)
        {
            playerSecondResponseBox.transform.position = response1Position;
            playerThirdResponseBox.transform.position = response2Position;
        }
        if(activeNode.secondPathLocked)
        {
            
            playerFirstResponseBox.transform.position = response1Position;
            playerThirdResponseBox.transform.position = response2Position;
        }
        if(activeNode.firstPathLocked && activeNode.secondPathLocked)
        {
            playerThirdResponseBox.transform.position = response1Position;
        }
        if(activeNode.firstPathLocked && activeNode.thirdPathLocked)
        {
            playerSecondResponseBox.transform.position = response1Position;
        }
    }

    private void UpdateRollingText() //Used to create the rolling effect displayed on the dialogue UI
    {
        if (responseCount == 1)
        {
            npcStatement.SetActive(true);
            npcStatement.GetComponent<TextMeshProUGUI>().text = npcLastResponse1;          
            npcStatement3.SetActive(true);
            npcStatement3.GetComponent<TextMeshProUGUI>().text = npcLastResponse2;
            playerResponse2.SetActive(true);
            playerResponse2.GetComponent<TextMeshProUGUI>().text = lastResponse;
        }
        if(responseCount >1)
        {
            npcStatement.GetComponent<TextMeshProUGUI>().text = npcLastResponse1;
            npcStatement3.GetComponent<TextMeshProUGUI>().text = npcLastResponse2;
            playerResponse2.GetComponent<TextMeshProUGUI>().text = lastResponse;
            playerResponse4.SetActive(true);
            playerResponse4.GetComponent<TextMeshProUGUI>().text = lastResponse2;
        }
    }

    private void ClearDialogue() //Resets the dialogue variables and UI objects
    {
        npcStatement3.GetComponent<TextMeshProUGUI>().text = "";
        npcStatement3.SetActive(false);
        playerResponse2.GetComponent<TextMeshProUGUI>().text = "";
        playerResponse2.SetActive(false);
        playerResponse4.GetComponent<TextMeshProUGUI>().text = "";
        playerResponse4.SetActive(false);
        lastResponse = null;
        lastResponse2 = null;
        responseCount = 0;
    }

    private void SwitchEmotion() //Method responsible for updating the emotion displayed by NPC depending on what is required for the active node
    {
        switch(activeNode.nodeEmotion)
        {
            case 0: activeNPC.GetComponent<NPCDialogue>().textureToChange.SetTexture("_DetailAlbedoMap", activeNPC.GetComponent<NPCDialogue>().defaultEmotion);
                break;
            case 1: activeNPC.GetComponent<NPCDialogue>().textureToChange.SetTexture("_DetailAlbedoMap", activeNPC.GetComponent<NPCDialogue>().angryEmotion);
                break;
            case 2: activeNPC.GetComponent<NPCDialogue>().textureToChange.SetTexture("_DetailAlbedoMap", activeNPC.GetComponent<NPCDialogue>().cryingEmotion);
                break;
            case 3: activeNPC.GetComponent<NPCDialogue>().textureToChange.SetTexture("_DetailAlbedoMap", activeNPC.GetComponent<NPCDialogue>().guiltyEmotion);
                break;
            case 4: activeNPC.GetComponent<NPCDialogue>().textureToChange.SetTexture("_DetailAlbedoMap", activeNPC.GetComponent<NPCDialogue>().playfulEmotion);
                break;
            case 5: activeNPC.GetComponent<NPCDialogue>().textureToChange.SetTexture("_DetailAlbedoMap", activeNPC.GetComponent<NPCDialogue>().sadEmotion);
                break;
            case 6: activeNPC.GetComponent<NPCDialogue>().textureToChange.SetTexture("_DetailAlbedoMap", activeNPC.GetComponent<NPCDialogue>().shockedEmotion);
                break;
            case 7: activeNPC.GetComponent<NPCDialogue>().textureToChange.SetTexture("_DetailAlbedoMap", activeNPC.GetComponent<NPCDialogue>().thinkingEmotion);
                break;
        }
    }

    public void ClickChoice1() //Allows the player to use mouse for response selection
    {
        
        playerChoice = 0;
        ContinueConversation(); //Allows the conversation to continue
        
    }
    public void ClickChoice2() //''
    {
        playerChoice = 1;
        ContinueConversation(); //Allows the conversation to continue
        
    }

    public void ClickChoice3() //''
    {
        playerChoice = 2;
        ContinueConversation(); //Allows the conversation to continue
        
    }

    public void EnterInterrogation2()
    {
        LoadNodeInfo(activeNPC.GetComponent<NPCDialogue>().interrogationNode);
        npcStatement.GetComponent<TextMeshProUGUI>().text = activeNode.speech;
        summaryPanel.SetActive(false);
        
    }

    public void ToggleSummary()
    {
       // if(firstInterrogation)
       // {
       //     Time.timeScale = 0;
       //     tutorialManager.GetComponent<Tutorials>().ActivateTutorial(tutorialManager.GetComponent<Tutorials>().interrogationTutorial);
      //      firstInterrogation = false;
      //  }
        summaryPanel.SetActive(!summaryPanel.activeSelf);
        if(activeNPC == chase)
        {
            summaryPanel.GetComponent<Image>().sprite = chaseSummary;
        }
        if(activeNPC == scarlet)
        {
            summaryPanel.GetComponent<Image>().sprite = scarletSummary;
        }
        if(activeNPC == eddie)
        {
            summaryPanel.GetComponent<Image>().sprite = eddieSummary;
        }
        if(activeNPC == juiceBox)
        {
            summaryPanel.GetComponent<Image>().sprite = jbSummary;
        }
        if(activeNPC == grace)
        {
            //if(tutorialManager.GetComponent<Tutorials>().inISTutorial1)
          //  {
                tutorialManager.GetComponent<Tutorials>().isOverText.GetComponent<TextMeshProUGUI>().text = tutorialManager.GetComponent<Tutorials>().isText1;
                tutorialManager.GetComponent<Tutorials>().isTextObject.SetActive(true);
                summaryLeaveButton.SetActive(false);
                tutorialManager.GetComponent<Tutorials>().pbTutorialButton.SetActive(true);
          //  }
            summaryPanel.GetComponent<Image>().sprite = graceSummary;
        }    
        
        if(summaryPanel.activeSelf)
        {
            tick1.SetActive(false);
            tick2.SetActive(false);
            tick3.SetActive(false);
            tick4.SetActive(false);
            tick5.SetActive(false);
            qMark1.SetActive(true);
            qMark2.SetActive(true);
            qMark3.SetActive(true);
            qMark4.SetActive(true);
            qMark5.SetActive(true);
        }
        npcName.GetComponent<TextMeshProUGUI>().text = activeNPC.gameObject.name;
        //npcSprite.sprite = activeNPC.GetComponent<NPCDialogue>().sprite;
        //npcDescription.GetComponent<TextMeshProUGUI>().text = activeNPC.GetComponent<NPCDialogue>().description;

        foreach (string evidence in pinBoardManager.GetComponent<PinboardManager>().threadedEvidence)
        {
            if (activeNPC.GetComponent<NPCDialogue>().requiredEvidence1 == evidence)
            {
                tick1.SetActive(true);
                qMark1.SetActive(false);
                continue;
            }
            
            if(activeNPC.GetComponent<NPCDialogue>().requiredEvidence2 == evidence )
            {
                tick2.SetActive(true);
                qMark2.SetActive(false);
                continue;
            }
         
            if(activeNPC.GetComponent<NPCDialogue>().requiredEvidence3 == evidence)
            {
                tick3.SetActive(true);
                qMark3.SetActive(false);
                continue;
            }
        
            if(activeNPC.GetComponent<NPCDialogue>().requiredEvidence4 == evidence)
            {
                tick4.SetActive(true);
                qMark4.SetActive(false);
                continue;
            }
           
            if(activeNPC.GetComponent<NPCDialogue>().requiredEvidence5 == evidence)
            {
                tick5.SetActive(true);
                qMark5.SetActive(false);
                continue;
            }
           
        }
    }
}
