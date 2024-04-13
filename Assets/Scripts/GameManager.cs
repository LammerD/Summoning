using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UnityEvent GameOver;
    public UnityEvent WindShieldGet;
    public static GameManager Instance { get; private set; }
    public float GameOverTime;

    public bool HasWindshield;
    public bool IsGameOver;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetGameOver()
    {
        StartCoroutine(DelayBeforeGameOver());
    }

    public void WindShieldPickedUp()
    {
        HasWindshield = true;
        WindShieldGet.Invoke();
    }

    IEnumerator DelayBeforeGameOver()
    {
        IsGameOver = true;
        yield return new WaitForSeconds(5);
        GameOver.Invoke();
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(0);
    }
}
