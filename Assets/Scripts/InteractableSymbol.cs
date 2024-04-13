using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSymbol : MonoBehaviour
{
    [SerializeField] bool IsRequired;
    [SerializeField] Material OnMaterial;

    private Interactable interactable;
    private Material defaultMaterial;
    private MeshRenderer meshRenderer;
    private bool isDrawn = false;
    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        meshRenderer = GetComponent<MeshRenderer>();
        defaultMaterial = meshRenderer.material;
    }

    private void Update()
    {
        if (interactable.WasInteractedWith)
        {
            ToggleSymbol();
        }
    }

    void ToggleSymbol()
    {
        isDrawn = !isDrawn;
        interactable.WasInteractedWith = false;
        interactable.CanBeInteractedWith = false;
        interactable.ToggleHighlight(false);
        meshRenderer.material = isDrawn ? OnMaterial : defaultMaterial;
        interactable.DefaultMaterial = isDrawn ? OnMaterial : defaultMaterial;
        GameManager.Instance.InteractWithSymbol(isDrawn, IsRequired);
        ResetSymbol();
    }

    public void ResetSymbol()
    {
        StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(.5f);
        interactable.CanBeInteractedWith = true;
    }
}
