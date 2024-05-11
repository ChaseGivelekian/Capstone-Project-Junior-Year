using UnityEngine;

public class DoorToBoss : MonoBehaviour
{
    [SerializeField] private Camera defaultCam;
    [SerializeField] private Camera bossCam;

    private void Awake()
    {
        defaultCam = Camera.main.GetComponent<Camera>();
        bossCam = Camera.main.GetComponent<Camera>();
        defaultCam.enabled = true;
        bossCam.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                defaultCam.enabled = false;
                bossCam.enabled = true;
            }
            else
            {
                defaultCam.enabled = true;
                bossCam.enabled = false;
            }
        }
    }
}
