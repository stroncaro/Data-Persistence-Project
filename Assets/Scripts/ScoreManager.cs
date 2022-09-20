using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public string playerName;
    public int playerScore = 0;
    public string hiScoreName = "";
    public int hiScoreScore = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScore();
    }

    [System.Serializable]
    public class ScoreData
    {
        public string playerName;
        public int playerScore;
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/scoreData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ScoreData data = JsonUtility.FromJson<ScoreData>(json);

            hiScoreName = data.playerName;
            hiScoreScore = data.playerScore;
        }
    }

    public void SaveScore()
    {
        if (playerScore > hiScoreScore) {
            hiScoreName = playerName;
            hiScoreScore = playerScore;

            ScoreData data = new ScoreData();
            data.playerName = hiScoreName;
            data.playerScore = hiScoreScore;

            string path = Application.persistentDataPath + "/scoreData.json";
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(path, json);
        }
    }
}
