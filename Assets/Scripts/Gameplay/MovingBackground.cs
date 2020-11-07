using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    public Transform repeatPos;
    public float moveSpeed;

    private Vector2 originPos;

    private void Start()
    {
        originPos = transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        if (transform.position.x <= repeatPos.position.x)
        {
            transform.position = originPos;
        }
    }
}
