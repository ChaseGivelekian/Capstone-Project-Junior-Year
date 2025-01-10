using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private Vector3[] _initialPosition;

    private void Awake()
    {
        //Save the initial positions of the enemies
        _initialPosition = new Vector3[enemies.Length];
        for (var i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                _initialPosition[i] = enemies[i].transform.position;
            }
        }
    }
    public void ActivateRoom(bool status)
    {
        //Activate/deactivate enemies
        for (var i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null) continue;
            enemies[i].SetActive(status);
            enemies[i].transform.position = _initialPosition[i];
        }
    }
}
