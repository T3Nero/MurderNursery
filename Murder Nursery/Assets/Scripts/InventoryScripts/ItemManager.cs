using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ItemManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public GameObject itemTooltip;
    public GameObject notebook;
    

    //private bool firstMG = true;
    //private bool firstFP = true;
    public GameObject tutorialManager;
    public string currentOutfit = null;

    public GameObject dressUp;
    private bool outfitChanging = false;

    public void Start()
    {
        tutorialManager = GameObject.FindGameObjectWithTag("Tutorial Manager");
        dressUp = GameObject.FindGameObjectWithTag("Dress Up Manager");

        
    }

    // Called from inventory manager when an item is picked up
    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    // Destroys the item and removes it from inventory
    public void RemoveItem()
    {
        InventoryManager.inventory.RemoveItem(item);
        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemTooltip.SetActive(true);
        itemTooltip.GetComponentInChildren<TextMeshProUGUI>().text = item.itemDescription;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemTooltip.SetActive(false);
    }

    // Called when we click an item in our inventory (item type determines the items use functionality)
    public void UseItem()
    {
        switch (item.itemType)
        {
            case Item.ItemType.Bribery:
                break;
            case Item.ItemType.Gift:
                Debug.Log(item.itemName);
                RemoveItem();
                break;
            case Item.ItemType.MagnifyingGlass:
                //if(firstMG)
                //{
                //    Time.timeScale = 0;
                //    tutorialManager.GetComponent<Tutorials>().ActivateTutorial(tutorialManager.GetComponent<Tutorials>().magnifyingGlassTutorial);
                //    firstMG = false;
                //}
                if (!tutorialManager.GetComponent<Tutorials>().nbTutorial2)
                {
                    if (InventoryManager.inventory.MG.GetComponent<MagnifyingGlass>().OutfitCheck())
                    {
                        InventoryManager.inventory.MG.GetComponent<MagnifyingGlass>().ToggleMagnifyingGlass();
                        InventoryManager.inventory.UIVisibility.ToggleInventory();
                        itemTooltip.SetActive(false);
                    }
                }
                
                break;
            case Item.ItemType.PinBoard:
                if (!tutorialManager.GetComponent<Tutorials>().mGlassTutorial1 && !tutorialManager.GetComponent<Tutorials>().nbTutorial2)
                {
                    InventoryManager.inventory.UIVisibility.TogglePinboard();
                    itemTooltip.SetActive(false);
                }
                break;
            case Item.ItemType.Jotter:
                InventoryManager.inventory.UIVisibility.ToggleJotter();
                break;
            case Item.ItemType.NoteBook:
                if (!tutorialManager.GetComponent<Tutorials>().mGlassTutorial1)
                {
                    InventoryManager.inventory.UIVisibility.ToggleNotebook();
                }
                break;
        }
    }
}
