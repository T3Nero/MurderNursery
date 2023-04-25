using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GraceTutorial2 : MonoBehaviour
{
    public GameObject tutorialManager;
    public GameObject dressUpManager;
    public GameObject manager;
    public GameObject inventoryManager;
    public GameObject mGlass;
    // Start is called before the first frame update
    void Start()
    {
        tutorialManager = GameObject.FindGameObjectWithTag("Tutorial Manager");
        dressUpManager = GameObject.FindGameObjectWithTag("Dress Up Manager");
        manager = GameObject.FindGameObjectWithTag("Manager");
        inventoryManager = GameObject.FindGameObjectWithTag("Inventory");
    }

    // Update is called once per frame
    void Update()
    {
        
        if(tutorialManager.GetComponent<Tutorials>().nbTutorial2 && Input.GetKeyUp(KeyCode.I) && !inventoryManager.GetComponent<ToggleUIVisibility>().inventoryOpen)
        {
            
            inventoryManager.GetComponent<ToggleUIVisibility>().ToggleInventory();
            
        }

        if(tutorialManager.GetComponent<Tutorials>().mGlassTutorial1 && Input.GetKeyUp(KeyCode.I))
        {
            manager.GetComponent<DialogueManager>().ExitConversation();
            inventoryManager.GetComponent<ToggleUIVisibility>().ToggleInventory();
        }

        if(mGlass)
        {
            if(tutorialManager.GetComponent<Tutorials>().mGlassTutorial1 && mGlass.GetComponent<MagnifyingGlass>().usingMagnifyingGlass)
            {
                tutorialManager.GetComponent<Tutorials>().mgTextObject.SetActive(true);
                tutorialManager.GetComponent<Tutorials>().mgText.GetComponent<TextMeshProUGUI>().text = tutorialManager.GetComponent<Tutorials>().mgText1;
                tutorialManager.GetComponent<Tutorials>().mGlassTutorial1 = false;
                tutorialManager.GetComponent<Tutorials>().mGlassTutorial2 = true;
            }

            if(mGlass.GetComponent<MagnifyingGlass>().evidenceItem)
            {
                if(tutorialManager.GetComponent<Tutorials>().mGlassTutorial2 && mGlass.GetComponent<MagnifyingGlass>().evidenceItem.GetComponent<EvidenceItem>().inspectingItem)
                {
                    tutorialManager.GetComponent<Tutorials>().mgButton.SetActive(true);
                    tutorialManager.GetComponent<Tutorials>().mGlassTutorial2 = false;
                    tutorialManager.GetComponent<Tutorials>().mGlassTutorial3 = true;
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (tutorialManager.GetComponent<Tutorials>().nbTutorial && dressUpManager.GetComponent<DressUp>().activeOutfit == "Detective Outfit" && tutorialManager.GetComponent<Tutorials>().inTutorial)
        {
            manager.GetComponent<DialogueManager>().StartConversation(manager.GetComponent<DialogueManager>().grace.GetComponent<NPCDialogue>().dialogueTree[14], manager.GetComponent<DialogueManager>().grace, manager.GetComponent<DialogueManager>().graceCam4);
            tutorialManager.GetComponent<Tutorials>().nbTutorial2 = true;
            tutorialManager.GetComponent<Tutorials>().nbTutorial = false;
        }
    }
}
