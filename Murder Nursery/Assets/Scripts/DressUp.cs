using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DressUp : MonoBehaviour
{
    private bool interactable = false;//Signals that the player is within interaction range
    public string activeOutfit = null; //Stores the outfit currently worn by the player
    [HideInInspector]
    public bool inDressUp = false; //Signals that the player is in the dressup menu

    [Header("UI Elements")]
    public GameObject interactableText; //Text that is displayed when the player is within range of the dress up box
    public GameObject dressUpMenu; //UI element containing all of the dress up menu components 
    public GameObject artistButton; //Button used for changing outfits
    public GameObject punkButton; //''
    public GameObject gangsterButton; //''
    public GameObject detectiveButton;//''
    private GameObject inactiveButton;//Stores button which contains the outfit the player is currently wearing

    //Position vectors for UI management
    private Vector3 firstButton;
    private Vector3 secondButton;
    private Vector3 thirdButton;

    [Header("Audio")]
    public AudioSource playerAudio; //Audio heard by the player
    public AudioClip openBoxSound; //Sound played when entering dress up menu
    public AudioClip changeOutfitSound; //Sound played when changing an outfit

    [Header("Character Outfits")]
    public GameObject gangsterOutfit;
    public GameObject detectiveOutfit;
    public GameObject artistOutfit;
    public GameObject currentOutfit;
    public GameObject punkOutfit;

    private Transform playerPosition;
    private Vector3 yOffset = new Vector3(0f, 0.3876f, 0f);
    public GameObject playerCam;

    private bool firstDressUP = true;
    public GameObject tutorialManager;
    public GameObject magGlass;

    public GameObject inventoryManager;
    public Item magnifyingGlass;

    public bool outfitChanging = false;

    [Header("Pop Ups")]
    public GameObject popUpManager;
    public Image artistEquipped;
    public GameObject artistPUText;
    public Image punkEquipped;
    public GameObject punkText;
    public Image detectiveEquipped;
    public GameObject detectiveText;
    public Image gangsterEquipped;
    public GameObject gangsterText;

    public GameObject manager;
    public GameObject graceTutorial;

    public GameObject mainCam;
    public GameObject dressUpCam;
    public GameObject dresser2;
    public GameObject graceDressUp;

    
    // Start is called before the first frame update
    void Start()
    {
        activeOutfit = "Detective Outfit"; //Sets the active outfit to the detective outfit at the start of the game 
        firstButton = new Vector3(431.3134765625f, 762.06396484375f, 0.0f);
        secondButton = new Vector3(968.8134765625f, 762.06396484375f, 0.0f);
        thirdButton = new Vector3(1480.3134765625f, 757.7489624023438f, 0.0f);
        inactiveButton = detectiveButton; //Sets the inactive button as the detective button
        currentOutfit = detectiveOutfit;
        popUpManager = GameObject.FindGameObjectWithTag("PUManager");
        manager = GameObject.FindGameObjectWithTag("Manager");
        tutorialManager = GameObject.FindGameObjectWithTag("Tutorial Manager");
    }

    


    // Update is called once per frame
    void Update()
    {
        if (interactable && Input.GetKeyDown(KeyCode.E) && !magGlass.GetComponent<MagnifyingGlass>().usingMagnifyingGlass &&!inventoryManager.GetComponent<ToggleUIVisibility>().inventoryOpen) //Allows the player to open the dress up menu 
        {
            //  if(firstDressUP)
            // {
            //      tutorialManager.GetComponent<Tutorials>().ActivateTutorial(tutorialManager.GetComponent<Tutorials>().dressupTutorial);
            //       firstDressUP = false;
            //   }
            EnterDressUp();
        }

        if(inDressUp && Input.GetKeyDown(KeyCode.Escape)) //Allows the player to leave the dress up menu 
        {
            ExitDressUp();
            playerAudio.PlayOneShot(openBoxSound, 0.5f); //Plays the toggle dress up menu sound 
            Cursor.visible = false; //CURSOR STUFF- UPDATE
        }

        if(interactableText.activeInHierarchy && interactable && inventoryManager.GetComponent<ToggleUIVisibility>().inventoryOpen)
        {
            interactableText.SetActive(false);
        }
        if(!interactableText.activeInHierarchy && interactable && !inventoryManager.GetComponent<ToggleUIVisibility>().inventoryOpen)
        {
            interactableText.SetActive(true);
        }

    }

    public bool CheckOutfit(string requiredOutfit) //Used to check if the player is wearing the relevant outfit 
    {
        if(requiredOutfit == activeOutfit)
        {
            return true;
        }
        if(requiredOutfit != activeOutfit)
        {
            return false;
        }
        return false;
    }

    public void ChangeToArtistOutfit() //Changes the player's current outfit to the artist outfit
    {
        outfitChanging = true;
       // inventoryManager.GetComponent<InventoryManager>().RemoveItem(magnifyingGlass);
        activeOutfit = "Artist Outfit";
        print("You are now wearing the " + activeOutfit);
        UpdateOutfitChoices(artistButton);
        ExitDressUp();
        playerAudio.PlayOneShot(changeOutfitSound, 0.5f); //Plays the outfit change sound
        //playerPosition = currentOutfit.transform;
        currentOutfit.SetActive(false);        
        currentOutfit = artistOutfit;
        artistOutfit.SetActive(true);
        popUpManager.GetComponent<PopUpManager>().FadeImage(artistEquipped, artistPUText);


        //artistOutfit.transform.position = playerPosition.position;
        //playerCam.GetComponent<CinemachineVirtualCamera>().Follow = artistOutfit.transform;
        //playerCam.GetComponent<CinemachineVirtualCamera>().LookAt = artistOutfit.transform;

    }

    public void ChangeToPunkOutfit() //Changes the player's current outfit to the wizard outfit 
    {
        outfitChanging=true;
        activeOutfit = "Punk Outfit";
        print("You are now wearing the " + activeOutfit);
        UpdateOutfitChoices(punkButton);
        ExitDressUp();
        playerAudio.PlayOneShot(changeOutfitSound, 0.5f); //Plays the outfit change sound
        //playerPosition = currentOutfit.transform;
        currentOutfit.SetActive(false);
        currentOutfit = punkOutfit;
        punkOutfit.SetActive(true);
        popUpManager.GetComponent<PopUpManager>().FadeImage(punkEquipped, punkText);
        //punkOutfit.transform.position = playerPosition.position;
        //playerCam.GetComponent<CinemachineVirtualCamera>().Follow = punkOutfit.transform;
    }

    public void ChangeToGangsterOutfit() //Changes the player's current outfit to the gangster outfit
    {
        outfitChanging = true;
        activeOutfit = "Gangster Outfit";
        print("You are now wearing the " + activeOutfit);
        UpdateOutfitChoices(gangsterButton);
        ExitDressUp();
        playerAudio.PlayOneShot(changeOutfitSound, 0.5f); //Plays the outfit change sound
        //playerPosition = currentOutfit.transform;
        currentOutfit.SetActive(false);
        currentOutfit = gangsterOutfit;
        gangsterOutfit.SetActive(true);
        popUpManager.GetComponent<PopUpManager>().FadeImage(gangsterEquipped, gangsterText);
        //gangsterOutfit.transform.position = playerPosition.position;
    }

    public void ChangeToDetectiveOutfit() //Changes the player's current outfit to the detective outfit 
    {
        outfitChanging = true;
        inventoryManager.GetComponent<InventoryManager>().AddItem(magnifyingGlass);
        activeOutfit = "Detective Outfit";
        print("You are now wearing the " + activeOutfit);
        UpdateOutfitChoices(detectiveButton);
        ExitDressUp();
        playerAudio.PlayOneShot(changeOutfitSound, 0.5f); //Plays the outfit change sound
        //playerPosition = currentOutfit.transform;
        currentOutfit.SetActive(false);
        currentOutfit = detectiveOutfit;
        detectiveOutfit.SetActive(true);
        popUpManager.GetComponent<PopUpManager>().FadeImage(detectiveEquipped, detectiveText);
        //detectiveOutfit.transform.position = playerPosition.position;
    }

    private void UpdateOutfitChoices(GameObject buttonClicked) //Updates the dress up menu UI to display the current available outfits
    {
        if(buttonClicked.transform.position == firstButton)
        {
            buttonClicked.SetActive(false);
            inactiveButton.transform.position = firstButton;
            inactiveButton.SetActive(true);
            inactiveButton = buttonClicked;
        }
        if(buttonClicked.transform.position == secondButton)
        {
            buttonClicked.SetActive(false);
            inactiveButton.transform.position = secondButton;
            inactiveButton.SetActive(true);
            inactiveButton = buttonClicked;
        }
        if(buttonClicked.transform.position == thirdButton)
        {
            buttonClicked.SetActive(false);
            inactiveButton.transform.position = thirdButton;
            inactiveButton.SetActive(true);
            inactiveButton = buttonClicked;
        }
    }
    public void OnTriggerEnter(Collider other) //Used to detect if the player is within range of the dress up box
    {
        if (other.gameObject.name == "DetectiveDrew" && !magGlass.GetComponent<MagnifyingGlass>().usingMagnifyingGlass && !tutorialManager.GetComponent<Tutorials>().inDUTutorial2 && !tutorialManager.GetComponent<Tutorials>().inDUTutorial)
        {
            interactable = true;
            interactableText.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "DetectiveDrew")
        {
            interactable = false;
            interactableText.SetActive(false);
        }
        
    }

    public void ExitDressUp() //Used to exit the dress up menu 
    {
       
        
        dressUpCam.SetActive(false);
        mainCam.SetActive(true);
        dressUpMenu.SetActive(false);
        interactableText.SetActive(true);
        inDressUp = false;
        Cursor.visible = false;
        if(tutorialManager.GetComponent<Tutorials>().inDUTutorial2)
        {
            manager.GetComponent<DialogueManager>().StartConversation(graceTutorial.GetComponent<NPCDialogue>().dialogueTree[11], graceTutorial, tutorialManager.GetComponent<Tutorials>().graceCam2);
            interactableText.SetActive(false);
        }
    }

    public void EnterDressUp()
    {
        
        
        dressUpCam.SetActive(true);
        mainCam.SetActive(false);
        inDressUp = true; //Signals that the player is in dress up 
        interactableText.SetActive(false);
        dressUpMenu.SetActive(true); //Activates the dress up menu UI
        Cursor.visible = true; //CURSOR STUFF - UPDATE
        Cursor.lockState = CursorLockMode.None;
        playerAudio.PlayOneShot(openBoxSound, 0.5f); //Plays the toggle dress up menu sound 
    }
}
