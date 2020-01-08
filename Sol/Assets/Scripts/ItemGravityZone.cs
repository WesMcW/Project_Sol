using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGravityZone : MonoBehaviour
{
    [SerializeField] float speed;
    private ItemInfo parentInfo;
    [SerializeField] private Transform parent;
    bool gravitate;
    Transform player;

    private void Awake()
    {
        parentInfo = GetComponentInParent<ItemInfo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (parentInfo.canBePickedUp && collision.CompareTag("Player"))
        {
            //Debug.Log("Player detected");
            player = collision.transform;
            gravitate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (parentInfo.canBePickedUp && collision.CompareTag("Player"))
        {
            //Debug.Log("Player detected");
            gravitate = false;
        }
    }

    private void Update()
    {
        if (gravitate)
        {
            //go towards player
            //Gravitate towards player
            parent.position = Vector3.Lerp(parent.position, player.position, speed * Time.deltaTime);
        }
    }
}
