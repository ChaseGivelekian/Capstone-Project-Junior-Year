using UnityEngine;

public class DoorToBoss : MonoBehaviour
{
    [SerializeField] private Transform door;
    [SerializeField] private Transform player;
    [SerializeField] private Camera defaultCam;
    [SerializeField] private Camera bossCam;

    private void Update()
    {
        player = player.GetComponent<Transform>();
        door = door.GetComponent<Transform>();
        if (player.position.x > door.position.x)
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
