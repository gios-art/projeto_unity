using UnityEngine;

public class InfoObject : MonoBehaviour
{
    public GameObject painelInfo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            painelInfo.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            painelInfo.SetActive(false);
        }
    }
}