using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData data;
    public string playerName;
    public string OpponentName;
    public bool isOpponentAI;


    private void Awake()
    {
        if(data == null)
        {
            data = this;
            playerName = "Player";
            OpponentName = "AI";
            isOpponentAI = true;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
