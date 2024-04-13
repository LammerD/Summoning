using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource GlobalAudio;
    [SerializeField] AudioSource PlayerAudio;

    [SerializeField] AudioClip GameOverLaugh;

    public void PlayGameOverLaugh()
    {
        StartCoroutine(FadeOut(GlobalAudio, GameManager.Instance.GameOverTime));
    }
    public IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        var startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
        PlayerAudio.clip = GameOverLaugh;
        PlayerAudio.Play();
    }
}
