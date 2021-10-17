using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    public static bool GetEnterDown()
    {
        return Input.GetKeyDown(KeyCode.Return);
    }

    public static (bool isHitBoard, int boardY, int boardX) GetHitBoardPosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 0.5f, false);
        Physics.Raycast(ray, out hit, 100);
        int boardY = (int)(-hit.point.z + Consts.BoardHeight / 2.0f);
        int boardX = (int)(hit.point.x + Consts.BoardWidth / 2.0f);
        if (BoardUtil.IsInBoard(boardY, boardX))
        {
            return (true, boardY, boardX);
        }
        else
        {
            return (true, -1, -1);
        }


    }
}
