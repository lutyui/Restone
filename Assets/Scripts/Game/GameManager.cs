using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = new GameManager();
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }
    [SerializeField]
    private BoardManager boardManager;
    public eStoneKind Turn { get; set; }
    public int TurnNum { get; set; }

    public int initialStoneCount;
    public int newStoneCount;
    public bool isActivated = true;

    public bool isPlayable = false;
    public eGameState gameState { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = eGameState.Initializing;
        Turn = eStoneKind.Black;
        TurnNum++;
        boardManager.InitializeBoard(initialStoneCount);
        boardManager.ActivatePlaceableCells(Turn);
        boardManager.UpdateSelf();
        boardManager.CalcBoardStones(out int blackNum, out int whiteNum);
        ScoreManager.Instance.SetScore(blackNum, whiteNum);
        UIManager.Instance.UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isPlayable)
        {
            (bool isHitBoard, int hitBoardY, int hitBoardX) = InputManager.GetHitBoardPosition();
            if (isHitBoard)
            {
                if (boardManager.PutStone(hitBoardY, hitBoardX, Turn, newStoneCount, TurnNum, true, isActivated))
                {

                    isPlayable = false;
                    ChangeTurn();
                    bool canPut = boardManager.ActivatePlaceableCells(Turn);
                    if (canPut == false)
                    {
                        ChangeTurn();
                    }
                    boardManager.CalcBoardStones(out int blackNum, out int whiteNum);
                    ScoreManager.Instance.SetScore(blackNum, whiteNum);
                    boardManager.UpdateSelf();
                    UIManager.Instance.UpdateUI();

                }
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            newStoneCount++;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            newStoneCount--;
        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            isActivated = !isActivated;
        }
    }

    private void ChangeTurn()
    {
        Turn = Util.GetOppositeTurn(Turn);
        TurnNum++;
    }

    IEnumerator WaitForPalyable()
    {
        yield return new WaitForSeconds(2.0f);
        isPlayable = true;
    }
}