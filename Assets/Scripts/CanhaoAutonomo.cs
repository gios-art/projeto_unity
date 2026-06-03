using System.Collections;
using UnityEngine;

public class CanhaoAutonomo : MonoBehaviour
{
    [Header("Patrulha")]
    public Transform[] pontosDePaRulha;
    public float velocidadePatrulha = 1.5f;
    public float tempoEsperaNoPoonto = 1.5f;

    [Header("Interação")]
    public KeyCode teclaInteracao = KeyCode.E;
    public float raioInteracao = 4f;

    private bool ativo = false;
    private int indicePonto = -90;
    private int direcao = 1;
    private bool esperando = false;
    private Transform jogador;
    private bool avisouPerto = false;

    void Start()
    {
        // Trava física para não cair
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        // Busca o jogador uma vez só
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null)
        {
            jogador = obj.transform;
            Debug.Log("[Canhao] Jogador encontrado: " + obj.name);
        }
        else
        {
            Debug.LogError("[Canhao] ERRO: nenhum objeto com tag 'Player' encontrado!");
        }
    }

    void Update()
    {
        if (jogador == null) return;

        float dist = Vector3.Distance(transform.position, jogador.position);
        bool perto = dist <= raioInteracao;

        // Avisa quando chegar perto
        if (perto && !avisouPerto)
        {
            avisouPerto = true;
            Debug.Log("[Canhao] Perto do canhão! Pressione E para ligar/desligar.");
        }
        else if (!perto)
        {
            avisouPerto = false;
        }

        // Pressionar E perto do canhão
        if (perto && Input.GetKeyDown(teclaInteracao))
        {
            ativo = !ativo;
            Debug.Log(ativo ? "[Canhao] LIGADO!" : "[Canhao] DESLIGADO.");
        }

        // Movimento
        if (ativo && !esperando && pontosDePaRulha != null && pontosDePaRulha.Length >= 2)
            Patrulhar();
    }

    private void Patrulhar()
    {
        Transform alvo = pontosDePaRulha[indicePonto];
        Vector3 destino = new Vector3(alvo.position.x, transform.position.y, alvo.position.z);

        transform.position = Vector3.MoveTowards(transform.position, destino, velocidadePatrulha * Time.deltaTime);

        Vector3 dir = destino - transform.position;
        dir.y = 0;
        if (dir.sqrMagnitude > 0.01f)
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                Quaternion.LookRotation(dir), 120f * Time.deltaTime);

        if (Vector3.Distance(transform.position, destino) < 0.05f)
            StartCoroutine(EsperarEAvancar());
    }

    private IEnumerator EsperarEAvancar()
    {
        esperando = true;
        yield return new WaitForSeconds(tempoEsperaNoPoonto);
        indicePonto += direcao;
        if (indicePonto >= pontosDePaRulha.Length || indicePonto < 0)
        {
            direcao *= -1;
            indicePonto += direcao * 2;
        }
        esperando = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 1, 0.25f);
        Gizmos.DrawWireSphere(transform.position, raioInteracao);
        if (pontosDePaRulha == null) return;
        Gizmos.color = Color.yellow;
        for (int i = 0; i < pontosDePaRulha.Length; i++)
        {
            if (pontosDePaRulha[i] == null) continue;
            Gizmos.DrawSphere(pontosDePaRulha[i].position, 0.15f);
            if (i < pontosDePaRulha.Length - 1 && pontosDePaRulha[i + 1] != null)
                Gizmos.DrawLine(pontosDePaRulha[i].position, pontosDePaRulha[i + 1].position);
        }
    }
}
