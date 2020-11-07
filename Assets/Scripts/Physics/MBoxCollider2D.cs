using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBoxCollider2D : MonoBehaviour
{
    public Vector2 offset;
    public Vector2 size;
    public bool isTrigger = false;

    public Vector2 Position
    {
        get => (Vector2)transform.position + offset;
    }

    public float LeftBound => Position.x - size.x / 2;
    public float RightBound => Position.x + size.x / 2;
    public float TopBound => Position.y + size.y / 2;
    public float BottomBound => Position.y - size.y / 2;

    public Rect ExportToRect()
    {
        return new Rect(Position, size);
    }
    
    public Vector2 DX(float xDirection, MBoxCollider2D other)
    {
        Vector2 result = Vector2.zero;
        
        if (xDirection > 0)
        {
            result.x = other.LeftBound - RightBound;
            result.y = other.RightBound - LeftBound;
        }
        else
        {
            result.x = other.RightBound - LeftBound;
            result.y = other.LeftBound - RightBound;
        }

        return result;
    }
    
    public Vector2 DY(float yDirection, MBoxCollider2D other)
    {
        Vector2 result = Vector2.zero;
        
        if (yDirection > 0)
        {
            result.x = other.BottomBound - TopBound;
            result.y = other.TopBound - BottomBound;
        }
        else
        {
            result.x = other.TopBound - BottomBound;
            result.y = other.BottomBound - TopBound;
        }

        return result;
    }
    
    private void Awake()
    {
        PhysicHandler.Instance.Add(this);
    }

    private void OnDestroy()
    {
        PhysicHandler.Instance?.Remove(this);
    }

    private void Reset()
    {
        var componets = GetComponents<MBoxCollider2D>();
        if (componets.Length > 1)
        {
            DestroyImmediate(this);
            Debug.LogError("Can not add more than one MBoxCollider2D in this gameobject!");
            return;
        }
        
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            size = sr.bounds.size;
        }
        else
        {
            size = Vector2.one / 2;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((Vector2)transform.position + offset, size);
        
        Gizmos.DrawSphere(new Vector3(LeftBound, Position.y, 0), 0.02f);
        Gizmos.DrawSphere(new Vector3(RightBound, Position.y, 0), 0.02f);
        Gizmos.DrawSphere(new Vector3(Position.x, TopBound, 0), 0.02f);
        Gizmos.DrawSphere(new Vector3(Position.x, BottomBound, 0), 0.02f);
    }

    public class Rect
    {
        public Vector2 pos;
        public Vector2 size;
        
        public float LeftBound => pos.x - size.x / 2;
        public float RightBound => pos.x + size.x / 2;
        public float TopBound => pos.y + size.y / 2;
        public float BottomBound => pos.y - size.y / 2;
        
        public Rect(Vector2 pos, Vector2 size)
        {
            this.pos = pos;
            this.size = size;
        }
    }
}
