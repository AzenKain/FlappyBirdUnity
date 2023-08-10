using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingleTon<DataManager>
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateHighScore(string name, int score)
    {
        string dataUser = PlayerPrefs.GetString("UserData");
        var playerData = JSON.Parse(dataUser).AsObject;
        playerData[name] = new JSONNumber(score);
        string newDataUser = JSONObject.Escape(playerData);
        PlayerPrefs.SetString("UserData", newDataUser);
    }

    public KeyValuePair<string, JSONNode> GetHighScoreItem()
    {
        string dataUser = PlayerPrefs.GetString("UserData");
        var playerData = JSON.Parse(dataUser).AsObject;
        KeyValuePair<string, JSONNode> tmpItem = new KeyValuePair<string, JSONNode>();
        int maxScore = 0;
        foreach (var item in playerData)
        {
            if (item.Value.AsInt > maxScore)
            {
                maxScore = item.Value.AsInt;
                tmpItem = item;
            }
        }
        return tmpItem;
    }

}
