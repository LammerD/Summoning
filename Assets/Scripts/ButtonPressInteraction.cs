using System.Collections;
using UnityEngine;

public class ButtonPressInteraction : MonoBehaviour
{
    [SerializeField] Interactable Interactable;
    [SerializeField] int Order;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Material OnMaterial;

    private Material defaultMaterial;
    private void Awake() => defaultMaterial = meshRenderer.material;

    private void Update()
    {
        if (Interactable.WasInteractedWith)
        {
            ActivateButton();
        }
    }

    void ActivateButton()
    {
        Interactable.WasInteractedWith = false;
        Interactable.CanBeInteractedWith = false;
        Interactable.ToggleHighlight(false);
        meshRenderer.material = OnMaterial;
        GameManager.Instance.InteractWithButton(Order);
    }

    public void ResetButton()
    {
        StartCoroutine(ResetAfterDelay());
    }
    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(.5f);
        meshRenderer.material = defaultMaterial;
        Interactable.CanBeInteractedWith = true;
    }
}
