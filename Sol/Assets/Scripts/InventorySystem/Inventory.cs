using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Slot[] slots;
    public int amountLimit;
    public CursorInventory CI;
    public static Inventory inventory;

    [SerializeField]
    private List<ItemInfo> items;

    private void Start()
    {
        if(inventory != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            inventory = this;
        }

        if(items == null)
        {
            items = new List<ItemInfo>();
        }

    }

    public void AddItemToInventory(ItemInfo theItem, int amount)
    {
       

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].empty)
            {
                //were good to go.
                for (int j = 0; j < amount; j++)
                {

                    items.Add(theItem);
                }
                break;

            } 
        }
        //Adds individual items for the amount there are.
       
       

        for (int i = 0; i < slots.Length; i++)
        {
           
            if (slots[i].empty)
            {
                slots[i].AddItem(theItem, amount);
                break;
            }
        }
    }

   
}
