using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public float speed;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private float inputX;

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");

        //walk animation toggle        
        animator.SetBool("isWalking", inputX != 0);

        //flip the sprite
        if (inputX > 0)
            spriteRenderer.flipX = false;
        else if (inputX < 0)
            spriteRenderer.flipX = true;
    }

    void FixedUpdate()
    {
        playerRb.velocity = new Vector2 (inputX * speed, 0f);
    }
}
