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

    public float moveSpeed;
    public float rollSpeed;

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
    //THIS WILL NOT BE ABLE TO SUPPORT MULTIPLE NPC AT ONE TIME
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            DM.FoundNPC(collision.GetComponent<NPC_Dialogue>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            DM.EndConversation();
            DM.RemoveNPC();
        }
    }
   
}
