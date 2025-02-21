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


    [System.Serializable]
class SaveData
{
    public string playerName;
    public int playerScore;
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

            playerBaneText = data.playerName;
            playerScore = data.playerScore;

            if (playerName != null) playerName.text = playerNameText;
        }
    }


}
