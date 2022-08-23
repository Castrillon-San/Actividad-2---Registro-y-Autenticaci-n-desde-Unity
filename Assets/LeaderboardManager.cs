using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] Text names, scoresText;
    public static LeaderboardManager Instance { get; private set; }
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public void WriteScores(string name, int userScore)
    {
        if(names != null && scoresText != null)
        {
            names.text += name + "\n";
            scoresText.text += userScore.ToString() + "\n";
        }
    }
    public void WriteScores(List<ScoreData> listScores)
    {
        listScores.Sort((c1, c2) => int.Parse(c2.score).CompareTo(int.Parse(c1.score)));
        //foreach (ScoreData scoreData in listScores)
        //{
        //    if (scoreData.username != null && scoreData.score != null)
        //    {
        //        names.text += scoreData.username + "\n";
        //        scoresText.text += scoreData.score + "\n";
        //    }
        //}
        for (int i = 0; i < 7; i++)
        {
            if (listScores[i].username != null && listScores[i].score != null)
            {
                names.text += listScores[i].username + "\n";
                scoresText.text += listScores[i].score + "\n";
            }
        }

    }
    public void Clear()
    {
        names = null;
        scoresText = null;
    }
}
