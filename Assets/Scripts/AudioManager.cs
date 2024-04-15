using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource GlobalAudio;
    [SerializeField] AudioSource PlayerAudio;

    [SerializeField] AudioClip GameOverLaugh;
    [SerializeField] AudioClip GameWonTrack;

    public void PlayGameOverLaugh()
    {
        StartCoroutine(FadeOut(GlobalAudio, GameManager.Instance.GameOverTime));
    }

    public IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        var startVolume = audioSource.volume;
        PlayerAudio.clip = GameOverLaugh;
        PlayerAudio.Play();
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public void GameWon()
    {
        StartCoroutine(FadeOutWon(2.5f)); ;
    }

    public IEnumerator FadeOutWon(float fadeTime)
    {
        var startVolume = GlobalAudio.volume;
        while (GlobalAudio.volume > 0)
        {
            GlobalAudio.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }
        GlobalAudio.Stop();
        GlobalAudio.clip = GameWonTrack;
        GlobalAudio.Play();
        while (GlobalAudio.volume < 1)
        {
            GlobalAudio.volume += startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }
    }
}
