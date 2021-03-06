﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangMove : MonoBehaviour
{
    public GameObject parent;
    GameObject player;
    Vector2 myDirection;
    float speed;
    float damage, critChance, critMulti;

    float returnTime = 1;
    float cameraSize = 6;

    bool canMove = false, goBack = false;

    void Update()
    {
        if (canMove)
        {
            parent.transform.position = Vector3.MoveTowards(parent.transform.position, myDirection, Time.deltaTime * (speed * 12));
            if (Mathf.Abs(Vector2.Distance(myDirection, parent.transform.position)) < 0.3F)
            {
                canMove = false;
                goBack = true;
            }
        }
        else
        {
            if(goBack)
            {
                returnTime = (Time.deltaTime + 1) * 1.8F;

                myDirection = findPlayer();
                parent.transform.position = Vector3.MoveTowards(transform.position, myDirection, Time.deltaTime * (speed * 15 * returnTime));
                if (Mathf.Abs(Vector2.Distance(myDirection, parent.transform.position)) < 0.5F)
                {
                    player.GetComponent<ChargeAttacks>().isAttacking = false;
                    Destroy(transform.parent.gameObject);
                }
            }
        }
    }

    public void setValues(GameObject p, Sprite s, float attSpd, float[] values)
    {
        player = p;
        if (s != null) GetComponent<SpriteRenderer>().sprite = s;
        speed = attSpd + 1;

        damage = values[0];
        critChance = values[1];
        critMulti = values[2];

        myDirection = findDirection();
        canMove = true;
    }

    Vector2 findDirection()
    {
        Vector2 moveAngle = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 mouseAngle = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        moveAngle.Normalize();

        mouseAngle -= new Vector2(parent.transform.position.x, parent.transform.position.y);
        mouseAngle.Normalize();

        if (moveAngle == Vector2.zero)
        {
            moveAngle = mouseAngle;
        }

        cameraSize = Camera.main.GetComponent<Camera>().orthographicSize * 1.5F;

        //Debug.Log("Angle: (" + moveAngle.x + ", " + moveAngle.y + ")");
        moveAngle *= new Vector2(cameraSize, cameraSize);
        moveAngle += new Vector2(parent.transform.position.x, parent.transform.position.y);
        //Debug.Log("After multi: (" + moveAngle.x + ", " + moveAngle.y + ")");

        return moveAngle;
    }

    Vector2 findPlayer()
    {
        return new Vector2(player.transform.position.x, player.transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit something");

        IEnemy enemy = collision.gameObject.GetComponent<IEnemy>();
        if (enemy != null)
        {
            Debug.Log("enemy hit");
            if (Random.Range(0f, 1f) < critChance)
            {
                enemy.TakeDamage((int)(damage * critMulti));
            }
            else
            {
                enemy.TakeDamage(Mathf.RoundToInt(damage));
            }
            // start enemy cooldown
        }

        /*if (goBack && collision.CompareTag("Player"))
        {
            player.GetComponent<ChargeAttacks>().isAttacking = false;
            Destroy(gameObject);
        }*/
    }
}
