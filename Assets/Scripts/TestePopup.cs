using UnityEngine;

public class TestePopup : MonoBehaviour
{
    public POIPopup poiPopup;
    public FadeTransicao fadeTransicao;

    void Update()
    {
        // Aperta Espaço pra abrir o popup
        if (Input.GetKeyDown(KeyCode.Space))
        {
            poiPopup.AbrirPopup();
        }

        // Aperta Escape pra fechar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            poiPopup.FecharPopup();
        }

        // Aperta E pra iniciar transição de cena no teste local
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (fadeTransicao != null)
            {
                Debug.Log("Tecla E pressionada - forçando a transição!");
                fadeTransicao.IniciarTransicao();
            }
            else
            {
                Debug.LogWarning("Script de Fade não encontrado! Arraste o CanvasFade para o script TestePopup.");
            }
        }
    }
}