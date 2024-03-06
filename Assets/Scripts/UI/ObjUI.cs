using UnityEngine;

public class ObjUI : MonoBehaviour
{
    public float distance;
    public float ScaleDistance = 4f;

    public string NameText;
    public string CostText;

    public TMPro.TextMeshProUGUI Name;
    public TMPro.TextMeshProUGUI Cost;
    public GameObject image;

    private float scale;

    void Start()
    {   
        Name.text = NameText;
        Cost.text = CostText;
        Hide();
    }
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        scale = Mathf.Clamp01(distance / ScaleDistance);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    public void OnTriggerStay(Collider other)
    {
        // Проверяем, имеет ли объект необходимый тег
        if (other.CompareTag("ScanerColider"))
        {
            Visible();
        }
    }

    public void Visible()
    {
        image.SetActive(true);
        Invoke("Hide", 6f); 
    }

    void Hide()
    {
        image.SetActive(false);
    }
}
