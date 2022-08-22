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

    public void Clear()
    {
        names = null;
        scoresText = null;
    }
}
