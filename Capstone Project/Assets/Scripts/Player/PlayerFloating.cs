using UnityEngine;

public class PlayerFloating : MonoBehaviour
{
    public float speed = .01f; // Speed at which the object moves up
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

    }
    private void Update()
    {
        bool triggerValue = anim.GetBool("die");
        if (triggerValue)
        {
            if (transform.position.y < 3.82f)
            {
                Vector3 temp = transform.position;
                // while (transform.position.y < 3.82f)
                // {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
                //}
                // while (temp.y < 3.82f)
                // {
                //     temp.y += .001f;
                //     transform.position = temp;
                //     Debug.Log(transform.position.y);
                // }
            }
        }
    }
}
// try the smooth damp from the camera controller script
