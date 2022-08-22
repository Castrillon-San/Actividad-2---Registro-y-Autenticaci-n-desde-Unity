using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HttpManager : MonoBehaviour
{
    [SerializeField]
    private string URL;
    [SerializeField] private InputField usernameUI;
    [SerializeField] private InputField passwordUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ClickGetScores()
    {
        StartCoroutine(GetScores());
    }
    public void ClickSignUp()
    {
        string username = usernameUI.text;
        string password = passwordUI.text;
        AuthData data = new AuthData(username, password);
        string postData = JsonUtility.ToJson(data);
        StartCoroutine(SignUp(postData));
    }

    IEnumerator GetScores()
    {
        string url = URL + "/leaders";
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        //if (www.isNetworkError)
        //{
        //    Debug.Log("NETWORK ERROR " + www.error);
        //}
        if (www.result == UnityWebRequest.Result.ConnectionError)
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
    IEnumerator SignUp(string postData)
    {

        string url = URL + "/api/usuarios"; 
        UnityWebRequest www = UnityWebRequest.Post(url,postData );

        yield return www.SendWebRequest();

        //if (www.isNetworkError)
        //{
        //    Debug.Log("NETWORK ERROR " + www.error);
        //}
        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("NETWORK ERROR " + www.error);
        }
        else if (www.responseCode == 200)
        {
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

[System.Serializable]
public class AuthData
{
    public string username;
    public string password;

    public AuthData(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
}
