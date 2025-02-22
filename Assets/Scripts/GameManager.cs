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
    public string bestPlayerNameText = "";

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
        bestScoreText.text = $"Best Score : {bestPlayerNameText} : {bestScore}";
    }
    // borrar update
    void Update()
    {
        Guardar();
    }
    private void OnApplicationQuit()
    {
        // ResetGameData();
        SaveInfo(); // Guardar la mejor puntuación al cerrar el juego
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
        bestPlayerNameText = "";
        playerName.text = "";
        playerScore = 0;
        bestScore = 0;
    }

    public void Guardar()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SaveInfo();
        }
    }


    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public string bestPlayerNameTextData;
        public int playerScore;
        public int bestScore;
    }
    public void SaveInfo()
{
    if (playerName != null)
    {
        playerNameText = playerName.text; // Solo guardar si playerName existe
    }
    SaveData data = new SaveData();
    // playerNameText = playerName.text; // Guarda el nombre del jugador
    data.playerName = playerNameText; // Usa el nombre guardado en memoria
    data.playerScore = playerScore;
    data.bestScore = bestScore;
    data.bestPlayerNameTextData = bestPlayerNameText; // Usa el nombre guardado en memoria

    string json = JsonUtility.ToJson(data);
    File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

    // Debug.Log("Datos guardados correctamente: " + json);
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
            bestPlayerNameText = data.bestPlayerNameTextData;
        // Debug.Log($"Datos cargados: Nombre: {playerNameText}, Puntuación: {playerScore}");

            if (playerName != null)
            {
                playerName.text = playerNameText;
            }
        }
    }
}