using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public BoxCollider2D boundsCollider;

    private float halfHeight;
    private float halfWidth;
    private Bounds bounds;

    void Start()
    {
        Camera cam = Camera.main;
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;

        if (boundsCollider != null )
        {
            bounds = boundsCollider.bounds;
        }
    }

    void LateUpdate()
    {
        if (player == null || boundsCollider == null) return;

        Vector3 newPos = player.position;
        newPos.z = transform.position.z;

        //clamp camera within bounds
        float clampedX = Mathf.Clamp(newPos.x, bounds.min.x + halfWidth, bounds.max.x - halfWidth);
        float clampedY = Mathf.Clamp(newPos.y, bounds.min.y + halfHeight, bounds.max.y - halfHeight);

        transform.position = new Vector3(clampedX, clampedY, newPos.z);
    }
}
