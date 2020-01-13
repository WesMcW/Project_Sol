using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainer : Interactable
{
    [Header("Use the Item ID's for the Loot!")]
    [SerializeField] int[] possibleLoot;
    [Header("How much loot is in the container?")]
    [SerializeField] int lootAmount;
    [SerializeField] bool saveState = false;
    [SerializeField] string saveID = "0";


    void Start() {
        //0 is no interaction, 1 means we've meet before... havent we???
        if (saveState) {
            int state = PlayerPrefs.GetInt("Loot" + saveID, 0);
            if (state == 1){
                Destroy(this.gameObject);
            }
        }
        
    }

    public override void Interact(){
        for (int i = 0; i < lootAmount; i++){
            GameObject tmp = Instantiate(ItemIDManager.instance.GetItem(possibleLoot[Random.Range(0, possibleLoot.Length - 1)]), this.transform.position, Quaternion.identity);
            
        }

        if (saveState) {
            PlayerPrefs.SetInt("Loot" + saveID, 1);
        }

        InteractionManager.IM.StopInteraction();
        Destroy(this.gameObject);
    }
   
}
