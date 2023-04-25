using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField]
    Item item;
    public GameObject text;
    public GameObject magGlass;
    public GameObject TM; // tutorial manager

    private bool canPickUp;

    // adds the item ui to the inventory & destroys its gameObject
    void PickUp()
    {
        InventoryManager.inventory.AddItem(item);
        Destroy(gameObject);

    }

    private void Update()
    {
        // once we are in range of an items trigger & press E, the item is added to the players inventory
        if(Input.GetKeyUp(KeyCode.E) && canPickUp && !magGlass.GetComponent<MagnifyingGlass>().usingMagnifyingGlass)
        {
            PickUp();
            text.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !magGlass.GetComponent<MagnifyingGlass>().usingMagnifyingGlass && !TM.GetComponent<Tutorials>().inTutorial)
        {
            canPickUp = true;
            text.GetComponent<TextMeshProUGUI>().text = "Press [E] to pick up " + item.name;
            text.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canPickUp = false;
            text.SetActive(false);
        }
    }
}
