using UnityEngine;

public class FuelController : MonoBehaviour
{
    [Header("Configuration")]
    public float fuelToAdd;

    public void AddFuel() => GCHillDrive.Instance.SetFuel(fuelToAdd);
}
