using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float distScaler = 2f;
    Vector3 refVel, mousePos, target;
    float smoothTime = 0.1f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        mousePos = CaptureMousePosition();
        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        Vector3 temp = Vector3.SmoothDamp(transform.position, player.position + mousePos, ref refVel, smoothTime);
        temp.z = -10;
        transform.position = temp;
    }

    Vector3 CaptureMousePosition()
    {
        Vector2 r = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        r *= distScaler;
        r -= Vector2.one;
        return r;
    }
}
