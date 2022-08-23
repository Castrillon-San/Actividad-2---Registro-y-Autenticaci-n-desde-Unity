using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataReader : MonoBehaviour
{
    private string Token, Username;
    [SerializeField] Text text1, text2;
    void Start()
    {
        Token = PlayerPrefs.GetString("token");
        Username = PlayerPrefs.GetString("username");
        text1.text = Username + "\n" + Token; 
        text2.text = Username + "\n" + Token; 
    }
}
