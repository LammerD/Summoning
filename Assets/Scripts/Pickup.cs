using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.WindShieldPickedUp();
            this.gameObject.SetActive(false);
        }
    }
}
