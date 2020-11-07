using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public MRigidbody2D rb;

    [SerializeField] private Transform returnPos;

    public void Setup(Vector2 startPos, float moveSpeed, Transform returnPos = null)
    {
        transform.position = startPos;
        rb.Velocity = Vector2.left * moveSpeed;

        if (returnPos != null)
        {
            this.returnPos = returnPos;
        }
    }

    public void Stop()
    {
        rb.Velocity = Vector2.zero;
    }

    private void Update()
    {
        if (transform.position.x <= returnPos.position.x)
        {
            gameObject.SetActive(false);
        }
    }
}
