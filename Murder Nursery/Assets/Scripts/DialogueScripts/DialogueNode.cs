using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNode : MonoBehaviour
{
    
    [Header("Node Structure")]
    public DialogueNode parent; //Stores the previous node
    public DialogueNode[] children; //Stores the connected child nodes
    public string speech; //Stores the npc statement
    public string[] responses; //Stores the players possible responses
    public string nonAnimatedSpeech;

    [Header("Reputation Modifiers")]
    public int repLevelOption1; //Rep Level Checker
    public int repLevelOption2; //''
    public int repLevelOption3; //'' 
    public int repGainResponse1; //Rep gained by response
    public int repGainResponse2; //''
    public int repGainResponse3; //'' 

    [Header("Node Identifiers")]
    public bool nodeActive = false; //Used to indicate if node is active
    public bool interrogationNode = false; //Indicates if node triggers interrogation
    public bool exitNode = false; //Indicates if interaction is finished
    public bool nodeVisited = false; //Records if node has been visited before
    public bool briberyAvailable = false; //Indicates if bribery is available
    public bool fitCheck = false; //Indicates if a dress up check is available
    public EvidenceClass evidenceToDiscover = null; //Stores the evidence to be discovered from the node


    [Header("Node Locking Variables")]
    public bool lockingNode = false; //Indicates if node can be locked off
    public bool firstPathLocked = false; //Signals that first path is locked
    public bool secondPathLocked = false; //Signals that second path is locked
    public bool thirdPathLocked = false; //Signals that third path is locked 
    public bool lockingFirstPath = false; //Signals that path will be locked after moving on 
    public bool lockingSecondPath = false; //''
    public bool lockingThirdPath = false;//''

    [Header("Conclusion Variables")]  
    public bool triggerTrueEnd = false; //Indicates that player has chosen the true ending
    public bool triggerGoodEnd = false; //Indicates that player has chosen the good ending
    public bool triggerBadEnd = false; //Indicates that the player has chosen the bad ending

    [Header("Dress Up Variables")]
    public DialogueNode dressUpNode; //Stores the dialogue node locked behind a dress up check
    public string requiredOutfit; //Stores the outfit required to pass the dress up check

    [Header("Interrogation Variables")]
    public int lifeLoss = 0; //Used to remove a life from the player in the event of an incorrect interrogation guess
    public string evidenceRequired; //Evidence required to proceed
    public Sprite evidenceRequiredImage; //Evidence piece image
    public bool evidenceNeededCheck = false; //Checks if evidence is required
    public bool evImagesGenerated = false; //Checks if fake evidence pieces have been generated

    [Header("Node Emotion")]
    public int nodeEmotion; //Emotion displayed by node

    [Header("Bribery")]
    public bool bribeGiven = false; //Indicates if a bribe has been given

    [Header("Tutorial")]
    public bool exitTutorial;
    public bool pinboardTutorial;
    public bool interroSummaryTutorial;
    public bool dressUpTutorial;
    public bool dressUpTutorial2;
    public bool notebookTutorial;
    public bool tutorialChosen;
    private void Start()
    {
        firstPathLocked = false; //Sets all the paths to unlocked at the beginning of play
        secondPathLocked = false;
        thirdPathLocked = false;
        
    }
}
