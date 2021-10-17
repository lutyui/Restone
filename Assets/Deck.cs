using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField]
    private eStoneKind stoneKind;

    public List<int> stoneIds;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int Draw()
    {
        int idIndex = Random.Range(0, stoneIds.Count);
        int id = stoneIds[idIndex];
        stoneIds.Remove(id);
        return id;
    }
}
