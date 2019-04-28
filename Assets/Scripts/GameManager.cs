using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameSettings gameSettings;
    private List<DifficultyMode> difficultyModes = new List<DifficultyMode>();
    private List<Enemy> enemies = new List<Enemy>();

    private void Awake()
    {
        gameSettings = Resources.Load<GameSettings>("GameSettings");
        difficultyModes.AddRange(Resources.LoadAll<DifficultyMode>("DifficultyModes"));

        foreach (DifficultyMode difficultyMode in difficultyModes)
        {
            if (difficultyMode.DifficultyLevel == gameSettings.DifficultyLevel)
            {
                Player player = FindObjectOfType<Player>();
                player.MaxAmmo = difficultyMode.PlayerMaxAmmo;
                player.InitialAmmo = difficultyMode.PlayerInitialAmmo;
                break;
            }
        }

    }

    //void Start()
    //{
    //}

    //void Update()
    //{
    //}

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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1, LoadSceneMode.Single);
        }

        Debug.Log($"A new enemy has been destroyed. New count: {enemies.Count}");
    }

    public void GameOver()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1, LoadSceneMode.Single);
    }
}
