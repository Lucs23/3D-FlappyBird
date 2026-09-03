using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float JumpPower = 5f;
    [SerializeField] private float MovePower = 5f;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
