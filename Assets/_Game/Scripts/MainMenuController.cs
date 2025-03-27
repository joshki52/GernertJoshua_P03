using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameHudController : MonoBehaviour
{
    [SerializeField] string _mainLevelName = "";

    private UIDocument _document;
    private VisualElement _mainMenuVisualTree;

    private Button _quitButton;
    private Button _newGameButton;
    private Button _loadGameButton;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        // get sub objects
        _mainMenuVisualTree = _document.rootVisualElement.Q("MainMenuVisualTree");

        // assign button callbacks
        _quitButton = _mainMenuVisualTree.Q("QuitButton") as Button;
        _newGameButton = _mainMenuVisualTree.Q("NewGameButton") as Button;
        _loadGameButton = _mainMenuVisualTree.Q("LoadGameButton") as Button;
    }

    private void OnEnable()
    {
        _quitButton.RegisterCallback<ClickEvent>(OnQuitButtonClick);
        _newGameButton.RegisterCallback<ClickEvent>(OnNewGameButtonClick);
        _loadGameButton.RegisterCallback<ClickEvent>(OnLoadGameButtonClick);
    }

    private void OnDisable()
    {
        _quitButton.UnregisterCallback<ClickEvent>(OnQuitButtonClick);
        _newGameButton.UnregisterCallback<ClickEvent>(OnNewGameButtonClick);
        _loadGameButton.UnregisterCallback<ClickEvent>(OnLoadGameButtonClick);
    }

    public void OnQuitButtonClick(ClickEvent evt)
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OnNewGameButtonClick(ClickEvent evt)
    {
        Debug.Log("New Game");
        SceneManager.LoadScene(_mainLevelName);
    }
    public void OnLoadGameButtonClick(ClickEvent evt)
    {
        Debug.Log("Load Game");
    }


    /*
    ******************************** Reference Code ********************************

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        
        // get sub objects
        _loseMenuVisualTree = _document.rootVisualElement.Q("LoseMenuVisualTree");
        _winMenuVisualTree = _document.rootVisualElement.Q("WinMenuVisualTree");
        _playerHUDVisualTree = _document.rootVisualElement.Q("PlayerHUDVisualTree");
        _expBarFill = _playerHUDVisualTree.Q("EXPBarFill");
        _healthBarFill = _playerHUDVisualTree.Q("HealthBarFill");

        _elapsedTimeLabel = _document.rootVisualElement.Q("ElapsedTimeLabel") as Label;

        // assign button callbacks
        _loseRetryButton = _loseMenuVisualTree.Q("RetryButton") as Button;
        _winRetryButton = _winMenuVisualTree.Q("RetryButton") as Button;
    }

    private void OnEnable()
    {
        _loseRetryButton.RegisterCallback<ClickEvent>(OnLoseRetryButtonClick);
        _winRetryButton.RegisterCallback<ClickEvent>(OnWinRetryButtonClick);

        _gameController.OnLose.AddListener(EnableLoseDisplay);
        _gameController.OnWin.AddListener(EnableWinDisplay);
    }

    private void OnDisable()
    {
        _loseRetryButton.UnregisterCallback<ClickEvent>(OnLoseRetryButtonClick);
        _winRetryButton.UnregisterCallback<ClickEvent>(OnWinRetryButtonClick);

        _gameController.OnLose.RemoveListener(EnableLoseDisplay);
        _gameController.OnWin.RemoveListener(EnableWinDisplay);
    }

    private void Start()
    {
        DisableLoseDisplay();
        DisableWinDisplay();
        EnablePlayerHUD();
    }

    private void DisableAllDisplays()
    {
        // iterates through all children of GameHUDVisualTree
        // solution taken from https://discussions.unity.com/t/how-do-i-iterate-over-all-visualelements-in-a-visualtreeasset/902249
        foreach (var child in _document.rootVisualElement.hierarchy.Children())
        {
            child.style.display = DisplayStyle.None;
        }
    }

    private void DisableLoseDisplay()
    {
        _loseMenuVisualTree.style.display = DisplayStyle.None;
    }

    private void DisableWinDisplay()
    {
        _winMenuVisualTree.style.display = DisplayStyle.None;
    }

    public void DisablePlayerHUDDisplay()
    {
        _playerHUDVisualTree.style.display = DisplayStyle.Flex;
    }

    public void EnableLoseDisplay()
    {
        _playerHUDVisualTree.style.display = DisplayStyle.None;
        _loseMenuVisualTree.style.display = DisplayStyle.Flex;
    }

    public void EnableWinDisplay()
    {
        _playerHUDVisualTree.style.display = DisplayStyle.None;
        _winMenuVisualTree.style.display = DisplayStyle.Flex;
    }

    public void EnablePlayerHUD()
    {
        _playerHUDVisualTree.style.display = DisplayStyle.Flex;
    }

    private void OnLoseRetryButtonClick(ClickEvent evt)
    {
        DisableLoseDisplay();
        _gameController.ReloadLevel();
    }

    private void OnWinRetryButtonClick(ClickEvent evt)
    {
        DisableWinDisplay();
        _gameController.ReloadLevel();
    }
    */
}
