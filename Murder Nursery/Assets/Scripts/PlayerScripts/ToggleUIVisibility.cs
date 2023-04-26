using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

// attached to inventory manager object (called any time we want to update ui visibility)
public class ToggleUIVisibility : MonoBehaviour
{
    [SerializeField]
    public GameObject inventoryUI;

    [SerializeField]
   public GameObject jotterUI;

    [SerializeField]
   public GameObject notebook;

  

    [SerializeField]
    public GameObject pinboardUI;

    [SerializeField]
    public GameObject evidenceJotter;

    [HideInInspector]
    public bool inventoryOpen = false;

    [HideInInspector]
    public bool pinboardOpen = false;

    [HideInInspector]
    public bool jotterOpen = false;

    [HideInInspector]
    public bool notebookOpen = false;

    public AudioSource playerAudioSource;
    public AudioClip openInventorySound;
    public AudioClip openPinBoardSound;
    public AudioClip openJotterSound;
    public GameObject blur;
    public GameObject interrogationManager;
    public GameObject interrogationUI;

    public bool firstPinboard = true;
    public bool firstNotebook = true;
    public bool firstInventory = true;

    public GameObject tutorialManager;
    public bool scarletEddieComplete = false;
    public bool chaseEddieComplete = false;
    public bool juiceboxChaseComplete = false;

    public GameObject evidenceSlider;

    public GameObject closePB;
    public void Start()
    {
        tutorialManager = GameObject.FindGameObjectWithTag("Tutorial Manager");
    }
    public void Update()
    {
        if (inventoryOpen)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if(pinboardOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // booleans used to determines if UI elements are visible - if so, hide them & vise versa
    public void ToggleInventory()
    {
        
        if(inventoryOpen = !inventoryOpen)
        {
        //    if(firstInventory)
         //   {
         //       tutorialManager.GetComponent<Tutorials>().ActivateTutorial(tutorialManager.GetComponent<Tutorials>().inventoryTutorial);
         //       firstInventory = false;
         //   }
                
            playerAudioSource.PlayOneShot(openInventorySound, 0.2f) ;
            inventoryUI.SetActive(true);
            inventoryOpen = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            inventoryUI.SetActive(false);
            inventoryOpen = false;
            Cursor.visible=false;            
        }

        if(pinboardOpen)
        {
            pinboardUI.SetActive(false);
          //  evidenceJotter.SetActive(false);
            pinboardOpen = false;
        }

        if(jotterOpen)
        {
            jotterUI.SetActive(false);
            jotterOpen = false;
        }
    }

    public void TogglePinboard() // Called when pinboard item clicked from inventory or when X clicked on pinboard
    {
        if (!interrogationManager.GetComponent<Interrogation>().inInterrogation)
        {
            if (pinboardOpen = !pinboardOpen)
            {
               // if (tutorialManager.GetComponent<Tutorials>().inTutorial && !closePB.activeInHierarchy)
           //     {
              //      closePB.SetActive(true);
               // }
                //   if(firstPinboard)
                //  {
                //       tutorialManager.GetComponent<Tutorials>().ActivateTutorial(tutorialManager.GetComponent<Tutorials>().pinboardTutorial);
                //       firstPinboard = false;
                //   }
                playerAudioSource.PlayOneShot(openPinBoardSound, 0.5f);
                pinboardUI.SetActive(true);
                blur.SetActive(true);
                pinboardOpen = true;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                pinboardUI.SetActive(false);
                blur.SetActive(false);
                pinboardOpen = false;
                Cursor.visible = false;



            }

            if (inventoryOpen)
            {
                inventoryUI.SetActive(false);
                inventoryOpen = false;
            }
        }
        if(interrogationManager.GetComponent<Interrogation>().inInterrogation)
        {
            pinboardUI.SetActive(false);
            pinboardOpen = false;
            interrogationUI.SetActive(true);
            interrogationManager.GetComponent<Interrogation>().ReloadText();
        }
        if(evidenceSlider.GetComponent<EvidenceSlider>().evidenceOpen)
        {
            evidenceSlider.GetComponent<EvidenceSlider>().ToggleEvidencePanel();
        }
    }

    public void ToggleJotter() // Called when jotter item clicked from inventory or when X clicked on jotter
    {
        if(jotterOpen = !jotterOpen)
        {
            playerAudioSource.PlayOneShot(openJotterSound, 0.5f);
            jotterUI.SetActive(true);
            blur.SetActive(true);
            jotterOpen = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            jotterUI.SetActive(false);
            blur.SetActive(false);
            jotterOpen = false;
            Cursor.visible = false;            
        }

        if(inventoryOpen)
        {
            inventoryUI.SetActive(false);
            inventoryOpen = false;
        }
    }

    public void ToggleNotebook()
    {
        if(!notebookOpen)
        {
           // if(firstNotebook)
         //   {
         //       tutorialManager.GetComponent<Tutorials>().ActivateTutorial(tutorialManager.GetComponent<Tutorials>().notebookTutorial);
         //       firstNotebook = false;
         //   }
            notebook.GetComponent<Notebook>().CheckConversationProgress();
            notebookOpen = true;
            notebook.SetActive(true);
            blur.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if(interrogationManager.GetComponent<Interrogation>().noneCompleted)
            {
                notebook.GetComponent<Notebook>().noIntMessage.SetActive(true);
            }
            else
            {
                notebook.GetComponent<Notebook>().noIntMessage.SetActive(false);
            }
            if(tutorialManager.GetComponent<Tutorials>().nbTutorial2)
            {
                tutorialManager.GetComponent<Tutorials>().isTextObject.SetActive(true);
                tutorialManager.GetComponent<Tutorials>().isOverText.GetComponent<TextMeshProUGUI>().text = tutorialManager.GetComponent<Tutorials>().nbText1;
                tutorialManager.GetComponent<Tutorials>().nbTutorial2 = false;
                tutorialManager.GetComponent<Tutorials>().nbTutorial3 = true;
                tutorialManager.GetComponent<Tutorials>().nbButton.SetActive(true);
                tutorialManager.GetComponent<Tutorials>().pbTutorialButton.SetActive(false);
            }
        }
        else
        {
            notebook.SetActive(false);
            blur.SetActive(false);
            Cursor.visible = false;
            notebookOpen = false;
        }
        if(inventoryOpen)
        {
            inventoryUI.SetActive(false);
            inventoryOpen = false;
        }
    }
}
