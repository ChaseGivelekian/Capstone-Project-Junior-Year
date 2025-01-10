using Core;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changeSound; //Sound played when moving arrow up/down
    [SerializeField] private AudioClip interactSound; //Sound played when clicking on an option
    private RectTransform _rect;
    private int _currentPosition;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }
    private void Update()
    {
        //Change position of the selection arrow
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangePosition(-1);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangePosition(1);
        }

        //Interact with options
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Interact();
        }
    }
    private void ChangePosition(int change)
    {
        _currentPosition += change;

        if (change != 0)
        {
            SoundManager.Instance.PlaySound(changeSound);
        }

        if (_currentPosition < 0)
        {
            _currentPosition = options.Length - 1;
        }
        else if (_currentPosition > options.Length - 1)
        {
            _currentPosition = 0;
        }

        //Assign the Y position of the current option to the arrow (this moves it up and down)
        _rect.position = new Vector3(_rect.position.x, options[_currentPosition].position.y, 0);
    }
    private void Interact()
    {
        SoundManager.Instance.PlaySound(interactSound);

        //Access the button component on each option and call it's function
        options[_currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
