using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{

    public eStoneKind stoneKind { get; set; }
    [SerializeField]
    private Rigidbody rigidBody;
    [SerializeField]
    private CellText cellText;
    public int count { get; set; }
    public int putTurn { get; set; }
    public bool IsInvokable { get; set; }
    public int stoneSkillId = 0;
    public int posY { get; set; }
    public int posX { get; set; }

    public void Initialize(eStoneKind stoneKind, int count, int putTurn, int posY, int posX)
    {
        this.stoneKind = stoneKind;
        this.count = count;
        this.putTurn = putTurn;
        this.posY = posY;
        this.posX = posX;
    }


    public void UpdateSelf()
    {
        cellText.UpdateText(count);

    }

    public void TurnOver()
    {
        this.stoneKind = Util.GetOppositeTurn(stoneKind);
        Vector3 direction = Quaternion.Euler(0, Random.Range(-180f, 180f), 0) * transform.right;
        rigidBody.AddForce(transform.up * 4f, ForceMode.Impulse);
        rigidBody.AddTorque(direction * 20f);
    }

    public void SetCount(int count)
    {
        this.count = count;
        cellText.UpdateText(count);
    }

    public IEnumerator Invoke()
    {
        if (IsInvokable)
        {
            IsInvokable = false;
            yield return StoneSkills.InvokeSkills(stoneSkillId, this);
        }
        yield return null;
    }
}
