using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance = new ScoreManager();
    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ScoreManager();
            }
            return instance;
        }
    }

    [SerializeField]
    private ScoreIndicator scoreIndicator;
    public int BlackScore { get; set; }
    public int WhiteScore { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SetScore(int blackScore, int whiteScore)
    {
        this.BlackScore = blackScore;
        this.WhiteScore = whiteScore;
    }
}