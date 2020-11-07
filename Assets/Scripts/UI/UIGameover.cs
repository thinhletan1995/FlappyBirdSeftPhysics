using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIGameover : UIGroup
{
    public List<MedalRank> medals = new List<MedalRank>();
    public Transform panel;
    public GameObject newScoreImg;
    public Image medalImg;
    public Text score;
    public Text bestScore;
    public Transform startPanelPos;
    public Transform finalPanelPos;
    public float delayShowTime;
    public float appearTime;
    
    public override void Show(object data = null)
    {
        base.Show(data);

        try
        {
            Data d = (Data) data;
            newScoreImg.SetActive(d.haveNewRecord);
            medalImg.sprite = GetMedalImg(d.score);
            score.text = d.score.ToString();
            bestScore.text = PlayerPrefs.GetInt(Key.SCORE).ToString();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

        panel.transform.position = startPanelPos.position;
        panel.transform.DOMove(finalPanelPos.position, appearTime)
            .SetEase(Ease.OutBounce)
            .SetDelay(delayShowTime)
            .OnStart(() =>
            {
                GameManager.Instance.audioManager.PlaySFX("GameOver");
            });
    }

    public void OnPlayButtonClick()
    {
        GameManager.Instance.audioManager.PlaySFX("ButtonClick");
        GameManager.Instance.Replay();
    }

    private Sprite GetMedalImg(int score)
    {
        for (int i = medals.Count - 1; i >= 0; i--)
        {
            if (score >= medals[i].score)
                return medals[i].medalImg;
        }

        return null;
    }

    public class Data
    {
        public int score;
        public bool haveNewRecord;
        
        public Data(int score, bool haveNewRecord)
        {
            this.score = score;
            this.haveNewRecord = haveNewRecord;
        }
    }
    
    [System.Serializable]
    public class MedalRank
    {
        public int score;
        public Sprite medalImg;
    }
}
