using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : MonoBehaviour
{
    public GameObject[] robotParts;
    public Material[] robotMaterials;
    public GameObject beltStart;
    public float generateTime;
    public float timer = 2;

    private int currentColorIndex;
    private int currentPartIndex;

    // Start is called before the first frame update
    void Start()
    {
        generateTime = 3.0f;
        currentColorIndex = 0;
        currentPartIndex = 0;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Random random = new Random();
            GameObject partCreated = Instantiate(robotParts[currentPartIndex], beltStart.transform.position, Quaternion.identity) as GameObject;
            MeshRenderer meshRenderer = partCreated.GetComponent<MeshRenderer>();
            meshRenderer.material = robotMaterials[currentColorIndex];
            timer = generateTime;
            currentColorIndex++;
            currentPartIndex++;
            if(currentColorIndex >= robotMaterials.Length)
            {
                currentColorIndex = 0;
            }
            if(currentPartIndex >= robotParts.Length)
            {
                currentPartIndex = 0;
            }
        }
    }
}
