using UnityEngine;

public class ShrinkEye : MonoBehaviour
{
    [SerializeField] Light Light;
    Vector3 startSize;
    float startIntensity;

    bool canResetSize;
    void Awake()
    {
        startSize = transform.localScale;
        startIntensity = Light.intensity;
    }

    void Update()
    {
        if (GameManager.Instance.BeatEndlessCorridor)
        {
            enabled = false;
        }
        if (GameManager.Instance.InEndlessCorridor && GameManager.Instance.IsLookingAtTarget && canResetSize)
        {
            canResetSize = false;
            ResetScale();
        }
        if (GameManager.Instance.InEndlessCorridor && !GameManager.Instance.IsLookingAtTarget)
        {
            canResetSize = true;
            var targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            targetDir.Normalize();
            if (targetDir.y != 0 || targetDir.x != 0)
            {
                transform.localScale *= 0.99f;
                Light.intensity *= 0.99f;
            }
        }
    }

    public void ResetScale()
    {
        transform.localScale = startSize;
        Light.intensity = startIntensity;
    }
}
