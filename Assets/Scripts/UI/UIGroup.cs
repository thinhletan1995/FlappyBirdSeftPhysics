using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGroup : MonoBehaviour
{
    public virtual void Show(object data = null)
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void UpdateUI(object data)
    {
        
    }
}
