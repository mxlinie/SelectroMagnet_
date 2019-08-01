using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int health = 3;
    public int maxHealth = 3;
    public int maxPlayerHealth = 3;
    //public int score = 0;
    //public static GameManager Instance;
    //private static GameManager instance;

    public string levelTwo;


    // Start is called before the first frame update
    void Start()
    {
        //score = 0;
        health = 3;
    }

    public void OnPickUp()
    {
        //Player.Instance.numOfHearts = 4;
        //Player.Instance.health = 4;
        maxHealth = 4;
        maxPlayerHealth = 4;
    }

    public void OnPhotoPickUp()
    {
        maxHealth = 5;
        maxPlayerHealth = 5;
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


    //private void Awake()
    //{
    //    Instance = this;
    //}
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
