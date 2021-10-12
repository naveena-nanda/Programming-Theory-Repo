using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    public string playerName;
    public string bestPlayer;
    public int bestScore;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public string playerNameData;
        public string bestPlayerData;
        public int bestScoreData;
    }
    public void SaveName()
    {
        SaveData data = new SaveData();
        data.playerNameData = playerName;
        data.bestPlayerData = bestPlayer;
        data.bestScoreData = bestScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        Debug.Log("saved " + playerName);
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerNameData;
            bestPlayer = data.bestPlayerData;
            bestScore = data.bestScoreData;
            Debug.Log("loaded " + playerName);
        }
    }
}
