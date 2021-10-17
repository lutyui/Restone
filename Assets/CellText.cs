using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellText : MonoBehaviour
{
    [SerializeField]
    private TextMesh textMesh;
    // Update is called once per frame
    void Update()
    {
        Vector3 p = Camera.main.transform.position;
        transform.LookAt(2 * transform.position - p);
    }
    public void UpdateText(int count)
    {
        if (count > 0)
        {

            textMesh.text = count.ToString();
        }
        else
        {

            textMesh.text = "";
        }
    }
}
