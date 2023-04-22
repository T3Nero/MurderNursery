using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCutscene : MonoBehaviour
{

    [Header("UI Elements")]
    public GameObject introTextBox; //Text object for dialogue
    public GameObject introTextBox2;//''
    public GameObject dialoguePanel1; //Panel to contain dialogue
    public GameObject dialoguePanel2;//''

    [Header("Cameras")]
    public GameObject initialCam; //Camera's for different perspectives 
    public GameObject mainGameCam; //Main game camera used by the player

    [Header("Intro Variables")]
    public GameObject manager; //Stores the game manager
    public GameObject player; //Stores the player game object
    public bool inIntro = false; //Signals that the player is in the intro
    private int progress = 0; //Tracks the player's progress through the intro

    public GameObject initialPos, chasePos, scarletPos, jBPos, eddiePos; // positions for camera to transition to

    readonly List<GameObject> positions = new(); // list of all possible positions
    
    
    //Intro Dialogue 
    private string playerStatement1 = "There I was, only one day into this new nursery gig, and naptime was callin’ my name somethin’ awful";
    private string playerStatement2 = "But really, my name was Drew. Detective Drew, if we’re friends. And Detective Drew if we ain’t.";
    private string playerStatement3 = "Point was, I was one sleepy bye-bye away from retirement. I barely knew these bozos, and yet there Grace was… dead.";
    private string playerStatement4 = "Somebody wanted this girl outta the picture…";
    private string playerStatement5 = "Eddie. He seems like a nasty piece of work and a dope to boot. Maybe he’d be a good place to start…";
    private string playerStatement6 = "Our resident artist, Juice Box. I can’t quite put a pin on the guy. Then again, he’s locked up tighter than the teacher’s desk";
    private string playerStatement7 = "Scarlet. The fiercest dame I ever done met. And I met at least two other dames at my last nursery - I mean precinct.";
    private string playerStatement8 = "What the - aw man, he’s not even wearing his costume!";
    private string playerStatement9 = "I mean, uh, his outfit. Yeah, I guess he’s in disguise. Classic Chase. As slick as they come. Definitely a man with somethin’ to hide…";
    private string playerStatement10 = "I guess the boss is giving me free reign on this one. Time to look around and start putting the pressure on these kids. I gotta figure out whodunnit by the time she comes back…";
    private string teacherStatement = "Okay kids, I’m popping into the staff room to grab your lunches! I’ll be back in ten minutes, so play nice and get to know your new friend Drew, alright?";

    [Header("Audio")]
    public AudioSource introAudioSource;//Audio source for the intro
    public AudioClip introMusic; //Music track for the intro
    public AudioClip gameplayMusic;

    // Start is called before the first frame update
    void Start()
    {
        
        //dialoguePanel1.SetActive(true);//Activates the intro dialogue UI 
        introTextBox.GetComponent<TextMeshProUGUI>().text = playerStatement1; //Loads the first dialogue piece
        introAudioSource.clip = introMusic;
        introAudioSource.Play();//Plays the intro music 
        positions.Add(initialPos);
    }

    // Update is called once per frame
    void Update()
    {
        if(inIntro &&  !dialoguePanel1.activeInHierarchy && this.name != "IntroCam")//&& !initialCam.activeInHierarchy)
        {
            initialCam.SetActive(true);
            mainGameCam.SetActive(false);
            dialoguePanel1.SetActive(true);//Activates the intro dialogue UI 
        }
        if(Input.GetKeyUp(KeyCode.Return) && inIntro) //Allows the player to navigate through the intro dialogue 
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
                case 3: ChangePosition(1);
                    introTextBox.GetComponent<TextMeshProUGUI>().text = playerStatement5;
                    progress++;
                    break;
                case 4: ChangePosition(4); 
                    introTextBox.GetComponent<TextMeshProUGUI>().text = playerStatement6;
                    progress++;
                    break;
                case 5: ChangePosition(3);
                    introTextBox.GetComponent<TextMeshProUGUI>().text = playerStatement7;
                    progress++;
                    break;
                case 6: ChangePosition(2);
                    introTextBox.GetComponent<TextMeshProUGUI>().text = playerStatement8;
                    progress++;
                    break;
                case 7: introTextBox.GetComponent<TextMeshProUGUI>().text = playerStatement9;
                    progress++;
                    break;
                case 8: 
                    
                    introTextBox.GetComponent<TextMeshProUGUI>().text = "Teacher: " + teacherStatement;
                    progress++;
                    break;
                case 9: ChangePosition(0);
                    dialoguePanel2.SetActive(false);
                    dialoguePanel1.SetActive(true);
                    introTextBox.GetComponent<TextMeshProUGUI>().text = playerStatement10;
                    progress++;
                    break;
                case 10: 
                    inIntro = false;
                    dialoguePanel1.SetActive(false);
                    ChangeCams(initialCam, mainGameCam);
                    introAudioSource.Stop();
                    manager.GetComponent<AudioSource>().clip = gameplayMusic;
                    manager.GetComponent<AudioSource>().Play();
                    break;
            }
           // MoveCameraPosition();
        }
       MoveCameraPosition();
    }

    public void ChangeCams(GameObject currentCam, GameObject newCam) //Changes the camera which is currently active
    {
        currentCam.SetActive(false);
        newCam.SetActive(true);
    }

    // Changes which transform position the camera will move to based on the index passed in
    public void ChangePosition(int index)
    {
        positions.RemoveAt(0);

        switch(index)
        {
            case 0:
                positions.Add(initialPos);
                break;
            case 1:
                positions.Add(eddiePos);
                break;
            case 2:
                positions.Add(chasePos);
                break;
            case 3:
                positions.Add(scarletPos);
                break;
            case 4:
                positions.Add(jBPos);
                break;
        }
    }

    // Lerps the position of the camera to the transform position which matches the first element of the positions array
    void MoveCameraPosition()
    {
        if(positions.Count > 0 && inIntro)
        {
                transform.SetPositionAndRotation(Vector3.Lerp(transform.position,
                    positions[0].transform.position, 1.5f * Time.deltaTime), Quaternion.Lerp(transform.rotation,
                    positions[0].transform.rotation, 1.5f * Time.deltaTime));
        }
     //   else
      //  {
      //     inIntro = false;
      //  }
    }
}
