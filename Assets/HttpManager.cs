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

    private string Token, Username; 
    // Start is called before the first frame update
    void Start()
    {
        Token = PlayerPrefs.GetString("token");
        Username = PlayerPrefs.GetString("username");
        StartCoroutine(GetProfile());

    }

    //public void ClickGetScores()
    //{
    //    StartCoroutine(GetScores());
    //}
    public void ClickSignUp()
    {
        string postData = GetInputData();
        StartCoroutine(SignUp(postData));
    }
    public void ClickLogIn()
    {
        string postData = GetInputData();
        StartCoroutine(LogIn(postData));
    }

    private string GetInputData()
    {
        string username = usernameUI.text;
        string password = passwordUI.text;
        AuthData data = new AuthData(username, password);
        string postData = JsonUtility.ToJson(data);
        //Debug.Log(postData);
        return postData;
    }

   /* IEnumerator GetScores()
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
            List<ScoreData> listScores = new List<ScoreData>();
            foreach (ScoreData score in resData.scores)
            {
                //Debug.Log(score.userId +" | "+score.value);
                listScores.Add(score);
                //LeaderboardManager.Instance.WriteScores(score.user_name, score.score);
                Debug.Log("entre");
                if (listScores.Count == 7) break;
            }
            LeaderboardManager.Instance.WriteScores(listScores);
        }
        else
        {
            Debug.Log(www.error);
        }
    }*/
    IEnumerator SignUp(string postData)
    {
        Debug.Log("Sign Up: " + postData);
        string url = URL + "/api/usuarios"; 
        UnityWebRequest www = UnityWebRequest.Put(url,postData);
        www.method = "POST";
        www.SetRequestHeader("content-type","application/json");
         
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("NETWORK ERROR " + www.error);
        }
        else if (www.responseCode == 200)
        {
            //Debug.Log(www.downloadHandler.text);
            AuthData resData = JsonUtility.FromJson<AuthData>(www.downloadHandler.text);

            Debug.Log("Registrado: " + resData.usuario.username + " , id: " + resData.usuario._id);
            StartCoroutine(LogIn(postData));
        }
        else
        {
            Debug.Log(www.error);
        }
    }
    IEnumerator LogIn(string postData)
    {
        Debug.Log("Log In: " + postData);
        string url = URL + "/api/auth/login";
        UnityWebRequest www = UnityWebRequest.Put(url, postData);
        www.method = "POST";
        www.SetRequestHeader("content-type", "application/json");

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
            AuthData resData = JsonUtility.FromJson<AuthData>(www.downloadHandler.text);
            Debug.Log("Autenticado: " + resData.usuario.username + " , id: " + resData.usuario._id);
            Debug.Log("Token: " + resData.token);
            PlayerPrefs.SetString("token", resData.token);
            PlayerPrefs.SetString("username", resData.usuario.username);
            //UnityEngine.SceneManagement.SceneManager.LoadScene("Main Game");

        }
        else
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
    } IEnumerator GetProfile()
    {
        string url = URL + "/api/usuarios/" + Username;
        UnityWebRequest www = UnityWebRequest.Get(url);
        www.SetRequestHeader("x-token", Token);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("NETWORK ERROR " + www.error);
        }
        else if (www.responseCode == 200)
        {
            //Debug.Log(www.downloadHandler.text);
            AuthData resData = JsonUtility.FromJson<AuthData>(www.downloadHandler.text);
            Debug.Log("Token Válido: " + resData.usuario.username + " , id: " + resData.usuario._id);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main Game");
        }
        else
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
    }
}


//[System.Serializable]
//public class ScoreData
//{
//    public string user_name;
//    public int score;

//}

//[System.Serializable]
//public class Scores
//{
//    public ScoreData[] scores;
//}

[System.Serializable]
public class AuthData
{
    public string username;
    public string password;
    public UserData usuario;
    public string token;
    public AuthData(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
}

[System.Serializable]
public class UserData
{
    public string _id;
    public string username;
    public bool estado;
    public int score;
}


