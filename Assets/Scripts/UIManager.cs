using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI UIObjectText;

    private void Start()
    {
        UIObjectText.text = ""; //Text is blank at the start of the game
    }

    private void Awake()
    {
        Instance = this;
    }

    public void ToggleText(string itemName) //Toggle text is individually set in inspector for each item
    {
        UIObjectText.text = itemName;
    }

    public void UpdateHealth()
    {
        healthText.text = "Health: <color=black>" + GameManager.Instance.health.ToString();
    }

}
