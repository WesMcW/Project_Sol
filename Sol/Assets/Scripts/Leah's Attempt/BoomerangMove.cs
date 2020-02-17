using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangMove : MonoBehaviour
{
    public GameObject parent;
    GameObject player;
    Vector2 myDirection;
    float speed;
    float damage, critChance, critMulti;

    bool canMove = false, goBack = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            parent.transform.position = Vector3.MoveTowards(parent.transform.position, myDirection, Time.deltaTime * (speed * 7));
            if (Mathf.Abs(Vector2.Distance(myDirection, parent.transform.position)) < 0.5F)
            {
                canMove = false;
                goBack = true;
            }
        }
        else
        {
            if(goBack)
            {
                myDirection = findPlayer();
                parent.transform.position = Vector3.MoveTowards(transform.position, myDirection, Time.deltaTime * (speed * 9));
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

        moveAngle *= new Vector2(6.5F, 6.5F);
        return moveAngle;
    }

    Vector2 findPlayer()
    {
        return new Vector2(player.transform.position.x, player.transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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

        if (goBack && collision.CompareTag("Player"))
        {
            player.GetComponent<ChargeAttacks>().isAttacking = false;
            Destroy(gameObject);
        }
    }
}
