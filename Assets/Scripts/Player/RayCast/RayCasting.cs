using Mechanics.Interactable;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Ray = UnityEngine.Ray;

public class RayCasting : MonoBehaviour
{
    [Header("RayCast Settings")]
    [SerializeField]
    private Camera mainCamera;

    [Header("Actions Settings")]
    [SerializeField]
    private GameObject ActionGameObject;
    [SerializeField]
    private Image ActionSlider;

    private IInteractable InteractObject;
    private bool AddingSlider = false;
    private bool toggleEnum = false;

    private void RayCast()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out hit, 25))
        {
            CancelAction();
            return;
        }

        if (hit.collider == null)
        {
            CancelAction();
            return;
        }

        if (hit.collider.gameObject.tag == "Interactable")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                AddingSlider = true;
                ActionGameObject.SetActive(true);
                InteractObject = hit.collider.gameObject.GetComponent<IInteractable>();
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                CancelAction();
            }
        }
        else
        {
            CancelAction();
        }

        Debug.DrawLine(ray.origin, hit.point, Color.red);
    }

    private IEnumerator Repeater()
    {
        while (ActionSlider.fillAmount < 1 && AddingSlider && InteractObject != null)
        {
            ActionSlider.fillAmount += 0.01f / (float)InteractObject.TimeToUse;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void CheckSlider()
    {
        if (AddingSlider && !toggleEnum)
        {
            toggleEnum = true;
            StartCoroutine(Repeater());
        }

        if (ActionSlider.fillAmount >= 1 && InteractObject != null)
        {
            InteractObject.Interact();
            CancelAction();
        }
    }

    private void CancelAction()
    {
        InteractObject = null;
        toggleEnum = false;
        AddingSlider = false;
        ActionSlider.fillAmount = 0;
        ActionGameObject.SetActive(false);
    }

    private void Update()
    {
        RayCast();
        CheckSlider();
    }
}
