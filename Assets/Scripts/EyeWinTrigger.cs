using UnityEngine;

public class EyeWinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.Instance.BeatEyeObstacle && other.CompareTag("Player"))
        {
            GameManager.Instance.WonEyeObstacle();
        }
    }
}
