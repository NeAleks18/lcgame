using Mechanics.Interactable;
using Mechanics.Inventory;
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

    [Header("Inventory")]
    [SerializeField]
    private Inventory Inventory;

    private IInteractable Interact;
    private GameObject InteractObject;
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
            if (Input.GetButtonDown("Interact"))
            {
                AddingSlider = true;
                ActionGameObject.SetActive(true);
                InteractObject = hit.collider.gameObject;
                Interact = InteractObject.GetComponent<IInteractable>();
            }
            else if (Input.GetButtonUp("Interact"))
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
        while (ActionSlider.fillAmount < 1 && AddingSlider && Interact != null)
        {
            ActionSlider.fillAmount += 0.01f / (float)Interact.TimeToUse;
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

        if (ActionSlider.fillAmount >= 1 && Interact != null)
        {
            /*switch (Interact.Type)
            {
                case "Scrap":
                    if (Inventory.getItem(Inventory.CurrentSlot) == null)
                    {
                        //Inventory.addItem(InteractObject.GetComponent<Item>(), Inventory.CurrentSlot);
                        Interact.Interact();
                    }
                    break;
                case "Sputnik":
                    Interact.Interact();
                    break;
            }*/
            Interact.Interact();
            CancelAction();
        }
    }

    private void CancelAction()
    {
        Interact = null;
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
