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

    private void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat");
        SaveData data = new SaveData();
        data.PlayerPosition[0] = player.position.x;
        data.PlayerPosition[1] = player.position.y;
        data.PlayerPosition[2] = player.position.z;

        data.PlayerRotation[0] = player.localEulerAngles.x;
        data.PlayerRotation[1] = player.localEulerAngles.y;
        data.PlayerRotation[2] = player.localEulerAngles.z;

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
            var x = data.PlayerPosition[0];
            var y = data.PlayerPosition[1];
            var z = data.PlayerPosition[2];

            var Rx = data.PlayerRotation[0];
            var Ry = data.PlayerRotation[1];
            var Rz = data.PlayerRotation[2];

            player.gameObject.GetComponent<PlayerController>().enabled = false;
            player.position = new Vector3(x, y, z);
            player.rotation = Quaternion.Euler(Rx, Ry, Rz);
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
