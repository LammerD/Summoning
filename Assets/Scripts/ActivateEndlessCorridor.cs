using UnityEngine;

public class ActivateEndlessCorridor : MonoBehaviour
{
    [SerializeField] ShrinkEye ShrinkEye;
    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.Instance.BeatEndlessCorridor && other.CompareTag("Player"))
        {
            GameManager.Instance.StartEndlessCorridor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!GameManager.Instance.BeatEndlessCorridor && other.CompareTag("Player"))
        {
            GameManager.Instance.StopEndlessCorridor();
            ShrinkEye.ResetScale();
        }
    }
}
