using UnityEngine.UI;
using UnityEngine;

public class Manabar : MonoBehaviour
{
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private PlayerMeleeAttack playerMeleeAttack;
    [SerializeField] private Image totalmanaBar;
    [SerializeField] private Image currentmanaBar;

    private void Update()
    {
        totalmanaBar.fillAmount = playerMeleeAttack.maxMana / 10 / 10;
        currentmanaBar.fillAmount = playerAttack.manaAmount / 10 / 10;
    }
}
