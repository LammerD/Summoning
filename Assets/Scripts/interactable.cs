using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] GameObject Highlight;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Material HighlightMaterial;
    public Material DefaultMaterial;

    private void Awake()
    {
        if (meshRenderer == null) return;
        DefaultMaterial = meshRenderer.material;
    }

    public bool WasInteractedWith;

    public bool CanBeInteractedWith = true;
    public void ToggleRaycastHighlight(bool highlight)
    {
        if (CanBeInteractedWith)
            ToggleHighlight(highlight);
    }

    public void Interact()
    {
        if (CanBeInteractedWith)
            WasInteractedWith = true;
    }

    public void ToggleHighlight(bool highlight)
    {
        if (HighlightMaterial != null)
        {
            meshRenderer.material = highlight ? HighlightMaterial : DefaultMaterial;
        }
        else
        {
            Highlight.SetActive(highlight);
        }
    }

}
