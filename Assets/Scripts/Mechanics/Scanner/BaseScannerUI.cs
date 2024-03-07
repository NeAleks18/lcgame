using UnityEngine;

namespace Mechanics.Scanner
{
    public abstract class BaseScannerUI : MonoBehaviour, IScanner
    {
        [field: SerializeField] public ScannerData Data { get; set; }

        [field: SerializeField] public TMPro.TextMeshProUGUI ForName { get; set; }
        [field: SerializeField] public TMPro.TextMeshProUGUI ForCost { get; set; }
        [field: SerializeField] public GameObject Image { get; set; }

        [SerializeField]
        public float Distance { get; protected set; }
        [SerializeField]
        public float Scale { get; protected set; }

        // Base
        public virtual void Init()
        {
            if (ForName != null) ForName.text = Data.NameOfItem;
            if (ForCost != null) ForCost.text = Data.Cost.ToString();
            Hide();
        }

        public virtual void Always()
        {
            if (Camera.main == null) return;
            transform.LookAt(Camera.main.transform.position);
            Distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            Scale = Mathf.Clamp01(Distance / Data.Distance);
            transform.localScale = new Vector3(Scale, Scale, Scale);
        }

        public virtual void Show()
        {
            Image.SetActive(true);
            Invoke("Hide", 6f);
        }

        public virtual void Hide()
        {
            Image.SetActive(false);
        }

        // Mono
        private void Start()
        {
            Init();
        }

        private void Update()
        {
            Always();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "ScannerColider") Show();
            else return;
        }

        private void Reset()
        {
            if (Data != null)
            {
                ForName.text = Data.NameOfItem;
                ForCost.text = Data.Cost.ToString();
                Image = GetComponent<UnityEngine.UI.Image>().gameObject;
            }
        }
    }
}