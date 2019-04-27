using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        Debug.Log($"A new enemy has been destroyed. New count: {enemies.Count}");
    }
}
