using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadLastUserData();
        LoadUserScoresData();
    }
    public class UserScore
    {
        public string Name = "Unknown";
        public int Score = 0;
    }
    public string lastUsername = "Unknown";
    public List<int> userScores;
    public List<string> userScoreNames;

    [Serializable]
    public class UserScoreData
    {
        public int[] userScores;
        public string[] userScoreNames;

    }

    [Serializable]
    public class LastUserData
    {
        public string lastName = "Unknown";

    }

    public void SaveLastUserData()
    {
        LastUserData data = new LastUserData();
        data.lastName = lastUsername;
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/lastUsername.json", json);
    }

    public void LoadLastUserData()
    {
        string path = Application.persistentDataPath + "/lastUsername.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            LastUserData data = JsonUtility.FromJson<LastUserData>(json);

            lastUsername = data.lastName;
        }
    }

    public void SaveUserScoresData()
    {
        UserScoreData data = new UserScoreData();
        data.userScores = userScores.ToArray();
        data.userScoreNames = userScoreNames.ToArray();
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/lastUserScores.json", json);
    }

    public void LoadUserScoresData()
    {
        string path = Application.persistentDataPath + "/lastUserScores.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            UserScoreData data = JsonUtility.FromJson<UserScoreData>(json);
            userScores = data.userScores != null ? data.userScores.ToList() : new List<int>();
            userScoreNames = data.userScoreNames != null ? data.userScoreNames.ToList() : new List<string>();
        }
    }
}
