using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notebook : MonoBehaviour
{
    public GameObject noIntMessage;
    private GameObject interrogationManager;

    public GameObject interrogationPage;
    public GameObject tutorialsPage;
    public GameObject ldConversationsPage;


    public bool scarletEddieComplete = false;
    public bool chaseEddieComplete = false;
    public bool juiceboxChaseComplete = false;

    public GameObject chaseEddiePlaceholder;
    public GameObject chaseEddie;

    public GameObject scarletEddiePlaceholder;
    public GameObject scarletEddie;

    public GameObject juiceboxChasePlaceholder;
    public GameObject juiceboxChase;

    public bool tutorialComplete = false;
    public GameObject tutorialManager;
    public GameObject tutorialTranscript;
    private void Start()
    {
        interrogationManager = GameObject.FindGameObjectWithTag("InterrogationManager");
        tutorialManager = GameObject.FindGameObjectWithTag("Tutorial Manager");
    }
    // Update is called once per frame
    void Update()
    {
           
    }

    public void SwitchToTutorials()
    {
        tutorialsPage.SetActive(true);
        interrogationPage.SetActive(false);
        ldConversationsPage.SetActive(false);
    }

    public void SwitchToInterrogations()
    {
        interrogationPage.SetActive(true);
        tutorialsPage.SetActive(false);
        ldConversationsPage.SetActive(false);
    }
    
    public void SwitchToConversations()
    {
        ldConversationsPage.SetActive(true);
        tutorialsPage.SetActive(false);
        interrogationPage.SetActive(false);
    }

    public void ReturnToInterrogation(GameObject currentPage)
    {
        currentPage.SetActive(false);
        interrogationPage.SetActive(true);
    }

    public void SwitchToSummary(GameObject summaryPage)
    {
        summaryPage.SetActive(true);
        interrogationPage.SetActive(false);
    }

    public void SwitchToTutorial(GameObject tutorial)
    {
        tutorialsPage.SetActive(false);
        tutorial.SetActive(true);
    }

    public void ReturnToTutorials(GameObject currentPage)
    {
        currentPage.SetActive(false);
        tutorialsPage.SetActive(true);
    }

    public void CheckConversationProgress()
    {
        if(chaseEddieComplete)
        {
            chaseEddiePlaceholder.SetActive(false);
            chaseEddie.SetActive(true);
        }
        if(scarletEddieComplete)
        {
            scarletEddiePlaceholder.SetActive(false);
            scarletEddie.SetActive(true);
        }
        if(juiceboxChaseComplete)
        {
            juiceboxChasePlaceholder.SetActive(false);
            juiceboxChase.SetActive(true);  
        }
       // if(tutorialManager.GetComponent<Tutorials>().inTutorial)
       // {
        //    juiceboxChasePlaceholder.SetActive(false);
          //  chaseEddiePlaceholder.SetActive(false);
            //scarletEddiePlaceholder.SetActive(false);
            //if(tutorialComplete)
           // {
             //   tutorialTranscript.SetActive(true);
            //}
       // }
    }

    public void SwitchToConversation(GameObject conversation)
    {
        conversation.SetActive(true);
        ldConversationsPage.SetActive(false);
    }

    public void ReturnToConversation(GameObject currentPage)
    {
        currentPage.SetActive(false);
        ldConversationsPage.SetActive(true);
    }

}
