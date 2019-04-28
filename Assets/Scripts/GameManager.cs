using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameSettings gameSettings;
    private PlayerStatus playerStatus;
    private List<DifficultyMode> difficultyModes = new List<DifficultyMode>();
    private List<Enemy> enemies = new List<Enemy>();

    private void Awake()
    {
        playerStatus = Resources.Load<PlayerStatus>("PlayerStatus");
        gameSettings = Resources.Load<GameSettings>("GameSettings");
        difficultyModes.AddRange(Resources.LoadAll<DifficultyMode>("DifficultyModes"));

        foreach (DifficultyMode difficultyMode in difficultyModes)
        {
            if (difficultyMode.DifficultyLevel == gameSettings.DifficultyLevel)
            {
                playerStatus.MaxAmmo = difficultyMode.PlayerMaxAmmo;
                playerStatus.InitialAmmo = difficultyMode.PlayerInitialAmmo;
                break;
            }
        }
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
        Debug.Log("Should boot next stage");
        if (enemies.Count == 0)
        {
            int i = SceneManager.GetActiveScene().buildIndex + 1;
            if (i >= SceneManager.sceneCountInBuildSettings)
            {

                i = 0;
            }
            Debug.Log("loading scene with buildindex");
            FindObjectOfType<Player>().StageCompleted();
            SceneManager.LoadSceneAsync(i, LoadSceneMode.Single);
        }
        else if (enemies.Count == 1)
        {
            enemies[0].Enrage();
        }

        Debug.Log($"A new enemy has been destroyed. New count: {enemies.Count}");
    }

    public void GameOver()
    {
         SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }
}
