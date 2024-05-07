using UnityEngine.UI;
using UnityEngine;

public class Manabar : MonoBehaviour
{
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private Image totalmanaBar;
    [SerializeField] private Image currentmanaBar;
    // private float value;

    private void Start()
    {
        // value = playerAttack.GetComponent<PlayerAttack>().manaAmount;
        totalmanaBar.fillAmount = playerAttack.manaAmount / 10 / 10;
    }
    private void Update()
    {
        // value = playerAttack.GetComponent<PlayerAttack>().manaAmount;
        currentmanaBar.fillAmount = playerAttack.manaAmount / 10 / 10;
    }
}
