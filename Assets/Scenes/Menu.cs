using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private GameSettings gameSettings;
    private List<DifficultyMode> difficultyModes = new List<DifficultyMode>();
    private PlayerStatus playerStatus;
    private List<Gun> guns = new List<Gun>();

    private void Start()
    {
        playerStatus = Resources.Load<PlayerStatus>("PlayerStatus");
        gameSettings = Resources.Load<GameSettings>("GameSettings");
        difficultyModes.AddRange(Resources.LoadAll<DifficultyMode>("DifficultyModes"));
        guns.AddRange(Resources.LoadAll<Gun>("Guns"));
    }

    public void PlayGame()
    {
        ResetPlayerStatus();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }

    private void ResetPlayerStatus()
    {
        foreach (DifficultyMode difficultyMode in difficultyModes)
        {
            if (difficultyMode.DifficultyLevel == gameSettings.DifficultyLevel)
            {
                playerStatus.CurrentAmmo = difficultyMode.PlayerInitialAmmo;
                playerStatus.MaxAmmo = difficultyMode.PlayerMaxAmmo;
                playerStatus.InitialAmmo = difficultyMode.PlayerInitialAmmo;
                break;
            }
        }
        foreach (Gun gun in guns)
        {
            if (gun.name == "Revolver")
            {
                playerStatus.EquippedGun = gun;
                break;
            }
        }
        playerStatus.PlayerUpgrades.Clear();
    }

    public void ChangeDifficulty(int difficultyLevel)
    {
        gameSettings.DifficultyLevel = (DifficultyLevel)difficultyLevel;
        ResetPlayerStatus();
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
