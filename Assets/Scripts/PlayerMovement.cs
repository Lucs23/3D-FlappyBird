using System.Diagnostics;
// using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float JumpPower = 5f;
    [SerializeField] private float MovePower = 5f;

    private Vector3 position;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        position = transform.position;
        Vector3 viewPos = cam.WorldToViewportPoint(position);
        // Vector3 worldPos = cam.ViewportToWorldPoint(position);

        if (viewPos.x > 1)//Right
        {
            Vector3 RightX = new Vector3(1, viewPos.y, viewPos.z);
            Vector3 velocity = rb.linearVelocity;
            velocity.x = 0f;
            rb.transform.position = cam.ViewportToWorldPoint(RightX);
            rb.linearVelocity = velocity;
        }

        else if (viewPos.x < 0) //Left
        {
            Vector3 LeftX = new Vector3(0, viewPos.y, viewPos.z);
            Vector3 velocity = rb.linearVelocity;
            velocity.x = 0f;
            rb.transform.position = cam.ViewportToWorldPoint(LeftX);
            rb.linearVelocity = velocity;
        }


        if (viewPos.y > 1) //Above
        {
            Vector3 UpY = new Vector3(viewPos.x, 1, viewPos.z);
            Vector3 velocity = rb.linearVelocity;
            velocity.y = 0f;
            rb.transform.position = cam.ViewportToWorldPoint(UpY);
            rb.linearVelocity = velocity;
        }

    }

    void OnJump()
    {
        rb.AddForce(0, JumpPower, 0, ForceMode.VelocityChange);
    }

    void OnMove(InputValue value)
    {
        float x = value.Get<float>();
        rb.AddForce(x * MovePower, 0, 0, ForceMode.VelocityChange);
    }
}
