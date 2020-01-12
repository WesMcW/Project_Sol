using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class CursorInventory : MonoBehaviour
{
    private Image img;
    private int amount;
    [SerializeField]
    private ItemInfo currentItem;
    private RectTransform movingObject;
    public Vector3 offset;
    [SerializeField]
    private Transform playerTransform;


    /// <summary>
    /// REMEMBER TO MAKE THE CAMERA ORTHOGRAPHIC!
    /// </summary>


    private void Awake()
    {
        movingObject = GetComponent<RectTransform>();
        img = GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        img.sprite = null;
    }

    /*
    private void OnDisable()
    {
        if (this.enabled)
        {
            //When the inventory menu is disabled, dumb the item held.
            if (currentItem != null)
            {
                GameObject tmp = Instantiate(ItemIDManager.instance.GetItem(currentItem.ItemID), playerTransform.position, Quaternion.identity);
                tmp.GetComponent<ItemInfo>().amount = amount;
                tmp.GetComponent<ItemInfo>().canBePickedUp = false;
                RemoveItem();
            }
        }
       
       
    }
    */
   
    private void Update()
    {
        Vector3 pos = Input.mousePosition + offset;
        movingObject.position = pos;

        //Check for the click of an item NOT in the UI. 
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && currentItem != null)
        {
            GameObject tmp = Instantiate(ItemIDManager.instance.GetItem(currentItem.ItemID), new Vector3(Camera.main.ScreenToWorldPoint(pos - offset).x, Camera.main.ScreenToWorldPoint(pos - offset).y, 0), Quaternion.identity);
            tmp.GetComponent<ItemInfo>().amount = amount;
            tmp.GetComponent<ItemInfo>().canBePickedUp = true;
            RemoveItem();
        }
    }


    //Add an item to the cursors inventory.
    public void AddItem(int id, int amt)
    {

        GameObject theItem = ItemIDManager.instance.GetItem(id);
        //Debug.Log(theItem);
        img.sprite = theItem.GetComponent<ItemInfo>().sprite;
        img.color = new Color(100, 100, 100, 0.5f);
        amount = amt;
        currentItem = theItem.GetComponent<ItemInfo>();
        
    }


    //Removes Current Item Script
    public void RemoveItem()
    {
        img.sprite = null;
        img.color = new Color(100, 100, 100, 0);
        currentItem = null;
        amount = 0;
    }


    //Yields the proper ID
    public int GetCurrentItem()
    {
        if(currentItem == null)
        {
            return 0;
        } else
        {
            return currentItem.ItemID;
        }
    }

    //Returns the current amount being held in the cursor
    public int GetCurrentAmount()
    {
        return amount;
    }

    public void DecreaseCurrentAmount(int amt)
    {
        amount -= amt;
       // print("amount:" + amount);
        //Empty so remove item
        if(amount <= 0)
        {
            RemoveItem();
        }
    }



}
