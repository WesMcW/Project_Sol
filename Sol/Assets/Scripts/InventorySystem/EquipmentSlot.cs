using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class EquipmentSlot : MonoBehaviour, IBeginDragHandler, IDropHandler, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private TextMeshProUGUI descriptionText, nameText;
    public bool empty;
    [HideInInspector]
    public int itemID;
    private KeyCode switchKey, singleItemKey;
    [Header("Required Category ID")]
    public char requiredID;
    [Header("Default UI Image")]
    public Sprite defaultImage;
    private Image img;
    private Inventory inventoryManager;
    private int amount;
    // Start is called before the first frame update

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    private void Start()
    {
        inventoryManager = Inventory.inventory;
        switchKey = inventoryManager.quickSwitchKey;
        singleItemKey = inventoryManager.singleItemKey;
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
            img.sprite = theItem.GetComponent<ItemInfo>().sprite;
            empty = false;
            amount = theAmount;
        }
        itemID = id;  
    }

    public void RemoveItem()
    {
        img.sprite = defaultImage;
        itemID = 0;
        empty = true;
        amount = 0;
    }

    public int GetAmount()
    {
        return amount;
    }
    public void IncreaseAmount(int amt)
    {
        amount += amt;
      //  amountText.text = amount.ToString();
    }

    public void DecreaseAmount(int amt)
    {
        amount -= amt;
        if(amount <= 0)
        {
            RemoveItem();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Display information
        //Also check to make sure there is text to update
        if (itemID != 0 && descriptionText != null)
        {
            descriptionText.text = ItemIDManager.instance.GetItem(itemID).GetComponent<ItemInfo>().description;
            nameText.text = ItemIDManager.instance.GetItem(itemID).GetComponent<ItemInfo>().name;
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Checks to make sure there is text to update
        if (descriptionText != null)
        {
            //Delete Information
            descriptionText.text = null;
            nameText.text = null;
        }
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
        if (eventData.pointerCurrentRaycast.gameObject == this.gameObject || eventData.pointerCurrentRaycast.gameObject == this.transform.GetChild(0).gameObject && eventData.button == PointerEventData.InputButton.Left)
        {
            if (CheckCategory(inventoryManager.CI.GetCurrentItem()))
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
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if ((eventData.pointerCurrentRaycast.gameObject == this.gameObject || eventData.pointerCurrentRaycast.gameObject == this.transform.GetChild(0).gameObject) && inventoryManager.CI.GetCurrentItem() != 0 && eventData.button == PointerEventData.InputButton.Left)
        {
            if (CheckCategory(inventoryManager.CI.GetCurrentItem()))
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

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if ((eventData.pointerCurrentRaycast.gameObject == this.gameObject || eventData.pointerCurrentRaycast.gameObject == this.transform.GetChild(0).gameObject))
        {
            if (Input.GetKey(switchKey) && inventoryManager.CanAddItem(itemID, amount))
            {
                //Send the item here into an empty slot. Save the current information.
                int savedID = itemID;
                int savedAmount = amount;
                RemoveItem();
                inventoryManager.AddItemToInventory(savedID, savedAmount);
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                //Drop single item
                //only if the slot is empty or the id's match
                if (itemID == 0)
                {
                    //empty slot, put in 1 item
                    AddItem(inventoryManager.CI.GetCurrentItem(), 1);
                    inventoryManager.CI.DecreaseCurrentAmount(1);

                }
                else if (itemID == inventoryManager.CI.GetCurrentItem() && amount + 1 <= ItemIDManager.instance.GetItem(inventoryManager.CI.GetCurrentItem()).GetComponent<ItemInfo>().amountLimit)
                {
                    //Same item and can add
                    IncreaseAmount(1);
                    inventoryManager.CI.DecreaseCurrentAmount(1);
                }
            }
        }
    }

    /// <summary>
    /// Checks the id to make sure it matches the slot required ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    bool CheckCategory(int id)
    {
        char categoryID = '0';
        categoryID = id.ToString()[0];
        if(requiredID == categoryID)
        {
            return true;
        } else
        {
            return false;
        }
    }
   
}
