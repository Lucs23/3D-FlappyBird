using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
// using System.Numerics;

public class UiEvents : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private Rigidbody player;
    private UIDocument uiDoc;
    private Button resetButton;
    private VisualElement GameOverScreen;
    private Label score;
    private Label PressKey;
    public Obstacles obstaclesScript;
    private Animator animation;

    void OnEnable()
    {
        uiDoc = GetComponent<UIDocument>();

        GameObject player = GameObject.FindGameObjectWithTag("player");
        animation = player.GetComponent<Animator>();
        resetButton = uiDoc.rootVisualElement.Q("ResetButton") as Button;
        score = uiDoc.rootVisualElement.Q("ScoreDisplay") as Label;
        PressKey = uiDoc.rootVisualElement.Q("PressKey") as Label;
        resetButton.RegisterCallback<ClickEvent>(OnPlayGameClick);
        GameOverScreen = uiDoc.rootVisualElement.Q("GameOver");
    }
    void OnDisable()
    {
        resetButton.UnregisterCallback<ClickEvent>(OnPlayGameClick);
    }

    void resetGame()
    {
        animation.Play("Fly");
        GameObject player = GameObject.FindGameObjectWithTag("player");
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        Collision collisionScript = player.GetComponent<Collision>();
        collisionScript.gameOver = false;

        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].SetActive(false);
        }

        playerRb.useGravity = false;
        GameOverScreen.style.display = DisplayStyle.None;
        Vector3 temp = new Vector3(0, 5, 15);
        player.transform.position = temp;
        obstaclesScript.scoreCount = 0;
        score.text = obstaclesScript.scoreCount.ToString();
        PressKey.style.display = DisplayStyle.Flex;
    }
    void OnPlayGameClick(ClickEvent evt)
    {
        resetGame();
    }
}