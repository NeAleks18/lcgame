using Mechanics;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Ray = UnityEngine.Ray;


public enum StateOfRayCasting
{
    None,
    Satellite
}

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
    [SerializeField]
    private StateOfRayCasting state = StateOfRayCasting.None;

    private GameObject actionobject;
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

        if (hit.collider.gameObject.tag == "Satellite")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                AddingSlider = true;
                ActionGameObject.SetActive(true);
                actionobject = hit.collider.gameObject;
                state = StateOfRayCasting.Satellite;
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
        //Debug.Log("Start");
        while (ActionSlider.fillAmount < 1)
        {
            //Debug.Log("Add");
            yield return new WaitForSeconds(0.01f);
            ActionSlider.fillAmount += 0.001f;
        }
    }

    private void CheckSlider()
    {
        if (AddingSlider && !toggleEnum)
        {
            toggleEnum = true;
            StartCoroutine(Repeater());
        }

        if (ActionSlider.fillAmount >= 1)
        {
            switch (state)
            {
                case StateOfRayCasting.None:
                    Debug.Log("Unknown action!");
                    CancelAction();
                    break;

                case StateOfRayCasting.Satellite:
                    actionobject.GetComponent<Satellite>().Repair();
                    CancelAction();
                    break;

                default:
                    Debug.Log("Unknown action!");
                    CancelAction();
                    break;
            }
        }
    }

    private void CancelAction()
    {
        toggleEnum = false;
        AddingSlider = false;
        ActionSlider.fillAmount = 0;
        ActionGameObject.SetActive(false);
        actionobject = null;
        state = StateOfRayCasting.None;
    }

    private void Update()
    {
        RayCast();
        CheckSlider();
    }
}
