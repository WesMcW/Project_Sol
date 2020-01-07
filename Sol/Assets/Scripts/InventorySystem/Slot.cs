using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour, IBeginDragHandler, IDropHandler, IPointerClickHandler
{
    public bool empty;
    public int itemID;
    public Sprite defaultImage;
    [SerializeField]
    private int amount;
    private Image img;
    public Inventory inventoryManager;
    private KeyCode switchKey, singleItemKey;

    private TextMeshProUGUI amountText;

    private void Awake()
    {
        img = GetComponent<Image>();
        amountText = GetComponentInChildren<TextMeshProUGUI>();
    }


    void Start()
    {
        if (itemID == 0)
        {
            empty = true;
        }

        if (itemID == -1)
        {
            this.gameObject.SetActive(false);
        }


        amountText.text = amount.ToString();

        if (inventoryManager == null)
        {
            inventoryManager = Inventory.inventory;
        }

        switchKey = inventoryManager.quickSwitchKey;
        singleItemKey = inventoryManager.singleItemKey;

    }

    
    public void OnBeginDrag(PointerEventData eventData)
    {
       // Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.name);
        if (eventData.pointerCurrentRaycast.gameObject == this.gameObject || eventData.pointerCurrentRaycast.gameObject == this.transform.GetChild(0).gameObject)
        {
            //Pick up an item
            //Only if the slot is not empty, pick up the item in here.
            if (itemID != 0)
            {
                inventoryManager.CI.AddItem(itemID, amount);
                RemoveItem();
                print("Picked up: " + inventoryManager.CI.GetCurrentItem());
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
       
       // Debug.Log("Mouse Up: " + eventData.pointerCurrentRaycast.gameObject.name);
        if (eventData.pointerCurrentRaycast.gameObject == this.gameObject || eventData.pointerCurrentRaycast.gameObject == this.transform.GetChild(0).gameObject)
        {
            int amountLimit = ItemIDManager.instance.GetItem(inventoryManager.CI.GetCurrentItem()).GetComponent<ItemInfo>().amountLimit;
            //Place an Item
            //Check if the slot is currently empty or not
            if (itemID == 0)
            {
                //Slot is empty
                AddItem(inventoryManager.CI.GetCurrentItem(), inventoryManager.CI.GetCurrentAmount());
                inventoryManager.CI.RemoveItem();
            }
            else if (itemID == inventoryManager.CI.GetCurrentItem() )
            {
                //They're the samething

                //Check to make sure the amount limit isnt reached if the item is added
                if(inventoryManager.CI.GetCurrentAmount() + amount <= amountLimit)
                {
                    //Add the items
                    IncreaseAmount(inventoryManager.CI.GetCurrentAmount());
                    inventoryManager.CI.RemoveItem();
                } else if(inventoryManager.CI.GetCurrentAmount() + amount > amountLimit)
                {
                    //Add what you can
                    inventoryManager.CI.DecreaseCurrentAmount(amountLimit - amount);
                    IncreaseAmount(amountLimit - amount);
                    inventoryManager.AddItemToInventory(itemID, inventoryManager.CI.GetCurrentAmount());
                    inventoryManager.CI.RemoveItem();
                }
                
            } else if(itemID != inventoryManager.CI.GetCurrentItem())
            {
                //Theyre different
                //Put the dropped item into the slot and put the current new item into the cursor inventory
                int id = itemID;
                int amt = amount;
                AddItem(inventoryManager.CI.GetCurrentItem(), inventoryManager.CI.GetCurrentAmount());
                inventoryManager.CI.AddItem(id, amt);
            }
            else
            {
                //Swap 
                inventoryManager.AddItemToInventory(itemID, amount);
                AddItem(inventoryManager.CI.GetCurrentItem(), inventoryManager.CI.GetCurrentAmount());
                inventoryManager.CI.RemoveItem();
            }
        }
        else
        {
            //Swap 
            inventoryManager.AddItemToInventory(itemID, amount);
            AddItem(inventoryManager.CI.GetCurrentItem(), inventoryManager.CI.GetCurrentAmount());
            inventoryManager.CI.RemoveItem();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if ((eventData.pointerCurrentRaycast.gameObject == this.gameObject || eventData.pointerCurrentRaycast.gameObject == this.transform.GetChild(0).gameObject) && inventoryManager.CI.GetCurrentItem() != 0)
        {
            int amountLimit = ItemIDManager.instance.GetItem(inventoryManager.CI.GetCurrentItem()).GetComponent<ItemInfo>().amountLimit;
            //Place an Item
            //Check if the slot is currently empty or not
            if (itemID == 0)
            {
                //Slot is empty
                AddItem(inventoryManager.CI.GetCurrentItem(), inventoryManager.CI.GetCurrentAmount());
                inventoryManager.CI.RemoveItem();
            }
            else if (itemID == inventoryManager.CI.GetCurrentItem())
            {
                //They're the samething

                //Check to make sure the amount limit isnt reached if the item is added
                if (inventoryManager.CI.GetCurrentAmount() + amount <= amountLimit)
                {
                    //Add the items
                    IncreaseAmount(inventoryManager.CI.GetCurrentAmount());
                    inventoryManager.CI.RemoveItem();
                }
                else if (inventoryManager.CI.GetCurrentAmount() + amount > amountLimit)
                {
                    //Add what you can
                    inventoryManager.CI.DecreaseCurrentAmount(amountLimit - amount);
                    IncreaseAmount(amountLimit - amount);
                    inventoryManager.AddItemToInventory(itemID, inventoryManager.CI.GetCurrentAmount());
                    inventoryManager.CI.RemoveItem();
                }

            }
            else if (itemID != inventoryManager.CI.GetCurrentItem())
            {
                //Theyre different
                //Put the dropped item into the slot and put the current new item into the cursor inventory
                int id = itemID;
                int amt = amount;
                AddItem(inventoryManager.CI.GetCurrentItem(), inventoryManager.CI.GetCurrentAmount());
                inventoryManager.CI.AddItem(id, amt);
            }
            else
            {
                //Swap 
                inventoryManager.AddItemToInventory(itemID, amount);
                AddItem(inventoryManager.CI.GetCurrentItem(), inventoryManager.CI.GetCurrentAmount());
                inventoryManager.CI.RemoveItem();
            }
        }
        
    }
    
    /*
    public void OnPointerDown(PointerEventData eventData)
    {
        //Pick up an item
        //Only if the slot is not empty, pick up the item in here.
        if (itemID != 0)
        {
            inventoryManager.CI.AddItem(itemID, amount);
            RemoveItem();
            print("Picked up: " + inventoryManager.CI.GetCurrentItem());
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        print("here");
        //Place an Item
        //Check if the slot is currently empty or not
        if (itemID == 0)
        {
            print("Slot empty. Add item");
            //Slot is empty
            AddItem(inventoryManager.CI.GetCurrentItem(), inventoryManager.CI.GetCurrentAmount());
            inventoryManager.CI.RemoveItem();
        }
        else if (itemID == inventoryManager.CI.GetCurrentItem())
        {
            //They're the samething
            IncreaseAmount(inventoryManager.CI.GetCurrentAmount());
            inventoryManager.CI.RemoveItem();
            print("theyre the samething");
        }
        else
        {
            //Swap TEMPORARY FUNCTIONALITY
            inventoryManager.AddItemToInventory(itemID, amount);
            AddItem(inventoryManager.CI.GetCurrentItem(), inventoryManager.CI.GetCurrentAmount());
            inventoryManager.CI.RemoveItem();
        }

    }
    */
    public void AddItem(int id, int theAmount)
    {
        //if the item being fed is null. Default to blank
        if (id == 0)
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

    public void DecreaseAmount(int amt)
    {
        amount -= amt;
        if (amount <= 0)
        {
            RemoveItem();
        }
        else
        {
            amountText.text = amount.ToString();
        }

    }

    /*
    public bool empty;
    public int itemID;
    public Sprite defaultImage;
    [SerializeField]
    private int amount;
    private Image img;
    public Inventory inventoryManager;
    private KeyCode switchKey, singleItemKey;

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

        if(inventoryManager == null)
        {
            inventoryManager = Inventory.inventory;
        }

        switchKey = inventoryManager.quickSwitchKey;
        singleItemKey = inventoryManager.singleItemKey;

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

    public void DecreaseAmount(int amt)
    {
        amount -= amt;
        if(amount <= 0)
        {
            RemoveItem();
        }
        else
        {
            amountText.text = amount.ToString();
        }
       
    }

    public void OnClick()
    {

        //Declare the correct variables.
        GameObject theItem = null;
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
        //If the switch key is being pressed AND the current itemID is capable of being quick equipped
        if (Input.GetKey(switchKey) && inventoryManager.CategoryIDCheck(itemID))
        {
            //Send the item here into an empty slot. Save the current information.
            int savedID = itemID;
            int savedAmount = amount;
            RemoveItem();
            inventoryManager.AddItemToEquipment(savedID, savedAmount);
           
            //If the player is holding the single item key, only put 1 item.
        } else if(Input.GetKey(singleItemKey) && theItemID != 0)
        {
            //If slot has same item
            if(theItemID == itemID && (1 + amount) <= theItem.GetComponent<ItemInfo>().amountLimit)
            {
                IncreaseAmount(1);
                inventoryManager.CI.DecreaseCurrentAmount(1);
                //If slot is empty
            } else if(itemID == 0)
            {
                AddItem(theItemID, 1);
                inventoryManager.CI.DecreaseCurrentAmount(1);
            }
            //If different items
            else if (theItemID != itemID)
            {
                inventoryManager.CI.AddItem(itemID, amount);
                AddItem(theItemID, itemAmount);
            }
        }
        //If the player is NOT holding the shift key then do the pickup stuff
        else if (!Input.GetKey(switchKey))
        {
            //If there is an item being held by the cursor and this slot is empty, add the item.
            if (theItemID > 0 && itemID == 0)
            {
                AddItem(theItemID, itemAmount);
                inventoryManager.CI.RemoveItem();
            }
            //If the items are the same between both the cursor and this slot, add them together.
            else if (theItemID == itemID)
            {
                amount += itemAmount;
                inventoryManager.CI.RemoveItem();
                //Checks for a remainder
                if (amount > amountLimit)
                {
                    int leftOver = amount - amountLimit;
                    inventoryManager.CI.AddItem(theItemID, leftOver);
                    amount = amountLimit;
                }
            }
            //If the items are different, swap.
            else if (theItemID != itemID)
            {
                inventoryManager.CI.AddItem(itemID, amount);
                AddItem(theItemID, itemAmount);
            }
            //If the cursor has nothing in it and this slot holds an item
            else if (theItemID == 0 && itemID > 0)
            {

                inventoryManager.CI.AddItem(itemID, amount);
                RemoveItem();
            }
        }
       
      
        //Update Amount Text
        amountText.text = amount.ToString();

    }

    */

}
