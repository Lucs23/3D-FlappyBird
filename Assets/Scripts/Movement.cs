using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float currentSpeed = 0f;
    void Update()
    {
        Vector3 temp = transform.position;
        temp.z -= currentSpeed * Time.deltaTime;
        transform.position = temp;
    }
    public void SetSpeed(float speed)
    {
        currentSpeed = speed;
    }

}

