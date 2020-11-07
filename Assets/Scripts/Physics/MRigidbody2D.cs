using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MRigidbody2D : MonoBehaviour
{
    public float gravityScale = 1;
    
    public Vector2 Velocity
    {
        get
        {
            return (velocity + Vector2.down * gravityScale) * Time.deltaTime * collisionTime;
        }

        set
        {
            velocity = value;
            lastForce = Vector2.zero;
        }
    }
    [SerializeField] private Vector2 velocity;
    
    private float collisionTime = 1;
    private float forceDuration = 0;
    private Vector2 lastForce = Vector2.zero;    
    
    public void AddForce(Vector2 force, float duration = 0.5f)
    {
        velocity -= lastForce;
        forceDuration = duration;
        velocity += force;
        lastForce = force;
    }
    
    public void OnCheckingCollision(float entryTime, bool isTrigger, MBoxCollider2D collider2D)
    {
        if (entryTime > 0 && entryTime < 1)
        {
            if (!isTrigger && entryTime < collisionTime)
            {
                collisionTime = entryTime;
            }
            
            SendMessage("MOnColliderEnter2D", collider2D, SendMessageOptions.DontRequireReceiver);
        }
    }
    
    private void FixedUpdate()
    {
        transform.position += (Vector3) Velocity; // * collisionTime;
        collisionTime = 1;
    }

    private void Update()
    {
        if (forceDuration > 0)
        {
            forceDuration -= Time.deltaTime;
            if (forceDuration <= 0)
            {
                velocity -= lastForce;
                lastForce = Vector2.zero;
            }
        }
    }

    private void Reset()
    {
        var componets = GetComponents<MRigidbody2D>();
        if (componets.Length > 1)
        {
            DestroyImmediate(this);
            Debug.LogError("Can not add more than one MRigidbody2D in this gameobject!");
            return;
        }
    }
}
