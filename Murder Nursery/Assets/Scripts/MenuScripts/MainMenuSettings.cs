using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;


public class MainMenuSettings : MonoBehaviour
{
    DialogueManager DM;
    Colorblind colourblind;
    public GameObject colourblindObject; // colourblind script attached to main camera

    public AudioSource sfxManager;
    public AudioClip selectChoice;
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;

    public Slider musicSlider;
    public Slider sfxSlider;
    private float musicVolume = 1f;
    private float sfxVolume = 1f;

    public GameObject menuObject;
    public GameObject optionsMenu;
    public GameObject graphicsMenu;
    public GameObject resMenu;
    public GameObject audioMenu;
    public GameObject colourblindMenu;
    public GameObject controlsUI;

    // update graphics quality / resolution settings
    private string quality;
    private string index;
    private string colourblindIndex;

    public bool menuOpen;

    public ToggleUIVisibility inventoryManager;
    private bool inInventory = false;
    private bool inDialogue = false;
    public PinboardManager PM;
    private bool inPinboard = false;
    public Interrogation IM;
    private bool inInterrogation = false;
    private bool inNotebook = false;
    public Tutorials TM;
    private bool inTutorial = false;

    private void Start()
    {
        DM = FindObjectOfType<DialogueManager>();

        if(GameObject.FindGameObjectWithTag("IntroCam"))
        {
            if(GameObject.FindGameObjectWithTag("IntroCam").activeInHierarchy)
            {
                colourblindObject = GameObject.FindGameObjectWithTag("IntroCam");
            }
        }
        colourblind = colourblindObject.GetComponent<Colorblind>();

        musicVolume = PlayerPrefs.GetFloat("volume");
        sfxVolume = PlayerPrefs.GetFloat("sfxVolume", sfxVolume);
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;

        quality = PlayerPrefs.GetString("quality");
        index = PlayerPrefs.GetString("res");
        colourblindIndex = PlayerPrefs.GetString("cbIndex");


        ChangeQualityLevel();
        ChangeResolution();
        ChangeColourblindMode();

    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape) && SceneManager.GetActiveScene().name == "MainScene" && !DM.GetComponent<IntroCutscene>().inIntro)
        {
            if(!DM.dialogueZone.activeInHierarchy && !PM.pinBoard.activeInHierarchy && !IM.interrogationPanel.activeInHierarchy && !inventoryManager.inventoryUI.activeInHierarchy && !inventoryManager.notebook.activeInHierarchy)
            {
                ToggleMenu();
            }
            if(DM.dialogueZone.activeInHierarchy)
            {
                inDialogue = true;
                DM.dialogueZone.SetActive(false);              
                ToggleMenu();
            }
            if(PM.pinBoard.activeInHierarchy)
            {
                inPinboard = true;
                PM.pinBoard.SetActive(false);
                ToggleMenu();
            }
            if(IM.interrogationPanel.activeInHierarchy)
            {
                inInterrogation = true;
                IM.interrogationPanel.SetActive(false);
                ToggleMenu();
            }
            if(inventoryManager.inventoryUI.activeInHierarchy)
            {
                inInventory = true;
                inventoryManager.inventoryUI.SetActive(false);
                ToggleMenu();
            }
            if(inventoryManager.notebook.activeInHierarchy)
            {
                inNotebook = true;
                inventoryManager.notebook.SetActive(false);
                ToggleMenu();
            }
            
            
        }

        if(colourblindObject.GetComponent<IntroCutscene>())
        {
            if(!colourblindObject.GetComponent<IntroCutscene>().inIntro)
            {
                colourblindObject = GameObject.FindGameObjectWithTag("MainCamera");
                colourblind = colourblindObject.GetComponent<Colorblind>();
                colourblindIndex = PlayerPrefs.GetString("cbIndex");
                ChangeColourblindMode();
            }
        }


        // save slider volume value between sessions / scenes
        PlayerPrefs.SetFloat("volume", musicVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);

        // save quality / resoution settings
        PlayerPrefs.SetString("res", index);
        PlayerPrefs.SetString("quality", quality);
        PlayerPrefs.SetString("cbIndex", colourblindIndex);

    }

    public void SetVolume(float sliderValue)
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        musicVolume = sliderValue;
    }

    public void SetSFXVolume(float sliderValue)
    {
        sfxMixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        sfxVolume = sliderValue;
    }

    // Called as an OnClick() Event
    public void SetQuality()
    {
        ChangeQualityLevel();
        sfxManager.PlayOneShot(selectChoice, 0.4f);
    }

    // Called as an OnClick() Event
    public void SetResolution()
    {
        ChangeResolution();
        sfxManager.PlayOneShot(selectChoice, 0.4f);
    }

    // switch case selected based on selected objects name
    void ChangeQualityLevel()
    {
        if(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject)
        {
            quality =  UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        }
        
        
        switch (quality)
        {
            case "Low":
                QualitySettings.SetQualityLevel(0);
                break;
            case "Medium":
                QualitySettings.SetQualityLevel(1);
                break;
                
            case "High":
                QualitySettings.SetQualityLevel(2);
                break;
                
            case "Ultra":
                QualitySettings.SetQualityLevel(3);
                break;
        }
    }

    // switch case selected based on selected objects name
    void ChangeResolution()
    {
        if(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject)
        {
            index =  UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        }

        switch (index)
        {
            case "0":
                Screen.SetResolution(1152, 648, true);
                break;
            case "1":
                Screen.SetResolution(1280, 720, true);
                break;
            case "2":
                Screen.SetResolution(1360, 768, true);
                break;
            case "3":
                Screen.SetResolution(1920, 1080, true);
                break;
            case "4":
                Screen.SetResolution(2560, 1440, true);
                break;
        }
    }

    public void ChangeColourblindMode()
    {
        if(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject)
        {
            colourblindIndex =  UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        }

        switch (colourblindIndex)
        {
            case "0":
                colourblind.Type = 0;
                break;
            case "1":
                colourblind.Type = 1;
                break;
            case "2":
                colourblind.Type = 2;
                break;
            case "3":
                colourblind.Type = 3;
                break;
        }
    }

    // Toggles menu during gameplay (Escape key / Resume Game Button)
    public void ToggleMenu()
    {
        if (menuOpen = !menuOpen)
        {
            menuObject.SetActive(true);
            menuOpen = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            menuObject.SetActive(false);
            menuOpen = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            if (inDialogue)
            {
                DM.dialogueZone.SetActive(true);
                inDialogue = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            if (inPinboard)
            {
                PM.pinBoard.SetActive(true);
                inPinboard = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            if (inInterrogation)
            {
                IM.interrogationPanel.SetActive(true);
                inInterrogation = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            if (inInventory)
            {
                inventoryManager.inventoryUI.SetActive(true);
                inInventory = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            if (inNotebook)
            {
                inventoryManager.notebook.SetActive(true);
                inNotebook = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            if (inTutorial)
            {
                TM.tutorialPanel.SetActive(true);
                inTutorial = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        if(optionsMenu.activeInHierarchy)
        {
            optionsMenu.SetActive(false);
        }

        if(graphicsMenu.activeInHierarchy)
        {
            graphicsMenu.SetActive(false);
        }

        if(resMenu.activeInHierarchy)
        {
            resMenu.SetActive(false);
        }

        if(audioMenu.activeInHierarchy)
        {
            audioMenu.SetActive(false);
        }

        if(controlsUI.activeInHierarchy)
        {
            controlsUI.SetActive(false);
        }

        if(colourblindMenu.activeInHierarchy)
        {
            colourblindMenu.SetActive(false);
        }
    }

}

