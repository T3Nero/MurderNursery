using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreadButtons : MonoBehaviour //NEED TO REVISIT AND REWORK SCRIPT 
{
    public GameObject threadManager; //Stores the thread manager
    public GameObject firstObject; //Stores the object on first end of thread
    public GameObject secondObject; //Stores the object on second end of thread
    public Button firstButton; //Thread buttons
    public Button secondButton; // 
    public GameObject interrogationManager;

    public void Update()
    {
        if (interrogationManager == null)
        {
            interrogationManager = GameObject.FindGameObjectWithTag("InterrogationManager");
        }
    }
    public void SetThreadPosition()
    {
        if (!interrogationManager.GetComponent<Interrogation>().inInterrogation)
        {
            if (threadManager.GetComponent<ThreadManager>().firstThreadItem != null)
            {
                threadManager.GetComponent<ThreadManager>().secondThreadItem = this.gameObject;
            }
            if (threadManager.GetComponent<ThreadManager>().firstThreadItem == null)
            {
                if (this.gameObject.name == "Evidence")
                {
                    threadManager.GetComponent<ThreadManager>().firstThreadItem = this.gameObject;
                }

            }
        }
    }

    
}
