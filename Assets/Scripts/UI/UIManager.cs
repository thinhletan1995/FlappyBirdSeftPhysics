using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<UIGroup> uiGroups = new List<UIGroup>();

    private UIGroup currentGroup = null;

    public void ShowUIGroup<T>(object data = null) where T : UIGroup
    {
        var g = FindGroup<T>();
        if (g != null)
        {
            currentGroup?.Hide();
            g.Show(data);
            currentGroup = g;
        }
        else
        {
            Debug.LogError("Not found group type: " + typeof(T));
        }
    }

    public void UpdateCurrentUI(object data)
    {
        currentGroup?.UpdateUI(data);
    }

    public T FindGroup<T>() where T : UIGroup
    {
        return (T)uiGroups.Find(x => x.GetType() == typeof(T));
    }

    private void Start()
    {
        uiGroups.ForEach(x => x.Hide());
        ShowUIGroup<UIWaiting>();
    }
}
