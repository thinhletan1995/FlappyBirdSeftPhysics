using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlaying : UIGroup
{
    public Text score;

    public override void UpdateUI(object data)
    {
        try
        {
            Data d = (Data) data;
            score.text = d.score.ToString();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    public class Data
    {
        public int score;

        public Data(int score)
        {
            this.score = score;
        }
    }
}
