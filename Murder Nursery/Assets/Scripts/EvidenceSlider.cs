using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceSlider : MonoBehaviour
{
    private bool evidenceOpen = false;
    public GameObject evidencePanel;
    float movementProgress;
    float timeToMove = 0.1f;
    public Vector3 openTransform = new Vector3 (1004.0f, 160.18499755859376f, 0.0f);
    public Vector3 closeTransform = new Vector3(2616.5f, 160.18499755859376f, 0.0f);
    // Start is called before the first frame update
    void Start()
    {
        evidenceOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleEvidencePanel()
    { 
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
        int i = 0;
        while(xCoord >1000 ) 
        {
            movementProgress = timeToMove + 0.01f;
            evidencePanel.transform.position = new Vector3(movementProgress, evidencePanel.transform.position.y, evidencePanel.transform.position.z);
            xCoord = evidencePanel.transform.position.x;
        }
        yield return null;
    }
}
