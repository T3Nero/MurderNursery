using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager inventory;

    [SerializeField]
    Item pinboard;

    [SerializeField]
    Item jotter;

    [HideInInspector]
    public ToggleUIVisibility UIVisibility;

    [SerializeField]
    public List<Item> items = new List<Item>();

    [SerializeField]
    Transform itemContent;

    [SerializeField]
    GameObject inventoryItem;

    public ItemManager[] inventoryItems;
    public GameObject blur;
    public List<GameObject> invItems;

    private void Awake()
    {
        inventory = this;
        UIVisibility = GetComponent<ToggleUIVisibility>();
    }

    private void Start()
    {
        AddItem(pinboard);
        AddItem(jotter);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.I))
        {
            UIVisibility.ToggleInventory();
            if(blur.activeSelf)
            {
                blur.SetActive(false);
            }
        }
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        SetInventoryItems(item);
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        
    }

    public void SetInventoryItems(Item item)
    {
        
        // adds itemUI to Inventory UI (allows us to see the item in inventory) 
        GameObject itemObj = Instantiate(inventoryItem, itemContent);
        invItems.Add(itemObj);
        var itemIcon = itemObj.transform.Find("ItemIcon").GetComponent<Image>();
        var itemName = itemObj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        itemIcon.sprite = item.icon;
        itemName.text = item.itemName;

        // sets the item in ItemManager so we can access its UseItem() function
        inventoryItems = itemContent.GetComponentsInChildren<ItemManager>();

        for (int i = 0; i < items.Count; i++)
        {
            inventoryItems[i].AddItem(items[i]);
        }
    }

   
    
    
   
}
