using UnityEngine;

public class CadeiraSentarVR : MonoBehaviour
{
    [Header("Configuração do Assento")]
    public Transform seatPoint;
    public Transform xrOrigin;

    [Header("Tecla para sentar/levantar")]
    public KeyCode teclaSentar = KeyCode.E;

    private bool sentado = false;
    private bool jogadorDentroDoTrigger = false; // <-- flag que controla tudo
    private Vector3 posicaoAntes;

    void Start()
    {
        if (xrOrigin == null)
        {
            GameObject jogador = GameObject.FindGameObjectWithTag("Player");
            if (jogador != null)
                xrOrigin = jogador.transform;
            else
                Debug.LogWarning("[Cadeira] Tag 'Player' não encontrada!");
        }

        // Cria o trigger de proximidade automaticamente
        bool temTrigger = false;
        foreach (var col in GetComponents<Collider>())
            if (col.isTrigger) { temTrigger = true; break; }

        if (!temTrigger)
        {
            SphereCollider sc = gameObject.AddComponent<SphereCollider>();
            sc.isTrigger = true;
            sc.radius = 1.2f;
        }
    }

    void Update()
    {
        // SÓ escuta o E se o jogador estiver dentro da área da cadeira
        if (!jogadorDentroDoTrigger) return;

        if (Input.GetKeyDown(teclaSentar))
        {
            if (!sentado)
                Sentar();
            else
                Levantar();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!EhJogador(other)) return;
        jogadorDentroDoTrigger = true;
        Debug.Log("[Cadeira] Perto da cadeira — pressione E para sentar!");
    }

    void OnTriggerExit(Collider other)
    {
        if (!EhJogador(other)) return;
        if (!sentado)
        {
            jogadorDentroDoTrigger = false;
            Debug.Log("[Cadeira] Saiu da área da cadeira.");
        }
    }

    private bool EhJogador(Collider other)
    {
        return other.CompareTag("Player")
            || other.CompareTag("MainCamera")
            || other.name.ToLower().Contains("xr")
            || other.name.ToLower().Contains("camera");
    }

    public void Sentar()
    {
        if (seatPoint == null || xrOrigin == null) return;

        sentado = true;
        posicaoAntes = xrOrigin.position;

        Vector3 offsetCam = Vector3.zero;
        if (Camera.main != null)
            offsetCam = new Vector3(Camera.main.transform.localPosition.x, 0,
                                    Camera.main.transform.localPosition.z);

        xrOrigin.position = seatPoint.position - offsetCam;

        Vector3 frente = seatPoint.forward;
        frente.y = 0;
        if (frente.sqrMagnitude > 0.001f)
            xrOrigin.rotation = Quaternion.LookRotation(frente);

        Debug.Log("[Cadeira] SENTOU! Pressione E para levantar.");
    }

    public void Levantar()
    {
        sentado = false;
        jogadorDentroDoTrigger = false;

        if (xrOrigin != null)
        {
            Vector3 saida = transform.position + transform.forward * 1.0f;
            saida.y = posicaoAntes.y;
            xrOrigin.position = saida;
        }

        Debug.Log("[Cadeira] LEVANTOU!");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.2f);
        Gizmos.DrawWireSphere(transform.position, 1.2f);
        if (seatPoint != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(seatPoint.position, 0.12f);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(seatPoint.position, seatPoint.forward * 0.5f);
        }
    }
}
