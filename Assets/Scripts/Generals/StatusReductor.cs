using UnityEngine;

public class StatusReductor : MonoBehaviour
{
    public void ReductHealth()
    {
        GameManager.Instance.HealPet(-95f);
    }

    public void ReductEnergy()
    {
        GameManager.Instance.PutToBed(-95f);
    }

    public void ReductHappiness()
    {
        GameManager.Instance.PlayWithPet(-95f);
    }

    public void ReductHunger()
    {
        GameManager.Instance.FeedPet(-95f);
    }
}
