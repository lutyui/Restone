using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandStone : MonoBehaviour
{
    [SerializeField]
    List<Sprite> srcImages;
    [SerializeField]
    Text text;



    public void Initialize(eStoneKind stoneKind, int id)
    {
        Image image = GetComponent<Image>();
        if (stoneKind == eStoneKind.Black)
        {
            image.sprite = srcImages[0];
        }
        else
        {
            image.sprite = srcImages[1];
        }
        text.text = id.ToString();
    }

    public void OnSelected()
    {
        Debug.Log(text.text);
    }

}
