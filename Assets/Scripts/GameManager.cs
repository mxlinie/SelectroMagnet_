using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int health = 3;
    public int maxHealth = 3;
    //public int score = 0;
    public static GameManager Instance;

    public string levelTwo;


    // Start is called before the first frame update
    void Start()
    {
        //score = 0;
        health = 3;

    }

    public void HealthScore(int newHealth) //health trap hit
    {
        health -= newHealth;
        UIManager.Instance.UpdateHealth();
    }

    public void HealthScorePack(int newHealth) //health pack pickup
    {
        health += newHealth;
        UIManager.Instance.UpdateHealth();
    }


    private void Awake()
    {
        Instance = this;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
