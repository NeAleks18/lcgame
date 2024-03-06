using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaner : MonoBehaviour
{
    public GameObject ScanerColider;

    void Update()
    {   ScanerColider.SetActive(false);
        if (Input.GetKeyDown(KeyCode.E))
        {
            ScanerColider.SetActive(true);
            
        }
    }
}

