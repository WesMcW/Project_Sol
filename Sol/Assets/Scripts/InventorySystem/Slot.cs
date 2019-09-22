using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Slot : MonoBehaviour
{
    public bool empty;
    public int itemID;
    public Sprite defaultImage;
    [SerializeField]
    private int amount;
    private Image img;
    public Inventory inventoryManager;

    private TextMeshProUGUI amountText;
    // Start is called before the first frame update

    private void Awake()
    {
        img = GetComponent<Image>();
        amountText = GetComponentInChildren<TextMeshProUGUI>();
    }


    void Start()
    {
        if(itemID == 0)
        {
            empty = true;
        }
       
        if(itemID == -1)
        {
            this.gameObject.SetActive(false);
        }

        
        amountText.text = amount.ToString();

    }

    public void AddItem(int id, int theAmount)
    {
        //if the item being fed is null. Default to blank
        if(id == 0)
        {
            itemID = 0;
            empty = true;
            img.sprite = defaultImage;
            amount = 0;
        }
        else
        {
            GameObject theItem = ItemIDManager.instance.GetItem(id);
            amount = theAmount;
            img.sprite = theItem.GetComponent<ItemInfo>().sprite;
            empty = false;
        }
        itemID = id;
        amountText.text = amount.ToString();
    }

    public int GetAmount()
    {
        return amount;
    }

    public void IncreaseAmount(int amt)
    {
        amount += amt;
        amountText.text = amount.ToString();
    }

    public void RemoveItem()
    {
        amount = 0;
        img.sprite = defaultImage;
        itemID = 0;
        empty = true;
        amountText.text = amount.ToString();
    }

    public void OnClick()
    {
        
        //Declare the correct variables.
        GameObject theItem;
        //These MUST be declared as 0. 
        int theItemID = 0;
        int itemAmount = 0;
        int amountLimit = 0;//Intentianaly break with 0
        //Checks if there is an item in your curser
        if (inventoryManager.CI.GetCurrentItem() != 0)
        {
            
            theItem = ItemIDManager.instance.GetItem(inventoryManager.CI.GetCurrentItem());
            theItemID = theItem.GetComponent<ItemInfo>().ItemID;
            itemAmount = inventoryManager.CI.GetCurrentAmount();
            amountLimit = theItem.GetComponent<ItemInfo>().amountLimit;
        }
       
        //If there is an item being held by the cursor and this slot is empty, add the item.
        if (theItemID > 0 && itemID == 0)
        {
            AddItem(theItemID, itemAmount);
            inventoryManager.CI.RemoveItem();
        }
        //If the items are the same between both the cursor and this slot, add them together.
        else if(theItemID == itemID)
        {
            amount += itemAmount;
            inventoryManager.CI.RemoveItem();
            //Checks for a remainder
            if(amount > amountLimit)
            {
                int leftOver = amount - amountLimit;
                inventoryManager.CI.AddItem(theItemID, leftOver);
                amount = amountLimit;
            }
        }
        //If the items are different, swap.
        else if(theItemID != itemID)
        {
            inventoryManager.CI.AddItem(itemID, amount);
            AddItem(theItemID, itemAmount);
        }
        //If the cursor has nothing in it and this slot holds an item
        else if(theItemID == 0 && itemID > 0)
        {
            
            inventoryManager.CI.AddItem(itemID, amount);
            RemoveItem();
        }
        //Update Amount Text
        amountText.text = amount.ToString();

    }


   
}
