using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float speed = 1f;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>(); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) 
        {
            rb2D.AddForce(collision.transform.right * speed, ForceMode2D.Impulse);
        }
    }
}
