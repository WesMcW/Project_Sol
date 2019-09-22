using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CursorInventory : MonoBehaviour
{
    private Image img;
    private int amount;
    [SerializeField]
    private ItemInfo currentItem;
    private RectTransform movingObject;
    public Vector3 offset;



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
       
        img.sprite = null;
    }

    private void Update()
    {
        Vector3 pos = Input.mousePosition + offset;
        movingObject.position = pos;
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
    }



}
