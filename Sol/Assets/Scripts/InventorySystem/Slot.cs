using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour, IBeginDragHandler, IDropHandler, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private TextMeshProUGUI descriptionText, nameText;

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Display information
        if(itemID != 0)
        {
            descriptionText.text = ItemIDManager.instance.GetItem(itemID).GetComponent<ItemInfo>().description;
            nameText.text = ItemIDManager.instance.GetItem(itemID).GetComponent<ItemInfo>().name;
        }
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Delete Information
        descriptionText.text = null;
        nameText.text = null;
    }



    public void OnBeginDrag(PointerEventData eventData)
    {
       // Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.name);
        if (eventData.pointerCurrentRaycast.gameObject == this.gameObject || eventData.pointerCurrentRaycast.gameObject == this.transform.GetChild(0).gameObject && eventData.button == PointerEventData.InputButton.Left)
        {
            //Pick up an item
            //Only if the slot is not empty, pick up the item in here.
            if (itemID != 0 && inventoryManager.CI.GetCurrentItem() == 0)
            {
                inventoryManager.CI.AddItem(itemID, amount);
                RemoveItem();
               // print("Picked up: " + inventoryManager.CI.GetCurrentItem());
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
       
       // Debug.Log("Mouse Up: " + eventData.pointerCurrentRaycast.gameObject.name);
        if ((eventData.pointerCurrentRaycast.gameObject == this.gameObject || eventData.pointerCurrentRaycast.gameObject == this.transform.GetChild(0).gameObject) && inventoryManager.CI.GetCurrentItem() != 0 && eventData.button == PointerEventData.InputButton.Left)
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
                    int amt = amountLimit - amount;
                    if (amt == 0)
                    {
                        //Then swap the items
                        int id = itemID;
                        int amt2 = amount;
                        AddItem(inventoryManager.CI.GetCurrentItem(), inventoryManager.CI.GetCurrentAmount());
                        inventoryManager.CI.AddItem(id, amt2);
                    }
                    else
                    {
                        inventoryManager.CI.DecreaseCurrentAmount(amt);
                        IncreaseAmount(amt);
                    }
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
       
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if ((eventData.pointerCurrentRaycast.gameObject == this.gameObject || eventData.pointerCurrentRaycast.gameObject == this.transform.GetChild(0).gameObject) && inventoryManager.CI.GetCurrentItem() != 0 && eventData.button == PointerEventData.InputButton.Left)
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
                    int amt = amountLimit - amount;
                    if(amt == 0)
                    {
                        //Then swap the items
                        int id = itemID;
                        int amt2 = amount;
                        AddItem(inventoryManager.CI.GetCurrentItem(), inventoryManager.CI.GetCurrentAmount());
                        inventoryManager.CI.AddItem(id, amt2);
                    } else
                    {
                        inventoryManager.CI.DecreaseCurrentAmount(amt);
                        IncreaseAmount(amt);
                    }
                   
                   
                    
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

    public void OnPointerDown(PointerEventData eventData)
    {
        if ((eventData.pointerCurrentRaycast.gameObject == this.gameObject || eventData.pointerCurrentRaycast.gameObject == this.transform.GetChild(0).gameObject))
        {
            if (Input.GetKey(switchKey) && inventoryManager.CategoryIDCheck(itemID))
            {
                //Send the item here into an empty slot. Save the current information.
                int savedID = itemID;
                int savedAmount = amount;
                RemoveItem();
                inventoryManager.AddItemToEquipment(savedID, savedAmount);
            } else if (eventData.button == PointerEventData.InputButton.Right)
            {
               //Drop single item
               //only if the slot is empty or the id's match
               if(itemID == 0)
                {
                    //empty slot, put in 1 item
                    AddItem(inventoryManager.CI.GetCurrentItem(), 1);
                    inventoryManager.CI.DecreaseCurrentAmount(1);

                } else if(itemID == inventoryManager.CI.GetCurrentItem() && amount + 1 <= ItemIDManager.instance.GetItem(inventoryManager.CI.GetCurrentItem()).GetComponent<ItemInfo>().amountLimit)
                {
                    //Same item and can add
                    IncreaseAmount(1);
                    inventoryManager.CI.DecreaseCurrentAmount(1);
                }
            }
        }
    }
    
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


}
