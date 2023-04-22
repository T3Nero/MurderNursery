using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FingerprintComparrison : MonoBehaviour
{
    [SerializeField]
    GameObject fingerprintUI;

    [SerializeField]
    GameObject fingerprintFound;

    [SerializeField]
    GameObject incorrectMatchText;

    [SerializeField]
    GameObject correctMatchText;

    [SerializeField]
    GameObject particleLight;

    [SerializeField]
    GameObject particleStar;

    [SerializeField]
    GameObject dialogueManager;

    [SerializeField]
    GameObject graceTutoritalMessage;

    [SerializeField]
    GameObject playerMGResponceObject;

    [SerializeField]
    GameObject playerMGResponceObjectText;

    public AudioSource sfxAudio;
    public AudioClip sfxAudioClip;

    private GameObject evidenceItem;
    private bool comparingFingerprint;
    private string playerMGResponceText = "Detective Drew: Looks like Grace is the dame with a hidden love for juice! Who woulda thought it? Now I'm wonderin' just how far she'll go for the next hit…";

    public void CloseFingerprintUI()
    {

        fingerprintUI.SetActive(false);
        evidenceItem.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CompareFingerprints()
    {
        if(fingerprintFound.name == gameObject.name && !comparingFingerprint) // correct match
        {
            evidenceItem = EvidenceItem.evidenceItem.gameObject;
            comparingFingerprint = true;
            particleLight.GetComponent<ParticleSystem>().Play();
            StartCoroutine(StopLightParticle(3f));
        }
        else if(!comparingFingerprint)
        {
            comparingFingerprint = true;
            incorrectMatchText.SetActive(true);
            StartCoroutine(HideText(2f));
        }
    }

    IEnumerator HideText(float duration)
    {
        float time = 0;
        while(time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }

        if(time >= duration)
        {
            incorrectMatchText.SetActive(false);
            comparingFingerprint = false;
        }
    }

    IEnumerator StopLightParticle(float duration)
    {
        float time = 0;
        while(time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }

        if(time >= duration)
        {
            particleLight.GetComponent<ParticleSystem>().Stop();
            particleStar.GetComponent<ParticleSystem>().Play();
            sfxAudio.PlayOneShot(sfxAudioClip, 0.2f);
            StartCoroutine(StopStarParticle(1f));
            correctMatchText.SetActive(true);
            comparingFingerprint = false;

            if(playerMGResponceObject)
            {
                graceTutoritalMessage.SetActive(false);
                playerMGResponceObject.SetActive(true);
                playerMGResponceObjectText.GetComponent<TextMeshProUGUI>().text = playerMGResponceText;
            }
        }
    }

    IEnumerator StopStarParticle(float duration)
    {
        float time = 0;
        while(time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }

        if(time >= duration)
        {
            evidenceItem.GetComponent<EvidenceItem>().inspectingItem = false;
            InventoryManager.inventory.AddItem(evidenceItem.GetComponent<EvidenceItem>().item);
            InventoryManager.inventory.MG.GetComponent<MagnifyingGlass>().gameObject.SetActive(true);
            InventoryManager.inventory.MG.GetComponent<MagnifyingGlass>().magnifyingBlur.SetActive(true);
            particleStar.GetComponent<ParticleSystem>().Stop();
            correctMatchText.SetActive(false);
            CloseFingerprintUI();

            if(playerMGResponceObject)
            {
                InventoryManager.inventory.MG.GetComponent<MagnifyingGlass>().ToggleMagnifyingGlass();
                if(InventoryManager.inventory.UIVisibility.inventoryOpen)
                {
                    InventoryManager.inventory.UIVisibility.ToggleInventory();
                    InventoryManager.inventory.itemTooltip.SetActive(false);
                }
                Cursor.visible = true;
            }

            
        }
    }

    public void ContinueTutorial()
    {
        if(playerMGResponceObject)
            {
                dialogueManager.GetComponent<DialogueManager>().StartConversation(dialogueManager.GetComponent<DialogueManager>().grace.GetComponent<NPCDialogue>().dialogueTree[17], dialogueManager.GetComponent<DialogueManager>().grace, dialogueManager.GetComponent<DialogueManager>().graceCam4);
                playerMGResponceObject.SetActive(false);
            }
    }
}
