using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using TMPro;

public enum GameState
{
    Started,
    Playing,
    Completed
}

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    [HideInInspector] public GameState gameState;
    [HideInInspector] public int level;
    public TextMeshProUGUI firstPlaceText;
    public TextMeshProUGUI secondPlaceText;
    public TextMeshProUGUI thirdPlaceText;
    public Image progressBar;

    int bumperCount;

    void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        SetGameState(GameState.Started);
        level = PrefManager.GetInt("level", 1);
        EventManager.onDie += OnDie;
        EventManager.onCollisionWithBumper += OnCollisionWithBumper;
        progressBar.fillAmount = 0f;
        GameObject obj = Resources.Load<GameObject>($"Levels/{level}");
        if (obj != null)
        {
            Instantiate(obj, Vector3.zero, Quaternion.identity);
        }
        bumperCount = FindObjectOfType<Bumpers>().transform.childCount;
    }

    void SetGameState(GameState state)
    {
        gameState = state;
    }

    public IEnumerator GameWin()
    {

        if (gameState != GameState.Completed)
        {
            level++;
            PrefManager.SetInt("level", level);
            SetGameState(GameState.Completed);
            yield return new WaitForSeconds(2f);
            UIManager.instance.Show<GameWinUI>();
        }

    }

    public IEnumerator GameOver()
    {

        if(gameState != GameState.Completed)
        {
            SetGameState(GameState.Completed);
            yield return new WaitForSeconds(2f);
            UIManager.instance.Show<GameOverUI>();
        }

        
    }

    public void StartGame()
    {
        SetGameState(GameState.Playing);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnDie()
    {
        StartCoroutine(GameOver());
    }

    void OnCollisionWithBumper(float value)
    {
        float endValue = value / bumperCount;
        progressBar.DOFillAmount(endValue, .1f);
    }

    void OnDisable()
    {
        EventManager.onDie -= OnDie;
    }

}
