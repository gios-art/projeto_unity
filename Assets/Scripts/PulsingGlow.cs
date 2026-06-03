using UnityEngine;

public class PulsingGlow : MonoBehaviour
{
    public float pulseSpeed = 2f;
    public float minWidth = 3f;
    public float maxWidth = 8f;

    private Outline outline;

    void Start()
    {
        outline = GetComponent<Outline>();
    }

    void Update()
    {
        if (outline != null && outline.enabled)
        {
            float t = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f;
            outline.OutlineWidth = Mathf.Lerp(minWidth, maxWidth, t);
        }
    }
}