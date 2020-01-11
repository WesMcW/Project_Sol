using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    ChargeAttacks ca;
    PlayerAttack pa;
    PlayerController controller;
    SpriteRenderer spriteRenderer;
    DialogueManager DM;
    public Collider2D myHitBox;
    [SerializeField] Animator anim;

    public bool canDodge = false;
    bool dodging = false;
    
    
    public float moveSpeed;
    public float rollSpeed;

    public float attackingMoveSpeed;    // how fast player moves while swinging sword

    public bool doingSpecialAction = false;
    bool isMoving;
    bool dead;

    int lastDirection = 1;

    private void Awake()
    {
        pa = GetComponent<PlayerAttack>();
        controller = GetComponent<PlayerController>();
        if(GetComponent<ChargeAttacks>() != null) ca = GetComponent<ChargeAttacks>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        DM = DialogueManager.DM;
        pa.sword.SetActive(false);
    }

    void Update(){
        if (doingSpecialAction)
        {
            //spriteRenderer.color = Color.blue;
            if (!dodging) MoveUpdate(attackingMoveSpeed);
        }
        else if (dead || ca.isCharging /* || PauseMenu.GameIsPaused*/)
        {
            controller.Move(Vector2.zero);
            //anim.SetFloat("moveDirection", 0f);
            //Nothing, i'm dead
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            MoveUpdate(1);
            if(!ca.isAttacking) AttackUpdate();
            if (canDodge && !ca.isAttacking) RollUpdate();
            else if (canDodge && ca.isAttacking && ca.activeSkill == 1) RollUpdate();
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

    void MoveUpdate(float modify)
    {
        //MOVE CHARACTER
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity * modify);

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

        //Sends the movement values to the animator
        if (anim != null) anim.SetFloat("moveX", moveInput.x);
        if (anim != null) anim.SetFloat("moveY", moveInput.y);

    }

    void RollUpdate()
    {
        if (!doingSpecialAction && Input.GetMouseButtonDown(1))
        {
            doingSpecialAction = dodging = true;
            spriteRenderer.color = Color.blue;
            StartCoroutine(Roll());
        }
    }

    void AttackUpdate()
    {
        if (Inventory.inventory != null) {
            if (!doingSpecialAction && Input.GetMouseButtonDown(0) && Inventory.inventory.equipSlots[1].itemID != 0)
            {
                doingSpecialAction = true;
                pa.Attack();
            }
        }
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

            yield return new WaitForSeconds(.1f);
        }

        //yield return new WaitForSeconds(.75f);

        myHitBox.enabled = true;
        doingSpecialAction = dodging = false;

    }
   

    

}
