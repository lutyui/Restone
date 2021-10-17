using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoardUtil
{
    public static bool IsInBoard(int boardY, int boardX)
    {
        return ((0 <= boardY) && (boardY < Consts.BoardHeight) && (0 <= boardX) && (boardX < Consts.BoardWidth));
    }


}
