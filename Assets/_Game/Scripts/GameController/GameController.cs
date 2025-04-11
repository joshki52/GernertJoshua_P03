using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Scene Controls")]
    [SerializeField] string _mainMenuName   = "MainMenu";
    [SerializeField] string _levelName      = "Game";

    [Header("Dependencies")]
    [SerializeField] private PlayerTurret _playerTurretPrefab;
    [SerializeField] private PlayerTurretSpawner _playerTurretSpawner;
    [SerializeField] private InputHandlerV2 _input;
    [SerializeField] private GameHUDController _gameHUD;

    public PlayerTurret PlayerTurretPrefab => _playerTurretPrefab;
    public PlayerTurretSpawner PlayerTurretSpawner => _playerTurretSpawner;
    public InputHandlerV2 Input => _input;
    public GameHUDController GameHUD => _gameHUD;

    private void Start()
    {
        ResumeGame();   // make sure timescale is set to 1
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(_mainMenuName);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(_levelName);
    }
}
