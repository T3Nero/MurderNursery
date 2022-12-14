using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroCutscene : MonoBehaviour
{
    public GameObject introTextBox;
    public GameObject introTextBox2;
    public GameObject dialoguePanel1;
    public GameObject dialoguePanel2;
    public GameObject initialCam;
    public GameObject eddieCam;
    public GameObject juiceBoxCam;
    public GameObject scarletCam;
    public GameObject chaseCam;
    public GameObject mainGameCam;
    public GameObject player;
    public bool inIntro = true;
    private int progress = 0;
    private string playerStatement1 = "There I was, only one day into this new nursery gig, and naptime was callin? my name somethin? awful";
    private string playerStatement2 = "But really, my name was Drew. Detective Drew, if we?re friends. And Detective Drew if we ain?t.";
    private string playerStatement3 = "Point was, I was one sleepy bye-bye away from retirement. I barely knew these bozos, and yet there Grace was? dead.";
    private string playerStatement4 = "Somebody wanted this girl outta the picture?";
    private string playerStatement5 = "Eddie. He seems like a nasty piece of work and a dope to boot. Maybe he?d be a good place to start?";
    private string playerStatement6 = "Our resident artist, Juice Box. I can?t quite put a pin on the guy. Then again, he?s locked up tighter than the teacher?s desk";
    private string playerStatement7 = "Scarlet. The fiercest dame I ever done met. And I met at least two other dames at my last nursery - I mean precinct.";
    private string playerStatement8 = "What the - aw man, he?s not even wearing his costume!";
    private string playerStatement9 = "I mean, uh, his outfit. Yeah, I guess he?s in disguise. Classic Chase. As slick as they come. Definitely a man with somethin? to hide?";
    private string playerStatement10 = "I guess the boss is giving me free reign on this one. Time to look around and start putting the pressure on these kids. I gotta figure out whodunnit by the time she comes back?";
    private string teacherStatement = "Okay kids, I?m popping into the staff room to grab your lunches! I?ll be back in ten minutes, so play nice and get to know your new friend Drew, alright?";
    public GameObject manager;
    public AudioSource introCam;
    public AudioClip introMusic;

    // Start is called before the first frame update
    void Start()
    {
        dialoguePanel1.SetActive(true);
        introTextBox.GetComponent<TextMeshProUGUI>().text = playerStatement1;
        introCam.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Return) && inIntro)
        {
            switch(progress)
            {
                case 0: introTextBox.GetComponent<TextMeshProUGUI>().text = playerStatement2;
                    progress++;
                    break;
                case 1: introTextBox.GetComponent<TextMeshProUGUI>().text = playerStatement3;
                    progress++;
                    break;
                case 2: introTextBox.GetComponent<TextMeshProUGUI>().text = playerStatement4;
                    progress++;
                    break;
                case 3: ChangeCams(initialCam, eddieCam);
                    introTextBox.GetComponent<TextMeshProUGUI>().text = playerStatement5;
                    progress++;
                    break;
                case 4: ChangeCams(eddieCam, juiceBoxCam);
                    introTextBox.GetComponent<TextMeshProUGUI>().text = playerStatement6;
                    progress++;
                    break;
                case 5: ChangeCams(juiceBoxCam, scarletCam);
                    introTextBox.GetComponent<TextMeshProUGUI>().text = playerStatement7;
                    progress++;
                    break;
                case 6: ChangeCams(scarletCam, chaseCam);
                    introTextBox.GetComponent<TextMeshProUGUI>().text = playerStatement8;
                    progress++;
                    break;
                case 7: introTextBox.GetComponent<TextMeshProUGUI>().text = playerStatement9;
                    progress++;
                    break;
                case 8: dialoguePanel2.SetActive(true);
                    dialoguePanel1.SetActive(false);
                    introTextBox2.GetComponent<TextMeshProUGUI>().text = "Teacher: " + teacherStatement;
                    progress++;
                    break;
                case 9: ChangeCams(chaseCam, initialCam);
                    dialoguePanel2.SetActive(false);
                    dialoguePanel1.SetActive(true);
                    introTextBox.GetComponent<TextMeshProUGUI>().text = playerStatement10;
                    progress++;
                    break;
                case 10: dialoguePanel1.SetActive(false);
                    inIntro = false;
                    ChangeCams(initialCam, mainGameCam);
                    introCam.Stop();
                    manager.GetComponent<AudioSource>().Play();
                    break;

            }
        }
    }

    public void ChangeCams(GameObject currentCam, GameObject newCam)
    {
        currentCam.SetActive(false);
        newCam.SetActive(true);
    }
}
