using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    //public TextMeshProUGUI healthText;
    //public TextMeshProUGUI UIObjectText;

    public GameObject pausePanel;
    bool Paused = false;

    private void Start()
    {
        //UIObjectText.text = ""; //Text is blank at the start of the game
        pausePanel.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {

        {
            if (Input.GetKey("escape"))
            {
                if (Paused == true)
                {
                    Time.timeScale = 1.0f;
                    pausePanel.gameObject.SetActive(false);

                    Paused = false;
                }
                else
                {
                    Time.timeScale = 0.0f;
                    pausePanel.gameObject.SetActive(true);

                    Paused = true;
                }
            }
        }

    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        pausePanel.gameObject.SetActive(false);

    }

    private void Awake()
    {
        Instance = this;
    }

    public void ToggleText(string itemName) //Toggle text is individually set in inspector for each item
    {
        //UIObjectText.text = itemName;
    }

    public void UpdateHealth()
    {
        //healthText.text = "Health: <color=black>" + GameManager.Instance.health.ToString();
    }

}
