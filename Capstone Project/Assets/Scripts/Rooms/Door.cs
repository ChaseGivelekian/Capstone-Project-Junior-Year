using Core;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;

    private void Awake()
    {
        if (Camera.main != null) cam = Camera.main.GetComponent<CameraController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        cam.MoveToNewRoom(collision.transform.position.x < transform.position.x ? nextRoom : previousRoom);
    }
}
