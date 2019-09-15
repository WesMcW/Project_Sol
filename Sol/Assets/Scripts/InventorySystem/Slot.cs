using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Slot : MonoBehaviour
{
    public bool empty;
    public int itemID;
    [SerializeField]
    private ItemInfo item;
    public Sprite defaultImage;
    [SerializeField]
    private int amount;
    private Image img;
    public Inventory inventoryManager;

    private TextMeshProUGUI amountText;
    // Start is called before the first frame update
    void Start()
    {
        if(item == null)
        {
            empty = true;
        }
        img = GetComponent<Image>();
        if(itemID == -1)
        {
            this.gameObject.SetActive(false);
        }

        amountText = GetComponentInChildren<TextMeshProUGUI>();
        amountText.text = amount.ToString();

    }

    public void AddItem(ItemInfo theItem, int theAmount)
    {
        item = theItem;
        amount = theAmount;
        img.sprite = item.sprite;
        empty = false;
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
        item = null;
        amount = 0;
        img.sprite = defaultImage;
        itemID = 0;
        empty = true;
        amountText.text = amount.ToString();
    }

    public ItemInfo GetItemInfo()
    {
        return item;
    }


    public void OnClick()
    {
        //Checks to see if the cursor has an item and this slot has no item
        if(inventoryManager.CI.GetCurrentItem() != null && item == null)
        {
            AddItem(inventoryManager.CI.GetCurrentItem(), inventoryManager.CI.GetCurrentAmount());
            inventoryManager.CI.RemoveItem();
        }

        //Checks to see if the items are the same
        else if(inventoryManager.CI.GetCurrentItem() != null && item != null && inventoryManager.CI.GetCurrentItem().name == item.name)
        {
            amount += inventoryManager.CI.GetCurrentAmount();
            inventoryManager.CI.RemoveItem();

            //Checks to make the sure amount is NOT larger than the size cap
            if (amount > inventoryManager.amountLimit)
            {
                Debug.Log("The amount is greater than the limit");
                int leftOver = amount - inventoryManager.amountLimit;
                inventoryManager.CI.AddItem(item, leftOver);
                amount = inventoryManager.amountLimit;
            }

            //Update Amount Text
            amountText.text = amount.ToString();

            //Swapping items in inventory
        } else if(inventoryManager.CI.GetCurrentItem() != null && item != null && inventoryManager.CI.GetCurrentItem().name != item.name)
        {
            //Swap items
            ItemInfo temp = item;
            int tempAmount = amount;

            AddItem(inventoryManager.CI.GetCurrentItem(), inventoryManager.CI.GetCurrentAmount());
            inventoryManager.CI.AddItem(temp, tempAmount);
            //If this slot has an item in it and the cursor has nothing.
        } else if(inventoryManager.CI.GetCurrentItem() == null && item != null)
        {
            inventoryManager.CI.AddItem(item, amount);
            RemoveItem();
        } 
        
    }
}
