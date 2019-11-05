using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookButton : MonoBehaviour
{
    [SerializeField]
    EquipmentSlot slot1, slot2;

    [SerializeField] Transform player;

    private void Start()
    {
        if(player == null)
        {
            GameObject.FindGameObjectWithTag("Player");
        }
    }
    public void OnClick()
    {
        if(slot1.empty || slot2.empty)
        {
            //Empty
            print("A slot is empty");
        }else
        {
            CookingManager.CM.StartCooking(slot1.itemID, slot2.itemID);
            //Not empty
            slot1.DecreaseAmount(1);
            //print("Slot 1 Amount: " + slot1.GetAmount());
            slot2.DecreaseAmount(1);
            //print("Slot 2 Amount: " + slot2.GetAmount());

            //Check if slot1 is empty
            if (!slot1.empty)
            {
                //Check if first item can be added
                if(Inventory.inventory.CanAddItem(slot1.itemID, slot1.GetAmount()))
                {
                    Inventory.inventory.AddItemToInventory(slot1.itemID, slot1.GetAmount());
                    slot1.RemoveItem();

                }
                else
                {
                    //Drop the ingredient
                    GameObject tmp = Instantiate(ItemIDManager.instance.GetItem(slot1.itemID), player.position, Quaternion.identity);
                    tmp.GetComponent<ItemInfo>().amount = slot1.GetAmount();
                }
            }

            //Check if slot2 is empty
            if (!slot2.empty)
            {
                //Check to see if the second item can be added
                if (Inventory.inventory.CanAddItem(slot2.itemID, slot2.GetAmount()))
                {
                    Inventory.inventory.AddItemToInventory(slot2.itemID, slot2.GetAmount());
                    slot2.RemoveItem();
                }
                else
                {
                    //Drop the ingredient
                    GameObject tmp = Instantiate(ItemIDManager.instance.GetItem(slot2.itemID), player.position, Quaternion.identity);
                    tmp.GetComponent<ItemInfo>().amount = slot2.GetAmount();
                }
            }
          
            CookingManager.CM.CloseIngredientMenu();
        }
       
    }
}
