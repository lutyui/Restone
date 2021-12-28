using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField]
    private eStoneKind stoneKind;
    [SerializeField]
    private Deck deck;

    public List<int> stoneIds;

    [SerializeField]
    private GameObject handPositionLeft;
    public float handStonesDistance;
    private List<GameObject> handStones = new List<GameObject>();
    [SerializeField]
    private GameObject handStonesParent;
    [SerializeField]
    public GameObject handStonePrefab;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Draw();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            HideHand();
        }
    }

    public void Draw()
    {
        int id = deck.Draw();
        if (id == -1)
        {
            // cannot draw;
        }
        AddHand(id);
    }

    public void AddHand(int id)
    {
        stoneIds.Add(id);
        GameObject stone = Instantiate(handStonePrefab,
            Vector3.zero,
            Quaternion.identity,
            handStonesParent.transform
        ) as GameObject;
        HandStone handStone = stone.GetComponent<HandStone>();
        handStone.Initialize(eStoneKind.Black, id);
        handStones.Add(stone);
        UpdateHandStonePosition();
        ShowHand();
    }
    private void UpdateHandStonePosition()
    {
        for (int i = 0; i < handStones.Count; i++)
        {
            handStones[i].transform.localPosition = new Vector3(handPositionLeft.transform.localPosition.x + i * handStonesDistance + handPositionLeft.transform.localPosition.y, 0);
        }
    }
    public void ShowHand()
    {
        for (int i = 0; i < handStones.Count; i++)
        {
            handStones[i].SetActive(true);
        }
    }

    public void HideHand()
    {
        for (int i = 0; i < handStones.Count; i++)
        {
            handStones[i].SetActive(false);
        }

    }
}
