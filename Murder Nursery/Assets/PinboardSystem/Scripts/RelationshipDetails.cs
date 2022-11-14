using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RelationshipDetails : MonoBehaviour
{
    [SerializeField]
    GameObject relationshipUI;

    [SerializeField]
    public Transform relationshipContent;

    // used for opening relationship options panel
    public GameObject relationshipOptionsPanel;

    [SerializeField]
    GameObject relationshipOptionsUI;

    [SerializeField]
    Transform optionsContent;

    // set based on which relationship content we currently want to replace
    [HideInInspector]
    public string textToReplace;

    // contains list of undiscovered relationship details
    public List<string> goonJuiceList;
    public List<string> goonCoolguyList;


    [Header ("False/Undiscovered Relationship Details")]
    public List<string> goonJuiceDetails;
    public List<string> goonCoolguyDetails;


    // contains list of optional conclusions to replace undiscovered text with
    [Header("Relationship Conclusions")]
    public List<string> conclusionsList;

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            goonJuiceList.Add(goonJuiceDetails[0]);
            goonCoolguyList.Add(goonJuiceDetails[1]);
            AddToRelationshipOptionsUI(conclusionsList[0]);
            AddToRelationshipOptionsUI(conclusionsList[1]);
        }

        if(Input.GetKeyUp(KeyCode.Alpha2))
        {
            goonJuiceList.Add(goonJuiceDetails[1]);
        }
    }

    public void ClearDetails()
    {
        // Clear content before updating the next relationship
        if (relationshipContent.childCount > 0)
        {
            foreach (Transform content in relationshipContent)
            {
                Destroy(content.gameObject);
            }
        }
    }

    // Called when we to update the relationship panel with info that is incomplete or false
    public void UpdateRelationship(List<string> relationshipList)
    {
        ClearDetails();
        foreach (var item in relationshipList)
        {
            GameObject relationshipObj = Instantiate(relationshipUI, relationshipContent);
            var contentText = relationshipObj.transform.Find("RelationshipText").GetComponent<TextMeshProUGUI>();
            contentText.text = item;
        }
    }

    // called when we want to update the relationship options panel with statements
    // for the player to choose to replace undiscovered (?????) or false statements
    public void AddToRelationshipOptionsUI(string textToAdd)
    {
        GameObject optionsObj = Instantiate(relationshipOptionsUI, optionsContent);
        var optionsText = optionsObj.transform.Find("RelationshipOptionsText").GetComponent<TextMeshProUGUI>();
        optionsText.text = textToAdd;
    }
}
