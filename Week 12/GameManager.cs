using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyOnePrefab;
    public GameObject cloudPrefab;

    public TextMeshProUGUI livesText;

    [SerializeField] private TextMeshProUGUI scoreText; // ← assign in Inspector if you can

    public float horizontalScreenSize;
    public float verticalScreenSize;

    public int score;

    void Awake()
    {
        // Fallback: auto-find a UI object named "ScoreText" if not assigned
        if (scoreText == null)
        {
            GameObject go = GameObject.Find("ScoreText");
            if (go != null) scoreText = go.GetComponent<TextMeshProUGUI>();
        }
    }

    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;
        score = 0;

        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        CreateSky();
        InvokeRepeating(nameof(CreateEnemy), 1f, 3f);

        UpdateScoreText(); // show initial score
    }

    void CreateEnemy()
    {
        Instantiate(
            enemyOnePrefab,
            new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0),
            Quaternion.Euler(180, 0, 0)
        );
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(
                cloudPrefab,
                new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize),
                            Random.Range(-verticalScreenSize, verticalScreenSize), 0),
                Quaternion.identity
            );
        }
    }

    public void AddScore(int earnedScore)
    {
        score += earnedScore;
        UpdateScoreText();
    }

    public void ChangeLivesText(int currentLives)
    {
        if (livesText != null) livesText.text = "Lives: " + currentLives;
    }

    private void UpdateScoreText()
    {
        if (scoreText != null) scoreText.text = "Score: " + score;
        else Debug.LogError("GameManager: scoreText is not assigned and no 'ScoreText' object was found in the scene.");
    }
}
