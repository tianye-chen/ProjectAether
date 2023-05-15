using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
    
    public static void SavePlayer (CharacterBase c) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(c);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SavePlayerLevel (LevelSystem l) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerLevel.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerLevelData data2 = new PlayerLevelData(l);

        formatter.Serialize(stream, data2);
        stream.Close();
    }

    public static void ResetAllPlayerStats()
    {
      BinaryFormatter formatter = new BinaryFormatter();
      string playerDataPath = Application.persistentDataPath + "/player.data";
      string levelDataPath = Application.persistentDataPath + "/playerLevel.data";

      FileStream stream = new FileStream(playerDataPath, FileMode.Create);
      formatter.Serialize(stream, PlayerData.GetNewPlayer());

      stream = new FileStream(levelDataPath, FileMode.Create);
      formatter.Serialize(stream, PlayerLevelData.GetNewPlayerLevelData());

      stream.Close();
    }

    public static PlayerData LoadPlayer() {
        string path = Application.persistentDataPath + "/player.data";
        if(File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
            
        }
        else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }

    } 
    public static PlayerLevelData LoadPlayerLevel() {
        string path = Application.persistentDataPath + "/playerLevel.data";
        if(File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerLevelData data2 = formatter.Deserialize(stream) as PlayerLevelData;
            stream.Close();

            return data2;
            
        }
        else {
            Debug.LogError("Save file not found in " + path); 
            return null;
        }

    }  
}
