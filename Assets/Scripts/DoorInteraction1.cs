using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    private bool aberta = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (aberta)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                aberta = false;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                aberta = true;
            }
        }
    }
}