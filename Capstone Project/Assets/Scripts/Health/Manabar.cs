using UnityEngine.UI;
using UnityEngine;
using System;

public class Manabar : MonoBehaviour
{
    [SerializeField] private Image totalmanaBar;
    [SerializeField] private Image currentmanaBar;

    private void Start()
    {
        totalmanaBar.fillAmount = GetComponent<PlayerAttack>().manaAmount / 10;
    }
    private void Update()
    {
        currentmanaBar.fillAmount = GetComponent<PlayerAttack>().manaAmount / 10;
    }
}
