using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] int LastButtonValue;
    [SerializeField] int AmountOfRequiredSymbols;
    [SerializeField] MeshRenderer EndlessTunnelRenderer;
    [SerializeField] Animator CircleAnimator;
    [SerializeField] Image FadeImage;
    [SerializeField] GameObject WinText;
    public UnityEvent WrongButtonDoorOrder;
    public UnityEvent ButtonDoorOpen;
    public UnityEvent GameOver;
    public UnityEvent WindShieldGet;
    public UnityEvent AllSymbolsRight;

    public static GameManager Instance { get; private set; }
    public float GameOverTime;

    public bool HasWindshield;
    public bool IsGameOver;
    public bool DisabledPlayerMovement;

    List<int> lastButtons = new();
    int rightSymbolsDrawn;
    int wrongSymbolsDrawn;

    public bool InEndlessCorridor;
    public bool IsLookingAtTarget;
    public bool BeatEndlessCorridor;

    public bool BeatEyeObstacle;
    public bool InEyeObstacle;

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
        StartCoroutine(FadeIn());
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

    public void InteractWithButton(int order)
    {
        if (lastButtons.Count > 0 && order != lastButtons.Last() + 1)
        {
            lastButtons.Clear();
            WrongButtonDoorOrder.Invoke();
        }
        else
        {
            lastButtons.Add(order);
        }
        if (lastButtons.Count == LastButtonValue)
        {
            ButtonDoorOpen.Invoke();
        }
    }

    public void InteractWithSymbol(bool set, bool isRequired)
    {
        if (set)
        {
            if (isRequired)
                rightSymbolsDrawn++;
            else
               wrongSymbolsDrawn++;
        }
        else
        {
            if (isRequired)
                rightSymbolsDrawn--;
            else
                wrongSymbolsDrawn--;
        }

        if (rightSymbolsDrawn == AmountOfRequiredSymbols && wrongSymbolsDrawn == 0)
        {
            CircleAnimator.SetTrigger("Win");
        }
    }

    public void WinGame()
    {
        DisabledPlayerMovement = true;
        IsGameOver = true;
        AllSymbolsRight.Invoke();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        while (FadeImage.color.a < 1)
        {
            var tempColor = FadeImage.color;
            tempColor.a += 0.01f;
            FadeImage.color = tempColor;
            yield return new WaitForSeconds(0.01f);
        }
        WinText.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    IEnumerator FadeOutAfterLoss()
    {
        while (FadeImage.color.a < 1)
        {
            var tempColor = FadeImage.color;
            tempColor.a += 0.01f;
            FadeImage.color = tempColor;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator FadeIn()
    {
        while (FadeImage.color.a > 0)
        {
            var tempColor = FadeImage.color;
            tempColor.a -= 0.008f;
            FadeImage.color = tempColor;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator DelayBeforeGameOver()
    {
        IsGameOver = true;
        DisabledPlayerMovement = true;
        GameOver.Invoke();
        yield return new WaitForSeconds(5);
        StartCoroutine(FadeOutAfterLoss());
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainGame");
    }

    public void StartEndlessCorridor()
    {
        InEndlessCorridor = true;
        DisabledPlayerMovement = true;
    }

    public void StopEndlessCorridor()
    {
        DisabledPlayerMovement = false;
        InEndlessCorridor = false;
    }

    public void WonEndlessCorridor()
    {
        DisabledPlayerMovement = false;
        InEndlessCorridor = false;
        BeatEndlessCorridor = true;
        EndlessTunnelRenderer.enabled = true;
    }

    public void LookAtTarget(bool isLooking)
    {
        DisabledPlayerMovement = !isLooking;
        IsLookingAtTarget = isLooking;
    }

    public void StartEyeObstacle()
    {
        InEyeObstacle = true;
    }

    public void StopEyeObstacle()
    {
        InEyeObstacle = false;
    }

    public void WonEyeObstacle()
    {
        BeatEyeObstacle = true;
        InEyeObstacle = false;
    }
}
