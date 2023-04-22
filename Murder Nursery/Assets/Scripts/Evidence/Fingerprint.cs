using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class Fingerprint : MonoBehaviour
{
    public string ownerFingerprint;

    public GameObject fingerprintUI; // UI to compare fingerprints
    public GameObject fingerprintFound;
    public GameObject evidenceItem;
    public GameObject tutorialManager;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(gameObject.GetComponent<SpriteRenderer>().enabled)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if(hit.transform.name == "Fingerprint")
                    {
                        evidenceItem.GetComponentInParent<EvidenceItem>().helpText.SetActive(false);
                        fingerprintFound.name = ownerFingerprint;
                        Sprite fpSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                        fingerprintFound.GetComponent<Image>().sprite = fpSprite;
                        fingerprintUI.SetActive(true);
                        if(tutorialManager.GetComponent<Tutorials>().mGlassTutorial3)
                        {
                            tutorialManager.GetComponent<Tutorials>().mGlassTutorial3 = false;
                            tutorialManager.GetComponent<Tutorials>().mGlassTutorial4 = true;
                            tutorialManager.GetComponent<Tutorials>().NavigateMGText();
                        }
                    }
                }
            }
        }
    }
}
