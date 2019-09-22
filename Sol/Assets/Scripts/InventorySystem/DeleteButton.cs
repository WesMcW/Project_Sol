using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteButton : MonoBehaviour
{
    public void OnClick()
    {
        Inventory.inventory.CI.RemoveItem();
    }
}
