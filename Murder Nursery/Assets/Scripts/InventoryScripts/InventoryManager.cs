using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager inventory;

    public GameObject DM;
    public GameObject interManager;
    public GameObject MG; // allows access to magnifying glass script
    public GameObject Player; // used for stopping movement whilst toggling mag glass
    public GameObject menuManager;
    MainMenuSettings menuUI;
    GameObject itemObj;


    [SerializeField]
    Item pinboard; //Stores the pinboard for the player inventory

    [SerializeField]
    Item jotter; //Stores the jotter for the player inventory

    [SerializeField]
    public Item mGlass; // Stores the magnifying glass in the player inventory

    [SerializeField]
    public Item listeningDevice;

    [SerializeField]
    Item notebook;


    [HideInInspector]
    public ToggleUIVisibility UIVisibility; 

    [SerializeField]
    public List<Item> items = new List<Item>();//List of items present 

    [SerializeField]
    Transform itemContent; // transform the ui items are added to (player inventory ui)

    [HideInInspector]
    public GameObject inventoryItem; // ui sprite which gets instantiated to inventory

    public List<ItemManager> inventoryItems; //Stores items present in player inventory
    public GameObject blur; //Background blur object
    public GameObject itemTooltip;
    public GameObject tutorialPanel;
    GameObject dressUp;

    public bool inventoryToggled = false;
    private bool canToggleMGlass = true;
    public GameObject tutorialManager;
    private void Awake()
    {
        inventory = this;
        UIVisibility = GetComponent<ToggleUIVisibility>();
        dressUp = GameObject.FindGameObjectWithTag("Dress Up Manager");
        menuUI = menuManager.GetComponent<MainMenuSettings>();
    }

    private void Start()
    {
        tutorialManager = GameObject.FindGameObjectWithTag("Tutorial Manager");
        AddItem(pinboard); //Adds the fundamental items to the player's inventory
        AddItem(mGlass); //''
      //  AddItem(listeningDevice);
        AddItem(notebook);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.I) && !DM.GetComponent<IntroCutscene>().inIntro && !DM.GetComponent<DialogueManager>().inConvo && !DM.GetComponent<Conclusion>().inEnding && !interManager.GetComponent<Interrogation>().inInterrogation && !menuUI.menuOpen && !tutorialManager.GetComponent<Tutorials>().inTutorial && !tutorialManager.GetComponent<Tutorials>().lockMovementMainGame && !dressUp.GetComponent<DressUp>().inDressUp && dressUp.GetComponent<DressUp>().activeOutfit == "Detective Outfit")  //Used to open and close the player's inventory
        {
            UIVisibility.ToggleInventory();
            if(blur.activeSelf)
            {
                blur.SetActive(false);
            }
        }

        // close inventory if open & should not be accessible
        if(DM.GetComponent<Conclusion>().inEnding && UIVisibility.inventoryOpen)
        {
            UIVisibility.ToggleInventory();
        }

        if(DM.GetComponent<DialogueManager>().inConvo && UIVisibility.inventoryOpen && !tutorialManager.GetComponent<Tutorials>().inTutorial)
        {
            UIVisibility.ToggleInventory();
        }

        // Toggle Magnifying Glass
        if(Input.GetKeyUp(KeyCode.M) && !DM.GetComponent<IntroCutscene>().inIntro && !DM.GetComponent<DialogueManager>().inConvo && !DM.GetComponent<Conclusion>().inEnding && !interManager.GetComponent<Interrogation>().inInterrogation && !dressUp.GetComponent<DressUp>().inDressUp && canToggleMGlass && !menuUI.menuOpen && !tutorialManager.GetComponent<Tutorials>().inTutorial)
        {
            if (MG.GetComponent<MagnifyingGlass>().OutfitCheck())
            {
                StartCoroutine(MagnifyingGlassCooldown(1.0f));
                MG.GetComponent<MagnifyingGlass>().ToggleMagnifyingGlass();
                if(UIVisibility.inventoryOpen)
                {
                    UIVisibility.ToggleInventory();
                    itemTooltip.SetActive(false);
                }
            }
        }

        if (dressUp.GetComponent<DressUp>().outfitChanging)
        {
            if(dressUp.GetComponent<DressUp>().activeOutfit != "Detective Outfit")
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    if(inventoryItems[i].name == "Magnifying Glass")
                    {
                        inventoryItems[i].RemoveItem();
                        dressUp.GetComponent<DressUp>().outfitChanging = false;
                    }
                }
            }
        }
    }

    public void AddItem(Item item) //Called when an item is collected and added to the player's inventory 
    {
        items.Add(item);
        SetInventoryItems(item);
    }

    public void RemoveItem(Item item) //Called when an item is removed from the player inventory
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if(inventoryItems[i].name == item.itemName)
            {
                items.Remove(item);
                inventoryItems.Remove(inventoryItems[i]);
            }
        }
        
    }

    public void SetInventoryItems(Item item)
    {

        // adds itemUI to Inventory UI (allows us to see the item in inventory) 
        itemObj = Instantiate(inventoryItem, itemContent);

        var itemIcon = itemObj.transform.Find("ItemIcon").GetComponent<Image>();
        var itemName = itemObj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        itemIcon.sprite = item.icon;
        itemName.text = item.itemName;
        itemObj.name = itemName.text;
        itemObj.GetComponent<ItemManager>().itemTooltip = itemTooltip;

        // sets the item in ItemManager so we can access its UseItem() function
        inventoryItems.Add(itemObj.GetComponent<ItemManager>());

        // loops through each item in the inventory to add to the item manager
        // item manager allows us to get the items type which is required when using the item
        for (int i = 0; i < items.Count; i++)
        {
            inventoryItems[i].AddItem(items[i]);
        }
    }

    IEnumerator MagnifyingGlassCooldown(float duration)
    {
        float time = 0;
        while(time < duration)
        {
            canToggleMGlass = false;
            time += Time.deltaTime;
            yield return null;
        }

        if(time >= duration)
        {
            canToggleMGlass = true;
        }
    }
    
    
   
}
