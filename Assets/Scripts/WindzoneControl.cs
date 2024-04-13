using UnityEngine;

public class WindzoneControl : MonoBehaviour
{
    [SerializeField] GameObject WindZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WindZone.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WindZone.SetActive(false);
        }
    }
}
