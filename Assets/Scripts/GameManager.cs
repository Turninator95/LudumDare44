using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    List<Enemy> enemies = new List<Enemy>();
    

    void Start()
    {
        

    }

    
    void Update()
    {
        
    }

    public void EnemySpawned(Enemy enemy)
    {
        if (!enemies.Contains(enemy))
        {
            enemies.Add(enemy);
        }
        Debug.Log($"A new enemy has been spawned. New count: {enemies.Count}");
    }

    public void EnemyDestroyed(Enemy enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }

        if (enemies.Count == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        Debug.Log($"A new enemy has been destroyed. New count: {enemies.Count}");
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    
}
