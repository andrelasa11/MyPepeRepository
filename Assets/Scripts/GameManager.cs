using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region "Singleton"

    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }

    #endregion

    [Header("Money")]
    public float money;

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

    //private
    private DateTime lastPlayTime;
    private const float statusDecreaseRate = 5f; // Status decrease rate per hour

    #region "Properties"

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
        lastPlayTime = DateTime.Now;
        LoadStatus();
    }
    private void Update()
    {
        TimeSpan timeSinceLastPlay = DateTime.Now - lastPlayTime;
        float decreaseAmount = (float)timeSinceLastPlay.TotalHours * statusDecreaseRate;

        // Reduzir os status do pet com base no tempo de inatividade
        hunger -= decreaseAmount;
        energy -= decreaseAmount;
        health -= decreaseAmount;
        happiness -= decreaseAmount;

        // Garantir que os status permaneçam entre 0 e 100
        hunger = Mathf.Clamp(hunger, 0f, 100f);
        energy = Mathf.Clamp(energy, 0f, 100f);
        health = Mathf.Clamp(health, 0f, 100f);
        happiness = Mathf.Clamp(happiness, 0f, 100f);

        lastPlayTime = DateTime.Now;
    }

    #endregion

    #region "Save & Load Methods"

    public void SetStatus(float newHunger, float newEnergy, float newHealth, float newHappiness)
    {
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
            saveLoadManager.LoadGame();
        }
    }

    private void SaveStatus()
    {
        SaveLoadManager saveLoadManager = FindObjectOfType<SaveLoadManager>();

        if (saveLoadManager != null)
        {
            saveLoadManager.SaveGame();
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
        // Aumente o status de Fome quando o pet for alimentado
        hunger += value;
        hunger = Mathf.Clamp(hunger, 0f, 100f);
    }

    public void PutToBed(float value)
    {
        // Aumente o status de Sono quando o pet for colocado para dormir
        energy += value;
        energy = Mathf.Clamp(value, 0f, 100f);
    }

    public void HealPet(float value)
    {
        // Aumente o status de Saúde quando o pet for curado
        health += value;
        health = Mathf.Clamp(health, 0f, 100f);
    }

    public void PlayWithPet(float value)
    {
        // Aumente o status de Diversão quando o pet brincar
        happiness += value;
        happiness = Mathf.Clamp(value, 0f, 100f);
    }

    #endregion
}
