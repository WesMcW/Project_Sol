using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CursorInventory : MonoBehaviour
{
    private SpriteRenderer sr;
    private int amount;
    private Text amountText;
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

    public void AddItem(ItemInfo item, int amt)
    {
        sr.sprite = item.sprite;
        sr.color = new Color(100, 100, 100, 0.5f);
        amount = amt;
       // amountText.text = amount.ToString();
        currentItem = item;
    }

    public void RemoveItem()
    {
        sr.sprite = null;
        currentItem = null;
        amount = 0;
       // amountText.text = amount.ToString();
    }

    public ItemInfo GetCurrentItem()
    {
        return currentItem;
    }

    public int GetCurrentAmount()
    {
        return amount;
    }

    public void DecreaseCurrentAmount(int amt)
    {
        amount -= amt;
    }



}
