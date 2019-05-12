using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    
    public float maxSpeed = 10f;
    public float durability;
    SpriteRenderer sp;
    Rigidbody2D rb;
    ParticleSystem ps;
    bool isGrabbed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        ps = GetComponent<ParticleSystem>();
        ps.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized*maxSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.magnitude > durability)
        {
            Breake();
        }
    }

    void Breake()
    {
        ps.Play();
        sp.color = Color.red;
    }
}
