using UnityEngine;

public class KillTriggerWind : MonoBehaviour
{
    [SerializeField] private CandleLifeTime Candle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !GameManager.Instance.HasWindshield)
        {
            Candle.ExtinguishFlame();
        }
    }
}
