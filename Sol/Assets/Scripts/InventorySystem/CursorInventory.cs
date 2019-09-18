using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CursorInventory : MonoBehaviour
{
    private SpriteRenderer sr;
    private int amount;
    [SerializeField]
    private ItemInfo currentItem;

   
   

    /// <summary>
    /// REMEMBER TO MAKE THE CAMERA ORTHOGRAPHIC!
    /// </summary>



    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = null;
    }

    private void Update()
    {
        Vector3 _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(_mousePos.x, _mousePos.y, transform.position.z);

    }


    //Add an item to the cursors inventory.
    public void AddItem(int id, int amt)
    {

        GameObject theItem = ItemIDManager.itemIDmanager.GetItem(id);
        sr.sprite = theItem.GetComponent<ItemInfo>().sprite;
        sr.color = new Color(100, 100, 100, 0.5f);
        amount = amt;
        currentItem = theItem.GetComponent<ItemInfo>();
        
    }


    //Removes Current Item Script
    public void RemoveItem()
    {
        sr.sprite = null;
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
