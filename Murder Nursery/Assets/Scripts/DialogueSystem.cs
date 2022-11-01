using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public GameObject responseText1;
    public GameObject responseText2;
    public GameObject responseText3;
    public GameObject statementText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadNPCStatement(string statement) //Loads NPC dialogue into main box
    {
        statementText.GetComponent<TextMeshProUGUI>().text = statement;
    }
    public void LoadResponses(string firstResponse)
    {
        responseText1.GetComponent<TextMeshProUGUI>().text = firstResponse;
    }
    public void LoadResponses(string firstResponse, string secondResponse)
    {
        responseText1.GetComponent<TextMeshProUGUI>().text = firstResponse;
        responseText2.GetComponent<TextMeshProUGUI>().text = secondResponse;   
    }
    public void LoadResponses(string firstResponse, string secondResponse, string thirdResponse)
    {
        responseText1.GetComponent<TextMeshProUGUI>().text = firstResponse;
        responseText2.GetComponent<TextMeshProUGUI>().text = secondResponse;
        responseText3.GetComponent<TextMeshProUGUI>().text = thirdResponse;
    }

    
}