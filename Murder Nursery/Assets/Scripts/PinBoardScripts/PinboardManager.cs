using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

// displays characters traits likes/dislikes/events on a pin-board style ui
public class PinboardManager : MonoBehaviour
{
    public static PinboardManager pinboard;
    public GameObject interrogationManager;

    // Used in the sentence completion scripts
    [HideInInspector]
    public string objectNameBeingReplaced;

    [SerializeField]
    GameObject CharacterTraitsUI;

    [Header("Pinboard Variables")]
    public List<EvidenceClass> discoveredEvidence; //Stores the evidence which has been found by the player
    public List<string> threadedEvidence; //Stores the evidence which has been threaded by the player
    public int evidencePiecesPlaced = 0; //Stores how many evidence pieces have been placed
    public int correctConclusions = 0; //Will be used to store how many correct conclusions the player has made
    public int incorrectConclusions = 0; //Will be used to store how many incorrect conclusions the player has made
    public float rightPercentage = 0.00f; //Will be used to work out the correct answer percentage
    public float wrongPercentage = 0.00f; //Will be used to work out the incorrect answer percentage

    [Header("UI Objects")]
    public GameObject correctPercentText;
    public GameObject wrongPercentText;
    public GameObject chaseSectionZoom; //UI element which stores the components of a zoomed in character section
    public GameObject scarletSectionZoom;//''
    public GameObject juiceBoxSectionZoom; //''
    public GameObject eddieSectionZoom;//''
    public GameObject graceSectionZoom;//''
    public GameObject pinBoard; //UI element which stores the components of the pinboard 
    public GameObject firstThreadItem; //Temporarily stores one end of an item thread
    public GameObject lastThreadItem;//''
    public GameObject evidenceList; //Stores the evidence the player has found
    public GameObject textPrefab; 

    [Header("Chase Evidence")]
    public GameObject[] chaseEvidenceSlots;  
    public List<string> chaseThreadedLikes = new List<string>(); //Stores NPC likes
    public List<string> chaseThreadedDislikes = new List<string>(); //Stores NPC dislikes
    public List<string> chaseThreadedEvents = new List<string>(); //Stores NPC events

    [Header("Scarlet Evidence")]
    public GameObject[] scarletEvidenceSlots;
    public List<string> scarletThreadedLikes = new List<string>();
    public List<string> scarletThreadedDislikes = new List<string>();
    public List<string> scarletThreadedEvents = new List<string>();

    [Header("Eddie Evidence")]
    public GameObject[] eddieEvidenceSlots;
    public List<string> eddieThreadedLikes = new List<string>();
    public List<string> eddieThreadedDislikes = new List<string>();
    public List<string> eddieThreadedEvents = new List<string>();

    [Header("Juice Box Evidence")]
    public GameObject[] juiceBoxEvidenceSlots;
    public List<string> juiceBoxThreadedLikes = new List<string>();
    public List<string> juiceBoxThreadedDislikes = new List<string>();
    public List<string> juiceBoxThreadedEvents = new List<string>();

    [Header("Grace Evidence")]
    public GameObject[] graceEvidenceSlots;
    public List<string> graceThreadedLikes = new List<string>();
    public List<string> graceThreadedDislikes = new List<string>();
    public List<string> graceThreadedEvents = new List<string>();

    public List<EvidenceClass> evidencePieces; //Stores evidence pieces
    public GameObject[] slots;
    public int slotsProgress = 0;
    public GameObject threadsParent;

    public GameObject popUpManager;
    public Image evidenceIcon;
    public GameObject evidenceText;
    public bool clearingEv = false;
    public EvidenceClass tutorialEvidence;
    public GameObject tutorialManager;
    public GameObject closePB;
    // Start is called before the first frame update
    void Awake()
    {
        pinboard = this;
        interrogationManager = GameObject.FindGameObjectWithTag("InterrogationManager");
    }

    private void Start()
    {
        tutorialManager = GameObject.FindGameObjectWithTag("Tutorial Manager");
        popUpManager = GameObject.FindGameObjectWithTag("PUManager");
        foreach (EvidenceClass evidence in evidencePieces)
        {
            evidence.evidenceFound = false; //Marks each evidence piece as undiscovered at the start of the game 
        }
        discoveredEvidence = new List<EvidenceClass>();
        threadedEvidence = new List<string>();
       // UpdateEvidenceImages(tutorialEvidence);
    }
    
    // Called when we want to update the pin board after discovering a characters likes/dislikes/events
    // content determines whos likes/dislikes/events we are updating / string is what we want the content to say
    public void UpdatePinboard(Transform content, string pinboardText) 
    {
        GameObject pinboardObj = Instantiate(CharacterTraitsUI, content);
        var contentText = pinboardObj.transform.Find("TraitsText").GetComponent<TextMeshProUGUI>();
        contentText.text = pinboardText;
    }

    public void UpdateEvidenceListUI(EvidenceClass evidence) //Will be used to add text help to the evidence pinboard
    {
        
        GameObject newEvidence = Instantiate(textPrefab, evidenceList.transform);
        newEvidence.GetComponent<TextMeshProUGUI>().text = evidence.evidenceText;
        newEvidence.GetComponent<DragAndDrop>().itemID = evidence.evidenceID;         
    }

    public void UpdateEvidenceImages(EvidenceClass evidence) //Updates the evidence pinboard visually after player interaction
    {
        if(!clearingEv && !tutorialManager.GetComponent<Tutorials>().inTutorial)
        {
            popUpManager.GetComponent<PopUpManager>().FadeImage(evidenceIcon, evidenceText);
        }
        if(clearingEv)
        {
            clearingEv = false;
        }
        GameObject newImage = Instantiate(evidence.evidenceImage, evidenceList.transform);
        newImage.GetComponent<DragAndDrop>().itemID = evidence.evidenceID;
    }

    public void UpdateCharacterZone(GameObject characterArea, GameObject evidence) //Updates a specific character area
    {
        Instantiate(evidence, characterArea.transform);
    }
    
    public void CalculateAnswerPercentages() //Will be used to calculate correct answer percentages 
    {      
        rightPercentage = (correctConclusions / evidencePiecesPlaced) * 100;      
        correctPercentText.GetComponent<TextMeshProUGUI>().text = ("Correct Answers " + rightPercentage + "%");
        wrongPercentage = (incorrectConclusions / evidencePiecesPlaced) * 100;
        wrongPercentText.GetComponent<TextMeshProUGUI>().text = ("Wrong Answers " + wrongPercentage + "%");
    }

    public void TransitionToChaseArea() //Transitions to the zoomed in Chase pinboard area
    {
        foreach(GameObject slot in chaseEvidenceSlots)
        {
            if(slot.GetComponent<EvidenceSlot>().slotFilled)
            {
                slot.SetActive(true);
            }
        }
        pinBoard.SetActive(false);
        chaseSectionZoom.SetActive(true);
        interrogationManager.GetComponent<Interrogation>().activeZoomIn = chaseSectionZoom;
    }

    public void TransitionToScarletArea() //Transitions to the zoomed in Scarlet pinboard area
    {
        foreach(GameObject slot in scarletEvidenceSlots)
        {
            if(slot.GetComponent<EvidenceSlot>().slotFilled)
            {
                slot.SetActive(true);
            }
        }
        pinBoard.SetActive(false);
        scarletSectionZoom.SetActive(true);
        interrogationManager.GetComponent<Interrogation>().activeZoomIn = scarletSectionZoom;
    }

    public void TransitionToEddieArea() //Transitions to the zoomed in Eddie pinboard area
    {
        foreach(GameObject slot in eddieEvidenceSlots)
        {
            if(slot.GetComponent<EvidenceSlot>().slotFilled)
            {
                slot.SetActive(true);
            }
        }
        pinBoard.SetActive(false);
        eddieSectionZoom.SetActive(true);
        interrogationManager.GetComponent<Interrogation>().activeZoomIn = eddieSectionZoom;
    }

    public void TransitionToJuiceBox() //Transitions to the zoomed in Juice Box pinboard area
    {
        foreach(GameObject slot in juiceBoxEvidenceSlots)
        {
            if(slot.GetComponent<EvidenceSlot>().slotFilled)
            {
                slot.SetActive(true);
            }
        }
        pinBoard.SetActive(false);
        juiceBoxSectionZoom.SetActive(true);
        interrogationManager.GetComponent<Interrogation>().activeZoomIn = juiceBoxSectionZoom;
    }

    public void TransitionToGraceArea() //Transitions to the zoomed in Grace area
    {
        foreach (GameObject slot in graceEvidenceSlots)
        {
            if (slot.GetComponent<EvidenceSlot>().slotFilled)
            {
                slot.SetActive(true);
            }
        }
            pinBoard.SetActive(false);
            graceSectionZoom.SetActive(true);
        interrogationManager.GetComponent<Interrogation>().activeZoomIn = graceSectionZoom;


    }

    public void TransitionToPinboard() //Transitions back to the main pinboard
    {
        pinBoard.SetActive(true);
        chaseSectionZoom.SetActive(false);
        scarletSectionZoom.SetActive(false);
        eddieSectionZoom.SetActive(false);
        juiceBoxSectionZoom.SetActive(false);
        graceSectionZoom.SetActive(false);
        interrogationManager.GetComponent<Interrogation>().activeZoomIn = null;
    }
    
    

}
