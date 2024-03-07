using UnityEngine;

public class PlayerScanner : MonoBehaviour
{
    [SerializeField]
    private GameObject ScannerColider;

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            ScannerColider.SetActive(true);
            Debug.Log("turn");
        }
        else ScannerColider.SetActive(false);
    }
}

