using UnityEngine;

public class BadBillboard : MonoBehaviour
{
    void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward,Vector3.up);
        
    }
}
