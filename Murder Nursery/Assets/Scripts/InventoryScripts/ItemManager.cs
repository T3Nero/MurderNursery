using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private Item item;

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

    // Called when we click an item in our inventory (item type determines the items use functionality)
    public void UseItem()
    {
        switch (item.itemType)
        {
            case Item.ItemType.Bribery:
                if(item.itemName == ("Candy"))
                {
                    Debug.Log("I can bribe someone with this half eaten candy.");
                }
                break;
            case Item.ItemType.Gift:
                Debug.Log(item.itemName);
                RemoveItem();
                break;
            case Item.ItemType.MagnifyingGlass:
                // Used for investigating
                break;
            case Item.ItemType.PinBoard:
                InventoryManager.inventory.UIVisibility.TogglePinboard();
                break;
            case Item.ItemType.Jotter:
                InventoryManager.inventory.UIVisibility.ToggleJotter();
                break;
        }
    }
}
