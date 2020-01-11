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
    public float attackSpeed = 0; //Additional attack speed

    float critChance = 0;
    float critMultiplier = 0;

    //float blockChance = 0;
    float stunChance = 0;

    SetSkills skills;

    // Start is called before the first frame update
    void Start(){
        sr = GetComponent<SpriteRenderer>();
        if(SkillsManager.inst != null) skills = SkillsManager.inst.gameObject.GetComponent<SetSkills>();
    }

    public void ClearSet() {
        hits.Clear();
    }

    public void GetStats() {
        WeaponInfo info = ItemIDManager.instance.GetItem(Inventory.inventory.equipSlots[1].itemID).GetComponent<WeaponInfo>();
        sr.sprite = info.sprite;

        damage = info.damage;
        attackSpeed = info.attackSpeed + skills.attackSpeed;
        critChance = info.critChance + skills.critChance;
        critMultiplier = info.critMultiplier + skills.critDmg;
        //blockChance
        stunChance = info.stunChance + skills.stunChance;
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
        IEnemy enemy = hit.collider.GetComponent<IEnemy>();
        if (enemy != null) {
            //Do all the things
            if (Random.Range(0f, 1f) < critChance) {
                enemy.TakeDamage((int)(damage * critMultiplier));
            }
            else{
                enemy.TakeDamage(damage);
            }
            
            //Debug.Log(hit.collider.gameObject.name + " takes " + damage + " damage.");
            
        }
    }


}
