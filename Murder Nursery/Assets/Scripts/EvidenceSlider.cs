using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceSlider : MonoBehaviour
{
    public bool evidenceOpen = false;
    public GameObject evidencePanel;
    float movementProgress;
    float timeToMove = 0.1f;
    public Vector3 openTransform;
    public Vector3 closeTransform;
    public GameObject tutorialManager;
    // Start is called before the first frame update
    void Start()
    {
        evidenceOpen = false;
        tutorialManager = GameObject.FindGameObjectWithTag("Tutorial Manager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleEvidencePanel()
    { 
        if(tutorialManager.GetComponent<Tutorials>().inPBTutorial1)
        {
            tutorialManager.GetComponent<Tutorials>().pbTextObject.SetActive(false);
            tutorialManager.GetComponent<Tutorials>().inPBTutorial1 = false;
            tutorialManager.GetComponent<Tutorials>().inPBTutorial2 = true;
            tutorialManager.GetComponent<Tutorials>().togglePBMessageButton.SetActive(false);
        }
        if(!evidenceOpen)
        {
            evidencePanel.transform.position = openTransform;
            evidenceOpen = true;
            return;
        }
         if(evidenceOpen)
        {
            evidencePanel.transform.position = closeTransform;
            evidenceOpen = false;
            return;
        }
    }

    public IEnumerator MovePanel()
    {
        float xCoord = evidencePanel.transform.position.x;
        //int i = 0;
        while(xCoord >1000 ) 
        {
            movementProgress = timeToMove + 0.01f;
            evidencePanel.transform.position = new Vector3(movementProgress, evidencePanel.transform.position.y, evidencePanel.transform.position.z);
            xCoord = evidencePanel.transform.position.x;
        }
        yield return null;
    }
}
