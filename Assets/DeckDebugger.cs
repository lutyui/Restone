using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class DeckDebugger : MonoBehaviour
{
    [SerializeField]
    private Deck deck;
    [SerializeField]
    private Text deckNum;
    [SerializeField]
    private Text idList;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        deckNum.text = deck.stoneIds.Count.ToString();
        List<string> stringIdList = deck.stoneIds.Select(x => x.ToString()).ToList();
        idList.text = string.Join(",", stringIdList);
    }
}
