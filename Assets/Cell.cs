using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private Material material;
    private bool isActive = false;
    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        material.EnableKeyword("_EMISSION");

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (isActive)
        {
            float factor = 0.5f * (Mathf.Sin(Time.time * Mathf.PI) + 1f);
            Color color = Consts.cellEmissionMax * factor + Consts.cellEmissionMin * (1f - factor);
            material.SetColor("_EmissionColor", color);
        }
    }
    public void SetActive(bool isActive)
    {
        this.isActive = isActive;
        this.gameObject.SetActive(isActive);
    }
}
