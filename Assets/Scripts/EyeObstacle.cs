#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeObstacle : MonoBehaviour
{
    [SerializeField] Light Light = new();
    [SerializeField] AudioSource Audio = new();
    [SerializeField] List<Color> SelectedColors = new();
    [SerializeField] float LightSwitchPeriod = 10f;
    [SerializeField] CandleLifeTime? Candle;

    int activeColorIndex;
    float startingIntesity;
    bool soundPlaying;
    Coroutine? currentCoroutine;

    private void Awake()
    {
        startingIntesity = Light.intensity;
        StartCoroutine(LightSwitcher());
    }

    private void Update()
    {
        if (GameManager.Instance.BeatEyeObstacle)
        {
            Light.intensity = startingIntesity;
            Light.color = SelectedColors[0];
            Audio.Stop();
            StopAllCoroutines();
            enabled = false;
        }
        if (GameManager.Instance.InEyeObstacle && activeColorIndex != 1 && !GameManager.Instance.IsGameOver)
        {
            var targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            targetDir.Normalize();
            if (targetDir.y != 0 || targetDir.x != 0)
            {
                Light.intensity *= 1.05f;
                if (!soundPlaying)
                {
                    currentCoroutine ??= StartCoroutine(VolumeIncrease());

                    soundPlaying = true;
                    Audio.Play();
                }
            }
            else
            {
                if (soundPlaying)
                {
                    soundPlaying = false;
                    Audio.Pause();
                }
            }
        }
    }

    IEnumerator LightSwitcher()
    {
        while (true)
        {
            yield return new WaitForSeconds(LightSwitchPeriod);
            Light.intensity = startingIntesity;
            Light.color = SelectedColors[activeColorIndex++];

            if (activeColorIndex == 1)
            {
                Audio.volume = 0;
                soundPlaying = false;
                Audio.Stop();
                if (currentCoroutine != null)
                {
                    StopCoroutine(currentCoroutine);
                    currentCoroutine = null;
                }
            }
            if (activeColorIndex >= SelectedColors.Count) activeColorIndex = 0;
        }
    }

    IEnumerator VolumeIncrease()
    {
        while (Audio.volume < 1)
        {
            if (soundPlaying)
            {
                Audio.volume += 0.025f;
            }
            yield return new WaitForSeconds(0.1f);
        }

        Light.enabled = false;
        Audio.enabled = false;
        Candle!.ExtinguishFlame();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.Instance.BeatEyeObstacle && other.CompareTag("Player"))
        {
            GameManager.Instance.StartEyeObstacle();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!GameManager.Instance.BeatEyeObstacle && other.CompareTag("Player"))
        {
            GameManager.Instance.StopEyeObstacle();
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
                currentCoroutine = null;
                Audio.Stop();
            }
        }
    }
}
