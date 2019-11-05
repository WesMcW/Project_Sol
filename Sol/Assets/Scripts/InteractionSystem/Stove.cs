using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : Interactable
{
    public override void Interact()
    {
        CookingManager.CM.OpenIngredientMenu();
    }
}
