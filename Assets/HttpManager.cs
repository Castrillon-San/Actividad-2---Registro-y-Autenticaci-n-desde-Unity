using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpManager : MonoBehaviour
{
    [SerializeField]
    private string URL;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ClickGetScores()
    {
        StartCoroutine(GetScores());
    }

    IEnumerator GetScores()
    {
        string url = URL + "/leaders";
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log("NETWORK ERROR " + www.error);
        }
        else if(www.responseCode == 200){
            //Debug.Log(www.downloadHandler.text);
            Scores resData = JsonUtility.FromJson<Scores>(www.downloadHandler.text);

            foreach (ScoreData score in resData.scores)
            {
                //Debug.Log(score.userId +" | "+score.value);
                LeaderboardManager.Instance.WriteScores(score.user_name, score.score);
                Debug.Log("entre");
            }
        }
        else
        {
            Debug.Log(www.error);
        }
    }
   
}


[System.Serializable]
public class ScoreData
{
    public string user_name;
    public int score;

}

[System.Serializable]
public class Scores
{
    public ScoreData[] scores;
}
