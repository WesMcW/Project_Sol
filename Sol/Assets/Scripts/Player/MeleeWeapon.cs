using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour{

    Sprite sprite;
    SpriteRenderer sr;
    HashSet<GameObject> hits = new HashSet<GameObject>();
    public LayerMask collisionMask;

    float weaponReach = 2f;
    int damage = 1;

    // Start is called before the first frame update
    void Start(){
        sr = GetComponent<SpriteRenderer>();
    }

    public void ClearSet() {
        hits.Clear();
    }

    public void GetStats() {
        sprite = ItemIDManager.instance.GetItem(Inventory.inventory.equipSlots[1].itemID).GetComponent<ItemInfo>().sprite;
        sr.sprite = sprite;
    }

    public void Cast() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, weaponReach, collisionMask);
        if (hit.collider != null){
            OnHitObject(hit);
        }
    }

    void OnHitObject(RaycastHit2D hit) {
        if (hits.Contains(hit.collider.gameObject)) {
            return;
        }
        hits.Add(hit.collider.gameObject);
        IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
        if (damageableObject != null) {
            //Do all the things
            //
            damageableObject.TakeDamage(damage);
            Debug.Log(hit.collider.gameObject.name + " takes " + damage + " damage.");
            
        }
    }


}
