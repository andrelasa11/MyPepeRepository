using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager instance;
    public static GameManager Instance => instance;

    #endregion

    [Header("Configuration")]
    [SerializeField] private float money;
    [SerializeField] private float gameTax;
    [SerializeField] private float gameReward;
    [SerializeField] private float eggPrice = 20;
    [SerializeField] private float statusDecreaseRate = 16.67f;
    [SerializeField] private int numOfPets;

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

    #region Private Variables

    private PetController petController;
    private DateTime lastPlayTime;
    private DateTime lastPlaySaved;

    #endregion

    #region Properties

    public float Money => money;
    public int NumOfPets => numOfPets;
    public float GameTax => gameTax;
    public float GameReward => gameReward;
    public float EggPrice => eggPrice;
    public float Hunger => hunger;
    public float Energy => energy;
    public float Health => health;
    public float Happiness => happiness;
    public float FoodDropRecord => foodDropRecord;
    public float HealthGameRecord => healthGameRecord;
    public float HillDriveRecord => hillDriveRecord;
    public float RunnerRecord => runnerRecord;

    #endregion

    #region MonoBehaviour Callbacks

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
        SceneManager.sceneLoaded += OnSceneLoaded;

        lastPlayTime = DateTime.Now;
        LoadGame();

        Debug.Log("Jogo carregado");
    }

    private void Update()
    {
        UpdateStatus();
        UpdatePetController();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    #endregion

    #region Status and Updates

    private void UpdateStatus()
    {
        TimeSpan timeSinceLastPlay = DateTime.Now - lastPlayTime;
        float decreaseAmount = statusDecreaseRate * (float)timeSinceLastPlay.TotalHours;

        hunger = Mathf.Clamp(hunger - decreaseAmount, 0f, 100f);
        energy = Mathf.Clamp(energy - decreaseAmount, 0f, 100f);
        health = Mathf.Clamp(health - decreaseAmount, 0f, 100f);
        happiness = Mathf.Clamp(happiness - decreaseAmount, 0f, 100f);

        lastPlayTime = DateTime.Now;
    }

    private void UpdatePetController()
    {
        petController?.UpdateMoneyText();
        petController?.UpdateRecordTexts();
    }

    #endregion

    #region Scene Management

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        petController = FindObjectOfType<PetController>();
    }

    #endregion

    #region Save and Load

    private void LoadGame()
    {
        SaveLoadManager saveLoadManager = FindObjectOfType<SaveLoadManager>();
        saveLoadManager?.LoadGame(ref lastPlaySaved);
    }

    public void CalculateInactiveTimeDecrease(TimeSpan inactiveTime)
    {
        float decreaseAmount = statusDecreaseRate * (float)inactiveTime.TotalHours;

        hunger = Mathf.Clamp(hunger - decreaseAmount, 0f, 100f);
        energy = Mathf.Clamp(energy - decreaseAmount, 0f, 100f);
        health = Mathf.Clamp(health - decreaseAmount, 0f, 100f);
        happiness = Mathf.Clamp(happiness - decreaseAmount, 0f, 100f);

        lastPlayTime = DateTime.Now;

        Debug.Log("Método chamado! Tempo inativo: " + inactiveTime);
    }

    public void SaveGame()
    {
        SaveStatus();
        Debug.Log("Jogo salvo");
    }

    private void SaveStatus()
    {
        SaveLoadManager saveLoadManager = FindObjectOfType<SaveLoadManager>();
        saveLoadManager?.SaveGame();
    }

    #endregion

    #region Setters

    public void SetStatus(float newMoney, int newNumOfPets, float newHunger, float newEnergy, float newHealth, float newHappiness)
    {
        money = newMoney;
        numOfPets = newNumOfPets;
        hunger = newHunger;
        energy = newEnergy;
        health = newHealth;
        happiness = newHappiness;
    }

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

    #region Actions

    public void FeedPet(float value)
    {
        hunger = Mathf.Clamp(hunger + value, 0f, 100f);
    }

    public void PutToBed(float value)
    {
        energy = Mathf.Clamp(energy + value, 0f, 100f);
    }

    public void HealPet(float value)
    {
        health = Mathf.Clamp(health + value, 0f, 100f);
    }

    public void PlayWithPet(float value)
    {
        happiness = Mathf.Clamp(happiness + value, 0f, 100f);
    }

    public void AddMoney(float value)
    {
        money += value;
        petController?.UpdateMoneyText();
    }

    public void AddPet(int value)
    {
        numOfPets += value;
    }

    #endregion
}
