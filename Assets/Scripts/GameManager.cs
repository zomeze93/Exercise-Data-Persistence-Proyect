using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UIElements;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_Text bestScoreText;

    public TMP_InputField playerName;
    public string playerNameText = "";
    public int playerScore;
    public int bestScore;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        LoadInfo();

        bestScoreText.text = $"Best Score : {playerNameText} : {bestScore}";
    }
    private void OnApplicationQuit()
    {
        // ResetGameData();
        // SaveInfo(); // Guardar la mejor puntuación al cerrar el juego
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode(); //para salir del modo play en unity
#else
            Application.Quit();
#endif
    }
    public void ResetGameData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Datos guardados eliminados.");
        }

        // Resetear variables en memoria
        playerNameText = "";
        playerScore = 0;
        bestScore = 0;
    }


    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int playerScore;
        public int bestScore;
    }
    public void SaveInfo()
    {
        if (playerName != null)
        {
            Instance.playerNameText = playerName.text; // Guardar en una variable extra
        }

        SaveData data = new SaveData();
        data.playerName = Instance.playerNameText;
        data.playerScore = playerScore;
        data.bestScore = bestScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

    }


    public void LoadInfo()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerNameText = data.playerName;
            playerScore = data.playerScore;
            bestScore = data.bestScore;

            if (playerName != null)
            {
                playerName.text = playerNameText;
            }
        }
    }
}