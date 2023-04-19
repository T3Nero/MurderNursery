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
        
        

        if(tutorialManager.GetComponent<Tutorials>().nbTutorial2 && Input.GetKeyUp(KeyCode.I))
        {
            inventoryManager.GetComponent<ToggleUIVisibility>().ToggleNotebook();
            tutorialManager.GetComponent<Tutorials>().pbTextObject.SetActive(true);
            tutorialManager.GetComponent<Tutorials>().overPBText.GetComponent<TextMeshProUGUI>().text = tutorialManager.GetComponent<Tutorials>().nbText1;
            tutorialManager.GetComponent<Tutorials>().nbTutorial2 = false;
            tutorialManager.GetComponent<Tutorials>().nbTutorial3 = true;
            tutorialManager.GetComponent<Tutorials>().pbTutorialButton.SetActive(false);
            tutorialManager.GetComponent<Tutorials>().nbButton.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tutorialManager.GetComponent<Tutorials>().nbTutorial && dressUpManager.GetComponent<DressUp>().activeOutfit == "Detective Outfit")
        {
            manager.GetComponent<DialogueManager>().StartConversation(manager.GetComponent<DialogueManager>().grace.GetComponent<NPCDialogue>().dialogueTree[14], manager.GetComponent<DialogueManager>().grace, manager.GetComponent<DialogueManager>().graceCam4);
            tutorialManager.GetComponent<Tutorials>().nbTutorial2 = true;
        }
    }
}
