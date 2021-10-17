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
    }

    public void Draw()
    {
        int id = deck.Draw();
        if (id == -1)
        {
            // cannot draw;
        }
        stoneIds.Add(id);
    }

    public void AddHand(int id)
    {

    }
}
