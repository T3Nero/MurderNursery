using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tutorials : MonoBehaviour
{
    public string pinboardTutorial = "This is the place where it all comes together! Every aspiring detective needs a pinboard.\r\n\r\nAll the evidence you gather while investigating will be collected in a list at the bottom of your pinboard. You can scroll through this with your mouse and hover over evidence to see a short blurb that describes it. \r\n\r\nTo assign evidence to a specific classmate, left-click and drag the evidence to the pinboard. Then, you need to left-click that character’s portrait to drop a pin, and left-click the piece of evidence you’d like to assign, to thread them together. Once this has been done, you are able to present these as clues if needed at interrogations! If they’re correct, of course…\r\n\r\nFinally, if you change your mind - to unassign a piece of evidence from a character, simply right-click on it. That should send it back to the evidence section.\r\n\r\nIf you think you’re still missing some crucial clues; get out there and chat to your classmates, use some of your detective kit, or succeed in interrogations!";
    public string bribeTutorial = "Got a bribe? Perfect! Now you’re a real detective. \r\n\r\nWhile in dialogue, you can choose to give this to one of your classmates by left-clicking on the item you’d like to bribe with in the top left corner of your screen. If you’re lucky, they may decide to give you a little more information… \r\n\r\nBe careful though - some of your friends won’t like certain gifts! You can find clues as to who may like each item by using your detective kit, interrogating, and talking to everyone. \r\n\r\nAnd remember: once you’ve chosen to use a bribe, you cannot use it on another classmate - so choose wisely! \r\n";
    public string dressupTutorial = "Need a disguise? The dress-up box has you covered! \r\n\r\nSimply walk to the shelves by Juice Box and press “E” to access three extra outfits for you to try on: the gangster, the artist, and the punk. Left-click the costume you’d like to change into. Once you switch an outfit, it will replace the old one in the box, so remember to come back if you’d like to get back into your detective gear later.\r\n\r\nDressing up is crucial in order to stealthily listen into conversations or to get classmates to drop their guard. Try some costumes out on your friends in dialogue too - you never know what might end up happening!\r\n";
    public string interrogationTutorial = "It’s time to play bad cop, bad cop. Let’s get interrogating! \r\n\r\nEach of the classmates you can speak to has the option to begin an interrogation at any time once you open dialogue with them. You should be able to see how many pieces of evidence you need to successfully complete the interrogation, and how many of those you currently have - either assigned correctly, or incorrectly! \r\n\r\nOnce inside the interrogation, you must refute the statements of your friends with the correct pieces of evidence by navigating to the correct section of the pinboard and clicking on the icon you’d like to present. If you get this right, you will proceed to the next stage of the interrogation. Each interrogation has a different number of stages and required pieces of evidence, so be careful - some of your friends are trickier to get talking than others! \r\n\r\nIf you give an incorrect piece of evidence or statement, you will lose a life - one of five - and if all are lost, you’ll be kicked from the interrogation altogether. Don’t worry though: when you re-enter, you’ll start where you left off last time. Find more evidence, move it around, and keep trying!\r\n\r\nThese interrogations are crucial for uncovering potential motives and backstory, so discover as much as you can to unravel the mystery!\r\n";
    public string magnifyingGlassTutorial = "Let’s split up and look for clues! \r\n\r\nYou can select the magnifying glass from your inventory. This should freeze you in place, where you can zoom in on your surroundings by moving the mouse! If you’re standing close enough to a clue, it should stand out somehow… You can left-click to inspect it and obtain a valuable piece of evidence! Make sure you explore and search thoroughly, as some clues will only appear through the magnifying glass.\r\n\r\nAll pieces of evidence will be added to the pinboard, so remember to check there and assign it correctly for your interrogations!\r\n";
    public string fingerprintTutorial = "You’re pretty sure you saw this on CSI once… \r\n\r\nThe fingerprint kit can be accessed from your inventory. It should bring up a list of clues you’ve found around the nursery! You can click on each one individually to see whether or not you’ve found a fingerprint on that item, and if so, whether that fingerprint has been matched to a culprit in the classroom! \r\n\r\nFinding the fingerprint is pretty easy. Just rotate the item until you see something that looks suspicious. Matching the fingerprint is a little more tricky… look carefully amongst the other fingerprints to see which one is the closest match! Once you’ve figured that out, you should be able to get a little more insight into the story behind that clue.\r\n\r\nOnce both these steps are completed, the evidence should be added to your pinboard, so remember to check back there to assign it correctly!\r\n\r\n";
    public string ldTutorial = "Did you hear something? Must’ve been the wind…\r\n\r\nWhy bother waiting around for your classmates to give you the information you need? If you come across certain hiding spots within the room, you can hide and listen in on private conversations to obtain more evidence! Be careful though - you’ll need to be wearing a disguise in order to access these locations, so make sure you visit the dress-up box first.\r\n\r\nTo revisit overheard conversations, simply visit the listening device through your inventory, where you can scroll through a transcript and make some deductions. Remember the evidence you discover will also be added to the pinboard, so remember to visit and assign it to your chosen classmate!\r\n";
    public string popQuiz = "Reading is fundamental! Are you keeping up with your education?\r\n\r\nEvery so often once you’ve collected certain amounts and pieces of evidence, you may get a surprise pop quiz! Questions will appear on your screen and you must select what you believe to be the correct answers to continue. At the end of your quiz, you’ll be shown how many you got wrong or right - but not which ones! That would be cheating, right? \r\n\r\nStay tuned for the credits to find out whether you’re a straight A student!\r\n";
   
    public string inventoryTutorial = "This is your trusty inventory, where you will find all manner of crime solving tools. You can get a preview of what each item does by hovering over it with the mouse.You can then select any item in the inventory by left-clicking.";
    public string notebookTutorial = "What detective would be complete without a notebook? This is where you will find helpful information such as tutorials, interrogation summaries, and listening conversation recaps.Browse at your pleasure!";

    public GameObject tutorialPanel;
    public GameObject tutorialText;
    public bool firstLD = true;

    [Header("New Tutorial")]
    public GameObject dialogueManager;
    public GameObject tutorialGrace;
    public GameObject deadGrace;
    public GameObject blanket;
    public Image blackFade;
    private float currentCountdownValue = 2;
    public bool inTutorial;
    public GameObject inventoryManager;

    [Header("Pinboard Tutorial")]
    public GameObject pbTextObject;
    public GameObject overPBText;
    private string pbText1 = "Now what you gotta do is scroll through the evidence in the bar at the bottom of your screen - there’s just one piece for now - and click and drag it onto your pinboard! Remember, if you forget what the evidence is, you can click on it to read a short description.";
    public bool inPBTutorial1 = false;
    public bool inPBTutorial2 = false;
    public string pbText2 = "Next, click a classmate to begin a thread, and then choose a piece of evidence to join that thread to! This is reeeeally important, because it means you can keep track of who’s who and what’s what in the nursery! ";
    public bool inPBTutorial3 = false;
    public string pbText3 = "It also means you can use that evidence to point out when your friends are lying, and argue your way to the truth - so long as you assign it to the right person! If you think you’ve connected a thread incorrectly, you can click on the rubber next to a clue on the pinboard to send it back to the evidence box.";
    public string pbText4 = "So, keep an eye out for whenever you get a new piece of evidence, okay? You should see a pop-up on your screen when you get one! Plus, getting to the pinboard is as easy as pressing “I” for inventory. Now let's thread that inventory one more time as practice!";
    public bool inPBTutorial4 = false;
    public GameObject pbTutorialButton;
    public string pbText5 = "Nice work, you're clearly more seasoned than you look detective!";
    public bool inPButorial5 = false;
    public GameObject pinboardCloseButton;
    public bool inPBTutorial6 = false;

    [Header("Interrogation Tutorial")]
    public GameObject isTextObject;
    public GameObject isOverText;
    public bool inISTutorial1 = true;
    public string isText1 = "Now, this is where you get to find out a little about the person you’re interrogating, and whether you have all the evidence you need to get your classmate to spill the beans! That’s what reveals some important info you can’t get anywhere else!";
    public string isText2 = "See that tick? That means you found the right evidence, and it’s assigned correctly! If you haven't find the evidence yet, it would be blank! ";
    public bool inISTutorial2 = false;
    public string isText3 = "I recommend checking back here every so often to see whether you’re on the right track. You can enter this screen anytime when you’re talking to your classmates! Remember though, different people will require different evidence! Everybody has their own side to the story, right? ";
    public bool inISTutorial3 = false;
    public string isText4 = "Okay, go ahead and begin the interrogation now!";
    public bool inISTutorial4 = false;
    public bool inIPBTutorial = false;
    public string ipbText1 = "Oh, you think you caught me in a lie, huh? Well, prove it! Click on the evidence you think contradicts my statement! ";

    [Header("Dress Up Tutorial")]
    public bool inDUTutorial = false;
    public bool inDUTutorial2 = false;

    [Header("Listening Device Tutorial")]
    public bool inLDTutorial = false;
    public GameObject ld1;
    public GameObject ld2;
    public GameObject ld3;
    public GameObject tutorialLD;


    [Header("Notebook Tutorial")]
    public bool nbTutorial = false;
    public bool nbTutorial2 = false;
    public bool nbTutorial3 = false;
    public bool nbTutorial4 = false;
    public string nbText1 = "This is where you can look back at conversations you've overheard, get help with figuring out how to investigate, and read over information you've obtained from interrogations!\r\n";
    public string nbText2 = "You'll get a notification whenever your notebook updates, so check back here whenever you need to from the inventory!";
    public GameObject nbButton;

    [Header("Magnifying Glass Tutorial")]
    public GameObject mgTextObject;
    public GameObject mgText;
    public GameObject mgButton;
    public string mgText1 = "While you're using the magnifying glass, you can see things you can't usually - as long as they're pretty close to you! Just try and look around a little and see if anything sticks out a little, and once you do, interact with it! There should be something near my feet…\r\n";
    public string mgText2 = "Perfect! Now, you can rotate the object around and look for a fingerprint. That's a key part of finding out if there's more to a clue!\r\n";
    public string mgText3 = "Some clues have a story all on their own without a fingerprint, so don't worry, if you don't find one. Give it a try now, click and drag in the direction you wish to rotate the object. Click on a fingerprint if you spot it!";
    public string mgText4 = "Nice, you found a fingerprint!. Now, you gotta find out who it belongs to before you can use it on your pinboard...";
    public string mgText5 = "Now, we have a list of all the fingerprints at the nursery, and who they belong to!. Your job is to click the one you think matches the one you found, to gain some juicy evidence!";
    public bool mGlassTutorial1 = false;
    public bool mGlassTutorial2 = false;
    public bool mGlassTutorial3;
    public bool mGlassTutorial4;
    public bool mGlassTutorial5;

    [Header("Cameras")]
    public Camera graceCam1;
    public Camera graceCam2;
    public Camera introCam;
    public Camera graceNotebookCam;

    public GameObject player;
    public GameObject togglePBMessageButton;
    public GameObject endPBButton;
    public bool lockMovement = false;
    public bool lockMovementMainGame = false;
    public GameObject toggleButtonText;
    public bool tutorialChosen = false;
    

    [SerializeField]
    private Vector3 drewStartPos = new Vector3(29.242000579833986f, 0.004000000189989805f, 4.067999839782715f);
    [SerializeField]
    GameObject graceNotebook;
    public GameObject tutorialJuiceBox;
    public GameObject notebookExitSlip;

    [Header("Found Items")]
    public GameObject fi1;
    public GameObject fi2;
    public GameObject fi3;
    public GameObject fi4;
    public GameObject fi5;
    public GameObject fi6;

    // Start is called before the first frame update
    void Start()
    {
        inTutorial = true;
        inISTutorial1 = true;
        //dialogueManager = GameObject.FindGameObjectWithTag("Manager");
        dialogueManager.GetComponent<DialogueManager>().StartConversation(tutorialGrace.GetComponent<NPCDialogue>().dialogueTree[0], tutorialGrace, graceCam1);
        tutorialGrace.GetComponent<NPCDialogue>().inConversation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!inTutorial && tutorialGrace.activeInHierarchy)
        {
            tutorialGrace.SetActive(false);
        }
        
      //  if(ld2.activeInHierarchy && inLDTutorial)
       // {
      //      ld2.SetActive(false);
     //   }
    }

    public void ActivateTutorial(string message)
    {
        tutorialPanel.SetActive(true);
        tutorialText.GetComponent<TextMeshProUGUI>().text = message;
    }

    public void DeactivateTutorial()
    {
        tutorialPanel.SetActive(false);
        Time.timeScale = 1.0f;
        inTutorial = false; 
        
    }

    public void PinboardTutorial()
    {
        togglePBMessageButton.SetActive(true);
        inPBTutorial1 = true;
        inventoryManager.GetComponent<ToggleUIVisibility>().TogglePinboard();
        pbTextObject.SetActive(true);
        overPBText.GetComponent<TextMeshProUGUI>().text = pbText1;
    }

    public void EndTutorial()
    {
        pinboardCloseButton.SetActive(true);
        lockMovement = true;
        mGlassTutorial5 = false;
        if (!tutorialChosen)
        {
            StartCoroutine(BlackTransition(graceCam1.gameObject, introCam.gameObject));
            StartCoroutine(WaitForSeconds());
        }
         if(tutorialChosen)
        {
            print("Transitioning to Main Game");
            graceNotebookCam.gameObject.SetActive(false);
            introCam.gameObject.SetActive(true);

            
            introCam.GetComponent<IntroCutscene>().inIntro = true;
            inTutorial = false;
            tutorialGrace.SetActive(false);
            graceNotebook.SetActive(false);
            deadGrace.SetActive(true);
            blanket.SetActive(true);

            dialogueManager.GetComponent<IntroCutscene>().inIntro = true;
            lockMovement = false;

            //   StartCoroutine(BlackTransition(graceNotebookCam.gameObject, introCam.gameObject));
            //   StartCoroutine(WaitForSeconds());
        }
       // player.GetComponent<Animator>().StopPlayback();
        player.transform.position = drewStartPos;
        player.transform.rotation = new Quaternion(0, 0, 0, 0);
      //  player.GetComponent<Animator>().StartPlayback();

        ld1.SetActive(true);
        ld2.SetActive(true);
        ld3.SetActive(true);
        tutorialLD.SetActive(false);
        tutorialJuiceBox.SetActive(false);
        notebookExitSlip.SetActive(true);
        fi1.SetActive(true);
        fi2.SetActive(true);
        fi3.SetActive(true);
        fi4.SetActive(true);
        fi5.SetActive(true);
        fi6.SetActive(true);
        

    }

    public IEnumerator BlackTransition(GameObject currentCam, GameObject desiredCam, bool transitionToBlack = true, int timeToFade = 1)
    {
        
        blackFade.gameObject.SetActive(true);
        Color screenColour = blackFade.color;
        float fadeProgress;
        if (transitionToBlack)
        {
            while (blackFade.color.a < 1)
            {
                fadeProgress = screenColour.a + (timeToFade * Time.deltaTime);
                screenColour = new Color(screenColour.r, screenColour.g, screenColour.b, fadeProgress);
                blackFade.color = screenColour;
                if (fadeProgress > 0.99f)
                {

                    ChangeCam(currentCam, desiredCam);
                    graceNotebookCam.gameObject.SetActive(false);
                    introCam.GetComponent<IntroCutscene>().inIntro = true;
                    inTutorial = false;
                    tutorialGrace.SetActive(false);
                    graceNotebook.SetActive(false);
                    deadGrace.SetActive(true);
                    blanket.SetActive(true);
                    


                }

                yield return null;
            }
        }
        else
        {
            while (blackFade.color.a > 0)
            {
                fadeProgress = screenColour.a - (timeToFade * Time.deltaTime);
                screenColour = new Color(screenColour.r, screenColour.g, screenColour.b, fadeProgress);
                blackFade.color = screenColour;
                if (fadeProgress < 0.01f)
                {

                    blackFade.gameObject.SetActive(false);
                    dialogueManager.GetComponent<IntroCutscene>().inIntro = true;
                    lockMovement = false;

                    yield return null;

                }
                yield return null;
            }
        }
    }

    private void ChangeCam(GameObject currentCam, GameObject desiredCam)
    {
        currentCam.SetActive(false);
        desiredCam.SetActive(true);
    }

    public IEnumerator WaitForSeconds(float countdownValue = 2) //Waits for a specified period of time 
    {
        currentCountdownValue = countdownValue;
        while (currentCountdownValue > 0)
        {
            yield return new WaitForSeconds(1);
            currentCountdownValue--;
        }
        StartCoroutine(BlackTransition(introCam.gameObject, graceCam1.gameObject, false));
    }

    public void StartSecondGraceConvo()
    {
        inventoryManager.GetComponent<ToggleUIVisibility>().TogglePinboard();
        pbTextObject.SetActive(false);
        dialogueManager.GetComponent<DialogueManager>().StartConversation(tutorialGrace.GetComponent<NPCDialogue>().dialogueTree[4], tutorialGrace, graceCam1);
        togglePBMessageButton.SetActive(false);
        endPBButton.SetActive(false);
    }

    public void NavigateISText()
    {
        if(inISTutorial3)
        {
            isOverText.GetComponent<TextMeshProUGUI>().text = isText4;
            inISTutorial4 = true;
            pbTutorialButton.SetActive(false);
        }
        if(inISTutorial2)
        {
            isOverText.GetComponent<TextMeshProUGUI>().text = isText3;
            inISTutorial2 = false;
            inISTutorial3 = true;
        }
        if(inISTutorial1)
        {
            isOverText.GetComponent<TextMeshProUGUI>().text = isText2;
            inISTutorial1 = false;
            inISTutorial2 = true;
        }
    }

    public void NavigateNBText()
    {
        if(nbTutorial4)
        {
            inventoryManager.GetComponent<ToggleUIVisibility>().ToggleNotebook();
            dialogueManager.GetComponent<DialogueManager>().npcStatement3.SetActive(true);
            dialogueManager.GetComponent<DialogueManager>().playerResponse2.SetActive(true);
            dialogueManager.GetComponent<DialogueManager>().npcStatement3.GetComponent<TextMeshProUGUI>().text = dialogueManager.GetComponent<DialogueManager>().activeNode.speech;
            dialogueManager.GetComponent<DialogueManager>().playerResponse2.GetComponent<TextMeshProUGUI>().text = "I for inventory, easy!";
            dialogueManager.GetComponent<DialogueManager>().npcStatement.GetComponent<TextMeshProUGUI>().text = tutorialGrace.GetComponent<NPCDialogue>().dialogueTree[15].speech;
            dialogueManager.GetComponent<DialogueManager>().LoadNodeInfo(tutorialGrace.GetComponent<NPCDialogue>().dialogueTree[15]);
            nbTutorial4 = false;
            mGlassTutorial1 = true;
            isTextObject.SetActive(false);
        }
        if(nbTutorial3)
        {
            isOverText.GetComponent<TextMeshProUGUI>().text = nbText2;
            nbTutorial3 = false;
            nbTutorial4 = true;
        }
    }

    public void NavigateMGText()
    {
        if(mGlassTutorial2)
        {
            mgText.GetComponent<TextMeshProUGUI>().text = mgText2;
        }

        if(mGlassTutorial3)
        {
            mgText.GetComponent<TextMeshProUGUI>().text = mgText3;
        }

        if(mGlassTutorial4)
        {
            mgText.GetComponent<TextMeshProUGUI>().text = mgText4;
            mGlassTutorial4 = false;
            mGlassTutorial5 = true;
        }

        if(mGlassTutorial5)
        {
            mgText.GetComponent<TextMeshProUGUI>().text = mgText5;
            
        }
    }

    public void TogglePBtext()
    {
        pbTextObject.SetActive(!pbTextObject.activeSelf);
        if(pbTextObject.activeInHierarchy)
        {
            toggleButtonText.GetComponent<TextMeshProUGUI>().text = "Hide Text";
        }
        if(!pbTextObject.activeInHierarchy)
        {
            toggleButtonText.GetComponent<TextMeshProUGUI>().text = "Show Text";
        }
    }

    public void EndPBTutorial()
    {
        if(!inPButorial5)
        {
            StartSecondGraceConvo();
            inPBTutorial6 = false;
        }
        if (inPButorial5)
        {
            overPBText.GetComponent<TextMeshProUGUI>().text = pbText5;
            togglePBMessageButton.SetActive(false);
            pbTextObject.SetActive(true);
            endPBButton.SetActive(true);
            inPButorial5 = false;
            inPBTutorial6 = true; 
        }
    }
}
