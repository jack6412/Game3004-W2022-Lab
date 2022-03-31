using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
class SaveData
{
    public float[] PlayerPosition, 
                   PlayerRotation;

    public SaveData()
    {
        PlayerPosition = new float[3];
        PlayerRotation = new float[3];
    }
}

public class GameSaveManager : MonoBehaviour
{
    public Transform player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            SaveGame();
        if (Input.GetKeyDown(KeyCode.L))
            LoadGame();
    }

    private void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat");
        SaveData data = new SaveData();
        
        data.PlayerPosition = new[] { player.position.x, player.position.y, player.position.z };
        data.PlayerRotation = new[] { player.rotation.eulerAngles.x, player.rotation.eulerAngles.y, player.rotation.eulerAngles.z };


        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }
    private void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();


            player.gameObject.GetComponent<PlayerController>().enabled = false;
            player.position = new Vector3(data.PlayerPosition[0], data.PlayerPosition[1], data.PlayerPosition[2]);
            player.rotation = Quaternion.Euler(data.PlayerRotation[0], data.PlayerRotation[1], data.PlayerRotation[2]);
            player.gameObject.GetComponent<PlayerController>().enabled = true;
            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }
    void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySaveData.dat");
            Debug.Log("Data reset complete!");
        }
        else
            Debug.LogError("No save data to delete.");
    }

    public void OnBut_Save()
    {
        SaveGame();
    }
    public void OnButLoad()
    {
        LoadGame();
    }
}
