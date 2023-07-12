using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private string saveFilePath;

    private void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    public void SaveGame()
    {
        GameData gameData = new GameData
        {
            hunger = GameManager.Instance.Hunger,
            energy = GameManager.Instance.Energy,
            health = GameManager.Instance.Health,
            happiness = GameManager.Instance.Happiness,
            foodDropRecord = GameManager.Instance.FoodDropRecord,
            healthGameRecord = GameManager.Instance.HealthGameRecord,
            hillDriveRecord = GameManager.Instance.HillDriveRecord,
            runnerRecord = GameManager.Instance.RunnerRecord,
            musicVolume = AudioManager.Instance.GetMusicVolume(),
            soundEffectsVolume = AudioManager.Instance.GetSoundEffectsVolume()
        };

        string jsonData = JsonUtility.ToJson(gameData);

        File.WriteAllText(saveFilePath, jsonData);
    }

    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string jsonData = File.ReadAllText(saveFilePath);

            GameData gameData = JsonUtility.FromJson<GameData>(jsonData);

            GameManager.Instance.SetStatus(gameData.hunger, gameData.energy, gameData.health, gameData.happiness);
            GameManager.Instance.SetFoodDropRecord(gameData.foodDropRecord);
            GameManager.Instance.SetHealthGameRecord(gameData.healthGameRecord);
            GameManager.Instance.SetHillDriveRecord(gameData.hillDriveRecord);
            GameManager.Instance.SetRunnerRecord(gameData.runnerRecord);
            AudioManager.Instance.SetMusicVolume(gameData.musicVolume);
            AudioManager.Instance.SetSoundEffectsVolume(gameData.soundEffectsVolume);
        }
        else
        {
            Debug.Log("Save file not found.");
        }
    }

    public void SaveVolumePreferences()
    {
        SaveGame();
    }
}

[System.Serializable]
public class GameData
{
    public float hunger;
    public float energy;
    public float health;
    public float happiness;
    public float foodDropRecord;
    public float healthGameRecord;
    public float hillDriveRecord;
    public float runnerRecord;
    public float musicVolume;
    public float soundEffectsVolume;
}