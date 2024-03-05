using UnityEngine;

public class LookAtCamera : MonoBehaviour
{ public float distance;
    
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        if(distance < 4)
        {
            transform.localScale = new Vector3(distance / 4, distance / 4, distance / 4);
        }
    }
}
