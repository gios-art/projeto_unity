using UnityEngine;
using UnityEngine.UI;

public class POIPopup : MonoBehaviour
{
    [Header("Referências")]
    [Tooltip("Canvas do Popup (começa desativado)")]
    public GameObject canvasPopup;

    [Tooltip("Canvas do POI (some quando popup abre)")]
    public GameObject canvasPOI;

    // Chamado pelo BotaoPOI ao clicar
    public void AbrirPopup()
    {
        if (canvasPopup != null)
            canvasPopup.SetActive(true);

        if (canvasPOI != null)
            canvasPOI.SetActive(false);
    }

    // Chamado pelo BotaoEntrar ou botão de fechar
    public void FecharPopup()
    {
        if (canvasPopup != null)
            canvasPopup.SetActive(false);

        if (canvasPOI != null)
            canvasPOI.SetActive(true);
    }
}
