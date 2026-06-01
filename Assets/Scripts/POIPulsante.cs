using UnityEngine;
using TMPro;

public class POIPulsante : MonoBehaviour
{
    [Header("Movimento Vertical")]
    public float alturaMovimento = 0.06f;  // quanto sobe/desce (bem sutil)
    public float velocidadeMovimento = 1.4f;

    [Header("Brilho")]
    public float velocidadeBrilho = 1.7f;
    public float brilhoMin = 0.4f;   // nunca some, só fica menos brilhante
    public float brilhoMax = 1.8f;

    [Header("Referências")]
    public TextMeshProUGUI textoPlaca;

    private Vector3 posicaoOriginal;

    void Start()
    {
        posicaoOriginal = transform.localPosition;
    }

    void Update()
    {
        float t = (Mathf.Sin(Time.time * velocidadeMovimento) + 1f) / 2f;

        // Movimento vertical suave
        Vector3 pos = posicaoOriginal;
        pos.y += Mathf.Sin(Time.time * velocidadeMovimento) * alturaMovimento;
        transform.localPosition = pos;

        // Brilho (alpha) — nunca some completamente
        if (textoPlaca != null)
        {
            Color cor = textoPlaca.color;
            cor.a = Mathf.Lerp(brilhoMin, brilhoMax, t);
            textoPlaca.color = cor;
        }
    }

    public void PararPulso()
    {
        transform.localPosition = posicaoOriginal;
        if (textoPlaca != null)
        {
            Color cor = textoPlaca.color;
            cor.a = 1f;
            textoPlaca.color = cor;
        }
        enabled = false;
    }
}