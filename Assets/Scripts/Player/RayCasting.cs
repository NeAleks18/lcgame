using Mechanics;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    [Header("RayCast Settings")]
    [SerializeField]
    private Camera mainCamera;

    private RaycastHit hit;
    private Ray ray;

    private void RayCast()
    {
        if (!Input.GetKeyDown(KeyCode.E)) return;
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 25))
        {
            if (hit.collider == null) return;
            if (hit.collider.gameObject.tag == "Satellite")
            {
                hit.collider.gameObject.GetComponent<Satellite>().Repair();
            }
            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }
    }
    private void Update()
    {
        RayCast();
    }
}
