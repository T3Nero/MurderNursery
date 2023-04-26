using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Conclusion : MonoBehaviour
{
    [Header("General Variables")]
    public GameObject mainScene;
    public GameObject manager;
    public GameObject outtroCam;
    public Image blackFade;
    public bool endingReady = false;
    public bool dialogueActive;
    private int progress = 0;
    private bool eddieChosen = false;
    private bool chaseChosen = false;
    private bool scarletChosen = false;
    private bool juiceboxChosen = false;
    private bool graceChosen = false;
    

    [Header("Character objects")]
    public GameObject scarletEnd;
    public GameObject eddieEnd;
    public GameObject juiceBoxEnd;
    public GameObject drewEnd;
    public GameObject chaseEnd;
    public GameObject graceEnd;
    public GameObject scarletMain;
    public GameObject eddieMain;
    public GameObject juiceBoxMain;
    public GameObject drewMain;
    public GameObject chaseMain;
    public GameObject graceMain;

    [Header("UI Objects")]
    public GameObject tempText;
    public GameObject endingText;
    public GameObject hintMessage;
    public GameObject trueEndingText;
    public GameObject goodEndingText;
    public GameObject badEndingText;

    [Header("Cameras")]
    public GameObject eddieCam;
    public GameObject scarletCam;
    public GameObject chaseCam;
    public GameObject juiceBoxCam;
    public GameObject drewCam;
    public GameObject graceCam;
    public GameObject suspectSelectionCam;
    public GameObject goodEndingCam;
    public GameObject badEndingCam;
    public GameObject trueEndingCam;

    //Bad ending dialogue
    private string badEnd1 = "It... It... It wasn't me!";
    private string badEnd2 = "I was… wrong? No - surely not… but I swore I had it all right here! Looks like I really flunked this case! Ohhh boy, the boss won’t be happy with me now that I’ve thrown an innocent in the can! Teach… take my badge, I don’t deserve it. Put me in time-out while you’re at it, I’ve failed you all!";
    private string badEnd3 = "Well, that won’t be necessary! But why don’t you change out of your little costume so you and I can go and have a little chat about being nice to our peers?";
    private string badEnd4 = "You got it, boss! Catch you on the flip side fellas... it was real nice while it lasted.";

    //Good ending dialogue
    private string goodEnd1 = "It was me... I'm sorry class, I let you all down!";
    private string goodEnd2 = "But why?";
    private string goodEnd3 = "Love makes us do crazy things, friend. I was real selfish watchin' other folks go down for what I did.";
    private string goodEnd4 = "You did this all yourself?";
    private string goodEnd5 = "Well... who's to say? Maybe I had help, maybe I didn't. But one thing's for sure, you caught me fair and square, and I know when the cat's outta the bag. Figured it was real easy to make out like Scarlet took her jealousy a step too far and finally put the squeeze on Grace. Maybe then I'd get the chance to go talk to her a little, share a cookie, watch some cartoons - I mean, grown-up movies. Honestly though, I woulda made out like it was anybody but me by the end, detective. Ya really know how to make a guy sweat!";
    private string goodEnd6 = "And here I was thinkin' we were all friends! I think you  owe us all a biiiiig apology, bucko.";
    private string goodEnd7 = "You're right, you're right! I'd like to apologise to everyone, especially you, Scarlet. ";
    private string goodEnd8 = "You were the real victim here, but I hope we can still be friends.";
    private string goodEnd9 = "Eddie, I'm sorry for roping you into this, and betrayin' your trust. I oughta tell you how much you mean to me more often! ";
    private string goodEnd10 = " Juice Box, I still think you're real strange, but I'm sorry 'bout your arm. I'm glad you're ambodexter or whatever cause there's no better fingerpaintings in this whole building.";
    private string goodEnd11 = " Detective, I'm sorry we got off on the wrong foot, you're a real character and we're lucky to have ya in the class here with us. ";
    private string goodEnd12 = "Grace... for what it's worth, I'm sorry to you, too. I hope your mom can wash the ketchup out okay.";
    private string goodEnd13 = "Well, what do we say kids, do we forgive 'em?";
    private string goodEnd14 = "YEAH!!!";
    private string goodEnd15 = "Well well well... looks I didn't quite crack the case this time. We got a culprit, but somebody's out there getting away with murder. Wonder what woulda happened if I'd done things a little differently? Maybe it ain't time to hang up the ol' trenchcoat after all...";

    //True ending dialogue
    private string trueEnd1 = "It was me... I'm sorry class, I let you all down!";
    private string trueEnd2 = "But why?";
    private string trueEnd3 = "Love makes us do crazy things, friend. I was real selfish watchin' other folks go down for what I did. I didn't want to, I swear!";
    private string trueEnd4 = "So tell us... who's your accomplice?";
    private string trueEnd5 = "Accomplice? I didn't have one. I was one. Grace and I came together on this, and faked her death. Wanted to frame Scarlet... it felt like I was on the right side of things, y'know? A kid like - I mean, man like me - would do anythin' for love.";
    private string trueEnd6 = "Grace was new here and felt real lonely too at first. Felt like she had to fight for the top spot or somethin', and I know what Scarlet's like, so I could understand that real good, seemed real plausa... plausabable? Seemed... seemed like it would be real easy to let Scarlet take the fall for this one, is what I'm sayin'.";
    private string trueEnd7 = " We never shoulda lied to you all like this. I just wanted to be able to spend time with Grace AND Scarlet without makin' either one jealous! I thought I was doin the right thing for Grace here";
    private string trueEnd8 = " I went about it all in a real bad way though... are you gonna throw me in the can now? I would understand if you all wanted that... PLEASE DON'T THROW ME IN JAIL I'LL BE GOOD DETECTIVE";
    private string trueEnd9 = "And here I was thinkin' we were all friends! I think you and your mastermind over there owe us all a biiiiig apology, bucko.";
    private string trueEnd10 = "You're right, you're right! I'd like to apologise to everyone, especially you, Scarlet. You were the real victim here, but I hope we can still be friends. ";
    private string trueEnd11 = "Eddie, I'm sorry for roping you into this, and betrayin' your trust. I oughta tell you how much you mean to me more often!";
    private string trueEnd12 = "Juice Box, I still think you're real strange, but I'm sorry 'bout your arm. I'm glad you're ambodexter or whatever cause there's no better fingerpaintings in this whole nursery. ";
    private string trueEnd13 = "Detective, I'm sorry we got off on the wrong foot, you're a real character and we're lucky to have ya in the class here with us. Grace... I'll let you speak your piece now";
    private string trueEnd14 = "Got anythin' to say for yourself here, doll?";
    private string trueEnd15 = "Well, say. I don't know where to start detective. I'm real sorry I got you all into this mess... it was all because of my pride. See, I'm not the gal I told you all I was. I'm real jealous, and mean for what I did to Scarlet!";
    private string trueEnd16 = "I'm sorry, Scarlet. You offered me the hand of friendship and I swatted it away! I hope the offer still stands. The kids at my old nursery were real mean and never shared their toys, nothin' like you guys! But I didn't even give you all a chance.";
    private string trueEnd17 = "I hope you can all forgive me. I hope I didn't make a bad first impression on ya detective! Can we put this all behind us and play fair again?";
    private string trueEnd18 = "Well what do we say kids, do we forgive 'em?";
    private string trueEnd19 = "Well, I thank ya for your honesty, Grace! I'm sorry to hear about your old friends at the other nursery... but we're nothing like that, see! We're all besties here, really. ";
    private string trueEnd20 = "And we'll all go to the big school together and live happily ever after where there's endless toys and even more new besties to make! Thanks for solvin' the case, detective! Take my hand Grace, let's go get your dress fixed up!!";

    public bool inEnding = false;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager"); //Retrieves the game manager object
        SwitchEmotion(0, drewEnd); //Resets the emotions of ending drew
    }

    // Update is called once per frame
    void Update()
    {
        if(inEnding)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if(dialogueActive)
        {
            Cursor.visible = false;
        }

        if(Input.GetKeyDown(KeyCode.C) && endingReady && !inEnding) //Temporary ending trigger
        {
            StartConclusion();
            hintMessage.SetActive(false);
            inEnding = true;

        }
        if(Input.GetKeyUp(KeyCode.Return) && dialogueActive)
        {
            outtroCam.SetActive(true);
            if(eddieChosen)
            {
                switch (progress) //Triggers the bad ending progress with Eddie
                {
                    
                    case 1:
                        endingText.GetComponent<TextMeshProUGUI>().text = "Detective Drew: " + badEnd2 + " \n <Press [Enter] to Continue>";
                        //ChangeCams(eddieCam, drewCam);
                        outtroCam.GetComponent<OuttroCutscene>().ChangePosition(0);
                        SwitchEmotion(6, drewEnd);
                        progress++;
                        break;
                    case 2:
                        endingText.GetComponent<TextMeshProUGUI>().text = "Teacher: " + badEnd3 + " \n <Press [Enter] to Continue>";
                        //ChangeCams(drewCam, badEndingCam);
                        outtroCam.GetComponent<OuttroCutscene>().ChangePosition(5);
                        progress++;
                        break;
                    case 3:
                        endingText.GetComponent<TextMeshProUGUI>().text = "Detective Drew: " + badEnd4 + "\n<Press [Enter] to Continue>";
                       // ChangeCams(badEndingCam, drewCam);
                       outtroCam.GetComponent<OuttroCutscene>().ChangePosition(0);
                        SwitchEmotion(5, drewEnd);
                        progress++;
                        break;
                    case 4: SceneManager.LoadScene(0);
                        break;
                }
            }
            if(scarletChosen) //Triggers the bad ending progress with Scarlet
            {
                switch (progress)
                {
                    case 1:
                        endingText.GetComponent<TextMeshProUGUI>().text = "Detective Drew: " + badEnd2 + "\n<Press [Enter] to Continue>";
                        progress++;
                        //ChangeCams(scarletCam, drewCam);
                        outtroCam.GetComponent<OuttroCutscene>().ChangePosition(0);
                        SwitchEmotion(6, drewEnd);
                        break;
                    case 2:
                        endingText.GetComponent<TextMeshProUGUI>().text = "Teacher: " + badEnd3 + "\n<Press [Enter] to Continue>";
                        progress++;
                        //ChangeCams(drewCam, badEndingCam);
                        outtroCam.GetComponent<OuttroCutscene>().ChangePosition(5);
                        break;
                    case 3:
                        endingText.GetComponent<TextMeshProUGUI>().text = "Detective Drew: " + badEnd4 + "\n<Press [Enter] to Continue>";
                        //ChangeCams(badEndingCam, drewCam);
                        outtroCam.GetComponent<OuttroCutscene>().ChangePosition(0);
                        SwitchEmotion(5, drewEnd);
                        progress++;
                        break;
                    case 4:
                        SceneManager.LoadScene(0);
                        break;
                }
            }
            if(juiceboxChosen) //Triggers the bad ending progress with Juice Box
            {
                switch (progress)
                {

                    case 1:
                        endingText.GetComponent<TextMeshProUGUI>().text = "Detective Drew: " + badEnd2 + "\n<Press [Enter] to Continue>";
                        progress++;
                        //ChangeCams(juiceBoxCam, drewCam);
                        outtroCam.GetComponent<OuttroCutscene>().ChangePosition(0);
                        SwitchEmotion(6, drewEnd);

                        break;
                    case 2:
                        endingText.GetComponent<TextMeshProUGUI>().text = "Teacher: " +  badEnd3 + "\n<Press [Enter] to Continue>";
                        //ChangeCams(drewCam, badEndingCam);
                        outtroCam.GetComponent<OuttroCutscene>().ChangePosition(5);
                        progress++;
                        break;
                    case 3:
                        endingText.GetComponent<TextMeshProUGUI>().text = "Detective Drew: " + badEnd4 + "\n<Press [Enter] to Continue>";
                        //ChangeCams(badEndingCam, drewCam);
                        outtroCam.GetComponent<OuttroCutscene>().ChangePosition(0);
                        SwitchEmotion(5, drewEnd);
                        progress++;
                        break;
                    case 4:
                        SceneManager.LoadScene(0);
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        break;
                }
            }

            if(chaseChosen) //Triggers the good ending progress
            {
                switch(progress) 
                {
                    
                        
                    case 1: endingText.GetComponent<TextMeshProUGUI>().text = "Detective Drew: " + goodEnd2 + "\n<Press [Enter] to Continue>";
                        //ChangeCams(chaseCam, drewCam);
                        outtroCam.GetComponent<OuttroCutscene>().ChangePosition(0);
                        SwitchEmotion(7, drewEnd);
                        progress++;
                        break;
                    case 2: endingText.GetComponent<TextMeshProUGUI>().text = "Chase: " + goodEnd3 + "\n<Press [Enter] to Continue>";
                        //ChangeCams(drewCam, chaseCam);
                        outtroCam.GetComponent<OuttroCutscene>().ChangePosition(2);
                        SwitchEmotion(5, chaseEnd);
                        progress++;
                        break;
                    case 3: endingText.GetComponent<TextMeshProUGUI>().text = "Detective Drew: " + goodEnd4 + "\n<Press [Enter] to Continue>";
                       // ChangeCams(chaseCam, drewCam);
                       outtroCam.GetComponent<OuttroCutscene>().ChangePosition(0);
                        SwitchEmotion(6, drewEnd);
                        progress++;
                        break;
                    case 4: endingText.GetComponent<TextMeshProUGUI>().text = "Chase: " +  goodEnd5 + "\n<Press [Enter] to Continue>";
                      //  ChangeCams(drewCam, chaseCam);
                      outtroCam.GetComponent<OuttroCutscene>().ChangePosition(2);
                        SwitchEmotion(3, chaseEnd);
                        progress++;
                        break;
                    case 5: endingText.GetComponent<TextMeshProUGUI>().text = "Detective Drew: " +  goodEnd6 + "\n<Press [Enter] to Continue>";
                      //  ChangeCams(chaseCam, drewCam);
                      outtroCam.GetComponent<OuttroCutscene>().ChangePosition(0);
                        SwitchEmotion(7, drewEnd);
                        progress++;
                        break;
                    case 6: endingText.GetComponent<TextMeshProUGUI>().text = "Chase: " +  goodEnd7 + "\n<Press [Enter] to Continue>";
                     //   ChangeCams(drewCam, chaseCam);
                     outtroCam.GetComponent<OuttroCutscene>().ChangePosition(2);
                        SwitchEmotion(6, chaseEnd);
                        progress++;
                        break;
                    case 7: endingText.GetComponent<TextMeshProUGUI>().text = "Chase: " +  goodEnd8 + "\n<Press [Enter] to Continue>";
                     //   ChangeCams(chaseCam, scarletCam);
                     outtroCam.GetComponent<OuttroCutscene>().ChangePosition(3);
                        SwitchEmotion(5, scarletEnd);
                        progress++;
                        break;
                    case 8: endingText.GetComponent<TextMeshProUGUI>().text = "Chase: " +  goodEnd9 + "\n<Press [Enter] to Continue>";
                     //   ChangeCams(scarletCam, eddieCam);
                     outtroCam.GetComponent<OuttroCutscene>().ChangePosition(1);
                        SwitchEmotion(4, eddieEnd);
                        progress++;
                        break;
                    case 9: endingText.GetComponent<TextMeshProUGUI>().text = "Chase: " + goodEnd10 + "\n<Press [Enter] to Continue>";
                      //  ChangeCams(eddieCam, juiceBoxCam);
                      outtroCam.GetComponent<OuttroCutscene>().ChangePosition(4);
                        SwitchEmotion(6, juiceBoxEnd);
                        progress++;
                        break;
                    case 10: endingText.GetComponent<TextMeshProUGUI>().text = "Chase: " + goodEnd11 + "\n<Press [Enter] to Continue>";
                      //  ChangeCams(juiceBoxCam, drewCam);
                      outtroCam.GetComponent<OuttroCutscene>().ChangePosition(0);
                        SwitchEmotion(4, drewEnd);
                        progress++;
                        break;
                    case 11: endingText.GetComponent<TextMeshProUGUI>().text = "Chase: " + goodEnd12 + "\n<Press [Enter] to Continue>";
                      //  ChangeCams(drewCam, graceCam);
                      outtroCam.GetComponent<OuttroCutscene>().ChangePosition(0);
                        SwitchEmotion(4, graceEnd);
                        progress++;
                        break;
                    case 12: endingText.GetComponent<TextMeshProUGUI>().text = "Detective Drew: " +  goodEnd13 + "\n<Press [Enter] to Continue>";                       
                        progress++;
                        break;
                    case 13: endingText.GetComponent<TextMeshProUGUI>().text = "Everyone: " + goodEnd14 + "\n<Press [Enter] to Continue>";
                       // ChangeCams(graceCam, badEndingCam);
                       outtroCam.GetComponent<OuttroCutscene>().ChangePosition(5);
                        SwitchEmotion(4, scarletEnd);
                        SwitchEmotion(4, eddieEnd);
                        SwitchEmotion(4, chaseEnd);
                        SwitchEmotion(4, juiceBoxEnd);
                        progress++;
                        break;
                    case 14:endingText.GetComponent<TextMeshProUGUI>().text = "Detective Drew: " + goodEnd15 + "\n<Press [Enter] to Continue>";
                      //  ChangeCams(badEndingCam, drewCam);
                      outtroCam.GetComponent<OuttroCutscene>().ChangePosition(0);
                        SwitchEmotion(7, drewEnd);
                        progress++;
                        break;
                    case 15: SceneManager.LoadScene(0);
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        break;
                        
                }
            }
            if(graceChosen) //Triggers the true ending dialogue
            {
                switch(progress)
                {
                    
                    case 1: endingText.GetComponent<TextMeshProUGUI>().text = "Detective Drew:" + trueEnd2 + "\n<Press [Enter] to Continue>";
                        ChangeCams(chaseCam, drewCam);
                        SwitchEmotion(7, drewEnd);
                        progress++;
                        break;
                    case 2: endingText.GetComponent<TextMeshProUGUI>().text = "Chase: " + trueEnd3 + "\n<Press [Enter] to Continue>";
                        ChangeCams(drewCam, chaseCam);
                        SwitchEmotion(5, chaseEnd);
                        progress++;
                        break;
                    case 3: endingText.GetComponent<TextMeshProUGUI>().text = "Detective Drew:" + trueEnd4 + "\n<Press [Enter] to Continue>";
                        ChangeCams(chaseCam, drewCam);                      
                        progress++;
                        break;
                    case 4: endingText.GetComponent<TextMeshProUGUI>().text = "Chase: " + trueEnd5 + "\n<Press [Enter] to Continue>";
                        ChangeCams(drewCam, chaseCam);
                        SwitchEmotion(3, chaseEnd);
                        progress++;
                        break;
                    case 5: endingText.GetComponent<TextMeshProUGUI>().text = "Chase: " + trueEnd6 + "\n<Press [Enter] to Continue>";
                        progress++;
                        break;
                    case 6: endingText.GetComponent<TextMeshProUGUI>().text = "Chase: " + trueEnd7 + "\n<Press [Enter] to Continue>";
                        progress++;
                        break;
                    case 7:endingText.GetComponent<TextMeshProUGUI>().text = "Chase: " + trueEnd8 + "\n<Press [Enter] to Continue>";
                        SwitchEmotion(2, chaseEnd);
                        progress++;
                        break;
                    case 8: endingText.GetComponent<TextMeshProUGUI>().text = "Detective Drew:" + trueEnd9 + "\n<Press [Enter] to Continue>";
                        ChangeCams(chaseCam, drewCam);
                        SwitchEmotion(7, drewEnd);
                        progress++;
                        break;
                    case 9: endingText.GetComponent<TextMeshProUGUI>().text = "Chase: " + trueEnd10 + "\n<Press [Enter] to Continue>";
                        ChangeCams(drewCam, scarletCam);
                        SwitchEmotion(5, scarletEnd);
                        progress++;
                        break;
                    case 10: endingText.GetComponent<TextMeshProUGUI>().text = "Chase: " + trueEnd11 + "\n<Press [Enter] to Continue>";
                        ChangeCams(scarletCam, eddieCam);
                        SwitchEmotion(4, eddieEnd);
                        progress++;
                        break;
                    case 11: endingText.GetComponent<TextMeshProUGUI>().text = "Chase: " + trueEnd12 + "\n<Press [Enter] to Continue>";
                        ChangeCams(eddieCam, juiceBoxCam);
                        SwitchEmotion(6, juiceBoxEnd);
                        progress++;
                        break;
                    case 12: endingText.GetComponent<TextMeshProUGUI>().text = "Chase: " + trueEnd13 + "\n<Press [Enter] to Continue>";
                        ChangeCams(juiceBoxCam, drewCam);
                        SwitchEmotion(4, drewEnd);
                        progress++;
                        break;
                    case 13: endingText.GetComponent<TextMeshProUGUI>().text = "Detective Drew: " + trueEnd14 + "\n<Press [Enter] to Continue>";
                        SwitchEmotion(7, drewEnd);
                        progress++;
                        break;
                    case 14: endingText.GetComponent<TextMeshProUGUI>().text = "Grace: " + trueEnd15 + "\n<Press [Enter] to Continue>";
                        ChangeCams(drewCam, graceCam);
                        SwitchEmotion(5, graceEnd);
                        progress++;
                        break;
                    case 15: endingText.GetComponent<TextMeshProUGUI>().text = "Grace: " + trueEnd16 + "\n<Press [Enter] to Continue>";
                        progress++;
                        break;
                    case 16: endingText.GetComponent<TextMeshProUGUI>().text = "Grace: " + trueEnd17 + "\n<Press [Enter] to Continue>";
                        progress++;
                        break;
                    case 17: endingText.GetComponent<TextMeshProUGUI>().text = "Detective Drew: " + trueEnd18 + "\n<Press [Enter] to Continue>";
                        ChangeCams(graceCam, drewCam);
                        SwitchEmotion(7, drewEnd);
                        progress++;
                        break;
                    case 18: endingText.GetComponent<TextMeshProUGUI>().text = "Scarlet: " + trueEnd19 + "\n<Press [Enter] to Continue>";
                        ChangeCams(drewCam, scarletCam);
                        SwitchEmotion(4, scarletEnd);
                        progress++;
                        break;
                    case 19: endingText.GetComponent<TextMeshProUGUI>().text = "Scarlet: " + trueEnd20 + "\n<Press [Enter] to Continue>";
                        progress++;
                        break;
                    case 20: SceneManager.LoadScene(0);
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        break;
                }
            }
        }
    }

    public void StartConclusion() //Begins the conclusion sequence
    {
        blackFade.gameObject.SetActive(true);
        StartCoroutine(BlackTransition(mainScene, suspectSelectionCam));
        StartCoroutine(WaitThenFadeIn());
        Cursor.visible = true;
        SwitchEmotion(0, eddieEnd);
        SwitchEmotion(0, scarletEnd);
        SwitchEmotion(0, chaseEnd);
        SwitchEmotion(0, juiceBoxEnd);
        SwitchEmotion(0, drewEnd);
    }

    public void SuspectChosen() //Indicates that the player has chosen a suspect and begins the ending cutscene
    {
        if(eddieChosen)
        {
            
            blackFade.gameObject.SetActive(true);
            StartCoroutine(BlackTransition(suspectSelectionCam, badEndingCam));
            StartCoroutine(WaitThenFadeIn(true));
            eddieMain.SetActive(false);
            scarletMain.SetActive(false);
            drewMain.SetActive(false);
            chaseMain.SetActive(false);
            juiceBoxMain.SetActive(false);
            graceMain.SetActive(false);
            graceEnd.SetActive(true);
            eddieEnd.SetActive(true);
            scarletEnd.SetActive(true);
            drewEnd.SetActive(true);
            juiceBoxEnd.SetActive(true);
            chaseEnd.SetActive(true);
            dialogueActive = true;
            endingText.SetActive(true);
            endingText.GetComponent<TextMeshProUGUI>().text = "Eddie: " + badEnd1 + "\n<Press [Enter] to Continue>";
            progress++;
            SwitchEmotion(2, eddieEnd);
            

        }
        if(chaseChosen)
        {
            print("Chase ending here");
            blackFade.gameObject.SetActive(true);
            StartCoroutine(BlackTransition(suspectSelectionCam, badEndingCam));
            StartCoroutine(WaitThenFadeIn(true));
            eddieMain.SetActive(false);
            scarletMain.SetActive(false);
            drewMain.SetActive(false);
            chaseMain.SetActive(false);
            juiceBoxMain.SetActive(false);
            graceMain.SetActive(false);
            graceEnd.SetActive(true);
            eddieEnd.SetActive(true);
            scarletEnd.SetActive(true);
            drewEnd.SetActive(true);
            juiceBoxEnd.SetActive(true);
            chaseEnd.SetActive(true);
            dialogueActive = true;
            endingText.SetActive(true);
            endingText.GetComponent<TextMeshProUGUI>().text = "Chase: " + goodEnd1 + "\n<Press [Enter] to Continue>";
            progress++;
            SwitchEmotion(2, chaseEnd);

        }
        if(scarletChosen)
        {
            print("Scarlet ending here");
            blackFade.gameObject.SetActive(true);
            StartCoroutine(BlackTransition(suspectSelectionCam, badEndingCam));
            StartCoroutine(WaitThenFadeIn(true));
            eddieMain.SetActive(false);
            scarletMain.SetActive(false);
            drewMain.SetActive(false);
            chaseMain.SetActive(false);
            juiceBoxMain.SetActive(false);
            graceMain.SetActive(false);
            graceEnd.SetActive(true);
            eddieEnd.SetActive(true);
            scarletEnd.SetActive(true);
            drewEnd.SetActive(true);
            juiceBoxEnd.SetActive(true);
            chaseEnd.SetActive(true);
            dialogueActive = true;
            endingText.SetActive(true);
            endingText.GetComponent<TextMeshProUGUI>().text = "Scarlet: " + badEnd1 + "\n<Press [Enter] to Continue>";
            progress++;
            SwitchEmotion(2, scarletEnd);
        }
        if(juiceboxChosen)
        {
            print("Juice Box ending here");
            blackFade.gameObject.SetActive(true);
            StartCoroutine(BlackTransition(suspectSelectionCam, badEndingCam));
            StartCoroutine(WaitThenFadeIn(true));
            eddieMain.SetActive(false);
            scarletMain.SetActive(false);
            drewMain.SetActive(false);
            chaseMain.SetActive(false);
            juiceBoxMain.SetActive(false);
            graceMain.SetActive(false);
            graceEnd.SetActive(true);
            eddieEnd.SetActive(true);
            scarletEnd.SetActive(true);
            drewEnd.SetActive(true);
            juiceBoxEnd.SetActive(true);
            chaseEnd.SetActive(true);
            dialogueActive = true;
            endingText.SetActive(true);
            endingText.GetComponent<TextMeshProUGUI>().text = "Juice Box: " + badEnd1 + "\n<Press [Enter] to Continue>";
            progress++;
            SwitchEmotion(2, juiceBoxEnd);

        }
        if(graceChosen)
        {
            print("Grace ending here");
            blackFade.gameObject.SetActive(true);
            StartCoroutine(BlackTransition(suspectSelectionCam, badEndingCam));
            StartCoroutine(WaitThenFadeIn(true));
            eddieMain.SetActive(false);
            scarletMain.SetActive(false);
            drewMain.SetActive(false);
            chaseMain.SetActive(false);
            juiceBoxMain.SetActive(false);
            graceMain.SetActive(false);
            graceEnd.SetActive(true);
            eddieEnd.SetActive(true);
            scarletEnd.SetActive(true);
            drewEnd.SetActive(true);
            juiceBoxEnd.SetActive(true);
            chaseEnd.SetActive(true);
            dialogueActive = true;
            endingText.SetActive(true);
            endingText.GetComponent<TextMeshProUGUI>().text = "Chase: " + trueEnd1 + "\n<Press [Enter] to Continue>";
            progress++;
            SwitchEmotion(2, chaseEnd);
        }

    }

    public IEnumerator BlackTransition(GameObject currentCam = null, GameObject desiredCam = null,  bool startDialogue = false, bool transitionToBlack = true, float fadeSpeed = 1) //Fades or unfades the screen to/from black
    {
        Color screenColour = blackFade.color;
        float fadeProgress;
        if (transitionToBlack)
        {
            while (blackFade.color.a < 1)
            {
                fadeProgress = screenColour.a + (fadeSpeed * Time.deltaTime);
                screenColour = new Color(screenColour.r, screenColour.g, screenColour.b, fadeProgress);
                blackFade.color = screenColour;
                if (fadeProgress > 0.95f && currentCam!=null && desiredCam!=null)
                {
                    manager.GetComponent<SceneTransition>().ChangeCam(currentCam, desiredCam);
                    
                }
                if (fadeProgress > 0.95f && manager.GetComponent<DialogueManager>().trueEndingReached)
                {
                    trueEndingText.SetActive(true);
                }
                if(fadeProgress > 0.95f && manager.GetComponent<DialogueManager>().goodEndingReached)
                {
                    goodEndingText.SetActive(true);
                }
                if(fadeProgress > 0.95f && manager.GetComponent<DialogueManager>().badEndingReached)
                {
                    badEndingText.SetActive(true);
                }

                yield return null;
            }
        }
        else
        {
            while (blackFade.color.a > 0)
            {
                fadeProgress = screenColour.a - (fadeSpeed * Time.deltaTime);
                screenColour = new Color(screenColour.r, screenColour.g, screenColour.b, fadeProgress);
                blackFade.color = screenColour;
                if (fadeProgress < 0.01f)
                {
                    blackFade.gameObject.SetActive(false);

                    if (eddieChosen)
                    {
                        tempText.SetActive(true);
                        ChangeCams(badEndingCam, eddieCam);
                    }
                    if(scarletChosen)
                    {
                        tempText.SetActive(true);
                        ChangeCams(badEndingCam, scarletCam);
                    }
                    if(chaseChosen)
                    {
                        tempText.SetActive(true);
                        ChangeCams(badEndingCam, chaseCam);
                    }
                    if (juiceboxChosen)
                    {
                        tempText.SetActive(true);
                        ChangeCams(badEndingCam, juiceBoxCam);
                    }
                    if(graceChosen)
                    {
                        tempText.SetActive(true);
                        ChangeCams(badEndingCam, chaseCam);
                    }
                    yield return null;
                }
                yield return null;
            }
        }
    }

    public IEnumerator WaitThenFadeIn(bool startDialogue = false, float countdownValue = 2) //Used to wait and visually transition to new area
    {
        float currentCountdown = countdownValue;
        while (currentCountdown > 0)
        {
            yield return new WaitForSeconds(1);
            currentCountdown--;
        }
        StartCoroutine(BlackTransition(null, null,  startDialogue, false));
    }

    public void EddieChosen() //Signals that the player has accused Eddie
    {
        eddieChosen = true;
        SuspectChosen();
    }

    public void ScarletChosen() //Signals that the player has accused Scarlet
    {
        scarletChosen = true;
        SuspectChosen();
    }

    public void JuiceBoxChosen() //Signals that the player has accused Juice Box
    {
        juiceboxChosen = true;
        SuspectChosen();
    }

    public void ChaseChosen() //Signals that the player has accused Chase
    {
        chaseChosen = true;
        SuspectChosen();
    }

    public void GraceChosen() //Signals that the player has accused Grace
    {
        graceChosen = true;
        SuspectChosen();
    }

   public void ChangeCams(GameObject oldCam, GameObject newCam) //Changes the active camera
    {
        oldCam.SetActive(false);
        newCam.SetActive(true);
    }

    private void SwitchEmotion(int emotionNumber, GameObject npcTalking) //Method responsible for updating the displayed emotions of the NPCs
    {
        switch (emotionNumber)
        {
            case 0:
                npcTalking.GetComponent<NPCDialogue>().textureToChange.SetTexture("_DetailAlbedoMap", npcTalking.GetComponent<NPCDialogue>().defaultEmotion);
                break;
            case 1:
                npcTalking.GetComponent<NPCDialogue>().textureToChange.SetTexture("_DetailAlbedoMap", npcTalking.GetComponent<NPCDialogue>().angryEmotion);
                break;
            case 2:
                npcTalking.GetComponent<NPCDialogue>().textureToChange.SetTexture("_DetailAlbedoMap", npcTalking.GetComponent<NPCDialogue>().cryingEmotion);
                break;
            case 3:
                npcTalking.GetComponent<NPCDialogue>().textureToChange.SetTexture("_DetailAlbedoMap", npcTalking.GetComponent<NPCDialogue>().guiltyEmotion);
                break;
            case 4:
                npcTalking.GetComponent<NPCDialogue>().textureToChange.SetTexture("_DetailAlbedoMap", npcTalking.GetComponent<NPCDialogue>().playfulEmotion);
                break;
            case 5:
                npcTalking.GetComponent<NPCDialogue>().textureToChange.SetTexture("_DetailAlbedoMap", npcTalking.GetComponent<NPCDialogue>().sadEmotion);
                break;
            case 6:
                npcTalking.GetComponent<NPCDialogue>().textureToChange.SetTexture("_DetailAlbedoMap", npcTalking.GetComponent<NPCDialogue>().shockedEmotion);
                break;
            case 7:
                npcTalking.GetComponent<NPCDialogue>().textureToChange.SetTexture("_DetailAlbedoMap", npcTalking.GetComponent<NPCDialogue>().thinkingEmotion);
                break;
        }
    }

}
