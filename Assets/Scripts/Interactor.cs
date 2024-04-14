#nullable enable
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] float InteractionRange;

    Interactable? lastInteractable;

    void Update()
    {

        if (GameManager.Instance.InEndlessCorridor)
        {
            var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, 500) && hit.collider.CompareTag("LookTarget"))
            {
                GameManager.Instance.LookAtTarget(true);
            }
            else
            {
                GameManager.Instance.LookAtTarget(false); 
            }
        }
        else
        {
            var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, InteractionRange) && hit.collider.CompareTag("Interactable"))
            {
                ResetLastInteractable();
                lastInteractable = hit.collider.GetComponent<Interactable>();
                lastInteractable.ToggleRaycastHighlight(true);
                if (Input.GetButton("Interact"))
                {
                    lastInteractable.Interact();
                }
            }
            else ResetLastInteractable();
        }
    }

    private void ResetLastInteractable()
    {
        if (lastInteractable != null)
        {
            lastInteractable.ToggleRaycastHighlight(false);
            lastInteractable = null;
        }
    }
}