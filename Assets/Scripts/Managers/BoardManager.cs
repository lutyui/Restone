using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoardManager : MonoBehaviour
{
    private static BoardManager instance = new BoardManager();
    public static BoardManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BoardManager();
            }
            return instance;
        }
    }

    /// <summary>
    /// 石の Prefab
    /// </summary>
    [SerializeField]
    private GameObject stonePrefab;
    [SerializeField]
    private GameObject cellPrefab;
    private Stone[,] stones = new Stone[Consts.BoardHeight, Consts.BoardWidth];
    public eStoneKind[,] stoneKinds = new eStoneKind[Consts.BoardHeight, Consts.BoardWidth];
    private Cell[,] cells = new Cell[Consts.BoardHeight, Consts.BoardWidth];

    public void InitializeBoard(int initialStoneCount)
    {
        InitializeStones(initialStoneCount);
        InitializeCells();
    }

    private void InitializeStones(int initialStoneCount)
    {
        for (int i = 0; i < stones.GetLength(0); i++)
        {
            for (int j = 0; j < stones.GetLength(1); j++)
            {
                if (Consts.initialStones[i, j] == eStoneKind.Black)
                {
                    GameObject stone = InstantiateStone(i, j, eStoneKind.Black);
                    stones[i, j] = stone.GetComponent<Stone>();
                    stones[i, j].Initialize(eStoneKind.Black, initialStoneCount, 0, i, j);
                    stoneKinds[i, j] = eStoneKind.Black;
                }
                else if (Consts.initialStones[i, j] == eStoneKind.White)
                {
                    GameObject stone = InstantiateStone(i, j, eStoneKind.White);
                    stones[i, j] = stone.GetComponent<Stone>();
                    stones[i, j].Initialize(eStoneKind.White, initialStoneCount, 0, i, j);
                    stoneKinds[i, j] = eStoneKind.White;
                }
                else
                {
                    stoneKinds[i, j] = eStoneKind.None;
                }
            }
        }
    }

    private void InitializeCells()
    {
        for (int i = 0; i < stones.GetLength(0); i++)
        {
            for (int j = 0; j < stones.GetLength(1); j++)
            {
                GameObject cell = InstantiateCell(i, j);
                cells[i, j] = cell.GetComponent<Cell>();
                cells[i, j].SetActive(false);
            }
        }
    }

    public GameObject InstantiateStone(int boardY, int boardX, eStoneKind stoneKind)
    {
        Quaternion initialRotation
            = Quaternion.Euler(Random.Range(-Consts.InitialStoneRotationRange, Consts.InitialStoneRotationRange),
            0f,
            Random.Range(-Consts.InitialStoneRotationRange, Consts.InitialStoneRotationRange)
        );
        if (stoneKind == eStoneKind.White)
        {
            initialRotation *= Quaternion.Euler(180f, 0, 0);
        }
        GameObject stone = Instantiate(stonePrefab,
                new Vector3(boardX * Consts.StoneSize - Consts.StonePositionOffset, Consts.InitialStoneHeight, -boardY * Consts.StoneSize + Consts.StonePositionOffset),
                initialRotation
         ) as GameObject;
        return stone;
    }

    public GameObject InstantiateCell(int boardY, int boardX)
    {
        GameObject cell = Instantiate(cellPrefab,
                new Vector3(boardX * Consts.StoneSize - Consts.StonePositionOffset, Consts.InitialCellHeight, -boardY * Consts.StoneSize + Consts.StonePositionOffset),
                Quaternion.identity
         ) as GameObject;
        return cell;
    }

    public void UpdateSelf()
    {
        var activatedStones = new List<(Stone, int, int)>();
        for (int i = 0; i < stones.GetLength(0); i++)
        {
            for (int j = 0; j < stones.GetLength(1); j++)
            {
                if (stoneKinds[i, j] != eStoneKind.None)
                {
                    stones[i, j].count--;
                    stones[i, j].UpdateSelf();
                    if (stones[i, j].count == 0)
                    {
                        activatedStones.Add((stones[i, j], i, j));
                    }
                }
            }
        }
        StartCoroutine(ExecStoneEvents(activatedStones));
    }

    private IEnumerator ExecStoneEvents(List<(Stone stone, int boardY, int boardX)> activatedStones)
    {
        yield return new WaitForSeconds(2f);
        activatedStones = activatedStones.OrderBy(x => x.stone.putTurn).ToList();
        for (int i = 0; i < activatedStones.Count; i++)
        {
            var activatedStone = activatedStones[i];
            activatedStone.stone.TurnOver();
            stoneKinds[activatedStone.boardY, activatedStone.boardX] = stones[activatedStone.boardY, activatedStone.boardX].stoneKind;
            ExecTurnOverStones(GetTurnOnverStones(activatedStone.boardY, activatedStone.boardX, activatedStone.stone.stoneKind));
            ActivatePlaceableCells(GameManager.Instance.Turn);
            CalcBoardStones(out int blackNum, out int whiteNum);
            ScoreManager.Instance.SetScore(blackNum, whiteNum);
            UIManager.Instance.UpdateUI();
            yield return new WaitForSeconds(2f);
        }
        GameManager.Instance.isPlayable = true;
    }
    public bool ActivatePlaceableCells(eStoneKind stoneKind)
    {
        bool existsPlaceableCell = false;
        for (int i = 0; i < stones.GetLength(0); i++)
        {
            for (int j = 0; j < stones.GetLength(1); j++)
            {
                if (CanPutStone(i, j, stoneKind))
                {
                    cells[i, j].SetActive(true);
                    existsPlaceableCell = true;
                }
                else
                {
                    cells[i, j].SetActive(false);
                }
            }
        }
        return existsPlaceableCell;
    }

    private bool CanPutStone(int boardY, int boardX, eStoneKind stoneKind)
    {
        if (stoneKinds[boardY, boardX] != eStoneKind.None)
        {
            return false;
        }
        List<List<(int y, int x)>> turnOverStones = GetTurnOnverStones(boardY, boardX, stoneKind);
        if (turnOverStones.Any(stones => stones.Count > 0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool PutStone(int boardY, int boardX, eStoneKind stoneKind, int count, int turnNum, bool shouldTurnOver)
    {
        if (stoneKinds[boardY, boardX] != eStoneKind.None)
        {
            return false;
        }
        if (CanPutStone(boardY, boardX, stoneKind))
        {
            if (shouldTurnOver)
            {
                ExecTurnOverStones(GetTurnOnverStones(boardY, boardX, stoneKind));
            }
            GameObject stone = InstantiateStone(boardY, boardX, stoneKind);
            stones[boardY, boardX] = stone.GetComponent<Stone>();
            stones[boardY, boardX].Initialize(stoneKind, count, turnNum, boardY, boardX);
            stoneKinds[boardY, boardX] = stoneKind;
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<List<(int y, int x)>> GetTurnOnverStones(int boardY, int boardX, eStoneKind stoneKind)
    {
        var result = new List<List<(int y, int x)>>();
        // 各方向を見ていく
        for (int i = 0; i < Consts.directions.Length; i++)
        {
            result.Add(new List<(int y, int x)>());
        }

        for (int i = 0; i < Consts.directions.Length; i++)
        {
            (int y, int x) = (boardY, boardX);
            int directionY = Consts.directions[i].y;
            int directionX = Consts.directions[i].x;
            int count = 0;
            y += directionY;
            x += directionX;
            if (BoardUtil.IsInBoard(y, x) == false)
            {
                continue;
            }
            if (stoneKinds[y, x] == stoneKind)
            {
                continue;
            }
            else if (stoneKinds[y, x] == eStoneKind.None)
            {
                continue;
            }
            else
            {
                count++;
            }
            while (true)
            {
                y += directionY;
                x += directionX;
                if (BoardUtil.IsInBoard(y, x) == false)
                {
                    count = 0;
                    break;
                }

                if (stoneKinds[y, x] == stoneKind)
                {
                    break;
                }
                else if (stoneKinds[y, x] == eStoneKind.None)
                {
                    count = 0;
                    break;
                }
                else
                {
                    count++;
                }
            }
            // 結果に追加する
            for (int j = 1; j <= count; j++)
            {
                result[i].Add((boardY + directionY * j, boardX + directionX * j));
            }
        }
        return result;
    }

    public void ExecTurnOverStones(List<List<(int y, int x)>> turnOverStones)
    {
        for (int directionIndex = 0; directionIndex < turnOverStones.Count; directionIndex++)
        {
            for (int i = 0; i < turnOverStones[directionIndex].Count; i++)
            {
                stones[turnOverStones[directionIndex][i].y, turnOverStones[directionIndex][i].x].TurnOver();
                stoneKinds[turnOverStones[directionIndex][i].y, turnOverStones[directionIndex][i].x] = GetTurnedStoneKind(stoneKinds[turnOverStones[directionIndex][i].y, turnOverStones[directionIndex][i].x]);
            }
        }
    }


    private eStoneKind GetTurnedStoneKind(eStoneKind stoneKind)
    {
        if (stoneKind == eStoneKind.Black)
        {
            return eStoneKind.White;
        }
        else if (stoneKind == eStoneKind.White)
        {
            return eStoneKind.Black;
        }
        else
        {
            return eStoneKind.None;
        }
    }

    public void CalcBoardStones(out int blackNum, out int whiteNum)
    {
        blackNum = whiteNum = 0;
        for (int i = 0; i < stones.GetLength(0); i++)
        {
            for (int j = 0; j < stones.GetLength(1); j++)
            {
                if (stoneKinds[i, j] == eStoneKind.Black)
                {
                    blackNum++;
                }
                else if (stoneKinds[i, j] == eStoneKind.White)
                {
                    whiteNum++;
                }
            }
        }
    }
}
