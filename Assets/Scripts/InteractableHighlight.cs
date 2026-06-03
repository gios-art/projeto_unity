using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable))]
public class InteractableHighlight : MonoBehaviour
{
    [Header("Outline Settings")]
    public Color hoverColor = new Color(1f, 0.8f, 0f, 1f);
    public Color defaultColor = new Color(0.2f, 0.8f, 1f, 1f);
    public float outlineWidth = 5f;

    private Outline outline;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = defaultColor;
        outline.OutlineWidth = outlineWidth;
        outline.enabled = true;
    }

    void OnEnable()
    {
        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit);
        interactable.selectEntered.AddListener(OnSelectEnter);
        interactable.selectExited.AddListener(OnSelectExit);
    }

    void OnDisable()
    {
        interactable.hoverEntered.RemoveListener(OnHoverEnter);
        interactable.hoverExited.RemoveListener(OnHoverExit);
        interactable.selectEntered.RemoveListener(OnSelectEnter);
        interactable.selectExited.RemoveListener(OnSelectExit);
    }

    void OnHoverEnter(HoverEnterEventArgs args)
    {
        outline.OutlineColor = hoverColor;
        outline.OutlineWidth = outlineWidth * 1.5f;
    }

    void OnHoverExit(HoverExitEventArgs args)
    {
        outline.OutlineColor = defaultColor;
        outline.OutlineWidth = outlineWidth;
    }

    void OnSelectEnter(SelectEnterEventArgs args)
    {
        outline.enabled = false;
    }

    void OnSelectExit(SelectExitEventArgs args)
    {
        outline.enabled = true;
    }
}