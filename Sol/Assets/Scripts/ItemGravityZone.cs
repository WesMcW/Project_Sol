using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGravityZone : MonoBehaviour
{
    [SerializeField] float speed;
    private ItemInfo parentInfo;
    [SerializeField] private Transform parent;

    private void Awake()
    {
        parentInfo = GetComponentInParent<ItemInfo>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (parentInfo.canBePickedUp && collision.CompareTag("Player"))
        {
            //Debug.Log("Player detected");
            //Gravitate towards player
            parent.position = Vector3.Lerp(parent.position, collision.gameObject.transform.position, speed * Time.deltaTime);
        }
    }
}
