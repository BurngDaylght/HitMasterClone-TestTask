using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Button _tapToPlayButton;
    [SerializeField] private Text _startGameText;

    private GameState _gameState;
    
    [Inject]
    private void Construct(GameState gameState)
    {
        _gameState = gameState;
    }

    private void OnEnable()
    {
        _gameState.OnGameStarted += HideStartUI;
        _gameState.OnGameFinished += ShowResultUI;
    }
    
    private void OnDisable()
    {
        _gameState.OnGameStarted -= HideStartUI;
        _gameState.OnGameFinished -= ShowResultUI;
    }


    private void Start()
    {
        _tapToPlayButton.onClick.AddListener(TapToPlayButtonClicked);
    }
    
    private void TapToPlayButtonClicked()
    {
        _tapToPlayButton.gameObject.SetActive(false);
        _gameState.StartGame();
    }

    private void HideStartUI()
    {
        // TOOD: animation
        _startGameText.gameObject.SetActive(false);
    }
    
    private void ShowResultUI()
    {
        // TOOD: animation
    }
}
