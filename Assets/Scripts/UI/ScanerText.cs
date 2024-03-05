using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanerText : MonoBehaviour
{
    public TMPro.TextMeshProUGUI Name;
    public TMPro.TextMeshProUGUI Cost;
    public string NameText;
    public string CostText;
    void Start()
    {
        Name.text = NameText;
        Cost.text = CostText;
    }
}
