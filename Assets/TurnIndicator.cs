using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnIndicator : MonoBehaviour
{
    [SerializeField]
    private Text turnText;
    [SerializeField]
    private Text turnCountText;
    private const string BLACK_TURN_TEXT = "Black Turn";
    private const string WHITE_TURN_TEXT = "White Turn";

    public void SetTurnText(eStoneKind stoneKind)
    {
        switch (stoneKind)
        {
            case eStoneKind.Black:
                turnText.text = BLACK_TURN_TEXT;
                break;
            case eStoneKind.White:
                turnText.text = WHITE_TURN_TEXT;
                break;
            default:
                break;
        }
        turnCountText.text = GameManager.Instance.TurnNum.ToString();
    }
}
