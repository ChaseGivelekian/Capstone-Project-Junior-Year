using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound; //Sound that plays when getting a new checkpoint
    [SerializeField] public Transform target;
    [SerializeField] public Health[] enemiesHealth;
    private Transform currentCheckpoint; //Stores the last checkpoint here
    private PlayerHealth playerHealth;
    private UIManager uiManager;
    private Animator anim;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        uiManager = FindObjectOfType<UIManager>();
        target.GetComponent<PlayerFloating>().enabled = false;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        bool triggerValue = anim.GetBool("die");
        if (triggerValue)
        {
            target.GetComponent<PlayerFloating>().enabled = true;
        }
        else
        {
            target.GetComponent<BoxCollider2D>().enabled = true;
            target.GetComponent<PlayerMovement>().enabled = true;
        }
        if (transform.position.y >= 5.38)
        {
            CheckRespawn();
        }
    }
    public void CheckRespawn()
    {
        //Check if check point available
        if (currentCheckpoint == null)
        {
            //Show game over screen
            uiManager.GameOver();

            return; //Don't execute the rest of this function
        }

        foreach (var enemy in enemiesHealth)
        {
            enemy.GetComponent<Health>().currentHealth = enemy.GetComponent<Health>().startingHealth;
        }
        transform.position = currentCheckpoint.position; //Move player to checkpoint position
        playerHealth.Respawn(); //Restore player health and reset animation

        //Move camera to checkpoint room (**for this to work the checkpoint objects have to be placed as a child of the room object)
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    }
    //Activate checkpoints
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform; //Store the checkpoint that we activated as the current one
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false; //Deactivate checkpoint collider
            collision.GetComponent<Animator>().SetTrigger("appear"); //Trigger checkpoint animation
        }
    }
}
