using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSkills
{
    public static IEnumerator InvokeSkills(int skillId, Stone stone)
    {
        if (skillId == 0)
        {
            yield return TurnOverSelff(stone);
        }
        yield return null;
    }

    public static IEnumerator TurnOverSelff(Stone stone)
    {
        stone.stoneKind = Util.GetOppositeTurn(stone.stoneKind);
        BoardManager.Instance.ExecTurnOverStones(BoardManager.Instance.GetTurnOnverStones(stone.posY, stone.posX, stone.stoneKind));
        BoardManager.Instance.stoneKinds[stone.posY, stone.posX] = stone.stoneKind;
        yield return null;
    }
}
