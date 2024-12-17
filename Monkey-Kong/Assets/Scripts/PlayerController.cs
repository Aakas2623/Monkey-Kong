using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Collider2D col;
    private Collider2D[] results;
    private Vector2 direction;
    public float moveSpeed = 1f;
    public float jumpStrength = 1f;

    private bool grounded;
    private bool climbing;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        results = new Collider2D[4];
    }

    private void Update()
    {
        CheckCollision();

        if (climbing)
        {
            direction.y = Input.GetAxis("Vertical") * moveSpeed;
        }
        else if(grounded && Input.GetButtonDown("Jump"))
        {
            direction = Vector2.up * jumpStrength;
        }
        else
        {
            direction += Physics2D.gravity * Time.deltaTime;
        }

        direction.x = Input.GetAxis("Horizontal") * moveSpeed;

        if (grounded)
        {
            direction.y = Mathf.Max(direction.y, -1f);
        }

        if(direction.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if(direction.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + direction * Time.fixedDeltaTime);
    }

    private void CheckCollision()
    {
        grounded = false;
        climbing = false;

        Vector2 size = col.bounds.size;
        size.y += 0.1f;
        size.x /= 2f;

        int amount = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0f, results);

        for (int i = 0; i < amount; i++)
        {
            GameObject hit = results[i].gameObject;

            if (hit.layer == LayerMask.NameToLayer("Ground"))
            {
                grounded = hit.transform.position.y < (transform.position.y - 0.5f);
                Physics2D.IgnoreCollision(col, results[i], !grounded);
            }
            else if (hit.layer == LayerMask.NameToLayer("Ladder"))
            {
                climbing = true;
            }
        }
    }
}
