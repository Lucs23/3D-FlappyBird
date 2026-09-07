using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float currentSpeed;
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("player");
        Collision collisionScript = player.GetComponent<Collision>();
        if (collisionScript.gameOver == false)
        {
            Vector3 temp = transform.position;
            temp.z -= currentSpeed * Time.deltaTime;
            transform.position = temp;
        }
    }
    public void SetSpeed(float speed)
    {
        currentSpeed = speed;
    }

}

