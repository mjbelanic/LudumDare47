using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RobotPart : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] pointArray;
    public int currentPoint;
    public Transform nextPoint;
    public float speed = 3f;
    
    private bool offOfConveyerBelt;

    void Start()
    {
        pointArray = new Transform[8];
        Transform[] transforms = GameObject.FindGameObjectWithTag("ArmBelt").GetComponentsInChildren<Transform>();
        Array.Copy(transforms, 1, pointArray, 0, transforms.Length - 1);
        offOfConveyerBelt = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!offOfConveyerBelt)
        {
            if (currentPoint < pointArray.Length)
            {
                if (nextPoint == null)
                {
                    nextPoint = pointArray[currentPoint];
                }
                transform.position = Vector3.MoveTowards(transform.position, nextPoint.position, speed * Time.deltaTime);
                if (transform.position == nextPoint.position)
                {
                    currentPoint++;
                    nextPoint = pointArray[currentPoint];
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "EndPoint")
        {
            Destroy(gameObject);
        }
    }

    internal void SetOffOfConveyerStatus()
    {
        offOfConveyerBelt = true;
    }

    internal Colors GetColorEnum()
    {
        string colorName = GetComponent<MeshRenderer>().materials[0].name.Replace(" (Instance)", "");
        if (colorName == "Blue")
        {
            return Colors.Blue;
        }
        if (colorName == "Green")
        {
            return Colors.Green;
        }
        if (colorName == "Red")
        {
            return Colors.Red;
        }
        return Colors.Yellow;

    }

    internal bool GetOffOfConveyerStatus()
    {
        return offOfConveyerBelt;
    }
}
