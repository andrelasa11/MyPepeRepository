using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region "Singleton"

    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }

    #endregion

    [Header("Configuration")]
    [SerializeField] private float money;
    [SerializeField] private float gameTax;
    [SerializeField] private float gameReward;
    [SerializeField] private float statusDecreaseRate = 16.67f; // Status decrease rate per hour

    [Header("PetStatus")]
    [SerializeField] private float health;
    [SerializeField] private float happiness;
    [SerializeField] private float energy;
    [SerializeField] private float hunger;

    [Header("Records")]
    [SerializeField] private float healthGameRecord;
    [SerializeField] private float foodDropRecord;
    [SerializeField] private float hillDriveRecord;
    [SerializeField] private float runnerRecord;
    [SerializeField] private PetController petController;

    // Private
    private DateTime lastPlayTime;

    #region "Properties"

    public float Money { get { return money; } }
    public float GameTax { get { return gameTax; } }
    public float GameReward { get { return gameReward; } }
    public float Hunger { get { return hunger; } }
    public float Energy { get { return energy; } }
    public float Health { get { return health; } }
    public float Happiness { get { return happiness; } }
    public float FoodDropRecord { get { return foodDropRecord; } }
    public float HealthGameRecord { get { return healthGameRecord; } }
    public float HillDriveRecord { get { return hillDriveRecord; } }
    public float RunnerRecord { get { return runnerRecord; } }

    #endregion

    #region "Awake/Start/Update"

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        petController = FindObjectOfType<PetController>();
        SceneManager.sceneLoaded += OnSceneLoaded; // Adicione o evento OnSceneLoaded ao SceneManager

        lastPlayTime = DateTime.Now;
        LoadStatus();
        petController.UpdateRecordTexts();

        SaveLoadManager saveLoadManager = FindObjectOfType<SaveLoadManager>();
        if (saveLoadManager != null)
        {
            saveLoadManager.LoadGame(lastPlayTime);
        }
    }

    private void Update()
    {
        TimeSpan timeSinceLastPlay = DateTime.Now - lastPlayTime;
        float decreaseAmount = statusDecreaseRate * (float)timeSinceLastPlay.TotalHours;

        hunger -= decreaseAmount;
        energy -= decreaseAmount;
        health -= decreaseAmount;
        happiness -= decreaseAmount;

        hunger = Mathf.Clamp(hunger, 0f, 100f);
        energy = Mathf.Clamp(energy, 0f, 100f);
        health = Mathf.Clamp(health, 0f, 100f);
        happiness = Mathf.Clamp(happiness, 0f, 100f);

        lastPlayTime = DateTime.Now;

        if (petController != null)
        {
            petController.UpdateMoneyText();
        }
    }

    #endregion

    #region "Save & Load Methods"

    public void CalculateInactiveTimeDecrease()
    {
        TimeSpan inactiveTime = DateTime.Now - lastPlayTime;
        float decreaseAmount = statusDecreaseRate * (float)inactiveTime.TotalHours;

        hunger -= decreaseAmount;
        energy -= decreaseAmount;
        health -= decreaseAmount;
        happiness -= decreaseAmount;

        hunger = Mathf.Clamp(hunger, 0f, 100f);
        energy = Mathf.Clamp(energy, 0f, 100f);
        health = Mathf.Clamp(health, 0f, 100f);
        happiness = Mathf.Clamp(happiness, 0f, 100f);

        lastPlayTime = DateTime.Now;

        Debug.Log("Método chamado! Tempo inativo: " + inactiveTime);
    }

    public void SaveGame()
    {
        SaveStatus();
    }

    public void SetStatus(float newMoney, float newHunger, float newEnergy, float newHealth, float newHappiness)
    {
        money = newMoney;
        hunger = newHunger;
        energy = newEnergy;
        health = newHealth;
        happiness = newHappiness;
    }

    private void LoadStatus()
    {
        SaveLoadManager saveLoadManager = FindObjectOfType<SaveLoadManager>();

        if (saveLoadManager != null)
        {
            saveLoadManager.LoadGame(lastPlayTime);
            Debug.Log("Jogo carregado");
        }
    }

    private void SaveStatus()
    {
        SaveLoadManager saveLoadManager = FindObjectOfType<SaveLoadManager>();

        if (saveLoadManager != null)
        {
            saveLoadManager.SaveGame();
            Debug.Log("Jogo salvo");
        }
    }

    private void OnApplicationQuit()
    {
        SaveStatus();
    }

    #endregion

    #region "Record Methods"

    public void SetHealthGameRecord(float value)
    {
        healthGameRecord = value;
        SaveStatus();
    }

    public void SetFoodDropRecord(float value)
    {
        foodDropRecord = value;
        SaveStatus();
    }

    public void SetHillDriveRecord(float value)
    {
        hillDriveRecord = value;
        SaveStatus();
    }

    public void SetRunnerRecord(float value)
    {
        runnerRecord = value;
        SaveStatus();
    }

    #endregion

    #region "Status Methods"

    public void FeedPet(float value)
    {
        hunger += value;
        hunger = Mathf.Clamp(hunger, 0f, 100f);
    }

    public void PutToBed(float value)
    {
        energy += value;
        energy = Mathf.Clamp(energy, 0f, 100f);
    }

    public void HealPet(float value)
    {
        health += value;
        health = Mathf.Clamp(health, 0f, 100f);
    }

    public void PlayWithPet(float value)
    {
        happiness += value;
        happiness = Mathf.Clamp(happiness, 0f, 100f);
    }

    #endregion

    #region "Inactive Time Update"

    public void UpdateStatusFromInactiveTime()
    {
        DateTime saveTime = DateTime.Now;
        TimeSpan inactiveTime = saveTime - lastPlayTime;

        float decreaseAmount = statusDecreaseRate * (float)inactiveTime.TotalHours;

        hunger -= decreaseAmount;
        energy -= decreaseAmount;
        health -= decreaseAmount;
        happiness -= decreaseAmount;

        hunger = Mathf.Clamp(hunger, 0f, 100f);
        energy = Mathf.Clamp(energy, 0f, 100f);
        health = Mathf.Clamp(health, 0f, 100f);
        happiness = Mathf.Clamp(happiness, 0f, 100f);

        lastPlayTime = saveTime;

        Debug.Log("Método chamado! Tempo inativo: " + inactiveTime);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        petController = FindObjectOfType<PetController>(); // Atualize a referência do petController
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Remova o evento OnSceneLoaded do SceneManager
    }

    #endregion

    public void AddMoney(float value)
    {
        money += value;
        if (petController != null)
        {
            petController.UpdateMoneyText();
        }
    }
}