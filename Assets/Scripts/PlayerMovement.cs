using System.Diagnostics;
using Unity.VisualScripting;
// using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float JumpPower = 5f;
    [SerializeField] private float MovePower = 5f;
    public GameObject GameUi;
    private UIDocument uiDoc;
    public UiEvents UiScript;
    private Label PressKey;
    private Vector3 position;
    private Rigidbody rb;
    private float beginTimer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        uiDoc = GameUi.GetComponent<UIDocument>();
        PressKey = uiDoc.rootVisualElement.Q("PressKey") as Label;
    }

    void FixedUpdate()
    {
        if(beginTimer < 5) beginTimer += Time.time / Time.time;
        position = transform.position;
        Vector3 viewPos = cam.WorldToViewportPoint(position);
        if (beginTimer > 2 && beginTimer < 5) rb.useGravity = true;


        //Maak van deze dingen een functie!
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
        GameObject player = GameObject.FindGameObjectWithTag("player");
        Collision collisionScript = player.GetComponent<Collision>();

        if (collisionScript.gameOver == false)
        {
            PressKey.style.display = DisplayStyle.None;
            rb.AddForce(0, JumpPower, 0, ForceMode.VelocityChange);
            rb.useGravity = true;
            Time.timeScale = 1;
            collisionScript.gameOver = false;
        }
    }

    void OnMove(InputValue value)
    {
        GameObject player = GameObject.FindGameObjectWithTag("player");
        Collision collisionScript = player.GetComponent<Collision>();
        if (collisionScript.gameOver == false)
        {
            PressKey.style.display = DisplayStyle.None;
            rb.useGravity = true;
            float x = value.Get<float>();
            rb.AddForce(x * MovePower, 0, 0, ForceMode.VelocityChange);
        
            collisionScript.gameOver = false;
        }
    }
}
