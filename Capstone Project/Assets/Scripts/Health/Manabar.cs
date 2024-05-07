using UnityEngine.UI;
using UnityEngine;

public class Manabar : MonoBehaviour
{
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private Image totalmanaBar;
    [SerializeField] private Image currentmanaBar;

    private void Start()
    {
        totalmanaBar.fillAmount = playerAttack.manaAmount / 10 / 10;
    }
    private void Update()
    {
        currentmanaBar.fillAmount = playerAttack.manaAmount / 10 / 10;
    }
}
