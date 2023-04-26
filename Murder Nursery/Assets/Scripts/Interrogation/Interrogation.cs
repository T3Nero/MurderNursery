using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interrogation : MonoBehaviour
{
    [Header("Managers")]
    public GameObject manager; //Stores the game manager
    public GameObject PinboardManager; //Stores the pinboard manager
    public GameObject repManager; //Stores the reputation manager

    [Header("UI Objects")]
    public GameObject interrogationPanel; //UI element containing all interrogation components
    public GameObject intResponseBox1; //Player response box 
    public GameObject intResponseBox2; //''
    public GameObject intResponseBox3;//''
    public GameObject intResponseText1;//Player response text
    public GameObject intResponseText2;//''
    public GameObject intResponseText3;//''
    public GameObject npcStatement; //NPC response text
    public GameObject playerResponse1 = null; //Player response option
    public GameObject playerResponse2; //''
    public GameObject npcStatement1; //NPC response text
    public GameObject npcStatement2;//''
    public Image npcSprite1; //Sprite to display the NPC currently being interrogated
    public Image npcSprite2; //''
    public GameObject summaryPanel;
    public GameObject interactMessage;

    [Header("Evidence UI Objects")]
    public GameObject evButton; //UI object holding the evidence screen select button
    public GameObject noEvMessage; //Message displayed when the player has no evidence to use
    public GameObject evidencePanel; //UI element containing all evidence screen components
    public Image evidencePiece1; //Image used to display evidence piece
    public Image evidencePiece2; //''
    public Image evidencePiece3;//''
    public List<Sprite> sprites; //List of potential evidence piece sprites

    [Header("Interrogation Variables")]
    [HideInInspector]
    public DialogueNode activeNode; //Stores the currently active node
    public int interrogationLives; //The number of lives the player has available for interrogation
    private GameObject activeInterrogant; //The NPC currently being interrogated
    public bool interrogationUnderway; //Signals that an interrogation is underway

    [Header("Interrogation Audio")]
    public AudioSource interrogationSource; //Audio source for interrogation
    public AudioClip lifeLostSound; //Sound played when the player makes a wrong decision

    [Header("Interrogation Pinboards")]
    public GameObject pinboard;
    public GameObject activeZoomIn;
    

    //Vectors storing response box positions 
    private Vector3 response1Position;
    private Vector3 response2Position;
    private Vector3 response3Position;

    //Variables for recording player responses
    //private int responseCount = 0;
    private string lastResponseJB = null;
    private string lastResponseScarlet = null;
    private string lastResponseEddie = null;
    private string lastResponseChase = null;
    private string lastResponseGrace = null;
    //private string lastResponse2;
    private string npcLastResponse1;
    private string npcLastResponse2;
    //private bool firstNode = true;
    public DialogueNode mostRecentChaseNode;
    private bool firstTry = true;
    public DialogueNode mostRecentEddieNode;
    public DialogueNode mostRecentJuiceBoxNode;
    public DialogueNode mostRecentScarletNode;
    public DialogueNode mostRecentGraceNode;
    private int pos = 0;
    private int playerChoice = 0;

    public bool inInterrogation = false;
    private bool lastResponsePlayer = false;

    public bool chaseCompleted = false;
    public bool jbCompleted = false;
    public bool scarletCompleted = false;
    public bool eddieCompleted = false;
    public bool noneCompleted = true;

    [Header("Player Response Objects")]
    public GameObject juiceBoxPlayerResponseText;
    public GameObject chasePlayerResponseText;
    public GameObject scarletPlayerResponseText;
    public GameObject eddiePlayerResponseText;
    public GameObject gracePlayerResponseText;

    public GameObject juiceBoxPlayerResponseBox;
    public GameObject chasePlayerResponseBox;
    public GameObject eddiePlayerResponseBox;
    public GameObject scarletPlayerResponseBox;
    public GameObject gracePlayerResponseBox;

    [Header("NPC Response Objects")]
    public GameObject juiceBoxStatementText;
    public GameObject scarletStatementText;
    public GameObject eddieStatementText;
    public GameObject chaseStatementText;
    public GameObject graceStatementText;

    public GameObject juiceBoxStatementBox;
    public GameObject scarletStatementBox;
    public GameObject eddieStatementBox;
    public GameObject chaseStatementBox;
    public GameObject graceStatementBox;

    [Header("Pop Ups")]
    public GameObject popUpManager;
    public Image successIcon;
    public GameObject successText;
    public Image failIcon;
    public GameObject failText;

    public GameObject tutorialManager;
    public GameObject tutorialGrace;
    public GameObject returnButton;

    [Header("Characters")]
    public GameObject eddie;
    public GameObject scarlet;
    public GameObject chase;
    public GameObject juiceBox;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager"); //Finds and stores game manager
        response1Position = new Vector3(2030.744140625f, 326.246826171875f, 0.0f); //Stores UI element position
        response2Position = new Vector3(2030.777587890625f, 228.91348266601563f, 0.0f); //''
        popUpManager = GameObject.FindGameObjectWithTag("PUManager");
        tutorialManager = GameObject.FindGameObjectWithTag("Tutorial Manager");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!tutorialManager.GetComponent<Tutorials>().inTutorial && !returnButton.activeInHierarchy)
        {
            returnButton.SetActive(true);
        }
        interrogationUnderway = manager.GetComponent<SceneTransition>().interrogationActive; //Checks if an interrogation is active

        //if(inInterrogation && lastResponse == null)
       // {
       //     lastResponse = activeNode.responses[0];
      //  }
        
        if(interrogationLives == 0 && interrogationUnderway) //Is called when a player fails an interrogation //NEEDS UPDATED
        {
            BadEnd(2, repManager.GetComponent<ReputationManager>().femmePoints);
        }
        if(interrogationUnderway && activeNode!=null)
        {
            if (activeNode.exitNode == true)
            {
                SuccessfulEnd(); //Is called when a player completes an interrogation successfully
            }
        }
        
        if(inInterrogation)
        {
            interactMessage.SetActive(false);
        }

    }

    public void ContinueInterrogation() //Used for continuing the interrogation sequence
    {
        if (!activeNode.evidenceNeededCheck && lastResponsePlayer)
        {
            if (playerChoice == 1)
            { 
                    if(activeInterrogant.name == "JB")
                    {
                        lastResponseJB = activeNode.responses[0];
                    }
                    if(activeInterrogant.name == "Scarlet (The Femme Fatale)")
                    {
                        lastResponseScarlet = activeNode.responses[0];
                    }
                if(activeInterrogant.name == "Eddie (The Goon")
                    {
                        lastResponseEddie = activeNode.responses[0];
                    }
                if(activeInterrogant.name == "Chase (The Cool Guy)")
                    {
                    lastResponseChase = activeNode.responses[0];
                   
                }
                if(activeInterrogant.name == "Grace")
                {
                    lastResponseGrace = activeNode.responses[0];
                }
                //lastResponse = activeNode.responses[0]; //Stores the last response from the player
                LoadIntNodeInfo(activeNode.children[0]); //Loads the next node 
                playerChoice = 0;
                lastResponsePlayer = false;
                return;
            }

            if (playerChoice == 2)
            {
                EarlyExit(); //Allows the player to leave an interrogation at a time of their choosing
                playerChoice = 0;
            }
        }
      
        if(!lastResponsePlayer)
        {
            
            playerResponse1.GetComponent<TextMeshProUGUI>().text =  activeNode.responses[0];
            lastResponsePlayer = true;
        }
    }

    public int RecordInterrogationResponse() //Used to retreive a response from the player using keys
    {
        int choice = 4;
        if (pos == 0)
        {
            intResponseBox1.GetComponent<Image>().color = Color.white;
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {

                pos++;
                intResponseBox1.GetComponent<Image>().color = Color.gray;
            }
            if (Input.GetKeyUp(KeyCode.Return))
            {
                
                    choice = 0;
                    intResponseBox1.GetComponent<Image>().color = Color.gray;                   
                    return choice;
                
            }
        }
        if (pos == 1)
        {
            intResponseBox2.GetComponent<Image>().color = Color.white;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                pos--;
                intResponseBox2.GetComponent<Image>().color = Color.gray;
            }
            

            if (Input.GetKeyUp(KeyCode.Return))
            {
                
                    choice = 1;
                    intResponseBox2.GetComponent<Image>().color = Color.gray;
                    return choice;
                
            }
        }
        

        return choice;
    }

    public void EarlyExit() //Called when a player wants to leave interrogation
    {
        inInterrogation = false;
        ClearDialogue();
        manager.GetComponent<SceneTransition>().ChangeToMainArea();//Transitions the player back to the main area
        
        interrogationPanel.SetActive(false); //Deactivates the interrogation UI
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        
    }
    public void SuccessfulEnd() //Called when a player succeeds in an interrogation
    {
        noneCompleted = false;
        if(activeInterrogant.name == "JB")
        {
            jbCompleted = true;
        }
        if(activeInterrogant.name == "Scarlet (The Femme Fatale)")
        {
            scarletCompleted = true;
        }
        if(activeInterrogant.name == "Chase (The Cool Guy)")
        {
            chaseCompleted = true;
        }
        if(activeInterrogant.name == "Eddie (The Goon)")
        {
            eddieCompleted = true;
        }
        
        inInterrogation = false;
        manager.GetComponent<SceneTransition>().successfulInterrogation = true;
        manager.GetComponent<SceneTransition>().ChangeToMainArea(); //Transitions the player back to the main area
        interrogationPanel.SetActive(false);
        ClearDialogue(); //Clears the last interrogation's data
        popUpManager.GetComponent<PopUpManager>().FadeImage(successIcon, successText);

        
        
    }

    public void BadEnd(int repLoss, int chosenRepLevel) //Is called when a player runs out of lives and fails an interrogation
    {
        inInterrogation = false;
        chosenRepLevel -= repLoss; //Reputation is lost
        manager.GetComponent<SceneTransition>().ChangeToMainArea(); //Transitions the player back to the main area
        interrogationPanel.SetActive(false);
        ClearDialogue(); //Clears the last interrogation's data
        popUpManager.GetComponent<PopUpManager>().FadeImage(failIcon, failText);
    }

    public void LoadIntNodeInfo(DialogueNode newNode) //Method is used to load a dialogue node's data and update the UI, similar to the method found in DialogueManager
    {
       // if (lastResponse != null)
       // {
       //     playerResponse1.GetComponent<TextMeshProUGUI>().text = "Detective Drew: " + lastResponse;
       // }
        if (activeNode != null)
        {
            activeNode.nodeActive = false;
        }
        activeNode = newNode;
        newNode.nodeActive = true;
        SwitchEmotion();
        if(activeInterrogant.name == "JB")
        {
            
            mostRecentJuiceBoxNode = activeNode;
            npcStatement.GetComponent<TextMeshProUGUI>().text = "Juice Box: " + newNode.speech;
        }
        if(activeInterrogant.name == "Scarlet (The Femme Fatale)")
        {
            mostRecentScarletNode = activeNode;
            npcStatement.GetComponent<TextMeshProUGUI>().text = "Scarlet: " + newNode.speech;
        }
        if(activeInterrogant.name == "Chase (The Cool Guy)")
        {
            mostRecentChaseNode = activeNode;
            npcStatement.GetComponent<TextMeshProUGUI>().text = "Chase: " + newNode.speech;
        }
        if(activeInterrogant.name == "Eddie (The Goon)")
        {
            mostRecentEddieNode = activeNode;
            npcStatement.GetComponent<TextMeshProUGUI>().text = "Eddie: " + newNode.speech;
        }
        if(activeInterrogant.name == "Grace")
        {
            mostRecentGraceNode = activeNode;
            npcStatement.GetComponent<TextMeshProUGUI>().text = "Grace: " + newNode.speech;
        }
       // npcStatement.GetComponent<TextMeshProUGUI>().text = activeInterrogant.name + ": " + newNode.speech;
        //intResponseText1.GetComponent<TextMeshProUGUI>().text = activeNode.responses[0];
        if (activeNode.lifeLoss > 0)
        {
            interrogationSource.PlayOneShot(lifeLostSound, 0.5f);
            interrogationLives -= activeNode.lifeLoss;
        }
        if (activeNode.evidenceNeededCheck)
        {
            evButton.SetActive(true);
            intResponseBox1.SetActive(false);

        }
        else
        {
            evButton.SetActive(false);
            intResponseBox1.SetActive(true);
        }
        
    }

    public void StartInterrogation(DialogueNode startNode, GameObject targetNPC) //Is called at the beginning of an interrogation 
    {
        inInterrogation = true;
        Cursor.visible = true; //CURSOR STUFF - UPDATE
        Cursor.lockState = CursorLockMode.None;
        pos = 0; //Resets selection position
      //  intResponseBox2.GetComponent<Image>().color = Color.gray; 
       // intResponseBox3.GetComponent<Image>().color = Color.gray;
        interrogationLives = 5;
        activeInterrogant = targetNPC;
       // if (!firstTry) //Checks if the player has been in this interrogation before
        //{
            if(activeInterrogant.name == "JB") 
            {
                if (mostRecentJuiceBoxNode == null)
                {
                  //  playerResponse1.GetComponent<TextMeshProUGUI>().text = "Detective Drew:";
                    LoadIntNodeInfo(startNode);

                }
                else if(mostRecentJuiceBoxNode != null)
                {
                    
                    LoadIntNodeInfo(mostRecentJuiceBoxNode); //Loads the node that was visited in the last interrogation
                    playerResponse1.GetComponent<TextMeshProUGUI>().text =  lastResponseJB;
                }

            }
            if(activeInterrogant.name == "Scarlet (The Femme Fatale)")
            {
                if (mostRecentScarletNode == null)
                {
                    //playerResponse1.GetComponent<TextMeshProUGUI>().text = "Detective Drew:";
                    LoadIntNodeInfo(startNode);

                }
                else if(mostRecentScarletNode !=null)
                {
                    LoadIntNodeInfo(mostRecentScarletNode); //Loads the node that was visited in the last interrogation
                    playerResponse1.GetComponent<TextMeshProUGUI>().text =  lastResponseScarlet;
                }
            }
            if(activeInterrogant.name == "Eddie (The Goon)")
            {
                if (mostRecentEddieNode == null)
                {
                   // playerResponse1.GetComponent<TextMeshProUGUI>().text = "Detective Drew:";
                    LoadIntNodeInfo(startNode);

                }
                else if(mostRecentEddieNode != null)
                {
                    LoadIntNodeInfo(mostRecentEddieNode); //Loads the node that was visited in the last interrogation
                    playerResponse1.GetComponent<TextMeshProUGUI>().text =  lastResponseEddie;
                }
            }
            if(activeInterrogant.name == "Chase (The Cool Guy)")
            {
            if (mostRecentChaseNode == null)
            {
               // playerResponse1.GetComponent<TextMeshProUGUI>().text = "Detective Drew:";
                LoadIntNodeInfo(startNode);

            }
            else if (mostRecentChaseNode != null)
                {
                    LoadIntNodeInfo(mostRecentChaseNode); //Loads the node that was visited in the last interrogation
                    playerResponse1.GetComponent<TextMeshProUGUI>().text =  lastResponseChase;
                }
            }
            if(activeInterrogant.name == "Grace")
        {       
            if(mostRecentGraceNode == null)
            {
                LoadIntNodeInfo(startNode);
            }
            else if(mostRecentGraceNode != null)
            {
                LoadIntNodeInfo(mostRecentGraceNode);
                playerResponse1.GetComponent<TextMeshProUGUI>().text = lastResponseGrace;
            }
        }
      //  }
      //  if (firstTry)
      //  {
        //    LoadIntNodeInfo(startNode); //Loads the start node of the interrogation
        //    firstTry = false; //Signals that the player has attempted the interrogation at least once
     //   }
        
        

    }

    public bool CheckNodeEvidence() //Method is used to determine if the player has the correct evidence //POSSIBLY REDUNDANT
    {
        bool evidencePresent;
        foreach(string evidence in PinboardManager.GetComponent<PinboardManager>().chaseThreadedLikes)
        {
            if (activeNode.evidenceRequired == evidence)
            {
                evidencePresent = true;
                return evidencePresent;
                
            }
        }
        return evidencePresent = false;
    }

    

    private void ClearDialogue() //Clears the dialogue from the UI elements and resets the response count 
    {
        // npcStatement.GetComponent<TextMeshProUGUI>().text = "";
        // npcStatement.SetActive(false);
        // playerResponse1.GetComponent<TextMeshProUGUI>().text = "";
        lastResponseJB = juiceBoxPlayerResponseText.GetComponent<TextMeshProUGUI>().text;
        lastResponseChase = chasePlayerResponseText.GetComponent<TextMeshProUGUI>().text;
        lastResponseEddie = eddiePlayerResponseText.GetComponent<TextMeshProUGUI>().text;
        lastResponseScarlet = scarletPlayerResponseText.GetComponent<TextMeshProUGUI>().text;
        juiceBoxStatementBox.SetActive(false);
        juiceBoxPlayerResponseBox.SetActive(false);
        chaseStatementBox.SetActive(false);
        chasePlayerResponseBox.SetActive(false);
        scarletStatementBox.SetActive(false);
        scarletPlayerResponseBox.SetActive(false);
        eddiePlayerResponseBox.SetActive(false);
        eddieStatementBox.SetActive(false);
        gracePlayerResponseBox.SetActive(false);
        graceStatementBox.SetActive(false);
        playerResponse1 = null;
        npcStatement = null;
       // playerResponse1.SetActive(false);
        //lastResponse = null;
        //lastResponse2 = null;
        //responseCount = 0;
    }

    public void BringUpEvidencePanel() //Activates the evidence panel UI elements
    {
        pinboard.SetActive(true);
        interrogationPanel.SetActive(false);
        if(tutorialManager.GetComponent<Tutorials>().inIPBTutorial)
        {
            tutorialManager.GetComponent<Tutorials>().overPBText.GetComponent<TextMeshProUGUI>().text = tutorialManager.GetComponent<Tutorials>().ipbText1;
            tutorialManager.GetComponent<Tutorials>().pbTextObject.SetActive(true);
            tutorialManager.GetComponent<Tutorials>().togglePBMessageButton.SetActive(true);
        }
        
    }

    public void ReturnToInterrogationPanel() //Transitions the player back to the interrogation panel
    {
        interrogationPanel.SetActive(true);
        pinboard.SetActive(false);
    }

    private void FillEvidenceImages() //Fills the evidence screen with decoy images and the correct evidence image
    {
        if (activeNode.evidenceNeededCheck)
        {
            noEvMessage.SetActive(true); //Signals that the player has no evidence available 
            evidencePiece1.gameObject.SetActive(false);
            evidencePiece2.gameObject.SetActive(false);
            evidencePiece3.gameObject.SetActive(false);
            foreach (string evidence in PinboardManager.GetComponent<PinboardManager>().threadedEvidence) //Checks if the evidence has been threaded
            {
                if (evidence == activeNode.evidenceRequired) //Checks if the evidence is relevant to the active node
                {
                    evidencePiece1.gameObject.SetActive(true);
                    evidencePiece2.gameObject.SetActive(true);
                    evidencePiece3.gameObject.SetActive(true);
                    noEvMessage.SetActive(false);
                    int evImage = Random.Range(0, 2); //Randomises the position of the correcr evidence piece on the screen
                    switch (evImage) //This switch statement is responsible for placing the images in the correct slots 
                    {
                        case 0:
                            evidencePiece1.sprite = activeNode.evidenceRequiredImage;
                            evidencePiece2.sprite = sprites[Random.Range(0, sprites.Count)];
                            if(evidencePiece2.sprite == activeNode.evidenceRequiredImage)
                            {
                                while(evidencePiece2.sprite == activeNode.evidenceRequiredImage)
                                {
                                    evidencePiece2.sprite = sprites[Random.Range(0, sprites.Count)];
                                }
                            }
                            evidencePiece3.sprite = sprites[Random.Range(0, sprites.Count)];
                            if (evidencePiece3.sprite == activeNode.evidenceRequiredImage)
                            {
                                while (evidencePiece3.sprite == activeNode.evidenceRequiredImage)
                                {
                                    evidencePiece3.sprite = sprites[Random.Range(0, sprites.Count)];
                                }
                            }

                            break;
                        case 1:
                            evidencePiece2.sprite = activeNode.evidenceRequiredImage;
                            evidencePiece1.sprite = sprites[Random.Range(0, sprites.Count)];
                            if (evidencePiece1.sprite == activeNode.evidenceRequiredImage)
                            {
                                while (evidencePiece1.sprite == activeNode.evidenceRequiredImage)
                                {
                                    evidencePiece1.sprite = sprites[Random.Range(0, sprites.Count)];
                                }
                            }
                            evidencePiece3.sprite = sprites[Random.Range(0, sprites.Count)];
                            if (evidencePiece3.sprite == activeNode.evidenceRequiredImage)
                            {
                                while (evidencePiece3.sprite == activeNode.evidenceRequiredImage)
                                {
                                    evidencePiece3.sprite = sprites[Random.Range(0, sprites.Count)];
                                }
                            }
                            break;
                        case 2:
                            evidencePiece3.sprite = activeNode.evidenceRequiredImage;
                            evidencePiece2.sprite = sprites[Random.Range(0, sprites.Count)];
                            if (evidencePiece2.sprite == activeNode.evidenceRequiredImage)
                            {
                                while (evidencePiece2.sprite == activeNode.evidenceRequiredImage)
                                {
                                    evidencePiece2.sprite = sprites[Random.Range(0, sprites.Count)];
                                }
                            }
                            evidencePiece1.sprite = sprites[Random.Range(0, sprites.Count)];
                            if (evidencePiece1.sprite == activeNode.evidenceRequiredImage)
                            {
                                while (evidencePiece1.sprite == activeNode.evidenceRequiredImage)
                                {
                                    evidencePiece1.sprite = sprites[Random.Range(0, sprites.Count)];
                                }
                            }
                            break;
                    }
                    activeNode.evImagesGenerated = true; //Marks that the images have been filled for this node
                }
                
            }
        }       
    }


    private void SwitchEmotion() //Method is used to update the emotions displayed by the NPC characters in the interrogation
    {
        switch (activeNode.nodeEmotion)
        {
            case 0:
                activeInterrogant.GetComponent<NPCDialogue>().textureToChange.SetTexture("_DetailAlbedoMap", activeInterrogant.GetComponent<NPCDialogue>().defaultEmotion);
                break;
            case 1:
                activeInterrogant.GetComponent<NPCDialogue>().textureToChange.SetTexture("_DetailAlbedoMap", activeInterrogant.GetComponent<NPCDialogue>().angryEmotion);
                break;
            case 2:
                activeInterrogant.GetComponent<NPCDialogue>().textureToChange.SetTexture("_DetailAlbedoMap", activeInterrogant.GetComponent<NPCDialogue>().cryingEmotion);
                break;
            case 3:
                activeInterrogant.GetComponent<NPCDialogue>().textureToChange.SetTexture("_DetailAlbedoMap", activeInterrogant.GetComponent<NPCDialogue>().guiltyEmotion);
                break;
            case 4:
                activeInterrogant.GetComponent<NPCDialogue>().textureToChange.SetTexture("_DetailAlbedoMap", activeInterrogant.GetComponent<NPCDialogue>().playfulEmotion);
                break;
            case 5:
                activeInterrogant.GetComponent<NPCDialogue>().textureToChange.SetTexture("_DetailAlbedoMap", activeInterrogant.GetComponent<NPCDialogue>().sadEmotion);
                break;
            case 6:
                activeInterrogant.GetComponent<NPCDialogue>().textureToChange.SetTexture("_DetailAlbedoMap", activeInterrogant.GetComponent<NPCDialogue>().shockedEmotion);
                break;
            case 7:
                activeInterrogant.GetComponent<NPCDialogue>().textureToChange.SetTexture("_DetailAlbedoMap", activeInterrogant.GetComponent<NPCDialogue>().thinkingEmotion);
                break;
        }
    }

    public void CheckEvidence(GameObject pressedButton)
    {
        if(inInterrogation)
        {
            if(pressedButton.GetComponent<EvidenceSlot>().evidenceText == activeNode.evidenceRequired)
            {
                pressedButton.GetComponent<EvidenceSlot>().evidenceTooltip.SetActive(false);
                playerResponse1.GetComponent<TextMeshProUGUI>().text = activeNode.responses[0];
                LoadIntNodeInfo(activeNode.children[0]);
                interrogationPanel.SetActive(true);
                tutorialManager.GetComponent<Tutorials>().pbTextObject.SetActive(false);
                tutorialManager.GetComponent<Tutorials>().togglePBMessageButton.SetActive(false);
                pinboard.SetActive(false);
                
            }
            else if(pressedButton.GetComponent<EvidenceSlot>().evidenceText != activeNode.evidenceRequired)
            {
                
               // playerResponse1.GetComponent<TextMeshProUGUI>().text = activeNode.responses[1];
                LoadIntNodeInfo(activeNode.children[1]);
                interrogationPanel.SetActive(true);
                pinboard.SetActive(false);
                pressedButton.GetComponent<EvidenceSlot>().evidenceTooltip.SetActive(false);

            }
        }
    }

    public void ClickOption1()
    {
        playerChoice = 1;
        ContinueInterrogation();
    }
    public void ClickOption2()
    {
        playerChoice = 2;
        ContinueInterrogation();
    }

    public void ReloadText()
    {
        if(activeInterrogant ==  eddie)
        {
            eddieStatementText.GetComponent<TextMeshProUGUI>().text = "Eddie: " + activeNode.speech;
        }
        if(activeInterrogant == chase)
        {
            chaseStatementText.GetComponent<TextMeshProUGUI>().text = "Chase: " + activeNode.speech;
        }
        if(activeInterrogant == juiceBox)
        {
            juiceBoxStatementText.GetComponent<TextMeshProUGUI>().text = "Juice Box: " + activeNode.speech;
        }
        if(activeInterrogant == scarlet)
        {
            scarletStatementText.GetComponent<TextMeshProUGUI>().text = "Scarlet: " + activeNode.speech;
        }
    }
    
}
