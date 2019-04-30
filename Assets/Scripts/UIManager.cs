using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI healthText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateHealth()
    {
        healthText.text = "Health: <color=black>" + GameManager.Instance.health.ToString();
    }

}
