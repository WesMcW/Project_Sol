﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIDManager : MonoBehaviour
{

    #region Singleton 

    public static ItemIDManager instance;



    [Header("Weapons: 1")]
    [SerializeField]
    GameObject[] weapons;

    [Header("Armor: 2")]
    [SerializeField]
    GameObject[] armor;

    [Header("Ingredients: 3")]
    [SerializeField]
    GameObject[] ingredients;

    [Header("Food: 4")]
    [SerializeField]
    GameObject[] food;

    //to add another category add another category as apove and add to jagged array in order

    //Jagged Hash Array
    GameObject[][] array;


    void Awake(){
        if (instance != null){
            Debug.LogWarning("More than one instance of ItemIDManager found");
            Destroy(gameObject);
            return;
        }
        instance = this;
        array = new GameObject[][] {
            weapons,
            armor,
            ingredients,
            food
        };
    }

    #endregion

    void Start() {
       

    }

    //Call this to get item: ItemIDManager.instance.GetItem( idnumber );
    public GameObject GetItem(int id) {
        int type = GetTypeInt(id);
        //print(id);
        if (id == 0 || id == -1)
        {
            return null;
        }
        //if there are errors, switch the order of array parameters
        return array[type - 1][GetID(type, id)];

    }

    int GetTypeInt(int id) {
        string s = id.ToString();
        int i = int.Parse(s.Substring(0, 1));
        return i;
    }

    int GetID(int firstValue, int i) {
        int id = i - firstValue * (int)Mathf.Pow(10, CountDigits(i) - 1);
        return id;
    }

    int CountDigits(int i) {
        int count = 0;
        while (i != 0) {
            i = i / 10;
            count++;
        }
        return count;
    }
}
