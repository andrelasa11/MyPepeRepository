using System;
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
            money = GameManager.Instance.Money,
            hunger = GameManager.Instance.Hunger,
            energy = GameManager.Instance.Energy,
            health = GameManager.Instance.Health,
            happiness = GameManager.Instance.Happiness,
            foodDropRecord = GameManager.Instance.FoodDropRecord,
            healthGameRecord = GameManager.Instance.HealthGameRecord,
            hillDriveRecord = GameManager.Instance.HillDriveRecord,
            runnerRecord = GameManager.Instance.RunnerRecord,
            musicVolume = AudioManager.Instance.GetMusicVolume(),
            soundEffectsVolume = AudioManager.Instance.GetSoundEffectsVolume(),
            saveTime = DateTime.Now // Adiciona o horário de salvamento
        };

        string jsonData = JsonUtility.ToJson(gameData);

        File.WriteAllText(saveFilePath, jsonData);
    }

    public void LoadGame(DateTime lastPlayTime)
    {
        if (File.Exists(saveFilePath))
        {
            string jsonData = File.ReadAllText(saveFilePath);

            GameData gameData = JsonUtility.FromJson<GameData>(jsonData);

            GameManager.Instance.SetStatus(gameData.money, gameData.hunger, gameData.energy, gameData.health, gameData.happiness);
            GameManager.Instance.SetFoodDropRecord(gameData.foodDropRecord);
            GameManager.Instance.SetHealthGameRecord(gameData.healthGameRecord);
            GameManager.Instance.SetHillDriveRecord(gameData.hillDriveRecord);
            GameManager.Instance.SetRunnerRecord(gameData.runnerRecord);
            AudioManager.Instance.SetMusicVolume(gameData.musicVolume);
            AudioManager.Instance.SetSoundEffectsVolume(gameData.soundEffectsVolume);

            lastPlayTime = gameData.saveTime; // Atualiza o lastPlayTime com o horário de salvamento

            TimeSpan inactiveTime = DateTime.Now - lastPlayTime;
            if (inactiveTime.TotalHours > 0)
            {
                GameManager.Instance.CalculateInactiveTimeDecrease(); // Atualiza os status com base no tempo inativo
            }

            Debug.Log("Jogo carregado");
        }
        else
        {
            Debug.Log("Save file not found.");
        }
    }

    [System.Serializable]
    public class GameData
    {
        public float money;
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
        public DateTime saveTime;
    }
}
