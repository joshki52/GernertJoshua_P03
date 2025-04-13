using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class GameHUDController : MonoBehaviour
{
    private GameController      _gameController;
    private InputHandlerV2      _input;

    private PlayerTurret        _playerTurret;
    private Weapon              _weapon;

    private UIDocument          _document;
    private VisualElement       _playerHUDVisualTree;
    private VisualElement       _pauseMenuVisualTree;
    private VisualElement       _loseMenuVisualTree;

    private Button              _pauseButtonHUD;
    private Button              _fireButtonHUD;
    //private Slider            _turretSliderHUD;

    private Button              _quitButtonPause;
    private Button              _resumeButtonPause;
    private Button              _restartButtonPause;

    private List<Button>        _buttonList = new List<Button>();

    public UnityEvent<string>   UIInteracted;

    //public bool TouchingButton { get; private set; } = false;
    //public float TurretSliderValue { get; private set; }

    private void Awake()
    {
        _gameController         = FindAnyObjectByType<GameController>();
        _input                  = FindAnyObjectByType<InputHandlerV2>();

        _document               = GetComponent<UIDocument>();

        _playerHUDVisualTree    = _document.rootVisualElement.Q("PlayerHUDVisualTree");
        _pauseMenuVisualTree    = _document.rootVisualElement.Q("PauseMenuVisualTree");
        _loseMenuVisualTree     = _document.rootVisualElement.Q("LoseMenuVisualTree");

        _pauseButtonHUD         = _playerHUDVisualTree.Q("PauseButton") as Button;
        _fireButtonHUD          = _playerHUDVisualTree.Q("FireButton") as Button;

        _quitButtonPause        = _pauseMenuVisualTree.Q("QuitButton") as Button;
        _resumeButtonPause      = _pauseMenuVisualTree.Q("ResumeGameButton") as Button;
        _restartButtonPause     = _pauseMenuVisualTree.Q("RestartGameButton") as Button;

        // list of all buttons in UI so foreach loop can be used
        _buttonList.Add(_pauseButtonHUD);
        _buttonList.Add(_fireButtonHUD);
        _buttonList.Add(_quitButtonPause);
        _buttonList.Add(_resumeButtonPause);
        _buttonList.Add(_restartButtonPause);

        //SetupPickingMode(); // used so player HUD and touch controls interact correctly during gameplay
    }

    private void OnEnable()
    {
        _pauseButtonHUD.RegisterCallback<ClickEvent>(OnPauseButtonHUDClick);

        _fireButtonHUD.RegisterCallback<PointerDownEvent>(evt => OnFireButtonHUDClick(evt), TrickleDown.TrickleDown);

        _quitButtonPause.RegisterCallback<ClickEvent>(OnQuitButtonPauseClick);
        _resumeButtonPause.RegisterCallback<ClickEvent>(OnResumeButtonPauseClick);
        _restartButtonPause.RegisterCallback<ClickEvent>(OnRestartButtonPauseClick);

        // the aforementioned foreach loop
        foreach (Button button in _buttonList)
        {
            string buttonName = button.name.ToString();
            button.RegisterCallback<PointerDownEvent>(evt => OnUIButtonDown(buttonName), TrickleDown.TrickleDown);
        }
    }

    private void OnDisable()
    {
        _pauseButtonHUD.UnregisterCallback<ClickEvent>(OnPauseButtonHUDClick);

        _fireButtonHUD.UnregisterCallback<PointerDownEvent>(evt => OnFireButtonHUDClick(evt), TrickleDown.TrickleDown);

        _quitButtonPause.UnregisterCallback<ClickEvent>(OnQuitButtonPauseClick);
        _resumeButtonPause.UnregisterCallback<ClickEvent>(OnResumeButtonPauseClick);
        _restartButtonPause.UnregisterCallback<ClickEvent>(OnRestartButtonPauseClick);

        // the aforementioned foreach loop
        foreach (Button button in _buttonList)
        {
            string buttonName = button.name.ToString();
            button.UnregisterCallback<PointerDownEvent>(evt => OnUIButtonDown(buttonName), TrickleDown.TrickleDown);
        }
    }
    private void Start()
    {
        _playerTurret = FindAnyObjectByType<PlayerTurret>();
        if (_playerTurret == null) Debug.Log("Turret not found");
        if (_playerTurret.TryGetComponent(out Weapon weapon)) _weapon = weapon; // find any component that inherits from Weapon
    }

    private void OnUIButtonDown(string buttonName)
    {
            UIInteracted?.Invoke(buttonName);
    }

    private void OnPauseButtonHUDClick(ClickEvent evt)
    {
        _gameController.PauseGame();

        _playerHUDVisualTree.style.display = DisplayStyle.None;
        _pauseMenuVisualTree.style.display = DisplayStyle.Flex;
    }

    private void OnFireButtonHUDClick(PointerDownEvent evt)
    {
        if (_input.Touch.phase == TouchPhase.Began)
        {
            _weapon.Fire();
        }
    }

    private void OnQuitButtonPauseClick(ClickEvent evt)
    {
        _gameController.LoadMainMenu();
    }

    private void OnResumeButtonPauseClick(ClickEvent evt)
    {
        _gameController.ResumeGame();

        _playerHUDVisualTree.style.display = DisplayStyle.Flex;
        _pauseMenuVisualTree.style.display = DisplayStyle.None;
    }

    private void OnRestartButtonPauseClick(ClickEvent evt)
    {
        _gameController.LoadLevel();
    }

    private void SetupPickingMode()
    {
        // player HUD
        _playerHUDVisualTree.pickingMode = PickingMode.Position;
        _playerHUDVisualTree.Q("Menus").pickingMode = PickingMode.Ignore;
        _playerHUDVisualTree.Q("Controls").pickingMode = PickingMode.Ignore;
        _playerHUDVisualTree.Q("PauseButton").pickingMode = PickingMode.Position;
        _playerHUDVisualTree.Q("FireButton").pickingMode = PickingMode.Position;
    }
}
