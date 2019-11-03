using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    PlayerController controller;
    SpriteRenderer spriteRenderer;
    DialogueManager DM;
    public Collider2D myHitBox;

    public bool canDodge = false;

    public GameObject sword;//, testA, testB;
    public Transform weaponRotatePoint;
    public float moveSpeed;
    public float rollSpeed;
    public float attackMoveSpeed;
    public float attackRange = 1f;
    public float attackDuration;

    bool doingSpecialAction = false;
    bool isMoving;
    bool dead;

    int lastDirection = 1;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        DM = DialogueManager.DM;
        sword.SetActive(false);
    }

    void Update(){
        if (doingSpecialAction)
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else if (dead /* || PauseMenu.GameIsPaused*/)
        {
            controller.Move(Vector2.zero);
            //anim.SetFloat("moveDirection", 0f);
            //Nothing, i'm dead
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            MoveUpdate();
            AttackUpdate();
            if (canDodge) RollUpdate();
        }
    }

    //currently called in functions not in update
    void FlipUpdate(Vector2 moveInput) {
        float bob;
        if (moveInput.x == 0)
        {
            bob = lastDirection;
        }
        else {
            bob = moveInput.x;
            lastDirection = (int)Mathf.Clamp(moveInput.x * 100, -1, 1);
        }

        float rotation = Mathf.Atan2(bob, moveInput.y) * Mathf.Rad2Deg - 90;
        if (rotation > 90f || rotation < -90f)
        {
            spriteRenderer.flipX = true;
            //anim.SetFloat("faceDirection", -1f);
        }
        else
        {
            spriteRenderer.flipX = false;
            //anim.SetFloat("faceDirection", 1f);
        }
    }

    void MoveUpdate()
    {
        //MOVE CHARACTER
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

        if (moveVelocity.magnitude > 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        
        FlipUpdate(moveInput);

        /*
        //ANIMATOR MOVEMENT DIRECTION
        anim.SetFloat("moveDirection", moveInput.x);
        if (moveInput.x == 0)
        {
            anim.SetFloat("moveDirection", moveInput.y);
        }
        */
    }

    void RollUpdate()
    {
        if (!doingSpecialAction && Input.GetMouseButtonDown(1))
        {
            doingSpecialAction = true;
            StartCoroutine(Roll());
        }
    }

    void AttackUpdate()
    {
        if (Inventory.inventory != null) {
            if (!doingSpecialAction && Input.GetMouseButtonDown(0) && Inventory.inventory.equipSlots[1].itemID != 0)
            {
                doingSpecialAction = true;
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack() {
        //Debug.Log("Attacking");
        controller.Move(new Vector2(0f, 0f));
        myHitBox.enabled = false;

        Vector2 moveAngle = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 mouseAngle = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        moveAngle.Normalize();

        mouseAngle -= new Vector2(weaponRotatePoint.position.x, weaponRotatePoint.position.y);
        mouseAngle.Normalize();
        if (moveAngle == Vector2.zero){
            moveAngle = mouseAngle;
        }


        //float a = (moveDirection.x + 1) / 2f;
        //float b = moveDirection.y;
        float startTheta = -60;// Mathf.PI * (1 - a / 2f) / (2 * (b + 2));
        
        Vector2 start = attackRange*RotateVectorByDeg(mouseAngle, startTheta);
        Vector2 end = attackRange*RotateVectorByDeg(mouseAngle, -startTheta);
        //testA.transform.position = weaponRotatePoint.position + new Vector3(start.x, start.y, 0f);
        //testB.transform.position = weaponRotatePoint.position + new Vector3(end.x, end.y, 0f);

        float attackTime = attackDuration; //THIS IS DURATION OF ATTACK
        float i = 0;
        float increment = -(startTheta*2) / (50f * attackTime);
        Vector2 current = start;
        //ACTIVATE SWORD
        sword.SetActive(true);
        sword.GetComponent<MeleeWeapon>().GetStats();
        sword.GetComponent<MeleeWeapon>().ClearSet();
        sword.transform.position = weaponRotatePoint.position + new Vector3(current.x, current.y, 0f);


        while (i <= attackTime) {

            current = RotateVectorByDeg(current, increment);
            sword.transform.position = weaponRotatePoint.position + new Vector3(current.x, current.y, 0f);

            ///////////////////////////////////////////////////////////
            float angle = Mathf.Atan2(current.y, current.x) * Mathf.Rad2Deg -90;
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

        sword.SetActive(false);
        myHitBox.enabled = true;
        doingSpecialAction = false;
        yield return null;
    }

    IEnumerator Roll()
    {
        controller.Move(new Vector2(0f, 0f));
        myHitBox.enabled = false;


        //BEGIN MOVEMENT
        //anim.SetTrigger("roll");
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (moveInput == new Vector2(0, 0))
        {
            //moveInput = new Vector2(anim.GetFloat("faceDirection"), 0);
        }
        Vector2 moveVelocity = moveInput.normalized * rollSpeed;
        controller.Move(moveVelocity);



        for (float i = 0; i < 0.75; i += .25f)
        {
            if (new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) != new Vector2(0, 0))
            {
                moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            }
            moveVelocity = moveInput.normalized * rollSpeed;
            controller.Move(moveVelocity * (2 - 2 * i - .45f));

            FlipUpdate(moveInput);
            /*
            //ANIMATION FACE DIRECTION
            float rotation = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg - 90;
            if (rotation > 90f || rotation < -90f)
            {
                spriteRenderer.flipX = true;
                //anim.SetFloat("faceDirection", -1f);
            }
            else
            {
                spriteRenderer.flipX = false;
                //anim.SetFloat("faceDirection", 1f);
            }
            */

            yield return new WaitForSeconds(.25f);
        }

        //yield return new WaitForSeconds(.75f);

        myHitBox.enabled = true;
        doingSpecialAction = false;

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
