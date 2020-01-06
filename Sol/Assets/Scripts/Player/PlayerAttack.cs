using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    PlayerController controller;
    Player player;

    public GameObject sword;//, testA, testB;
    public Transform weaponRotatePoint;

    public float attackMoveSpeed;
    public float attackRange = 1f;
    public float attackDuration; //Base attack speed

    int attackFlip = 1;

    void Start(){
        controller = GetComponent<PlayerController>();
        player = GetComponent<Player>();
    }

    public void Attack() {
        StartCoroutine(AttackCo());
    }

    IEnumerator AttackCo()
    {
        //Debug.Log("Attacking");
        controller.Move(new Vector2(0f, 0f));
        player.myHitBox.enabled = false;

        Vector2 moveAngle = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 mouseAngle = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        moveAngle.Normalize();

        mouseAngle -= new Vector2(weaponRotatePoint.position.x, weaponRotatePoint.position.y);
        mouseAngle.Normalize();
        if (moveAngle == Vector2.zero)
        {
            moveAngle = mouseAngle;
        }


        //float a = (moveDirection.x + 1) / 2f;
        //float b = moveDirection.y;
        float startTheta = -60*attackFlip;// Mathf.PI * (1 - a / 2f) / (2 * (b + 2));

        Vector2 start = attackRange * RotateVectorByDeg(mouseAngle, startTheta);
        Vector2 end = attackRange * RotateVectorByDeg(mouseAngle, -startTheta);
        //testA.transform.position = weaponRotatePoint.position + new Vector3(start.x, start.y, 0f);
        //testB.transform.position = weaponRotatePoint.position + new Vector3(end.x, end.y, 0f);

        Vector2 current = start;
        //ACTIVATE SWORD
        sword.SetActive(true);
        sword.GetComponent<MeleeWeapon>().GetStats();
        sword.GetComponent<MeleeWeapon>().ClearSet();

        ;
        sword.transform.position = weaponRotatePoint.position + new Vector3(current.x, current.y, 0f);

        float attackTime = attackDuration / (1 + sword.GetComponent<MeleeWeapon>().attackSpeed); //THIS IS DURATION OF ATTACK
        float i = 0;
        float increment = -(startTheta * 2) / (50f * attackTime);

        while (i <= attackTime)
        {

            current = RotateVectorByDeg(current, increment);
            sword.transform.position = weaponRotatePoint.position + new Vector3(current.x, current.y, 0f);

            ///////////////////////////////////////////////////////////
            float angle = Mathf.Atan2(current.y, current.x) * Mathf.Rad2Deg - 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            sword.transform.rotation = q;
            ///////////////////////////////////////////////////////////
            //Do stuff with sword
            sword.GetComponent<MeleeWeapon>().Cast();

            //////////////////////////////////////////////////////////
            controller.Move(moveAngle * attackMoveSpeed);
            i += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        attackFlip *= -1;
        sword.SetActive(false);
        player.myHitBox.enabled = true;
        player.doingSpecialAction = false;
        yield return null;
    }


    //Probly should put this somewhere NOT in player lol
    Vector2 RotateVectorByDeg(Vector2 v, float deg)
    {
        float sin = Mathf.Sin(deg * Mathf.Deg2Rad);
        float cos = Mathf.Cos(deg * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
}
