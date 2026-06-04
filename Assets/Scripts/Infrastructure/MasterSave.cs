using System;
using System.IO;
using UnityEngine;

public class MasterSave
{
    private string _savepath = Application.dataPath + "/MySaves/playerData.json";
    public SaveData SaveData { get; private set; } = new SaveData();
    public void SaveAllData ()
    {
        string jsonString = JsonUtility.ToJson(SaveData);
        File.WriteAllText(_savepath, jsonString);
    }

    public void LoadAllData()
    {
        if (File.Exists(_savepath))
        {
            string jsonstring = File.ReadAllText(_savepath);
            SaveData = JsonUtility.FromJson<SaveData>(jsonstring);
        }
    }
}

[Serializable]
public class SaveData
{
    public float MasterVolume;
    public float EffectsVolume;
    public float MusicVolume;
    public bool MusicToggel;
}
