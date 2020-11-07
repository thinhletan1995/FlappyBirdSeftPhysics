using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicHandler : SingletonComponent<PhysicHandler>
{
    [SerializeField]
    private List<MBoxCollider2D> physcObjects = new List<MBoxCollider2D>();

    public void Add(MBoxCollider2D o)
    {
        if (!physcObjects.Contains(o))
        {
            physcObjects.Add(o); 
        }
    }

    public void Remove(MBoxCollider2D o)
    {
        physcObjects.Remove(o);
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < physcObjects.Count - 1; i++)
        {
            for (int j = i + 1; j < physcObjects.Count; j++)
            {
                CheckPhysics(physcObjects[i], physcObjects[j]);
            }
        }
    }

    private void CheckPhysics(MBoxCollider2D o1, MBoxCollider2D o2)
    {
        MRigidbody2D r1 = o1.GetComponentInParent<MRigidbody2D>();
        MRigidbody2D r2 = o2.GetComponentInParent<MRigidbody2D>();

        float entryTime = CheckPhysicsAABB(o1, o2);

        if (r1 != null && r1.enabled)
        {
            r1.OnCheckingCollision(entryTime, o1.isTrigger || o2.isTrigger, o2);  
        }
        
        if (r2 != null && r2.enabled)
        {
            r2.OnCheckingCollision(entryTime, o2.isTrigger || o1.isTrigger, o1);
        }
    }

    private float CheckPhysicsAABB(MBoxCollider2D b1, MBoxCollider2D b2)
    {
        MRigidbody2D r1 = b1.GetComponentInParent<MRigidbody2D>();
        MRigidbody2D r2 = b2.GetComponentInParent<MRigidbody2D>();

        Vector2 v = Vector2.zero;

        if (r1 != null && r1.enabled)
        {
            v += r1.Velocity;
        }
        
        if (r2 != null && r2.enabled)
        {
            v -= r2.Velocity;
        }

        if (v == Vector2.zero)
        {
            return 1;
        }

        MBoxCollider2D.Rect boudingBox = GetBoundingBox(b1.ExportToRect(), v);
        if (!IsColliding(boudingBox, b2.ExportToRect()))
        {
            return 1;
        }

        Vector2 tx = b1.DX(v.x, b2) / v.x;
        if (v.x == 0)
        {
            tx.x = -float.MaxValue;
            tx.y = float.MaxValue;
        }
        
        Vector2 ty = b1.DY(v.y, b2) / v.y;
        if (v.y == 0)
        {
            ty.x = -float.MaxValue;
            ty.y = float.MaxValue;
        }

        float entryTime = Mathf.Max(tx.x, ty.x);
        float exitTime = Mathf.Min(tx.y, ty.y);

        if (entryTime > exitTime || (tx.x < 0 && ty.x < 0) || tx.x > 1 || ty.x > 1)
        {
            return 1;
        }

        return entryTime;
    }

    private bool IsColliding(MBoxCollider2D.Rect b1, MBoxCollider2D.Rect b2)
    {
        float left = b2.LeftBound - b1.RightBound;
        float right = b2.RightBound - b1.LeftBound;
        float top = b2.TopBound - b1.BottomBound;
        float bottom = b2.BottomBound - b1.TopBound;

        return !(left > 0 || right < 0 || top < 0 || bottom > 0);
    }

    private MBoxCollider2D.Rect GetBoundingBox(MBoxCollider2D.Rect b, Vector2 v)
    {
        if (v != Vector2.zero)
        {
            Vector2 oldSize = b.size / 2;
            b.size.x += Mathf.Abs(v.x);
            b.size.y += Mathf.Abs(v.y);

            Vector2 newSize = b.size / 2;
            b.pos.x = v.x > 0 ? b.pos.x + newSize.x - oldSize.x : b.pos.x - newSize.x + oldSize.x;
            b.pos.y = v.y > 0 ? b.pos.y + newSize.y - oldSize.y : b.pos.y - newSize.y + oldSize.y;
        }

        return b;
    }
}
