using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField]
    private TurnIndicator turnIndicator;
    [SerializeField]
    private ScoreIndicator scoreIndicator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void UpdateUI()
    {
        turnIndicator?.SetTurnText(GameManager.Instance.Turn);
        scoreIndicator?.SetScoreTexts();
    }
}
