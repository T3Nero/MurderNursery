using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class EvidenceSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool slotFilled = false; //Checks if the evidence slot is currently filled
    public Image evidenceImage = null; //The evidence image stored in the slot
    public string evidenceText = null; //The evidence text stored in the slot
    public GameObject pinboardManager;
    public int evidenceID;
    public GameObject prefab;
    public List<GameObject> threads = new List<GameObject>();

    public GameObject evidenceTooltip; // shows a description popup of the evidence placed

    //private bool clearing = false;
    // Start is called before the first frame update
    void Start()
    {
        evidenceTooltip = GameObject.FindGameObjectWithTag("Evidence Tooltip");
        evidenceTooltip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        evidenceTooltip.SetActive(true);
        evidenceTooltip.GetComponentInChildren<TextMeshProUGUI>().text = evidenceText;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        evidenceTooltip.SetActive(false);
    }

    public void ClearEvidence()
    {
        this.GetComponent<Image>().sprite = null;
        slotFilled = false;
        this.gameObject.SetActive(false);
        ReturnEvidence();
    }

    public void ReturnEvidence()
    {
        foreach(EvidenceClass evidence in pinboardManager.GetComponent<PinboardManager>().evidencePieces)
        {
            if(evidence.evidenceID == evidenceID)
            {
                
                Destroy(prefab);
                pinboardManager.GetComponent<PinboardManager>().clearingEv = true;
                pinboardManager.GetComponent<PinboardManager>().UpdateEvidenceImages(evidence);
            }
            
        }
        if (pinboardManager.GetComponent<PinboardManager>().threadedEvidence.Count >= 1)
        {
            foreach (string evidence in pinboardManager.GetComponent<PinboardManager>().threadedEvidence.ToList())
            {
                if (evidenceText == evidence)
                {
                    pinboardManager.GetComponent<PinboardManager>().threadedEvidence.Remove(evidence);
                    foreach (GameObject thread in threads)
                    {
                        Destroy(thread);
                    }
                }
            }
        }
    }
}
