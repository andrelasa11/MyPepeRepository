using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region "Singleton"

    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }

    #endregion

    [Header("Money")]
    public float money;

    [Header("PetStatus")]
    public int dirt;
    public int happiness;
    public int energy;
    public int food;

    [Header("DateTime")]
    public DateTime lastTimeDirt;
    public DateTime lastTimeHappiness;
    public DateTime lastTimeEnergy;
    public DateTime lastTimeFood;

    [Header("Records")]
    public float infinityJumpRecord;
    public float foodDropRecord;
    public float hillDriveRecord;

    [Header("Dependencies")]
    public SOGameManager managerConfig;

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

        //LoadPlayer();

        //RecordAllStatus();

        Debug.Log(lastTimeDirt.ToString());
        Debug.Log(lastTimeHappiness.ToString());

    }

    #region "Save & Load Methods"

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        money = data.money;

        dirt = data.dirt;
        happiness = data.happiness;
        energy = data.energy;
        food = data.food;

        lastTimeDirt = DateTime.Parse(data.lastTimeDirt);
        lastTimeHappiness = data.lastTimeHappiness;
        lastTimeEnergy = data.lastTimeEnergy;
        lastTimeFood = data.lastTimeFood;

        infinityJumpRecord = data.infinityJumpRecord;
        foodDropRecord = data.foodDropRecord;
        hillDriveRecord = data.hillDriveRecord;
    }

    #endregion

    #region "Record Methods"

    public void SetInfinityJumpRecord(float value)
    {
        infinityJumpRecord = value;
        SavePlayer();
    }

    public void SetFoodDropRecord(float value)
    {
        foodDropRecord = value;
        SavePlayer();
    }

    public void SetHillDriveRecord(float value)
    {
        hillDriveRecord = value;
        SavePlayer();
    }

    #endregion

    #region "Status Methods"

    public void SetDirtValue(int value, DateTime lastTime)
    {
        dirt = value;
        lastTimeDirt = lastTime;

        if (dirt < 0) dirt = 0;
                
        SavePlayer();
        RecordAllStatus();
    }

    public void SetHappinessValue(int value, DateTime lastTime)
    {
        happiness = value;
        lastTimeHappiness = lastTime;

        if (happiness < 0) happiness = 0;

        SavePlayer();
        RecordAllStatus();
    }

    public void AddHappinessValue(int value, DateTime lastTime)
    {
        happiness += value;
        lastTimeHappiness = lastTime;

        if (happiness > 100) happiness = 100;
        else if (happiness < 0) happiness = 0;

        SavePlayer();
        RecordAllStatus();
    }

    public void SetEnergyValue(int value, DateTime lastTime)
    {
        energy = value;
        lastTimeEnergy = lastTime;

        if (energy < 0) energy = 0;

        SavePlayer();
        RecordAllStatus();
    }

    public void SetFoodValue(int value, DateTime lastTime)
    {
        food = value;
        lastTimeFood = lastTime;

        if (food < 0) food = 0;

        SavePlayer();
        RecordAllStatus();
    }

    public void RecordAllStatus()
    {

        managerConfig.money = money;

        managerConfig.dirt = dirt;
        managerConfig.happiness = happiness;
        managerConfig.energy = energy;
        managerConfig.food = food;
        
        managerConfig.lastTimeDirt = lastTimeDirt;
        managerConfig.lastTimeHappiness = lastTimeHappiness;
        managerConfig.lastTimeEnergy = lastTimeEnergy;
        managerConfig.lastTimeFood = lastTimeFood;

        managerConfig.infinityJumpRecord = infinityJumpRecord;
        managerConfig.foodDropRecord = foodDropRecord;
        managerConfig.hillDriveRecord = hillDriveRecord;

    }

    #endregion
}
