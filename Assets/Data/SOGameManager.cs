using System;
using UnityEngine;


[CreateAssetMenu(fileName = "New GManagerConfig", menuName = "GameManager/Game Config", order = 0)]
public class SOGameManager : ScriptableObject
{
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
}
