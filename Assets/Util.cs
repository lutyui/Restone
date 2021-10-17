public static class Util
{
    public static eStoneKind GetOppositeTurn(eStoneKind stoneKind)
    {
        switch (stoneKind)
        {
            case eStoneKind.Black:
                return eStoneKind.White;
            case eStoneKind.White:
                return eStoneKind.Black;
            default:
                return eStoneKind.None;
        }
    }
}