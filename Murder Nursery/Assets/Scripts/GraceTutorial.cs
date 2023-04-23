using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraceTutorial : MonoBehaviour
{
    public GameObject tutorialManager;
    public GameObject dialogueManager;
    public GameObject graceTutorial;
    public Camera grace2ndCam;
    // Start is called before the first frame update
    void Start()
    {
        tutorialManager = GameObject.FindGameObjectWithTag("Tutorial Manager");
        dialogueManager = GameObject.FindGameObjectWithTag("Manager");
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.activeInHierarchy && tutorialManager.GetComponent<Tutorials>().nbTutorial)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(tutorialManager.GetComponent<Tutorials>().inDUTutorial && other.CompareTag("Player"))
        {
            dialogueManager.GetComponent<DialogueManager>().StartConversation(graceTutorial.GetComponent<NPCDialogue>().dialogueTree[9], graceTutorial, grace2ndCam);
            tutorialManager.GetComponent<Tutorials>().inDUTutorial2 = true;
            tutorialManager.GetComponent<Tutorials>().inDUTutorial = false;
            
        }
    }
}
