using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;

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
    }
    
    private void OnDisable()
    {
        _gameState.OnGameStarted -= HideStartUI;
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
        _startGameText.transform.DOScale(0f, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            _startGameText.gameObject.SetActive(false);
        });
    }
}
