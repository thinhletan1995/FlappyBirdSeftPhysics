using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public MRigidbody2D rb;
    public Animator animator;
    public float force;
    public float forceTime;
    
    public void MOnColliderEnter2D(MBoxCollider2D collider2D)
    {
        if (GameManager.Instance.State != GameManager.GameState.Playing)
            return;
        
        if (collider2D.tag.Equals("Block"))
        {
            Die();
            GameManager.Instance.State = GameManager.GameState.Dead;
        } else if (collider2D.tag.Equals("Point"))
        {
            GameManager.Instance.audioManager.PlaySFX("Score");
            GameManager.Instance.AddPoint(1);
        }
    }
    
    private void Update()
    {
        switch (GameManager.Instance.State)
        {
            case GameManager.GameState.Waiting:
                if (Input.GetMouseButtonDown(0))
                {
                    GameManager.Instance.State = GameManager.GameState.Playing;
                    GetReady();
                    Jump();
                }
                break;
            
            case GameManager.GameState.Playing:
                if (Input.GetMouseButtonDown(0))
                {
                    Jump();
                }
                break;
        }
    }

    private void Jump()
    {
        GameManager.Instance.audioManager.PlaySFX("Flap");
        rb.AddForce(Vector2.up * force, forceTime);
    }

    private void GetReady()
    {
        rb.Velocity = Vector2.zero;
        rb.gravityScale = 2;
    }

    private void Die()
    {
        GameManager.Instance.audioManager.PlaySFX("Hit");
        rb.Velocity = Vector2.zero;
        rb.gravityScale = 0;
        animator.SetTrigger("Die");
    }
}
