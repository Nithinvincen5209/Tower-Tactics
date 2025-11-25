using UnityEngine;

public class CannonManager : MonoBehaviour
{
    public static CannonManager Instance;

    public int ActiveCannons { get; private set; } = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RegisterCannon()
    {
        ActiveCannons++;
        // Debug.Log("Active cannons: " + ActiveCannons);
    }

    public void UnregisterCannon()
    {
        ActiveCannons = Mathf.Max(0, ActiveCannons - 1);
        // Debug.Log("Active cannons: " + ActiveCannons);
    }
}
