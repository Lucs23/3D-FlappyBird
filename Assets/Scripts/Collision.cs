using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class Collision : MonoBehaviour
{
    public GameObject GameUi;
    public Obstacles ObstaclesScript;
    private VisualElement GameOverScreen;
    private UIDocument uiDoc;
    private Label Score;
    private Label HighScore;
    private int HighScoreCount = 0;
    private Animator animation;
    public bool gameOver = false;

    void OnEnable()
    {
        animation = GetComponent<Animator>();
        uiDoc = GameUi.GetComponent<UIDocument>();
        Score = uiDoc.rootVisualElement.Q("ScoreInt") as Label;
        HighScore = uiDoc.rootVisualElement.Q("HighScoreInt") as Label;
        GameOverScreen = uiDoc.rootVisualElement.Q("GameOver");
    }

    void FixedUpdate()
    {
        Vector3 temp = transform.position;
        if (temp.y <= 0.039)
        {
            killPlayer();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            killPlayer();
        }
    }
    void killPlayer()
    {
        gameOver = true;
        animation.Play("Death");
        int Currentscore = ObstaclesScript.scoreCount;
        if (Currentscore > HighScoreCount)
        {
            HighScoreCount = Currentscore;
            HighScore.text = HighScoreCount.ToString();
        }
        Score.text = Currentscore.ToString();
        GameOverScreen.style.display = DisplayStyle.Flex;
    }
}
