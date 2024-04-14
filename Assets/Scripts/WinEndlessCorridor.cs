using UnityEngine;

public class WinEndlessCorridor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.Instance.BeatEndlessCorridor && other.CompareTag("Player"))
        {
            GameManager.Instance.WonEndlessCorridor();
        }
    }
}
