using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Temporary
            //GameObject.FindObjectOfType<CursorInventory>().AddItem(GetComponent<ItemInfo>(), 1);
            Inventory.inventory.AddItemToInventory(GetComponent<ItemInfo>(), 1);
            //Destroy(this.gameObject, 0.2f);
        }
    }
}
