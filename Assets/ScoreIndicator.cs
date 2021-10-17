using UnityEngine;
using UnityEngine.UI;

public class ScoreIndicator : MonoBehaviour
{
    [SerializeField]
    private Text scoreTextBlack;
    [SerializeField]
    private Text scoreTextWhite;

    public void SetScoreTexts()
    {
        scoreTextBlack.text = ScoreManager.Instance.BlackScore.ToString();
        scoreTextWhite.text = ScoreManager.Instance.WhiteScore.ToString();
    }
}
