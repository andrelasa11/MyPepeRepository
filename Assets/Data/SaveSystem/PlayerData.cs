using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [Header("Money")]
    public float money;

    [Header("PetStatus")]
    public int dirt;
    public int happiness;
    public int energy;
    public int food;

    [Header("DateTime")]
    public string lastTimeDirt;
    public DateTime lastTimeHappiness;
    public DateTime lastTimeEnergy;
    public DateTime lastTimeFood;

    [Header("Records")]
    public float infinityJumpRecord;
    public float foodDropRecord;
    public float hillDriveRecord;    

    public PlayerData(GameManager gameManager)
    {
        money = gameManager.money;

        dirt = gameManager.dirt;
        happiness = gameManager.happiness;
        energy = gameManager.energy;
        food = gameManager.food;

        lastTimeDirt = gameManager.lastTimeDirt.ToString();
        lastTimeHappiness = gameManager.lastTimeHappiness;
        lastTimeEnergy = gameManager.lastTimeEnergy;
        lastTimeFood = gameManager.lastTimeFood;

        infinityJumpRecord = gameManager.infinityJumpRecord;
        foodDropRecord = gameManager.foodDropRecord;
        hillDriveRecord = gameManager.hillDriveRecord;
    }
}