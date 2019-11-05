using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Cooking/Recipe", order = 1)]
public class Recipe : ScriptableObject
{
    [Header("Ingredient ID's, Lowest ID First")]
    public int id;
    [Header("ID matching ID in ItemID Manager")]
    public int finishedProductID;
}
