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
        listScores.Sort((c1, c2) => c1.score.CompareTo(c2.score));
        foreach (ScoreData scoreData in listScores)
        {
            if (scoreData.user_name != null && scoreData.score >= 0)
            {
                names.text += scoreData.user_name + "\n";
                scoresText.text += scoreData.score.ToString() + "\n";
            }
        }
        
    }
    public void Clear()
    {
        names = null;
        scoresText = null;
    }
}
