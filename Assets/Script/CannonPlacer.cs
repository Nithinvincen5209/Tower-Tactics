// ... your usings

using UnityEngine;

public class CannonPlacer : MonoBehaviour
{
    public GameObject cannonPrefab;
    public int cannonCost = 50;
    public TMPro.TextMeshProUGUI notenoughText;

    private CannonUI cannonUI;

    private void Start()
    {
        notenoughText.gameObject.SetActive(false);
        cannonUI = FindObjectOfType<CannonUI>(); // optional, for UI only
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Ground"))
            {
                if (GameManager.Instance.SpendCoins(cannonCost))
                {
                    Vector3 spawnPos = hit.point;
                    spawnPos.y += 0.0f;
                    var go = Instantiate(cannonPrefab, spawnPos, Quaternion.identity);

                    // ✅ mark as active
                    if (CannonManager.Instance != null)
                        CannonManager.Instance.RegisterCannon();

                    // (optional) update your UI counter
                    if (cannonUI != null)
                        cannonUI.UseCannon();
                }
                else
                {
                    notenoughText.gameObject.SetActive(true);
                    Invoke(nameof(HideNotEnough), 1.5f);
                }
            }
        }
    }

    void HideNotEnough()
    {
        notenoughText.gameObject.SetActive(false);
    }
}
