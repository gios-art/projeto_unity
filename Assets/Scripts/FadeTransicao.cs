using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeTransicao : MonoBehaviour
{
    [Header("Configurações")]
    [Tooltip("Nome exato da cena a carregar")]
    public string nomeDaCena;

    [Tooltip("Duração do fade em segundos")]
    public float duracaoFade = 1.5f;

    [Header("Referências")]
    [Tooltip("CanvasGroup anexado ao painel de fade")]
    public CanvasGroup painelFade;

    void Start()
    {
        // Garante que o painel começa transparente e não bloqueia cliques do jogador
        if (painelFade != null)
        {
            painelFade.alpha = 0f;
            painelFade.interactable = false;
            painelFade.blocksRaycasts = false;
            painelFade.gameObject.SetActive(true);
        }
    }

    // O [ContextMenu] cria um botão clicável dentro da Unity, no Inspector do script
    [ContextMenu("Forçar Fade (Teste)")]
    public void IniciarTransicao()
    {
        // Esse Log vai aparecer vermelho no Console para você achar fácil
        Debug.LogWarning("SUCESSO: A função IniciarTransicao foi chamada no script!");

        if (painelFade != null)
        {
            painelFade.blocksRaycasts = true;
            StartCoroutine(FadeECarregarCena());
        }
        else
        {
            Debug.LogError("ERRO: O CanvasGroup do PainelFade não foi atribuído no Inspector!");
        }
    }

    IEnumerator FadeECarregarCena()
    {
        // Fade OUT (tela escurece)
        yield return StartCoroutine(Fade(0f, 1f));

        // Carrega a cena
        SceneManager.LoadScene(nomeDaCena);
    }

    IEnumerator Fade(float alphaInicio, float alphaFim)
    {
        float tempo = 0f;

        while (tempo < duracaoFade)
        {
            tempo += Time.deltaTime;
            painelFade.alpha = Mathf.Lerp(alphaInicio, alphaFim, tempo / duracaoFade);
            yield return null;
        }

        painelFade.alpha = alphaFim;
    }
}