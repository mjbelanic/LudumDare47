using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class RobotPlatform : MonoBehaviour
{
    public GameObject legHolder;
    public GameObject armHolder;
    public GameObject headHolder;
    public HUDDisplay display;
    public Player player; 

    private GameObject legPieceAdded;
    private GameObject armPieceAdded;
    private GameObject headPieceAdded;
    private bool legAdded;
    private bool armAdded;
    private bool headAdded;

    // Start is called before the first frame update
    void Start()
    {
        legAdded = false;
        armAdded = false;
        headAdded = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfAllPiecesCollected();
    }

    void CheckIfAllPiecesCollected()
    {
        if(legAdded && armAdded && headAdded)
        {
            bool match = display.CheckForMatch(legPieceAdded, armPieceAdded, headPieceAdded);
            if (match)
            {
                player.IncreaseRobotMadeCount();
            }
            else
            {
                player.AddError();
            }
            Destroy(legPieceAdded);
            Destroy(armPieceAdded);
            Destroy(headPieceAdded);
            legAdded = false;
            armAdded = false;
            headAdded = false;
        }
        return;
    }

    public void SetPiece(GameObject piece)
    {
        if (piece.name == "RobotHead(Clone)")
        {
            CheckIfPieceIsAdded(headPieceAdded);
            headPieceAdded = piece;
            piece.transform.parent = transform;
            piece.transform.position = headHolder.transform.position;
            headAdded = true;
        }
        if(piece.name == "RobotBody(Clone)")
        {
            CheckIfPieceIsAdded(armPieceAdded);
            armPieceAdded = piece;
            piece.transform.parent = transform;
            piece.transform.position = armHolder.transform.position;
            armAdded = true;
        }
        if(piece.name == "RobotLegs(Clone)")
        {
            CheckIfPieceIsAdded(legPieceAdded);
            legPieceAdded = piece;
            piece.transform.parent = transform;
            piece.transform.position = legHolder.transform.position;
            legAdded = true;
        }
        piece.transform.rotation = Quaternion.identity;
        piece.transform.Rotate(0, 180, 0);
        return;
    }

    private void CheckIfPieceIsAdded(GameObject setPiece)
    {
        if (setPiece != null)
        {
            Destroy(setPiece);
        }
    }
}
