using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationPanel : MonoBehaviour
{
    public Player player;
    public HUDDisplay display;
    public Image headImage;
    public Image bodyImage;
    public Image legImage;
    public Slider timeSlider;
    public bool InUse;

    private Color[] colorArray = new Color[4];
    float currentTime = 90.0f;

    // Start is called before the first frame update
    void Start()
    {
        colorArray[0] = Color.blue;
        colorArray[1] = Color.green;
        colorArray[2] = Color.red;
        colorArray[3] = Color.yellow;
        InUse = false;
        timeSlider.maxValue = 90.0f;
    }

    // Update is called once per frame
    void Update()
    {
        CountDownTime();
    }

    public void GenerateInformationPanel()
    {
        headImage.color = colorArray[Random.Range(0, colorArray.Length)];
        bodyImage.color = colorArray[Random.Range(0, colorArray.Length)];
        legImage.color = colorArray[Random.Range(0, colorArray.Length)];
        timeSlider.value = timeSlider.maxValue;
        currentTime = timeSlider.maxValue;
        InUse = true;
    }

    internal Colors GetBodyImageColorEnum()
    {
        if (bodyImage.color == colorArray[0])
        {
            return Colors.Blue;
        }
        if (bodyImage.color == colorArray[1])
        {
            return Colors.Green;
        }
        if (bodyImage.color == colorArray[2])
        {
            return Colors.Red;
        }
        return Colors.Yellow;

    }

    internal Colors GetHeadImageColorEnum()
    {
        if (headImage.color == colorArray[0])
        {
            return Colors.Blue;
        }
        if (headImage.color == colorArray[1])
        {
            return Colors.Green;
        }
        if (headImage.color == colorArray[2])
        {
            return Colors.Red;
        }
        return Colors.Yellow;

    }

    internal Colors GetLegsImageColorEnum()
    {
        if (legImage.color == colorArray[0])
        {
            return Colors.Blue;
        }
        if (legImage.color == colorArray[1])
        {
            return Colors.Green;
        }
        if (legImage.color == colorArray[2])
        {
            return Colors.Red;
        }
        return Colors.Yellow;
    }

    void CountDownTime()
    {
        if (InUse)
        {
            currentTime -= Time.deltaTime;
            if (currentTime > 0.0f)
            {
                timeSlider.value = currentTime;
            }
            else
            {
                player.AddError();
                GetComponent<CanvasGroup>().alpha = 0;
                InUse = false;
                display.RemoveEntries();
            }
        }     
    }
}
