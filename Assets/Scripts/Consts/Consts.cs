using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Consts
{
    public const int BoardWidth = 8;
    public const int BoardHeight = 8;
    public const float InitialStoneHeight = 1f;
    public const float InitialCellHeight = 0.01f;
    public const float StonePositionOffset = 3.5f;
    public const float InitialStoneRotationRange = 30f;
    public const float StoneSize = 1f;
    public static eStoneKind[,] initialStones = new eStoneKind[,]
    {
    {eStoneKind.None, eStoneKind.None,eStoneKind.None,eStoneKind.None,eStoneKind.None,eStoneKind.None,eStoneKind.None,eStoneKind.None},
    {eStoneKind.None, eStoneKind.None,eStoneKind.None,eStoneKind.None,eStoneKind.None,eStoneKind.None,eStoneKind.None,eStoneKind.None},
    {eStoneKind.None, eStoneKind.None,eStoneKind.None,eStoneKind.None,eStoneKind.None,eStoneKind.None,eStoneKind.None,eStoneKind.None},
    {eStoneKind.None, eStoneKind.None,eStoneKind.None,eStoneKind.White,eStoneKind.Black,eStoneKind.None,eStoneKind.None,eStoneKind.None},
    {eStoneKind.None, eStoneKind.None,eStoneKind.None,eStoneKind.Black,eStoneKind.White,eStoneKind.None,eStoneKind.None,eStoneKind.None},
    {eStoneKind.None, eStoneKind.None,eStoneKind.None,eStoneKind.None,eStoneKind.None,eStoneKind.None,eStoneKind.None,eStoneKind.None},
    {eStoneKind.None, eStoneKind.None,eStoneKind.None,eStoneKind.None,eStoneKind.None,eStoneKind.None,eStoneKind.None,eStoneKind.None},
    {eStoneKind.None, eStoneKind.None,eStoneKind.None,eStoneKind.None,eStoneKind.None,eStoneKind.None,eStoneKind.None,eStoneKind.None}
    };

    public static readonly (int y, int x)[] directions = new (int, int)[]{
                            (-1,-1), (-1, 0), (-1, 1),
                            (0,-1), (0, 0), (0, 1),
                            (1,-1), (1, 0), (1, 1)
    };

    public static readonly Color cellEmissionMax = new Color(0.161f, 0.456f, 0.1f);
    public static readonly Color cellEmissionMin = new Color(0.05f, 0.05f, 0.05f);
}
