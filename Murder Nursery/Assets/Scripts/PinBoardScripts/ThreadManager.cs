using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class ThreadManager : MonoBehaviour
{
    
    public GameObject firstThreadItem; //The first end of the thread selected by the player
    public GameObject secondThreadItem; //The second end of the thread selected by the player
    public Image newEvidenceImage; //Stores a new evidence image
    public string newEvidenceText; //Stores a new evidence text
    public GameObject pinBoardManager; //Stores the pinboard manager
    public GameObject interrogationManager;
    public GameObject tutorialManager;

    public void Start()
    {
        tutorialManager = GameObject.FindGameObjectWithTag("Tutorial Manager");
    }

    // Update is called once per frame
    void Update() 
    {
        if(interrogationManager == null)
        {
            interrogationManager = GameObject.FindGameObjectWithTag("InterrogationManager");
        }
        if (firstThreadItem != null && secondThreadItem != null && !interrogationManager.GetComponent<Interrogation>().inInterrogation) //Checks if the player has selected two items to thread
        {
            if (secondThreadItem.name != "Chase" && secondThreadItem.name != "Scarlet" && secondThreadItem.name != "Grace" && secondThreadItem.name != "Eddie" && secondThreadItem.name != "JuiceBox") //Checks if the evidence has already been threaded
            {
                if (!CheckIfThreaded(secondThreadItem.GetComponent<EvidenceSlot>().evidenceText)) //Checks if the second item is valid and not a category 
                {
                    CheckThreadStart();
                    GameObject newThread = null;
                    if (firstThreadItem.name == "Scarlet")
                    {
                        if(tutorialManager.GetComponent<Tutorials>().inPBTutorial3)
                        {
                            tutorialManager.GetComponent<Tutorials>().pbTextObject.SetActive(true);
                            tutorialManager.GetComponent<Tutorials>().overPBText.GetComponent<TextMeshProUGUI>().text = tutorialManager.GetComponent<Tutorials>().pbText3;
                        }
                        
                         newThread = this.GetComponent<PinboardThread>().MakeLine(firstThreadItem.transform.position.x, firstThreadItem.transform.position.y, secondThreadItem.transform.position.x, secondThreadItem.transform.position.y, Color.red); //Creates the thread
                        if (tutorialManager.GetComponent<Tutorials>().inPBTutorial4)
                        {
                            
                            
                            tutorialManager.GetComponent<Tutorials>().inPBTutorial4 = false;
                            tutorialManager.GetComponent<Tutorials>().inPButorial5 = true;
                            tutorialManager.GetComponent<Tutorials>().EndPBTutorial();
                        }
                    }
                    if(firstThreadItem.name == "Chase")
                    {
                        if (tutorialManager.GetComponent<Tutorials>().inPBTutorial3)
                        {
                            tutorialManager.GetComponent<Tutorials>().pbTextObject.SetActive(true);
                            tutorialManager.GetComponent<Tutorials>().overPBText.GetComponent<TextMeshProUGUI>().text = tutorialManager.GetComponent<Tutorials>().pbText3;
                        }
                        newThread = this.GetComponent<PinboardThread>().MakeLine(firstThreadItem.transform.position.x, firstThreadItem.transform.position.y, secondThreadItem.transform.position.x, secondThreadItem.transform.position.y, Color.yellow);
                        if (tutorialManager.GetComponent<Tutorials>().inPBTutorial4)
                        {
                            
                            
                            tutorialManager.GetComponent<Tutorials>().inPBTutorial4 = false;
                            tutorialManager.GetComponent<Tutorials>().inPButorial5 = true;
                            tutorialManager.GetComponent<Tutorials>().EndPBTutorial();
                        }
                    }
                    if(firstThreadItem.name == "Grace")
                    {
                        if (tutorialManager.GetComponent<Tutorials>().inPBTutorial3)
                        {
                            tutorialManager.GetComponent<Tutorials>().pbTextObject.SetActive(true);
                            tutorialManager.GetComponent<Tutorials>().overPBText.GetComponent<TextMeshProUGUI>().text = tutorialManager.GetComponent<Tutorials>().pbText3;
                        }
                        newThread = this.GetComponent<PinboardThread>().MakeLine(firstThreadItem.transform.position.x, firstThreadItem.transform.position.y, secondThreadItem.transform.position.x, secondThreadItem.transform.position.y, Color.cyan);
                        if (tutorialManager.GetComponent<Tutorials>().inPBTutorial4)
                        {
                            
                            
                            tutorialManager.GetComponent<Tutorials>().inPBTutorial4 = false;
                            tutorialManager.GetComponent<Tutorials>().inPButorial5 = true;
                            tutorialManager.GetComponent<Tutorials>().EndPBTutorial();
                        }
                    }
                    if(firstThreadItem.name == "Eddie")
                    {
                        if (tutorialManager.GetComponent<Tutorials>().inPBTutorial3)
                        {
                            tutorialManager.GetComponent<Tutorials>().pbTextObject.SetActive(true);
                            tutorialManager.GetComponent<Tutorials>().overPBText.GetComponent<TextMeshProUGUI>().text = tutorialManager.GetComponent<Tutorials>().pbText3;
                        }
                        newThread = this.GetComponent<PinboardThread>().MakeLine(firstThreadItem.transform.position.x, firstThreadItem.transform.position.y, secondThreadItem.transform.position.x, secondThreadItem.transform.position.y, Color.blue);
                        if (tutorialManager.GetComponent<Tutorials>().inPBTutorial4)
                        {
                            
                            
                            tutorialManager.GetComponent<Tutorials>().inPBTutorial4 = false;
                            tutorialManager.GetComponent<Tutorials>().inPButorial5 = true;
                            tutorialManager.GetComponent<Tutorials>().EndPBTutorial();
                        }
                    }
                    if(firstThreadItem.name == "JuiceBox")
                    {
                        if (tutorialManager.GetComponent<Tutorials>().inPBTutorial3)
                        {
                            tutorialManager.GetComponent<Tutorials>().pbTextObject.SetActive(true);
                            tutorialManager.GetComponent<Tutorials>().overPBText.GetComponent<TextMeshProUGUI>().text = tutorialManager.GetComponent<Tutorials>().pbText3;
                        }
                        newThread = this.GetComponent<PinboardThread>().MakeLine(firstThreadItem.transform.position.x, firstThreadItem.transform.position.y, secondThreadItem.transform.position.x, secondThreadItem.transform.position.y, Color.green);
                        if (tutorialManager.GetComponent<Tutorials>().inPBTutorial4)
                        {
                            
                            
                            tutorialManager.GetComponent<Tutorials>().inPBTutorial4 = false;
                            tutorialManager.GetComponent<Tutorials>().inPButorial5 = true;
                            tutorialManager.GetComponent<Tutorials>().EndPBTutorial();
                            
                        }
                    }
                    secondThreadItem.GetComponent<EvidenceSlot>().threads.Add(newThread);
                    
                        firstThreadItem = null; //Resets the selected items 
                        secondThreadItem = null; //Resets the selected items                   
                }
                
            }
            firstThreadItem = null;
            secondThreadItem = null;
        }
    }

    public void CheckThreadStart() //Checks and stores the evidence piece threaded //CHANGE FOR EFFICICENCY 
    {
        
            newEvidenceImage = secondThreadItem.GetComponent<EvidenceSlot>().evidenceImage;
            newEvidenceText = secondThreadItem.GetComponent<EvidenceSlot>().evidenceText;
            if (firstThreadItem.tag == "ChaseSlot")
            {
                pinBoardManager.GetComponent<PinboardManager>().chaseThreadedLikes.Add(newEvidenceText);
                pinBoardManager.GetComponent<PinboardManager>().threadedEvidence.Add(newEvidenceText);
            }
            if(firstThreadItem.tag == "ScarletSlot")
            {
                pinBoardManager.GetComponent<PinboardManager>().scarletThreadedLikes.Add(newEvidenceText);
                pinBoardManager.GetComponent<PinboardManager>().threadedEvidence.Add(newEvidenceText);
            }
            if(firstThreadItem.tag == "EddieSlot")
            {
                pinBoardManager.GetComponent<PinboardManager>().eddieThreadedLikes.Add(newEvidenceText);
                pinBoardManager.GetComponent<PinboardManager>().threadedEvidence.Add(newEvidenceText);
            }
            if(firstThreadItem.tag == "JuiceBoxSlot")
            {
                pinBoardManager.GetComponent<PinboardManager>().juiceBoxThreadedLikes.Add(newEvidenceText);
                pinBoardManager.GetComponent<PinboardManager>().threadedEvidence.Add(newEvidenceText);
            }
            if(firstThreadItem.tag == "GraceSlot")
            {
                pinBoardManager.GetComponent<PinboardManager>().graceThreadedLikes.Add(newEvidenceText);
                pinBoardManager.GetComponent<PinboardManager>().threadedEvidence.Add(newEvidenceText);
            }
            
        
        if(firstThreadItem.gameObject.name == "Dislikes") //Stores the dislikes for each character 
        {
            newEvidenceImage = secondThreadItem.GetComponent<EvidenceSlot>().evidenceImage;
            newEvidenceText = secondThreadItem.GetComponent<EvidenceSlot>().evidenceText;
            if (firstThreadItem.tag == "ChaseSlot")
            {
                pinBoardManager.GetComponent<PinboardManager>().chaseThreadedDislikes.Add(newEvidenceText);
                pinBoardManager.GetComponent<PinboardManager>().threadedEvidence.Add(newEvidenceText);

            }
            if(firstThreadItem.tag == "ScarletSlot")
            {
                pinBoardManager.GetComponent<PinboardManager>().scarletThreadedDislikes.Add(newEvidenceText);
                pinBoardManager.GetComponent<PinboardManager>().threadedEvidence.Add(newEvidenceText);
            }
            if(firstThreadItem.tag == "EddieSlot")
            {
                pinBoardManager.GetComponent<PinboardManager>().eddieThreadedDislikes.Add(newEvidenceText);
                pinBoardManager.GetComponent<PinboardManager>().threadedEvidence.Add(newEvidenceText);
            }
            if(firstThreadItem.tag == "JuiceBoxSlot")
            {
                pinBoardManager.GetComponent<PinboardManager>().juiceBoxThreadedDislikes.Add(newEvidenceText);
                pinBoardManager.GetComponent<PinboardManager>().threadedEvidence.Add(newEvidenceText);
            }
            if(firstThreadItem.tag == "GraceSlot")
            {
                pinBoardManager.GetComponent<PinboardManager>().graceThreadedDislikes.Add(newEvidenceText);
                pinBoardManager.GetComponent<PinboardManager>().threadedEvidence.Add(newEvidenceText);
            }
        }
        if(firstThreadItem.gameObject.name == "Events") //Stores the events for each character
        {
            newEvidenceImage = secondThreadItem.GetComponent<EvidenceSlot>().evidenceImage;
            newEvidenceText = secondThreadItem.GetComponent<EvidenceSlot>().evidenceText;
            if (firstThreadItem.tag == "ChaseSlot")
            {
                pinBoardManager.GetComponent<PinboardManager>().chaseThreadedEvents.Add(newEvidenceText);
                pinBoardManager.GetComponent<PinboardManager>().threadedEvidence.Add(newEvidenceText);

            }
            if(firstThreadItem.tag == "ScarletSlot")
            {
                pinBoardManager.GetComponent<PinboardManager>().scarletThreadedEvents.Add(newEvidenceText);
                pinBoardManager.GetComponent<PinboardManager>().threadedEvidence.Add(newEvidenceText);
            }
            if(firstThreadItem.tag == "EddieSlot")
            {
                pinBoardManager.GetComponent<PinboardManager>().eddieThreadedEvents.Add(newEvidenceText);
                pinBoardManager.GetComponent<PinboardManager>().threadedEvidence.Add(newEvidenceText);
            }
            if(firstThreadItem.tag == "JuiceBoxSlot")
            {
                pinBoardManager.GetComponent<PinboardManager>().juiceBoxThreadedEvents.Add(newEvidenceText);
                pinBoardManager.GetComponent<PinboardManager>().threadedEvidence.Add(newEvidenceText);
            }
            if(firstThreadItem.tag == "GraceSlot")
            {
                pinBoardManager.GetComponent<PinboardManager>().graceThreadedEvents.Add(newEvidenceText);
                pinBoardManager.GetComponent<PinboardManager>().threadedEvidence.Add(newEvidenceText);
            }
        }
    }

    private bool CheckIfThreaded(string evidenceToCheck) //Checks if the evidence has already been threaded
    {
        bool evidenceThreaded = false;
        foreach(string found in pinBoardManager.GetComponent<PinboardManager>().threadedEvidence) //Checks if the evidence is already on the threaded evidence list 
        {
            if(evidenceToCheck == found)
            {
                return evidenceThreaded = true;
            }
        }
        return evidenceThreaded;
    }
}
