using UnityEngine;

public class CandleLifeTime : MonoBehaviour
{
    [SerializeField] GameObject Candle;
    [SerializeField] GameObject Flame;
    [SerializeField] GameObject WindShield;
    [SerializeField] ParticleSystem TrailingParticles;

    public float TimeLeft;

    public Vector3 TargetScale = Vector3.one * .5f;

    Vector3 startScale;

    float t = 0;

    void OnEnable()
    {
        startScale = transform.localScale;
        t = 0;
    }

    void Update()
    {
        if (GameManager.Instance.IsGameOver) return;
        t += Time.deltaTime / TimeLeft;

        transform.localScale = Vector3.Lerp(startScale, TargetScale, t);

        if (t > 1)
        {
            ExtinguishFlame();
            Candle.SetActive(false);
        }
    }

    [ContextMenu("ResetCandle")]
    public void ResetCandle()
    {
        transform.localScale = startScale;
        t = 0;
    }

    public void ExtinguishFlame()
    {
        Flame.SetActive(false);
        GameManager.Instance.SetGameOver();
    }

    [ContextMenu("ToggleWindshield")]
    public void EquipWindshield()
    {
        WindShield.SetActive(true);
        var collisionModule = TrailingParticles.collision;
        var mainModule = TrailingParticles.main;
        var externalForcesModule = TrailingParticles.externalForces;
        mainModule.simulationSpace = ParticleSystemSimulationSpace.Local;
        collisionModule.enabled = true;
        externalForcesModule.enabled = false;
    }
}
