using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("General UI Objects")]
    public GameObject dialogueZone; //Canvas object containing all dialogue UI elements
    public GameObject briberyOption; //UI object for the briber button
    public GameObject briberyPanel; //Canvas object containing all bribery UI elements
    public Image repGainSprite; //Sprite displayed when reputation is gained
    public Image repLossSprite; //Sprite displayed when reputation is lost
    public Image item1; //Image for first possible bribe
    public Image item2; //Image for second possible bribe
    public Image singleBribe; //Image used when only a single bribe is available

    [Header("Summary Panel")]
    public GameObject summaryPanel;
    public GameObject tick1;
    public GameObject tick2;
    public GameObject tick3;    
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

    [HideInInspector]
    public bool inConvo = false; //Signals that the player is speaking to an NPC
    [HideInInspector]
    public DialogueNode activeNode; //Stores the currently displayed node
    [HideInInspector]
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
    private bool lastResponsePlayer = false;
    private int lastResponseID;
    private bool firstNode = true;
    private string lastResponse;
    private string lastResponse2;
    private string npcLastResponse1;
    private string npcLastResponse2;
    private int playerChoice;

    //Manager Objects
    [HideInInspector]
    public GameObject manager;
    [SerializeField]
    GameObject repManager;
    public GameObject pinBoardManager;
    ReputationManager RM;


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
        if(gainingRep) //Checks if the player is gaining rep and displays the rep increase sprite
        {
            for (int i = 0; i < 1; i++)
            {
                
                StartCoroutine(RepFader(repGainSprite, true)); 
                StartCoroutine(WaitForSeconds(gainingRep, repGainSprite, 1.5f));
            }
        }
        if(losingRep) //Checks if the player is losing rep and displays the rep decrease sprite
        {
            for(int i = 0; i < 1; i++)
            {
                StartCoroutine(RepFader(repLossSprite, true));
                StartCoroutine(WaitForSeconds(losingRep, repLossSprite, 1.5f));
            }
        }
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
        Cursor.visible = true; //CURSOR STUFF
        player.SetActive(false); //Deactivates the player object for camera reasons
        currentNPCCam = npcCam; 
        currentNPCCam.gameObject.SetActive(true); //Changes camera to NPC cam
        playerCam.gameObject.SetActive(false);
        activeNPC = npc;//Updates the active NPC
        if (activeNPC == chase && chaseLastNode != null)
        {
            activeNode = chaseLastNode;
            LoadNodeInfo(chaseLastNode);
        }
        else if (activeNPC == chase && !chaseMidConvo)
        {
            activeNode = startNode; //Updates the active node to the start node of the conversation
            LoadNodeInfo(startNode); //Loads the node information to the UI elements
        }
        if (activeNPC == eddie && eddieLastNode != null)
        {
            activeNode = eddieLastNode;
            LoadNodeInfo(eddieLastNode);
        }
        else if (activeNPC == eddie && !eddieMidConvo)
        {
            activeNode = startNode; //Updates the active node to the start node of the conversation
            LoadNodeInfo(startNode); //Loads the node information to the UI elements
        }
        if(activeNPC == scarlet && scarletLastNode != null)
        {
            activeNode = scarletLastNode;
            LoadNodeInfo(scarletLastNode);
        }
        else if (activeNPC == scarlet && !scarletMidConvo)
        {
            activeNode = startNode; //Updates the active node to the start node of the conversation
            LoadNodeInfo(startNode); //Loads the node information to the UI elements
        }
        if (activeNPC == juiceBox && juiceBoxLastNode != null)
        {
            activeNode = juiceBoxLastNode;
            LoadNodeInfo(juiceBoxLastNode);
        }
        else if (activeNPC == juiceBox && !juiceBoxMidConvo)
        {
            activeNode = startNode; //Updates the active node to the start node of the conversation
            LoadNodeInfo(startNode); //Loads the node information to the UI elements
        }
        
        npcStatement.SetActive(true);
        npcStatement.GetComponent<TextMeshProUGUI>().text = activeNode.speech;
        npcLastResponse1 = activeNode.speech;
        dialogueZone.SetActive(true); //Activates the dialogue zone
        npcNameArea.GetComponent<TextMeshProUGUI>().text = npc.name;
        inConvo = true; //Signals that the player is in a conversation
        npcSprite1.sprite = npc.GetComponent<NPCDialogue>().sprite; //Updates the UI sprite boxes to that of the active NPC
        npcSprite2.sprite = npc.GetComponent<NPCDialogue>().sprite;//''
        npcSprite3.sprite = npc.GetComponent<NPCDialogue>().sprite;//''
        npcSprite4.sprite = npc.GetComponent<NPCDialogue>().sprite;//''
    }

    public void ExitConversation() //Is called when the conversation is exited
    {       
        player.SetActive(true);
        playerCam.gameObject.SetActive(true);
        currentNPCCam.gameObject.SetActive(false);
        activeNPC = null;
        dialogueZone.SetActive(false);
        inConvo = false;
        ClearDialogue();
    }

    public void LoadNodeInfo(DialogueNode newNode) //Loads the information of the new node
    {
        repLockResponse1.SetActive(false); //Preemptively resets the player response positions to prevent overspawning issues
        repLockResponse2.SetActive(false);
        repLockResponse3.SetActive(false);

        if (newNode.repLevelOption1 > 0) //Checks if a response is locked behind a reputation check
        {
            repLockResponse1.SetActive(true);
        }
        if (newNode.repLevelOption2 > 0) //''
        {
            repLockResponse2.SetActive(true);
        }
        if (newNode.repLevelOption3 > 0) //''
        {
            repLockResponse3.SetActive(true);
        }

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
        }
        if (!firstNode)
        {
            npcLastResponse2 = npcLastResponse1;
            npcLastResponse1 = activeNode.speech;

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
                playerFirstResponse.GetComponent<TextMeshProUGUI>().text = "Press Escape To Leave Conversation";
                playerSecondResponseBox.SetActive(false);
                playerThirdResponseBox.SetActive(false);
                break;
            case 1:
                playerSecondResponseBox.SetActive(false);
                playerThirdResponseBox.SetActive(false);
                playerFirstResponse.GetComponent<TextMeshProUGUI>().text = activeNode.responses[0].ToString();
                break;
            case 2:
                playerThirdResponseBox.SetActive(false);
                playerSecondResponseBox.SetActive(true);
                playerFirstResponse.GetComponent<TextMeshProUGUI>().text = activeNode.responses[0].ToString();
                playerSecondResponse.GetComponent<TextMeshProUGUI>().text = activeNode.responses[1].ToString();
                break;
            case 3:
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
        MoveOptions(); //Alters the UI to fit the number of responses available
        firstNode = false; 
        if (activeNode.briberyAvailable && !activeNode.bribeGiven) //Activates bribery option if applicable
        {
            briberyOption.SetActive(true);
        }
        else briberyOption.SetActive(false);
        
        

    }

    void UpdateRep(int repGain) //Called when the reputation levels require updating
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
            EnterInterrogation(activeNPC);
            inConvo = false;
        }
        if(playerChoice == 0 && activeNode.repGainResponse1 != 0) //Triggers reputation update if required
        {
            UpdateRep(activeNode.repGainResponse1);

        }
        if(playerChoice == 1 && activeNode.repGainResponse2 != 0) //''
        {
            UpdateRep(activeNode.repGainResponse2);
        }
        if(playerChoice == 2 && activeNode.repGainResponse3 != 0)//''
        {
            UpdateRep(activeNode.repGainResponse3);
        }

        if(playerChoice == 0|| playerChoice == 1 || playerChoice == 2) // Checks if the player has chosen a response
        {
            if(activeNode.children.Length > 0)
            {
                activeNode.nodeVisited = true; //Marks that the node has been visited
                playerAudio.PlayOneShot(selectOptionSound, 0.5f);
                if (responseCount >= 1)
                {
                    lastResponse2 = lastResponse;
                }
                switch (playerChoice) //Stores the player's last response for UI purposes
                {
                    case 0: lastResponse = activeNode.responses[0];
                        break;
                    case 1: lastResponse = activeNode.responses[1];
                        break;
                    case 2: lastResponse = activeNode.responses[2];
                        break;
                }
                
                responseCount++;
                LoadNodeInfo(activeNode.children[playerChoice]); //Loads the next relevant node
                

            }
        }
    }

    public void EnterInterrogation(GameObject targetNPC) //Enters an interrogation 
    {       
        ExitConversation();
        manager.GetComponent<SceneTransition>().ChangeToInterrogation(targetNPC);
    }


    public void Bribery() // Activates the bribery panel and updates it depending on what items the player possesses
    {
        dialogueZone.SetActive(false);
        briberyPanel.SetActive(true);
        singleBribe.gameObject.SetActive(false);
        item1.gameObject.SetActive(false);
        item2.gameObject.SetActive(false);
        if(inventoryManager.GetComponent<InventoryManager>().items.Count == 3)
        {
            inventoryManager.GetComponent<Bribing>().StoreInfo(inventoryManager.GetComponent<InventoryManager>().items[2]);
            singleBribe.gameObject.SetActive(true);
            singleBribe.sprite = inventoryManager.GetComponent<InventoryManager>().items[2].icon;
        }
        if(inventoryManager.GetComponent<InventoryManager>().items.Count ==4)
        {
            inventoryManager.GetComponent<Bribing>().StoreInfo(inventoryManager.GetComponent<InventoryManager>().items[2]);
            inventoryManager.GetComponent<Bribing>().StoreInfo(inventoryManager.GetComponent<InventoryManager>().items[3]);
            item1.gameObject.SetActive(true);
            item2.gameObject.SetActive(true);
            item1.sprite = inventoryManager.GetComponent<InventoryManager>().items[2].icon;
            item2.sprite = inventoryManager.GetComponent<InventoryManager>().items[3].icon;
        }
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
        StartCoroutine(RepFader(sprite, false));
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

    public void EnterInterrogation()
    {
        LoadNodeInfo(activeNPC.GetComponent<NPCDialogue>().interrogationNode);
        summaryPanel.SetActive(false);
        
    }

    public void ToggleSummary()
    {
        summaryPanel.SetActive(!summaryPanel.activeSelf);
        npcName.GetComponent<TextMeshProUGUI>().text = activeNPC.gameObject.name;
        npcSprite.sprite = activeNPC.GetComponent<NPCDialogue>().sprite;
        npcDescription.GetComponent<TextMeshProUGUI>().text = activeNPC.GetComponent<NPCDialogue>().description;

        foreach (string evidence in pinBoardManager.GetComponent<PinboardManager>().threadedEvidence)
        {
            if (activeNPC.GetComponent<NPCDialogue>().requiredEvidence1 == evidence )
            {
                tick1.SetActive(true);
                
            }
            else
            {
                tick1.SetActive(false);
            }
            if(activeNPC.GetComponent<NPCDialogue>().requiredEvidence2 == evidence )
            {
                tick2.SetActive(true);
            }
            else
            {
                tick2.SetActive(false);
            }
            if(activeNPC.GetComponent<NPCDialogue>().requiredEvidence2 == evidence)
            {
                tick3.SetActive(true);
            }
            else
            {
                tick3.SetActive(false);
            }
        }
    }
}
