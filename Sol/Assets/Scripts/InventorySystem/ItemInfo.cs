using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public string name;
    [TextArea(3, 5)]
    public string description;
    public Sprite sprite;
    public int ItemID;
    public int amountLimit = 1;
    public bool canBePickedUp = true;

    //Add one of the current item to the players inventory.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Inventory.inventory.CanAddItem(ItemID, 1) && canBePickedUp)
        {
            Inventory.inventory.AddItemToInventory(ItemID, 1);
            Destroy(this.gameObject);
        }
    }

    //This is for dropped items.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canBePickedUp = true;
        }
    }

}
