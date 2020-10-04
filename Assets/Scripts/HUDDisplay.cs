using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDDisplay : MonoBehaviour
{
    private int currentNumberOfEntries;
    private int totalNumberOfEntries = 4;
    public float timer = 2.0f;
    public float generateTime;
    public GameObject[] infoPanel = new GameObject[4];
    public Text errorCount;
    public Text scoreCount;
    public Text gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        generateTime = 5.0f;
        currentNumberOfEntries = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentNumberOfEntries < totalNumberOfEntries)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                for (int i = 0; i < totalNumberOfEntries; i++)
                {
                    if (!infoPanel[i].GetComponent<InformationPanel>().InUse)
                    {
                        int openPanel = i;
                        InformationPanel informationPanel = infoPanel[openPanel].GetComponent<InformationPanel>();
                        infoPanel[openPanel].GetComponent<CanvasGroup>().alpha = 1;
                        informationPanel.GenerateInformationPanel();
                        currentNumberOfEntries++;
                        timer = generateTime;
                        break;
                    }
                }
            }
        }
    }

    public bool CheckForMatch(GameObject legPiece, GameObject armPiece, GameObject headPiece)
    {

        for (int i = 0; i < totalNumberOfEntries; i++) {
            if (infoPanel[i].GetComponent<InformationPanel>().InUse)
            {
                InformationPanel informationPanel = infoPanel[i].GetComponent<InformationPanel>();
                if (informationPanel.GetBodyImageColorEnum() == armPiece.GetComponent<RobotPart>().GetColorEnum() &&
                    informationPanel.GetLegsImageColorEnum() == legPiece.GetComponent<RobotPart>().GetColorEnum() &&
                    informationPanel.GetHeadImageColorEnum() == headPiece.GetComponent<RobotPart>().GetColorEnum())
                {
                    informationPanel.GetComponent<CanvasGroup>().alpha = 0;
                    informationPanel.InUse = false;
                    RemoveEntries();
                    return true;
                }
            }
        }
        return false;
    }

    internal void RemoveEntries()
    {
        currentNumberOfEntries--;
    }

    internal void UpdateRobotsMadeText(int robotsMade)
    {
        scoreCount.text = "Score: " + robotsMade.ToString();
    }

    internal void UpdateErrorCount(int errorsMade)
    {
        errorCount.text = "Errors: " + errorsMade.ToString() + "/3";
    }

    internal void EditGameOverText(int robotsMade)
    {
        gameOverText.text = "You made " + robotsMade.ToString() + " Robots!";
    }
}
