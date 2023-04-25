using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    
    public GameObject itemPrefab; // the item we wish to drag

    RectTransform rectTrans;
    Canvas myCanvas;
    CanvasGroup canvasGroup;
    
    public int itemID;
    public GameObject hoverOverText;
    public string displayText;
    private Vector2 originalPos;
    public GameObject slotPrefab;
    public GameObject pinboardManager;
    public int i = 0;
    private GameObject[] evidencePanels;
    public GameObject tutorialManager;

    private void Start()
    {
        rectTrans = GetComponent<RectTransform>();
        myCanvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        hoverOverText = GameObject.FindGameObjectWithTag("TestText");
        pinboardManager = GameObject.FindGameObjectWithTag("PinBoard Manager");
        evidencePanels = GameObject.FindGameObjectsWithTag("Evidence Panel");
        tutorialManager = GameObject.FindGameObjectWithTag("Tutorial Manager");
    }

    // Called when object is clicked on
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            i = pinboardManager.GetComponent<PinboardManager>().slotsProgress;
            itemPrefab = this.gameObject;
            canvasGroup.blocksRaycasts = false;
            itemPrefab.GetComponent<Image>().maskable = false;
            originalPos = transform.position;
            evidencePanels[0].GetComponent<Image>().enabled = false;
            evidencePanels[1].GetComponent<EvidenceSlider>().ToggleEvidencePanel();
            evidencePanels[1].GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            evidencePanels[1].GetComponent<Image>().enabled = false;
            

            //hoverOverText.SetActive(true);
            //hoverOverText.GetComponent<TextMeshProUGUI>().text = this.GetComponent<EvidenceClass>().evidenceText;
            //hoverOverText.transform.position = this.transform.position;
        }
        
    }

    // Objects transform position follows mouse position
    public void OnDrag(PointerEventData eventData)
    {
        //rectTrans.anchoredPosition += eventData.delta / myCanvas.scaleFactor;
        gameObject.transform.position = Input.mousePosition;
        
        // hoverOverText.transform.position = Input.mousePosition;
    }

    // Called when mouse click is released
    public void OnEndDrag(PointerEventData eventData)
    {
        if(tutorialManager.GetComponent<Tutorials>().inPBTutorial2)
        {
            tutorialManager.GetComponent<Tutorials>().togglePBMessageButton.SetActive(true);
            tutorialManager.GetComponent<Tutorials>().pbTextObject.SetActive(true);
            tutorialManager.GetComponent<Tutorials>().overPBText.GetComponent<TextMeshProUGUI>().text = tutorialManager.GetComponent<Tutorials>().pbText2;
        }

        //hoverOverText.SetActive(false);
        evidencePanels[0].GetComponent<Image>().enabled = true;
        evidencePanels[1].GetComponent<EvidenceSlider>().ToggleEvidencePanel();
        evidencePanels[1].GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        evidencePanels[1].GetComponent<Image>().enabled = true;

        canvasGroup.blocksRaycasts = true;
        itemPrefab.GetComponent<Image>().maskable = true;
        pinboardManager.GetComponent<PinboardManager>().slots[i].SetActive(true);
        pinboardManager.GetComponent<PinboardManager>().slots[i].transform.position = eventData.position;
        pinboardManager.GetComponent<PinboardManager>().slots[i].GetComponent<Image>().sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
        pinboardManager.GetComponent <PinboardManager>().slots[i].GetComponent<EvidenceSlot>().slotFilled = true;
        pinboardManager.GetComponent<PinboardManager>().slots[i].GetComponent<EvidenceSlot>().evidenceText = eventData.pointerDrag.GetComponent<EvidenceClass>().evidenceText;
        pinboardManager.GetComponent<PinboardManager>().slots[i].GetComponent<EvidenceSlot>().evidenceID = eventData.pointerDrag.GetComponent<EvidenceClass>().evidenceID;
        pinboardManager.GetComponent<PinboardManager>().slotsProgress++;
        
        Destroy(eventData.pointerDrag.gameObject);
        eventData.Reset();
        ResetPosition();


    }


    public void OnMouseExit()
    {
        //hoverOverText.SetActive(false);
    }


    // objects position is set back to original position before being dragged
    public void ResetPosition()
    {
        transform.position = originalPos;
    }

}
