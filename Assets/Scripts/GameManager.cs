using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UIElements;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_InputField playerName;
    public string playerNameText = "";
    public int playerScore;
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
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         Debug.Log("Grabado nombre: " + playerNameText);
    //     }
    // }
    
    //private void OnApplicationQuit()
    //{
    //    SaveInfo(); // Guardar la mejor puntuación al cerrar el juego
    //}

    [System.Serializable]
class SaveData
{
    public string playerName;
    public int playerScore;
    public int bestScore;
}
    public void SaveInfo()
    {
        if (playerName == null)
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
        else
        {
            Debug.Log("playerName es null en SaveInfo(). No se guardará el nombre.");
        }
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
            
            if (playerName != null) playerName.text = playerNameText;
        }
    }


}
