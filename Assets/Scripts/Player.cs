using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed = 10.0f;
    public float runSpeed = 20.0f;
    public float rotationSpeed = 10.0f;
    public float gravity = -9.8f;
    public CanvasGroup pausePanel;
    public CanvasGroup gameOverPanel;
    public bool pauseUp = false;
    public bool gameOverUp = false;
    public HUDDisplay display; 

    private int errors;
    private int robotsMade;
    private float grabRadius = 10.0f;
    private float dropRadius = 11.0f;
    private bool holdingPart;
    private CharacterController _characterController;
    private GameObject heldPart;
    
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Transform _holdArea;


    // Start is called before the first frame update
    void Start()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        _characterController = GetComponent<CharacterController>();
        holdingPart = false;
        robotsMade = 0;
        errors = 0;
        pausePanel.alpha = 0;
        pausePanel.interactable = false;
        pausePanel.blocksRaycasts = false;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = (Input.GetKey(KeyCode.LeftShift)) ? runSpeed : walkSpeed;
        float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float deltaZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;
        _characterController.Move(movement);


        if (Input.GetKeyUp(KeyCode.E))
        {
            PickUpItem();
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            PlaceItem();
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            if (!pauseUp)
            {
                pauseUp = true;
                pausePanel.alpha = 1;
                pausePanel.interactable = true;
                pausePanel.blocksRaycasts = true;
                Time.timeScale = 0;
            }
            else
            {
                pauseUp = false;
                pausePanel.alpha = 0;
                pausePanel.interactable = false;
                pausePanel.blocksRaycasts = false;
                Time.timeScale = 1;
            }
        }
        display.UpdateErrorCount(errors);
        display.UpdateRobotsMadeText(robotsMade);
        CheckForGameOver();
    }

    void FixedUpdate()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0.0f;

        if (playerPlane.Raycast(ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    internal void AddError()
    {
        errors++;
        display.UpdateErrorCount(errors);
    }

    void PickUpItem()
    {
        if (holdingPart)
        {
            return;
        }
        else
        {
            GameObject[] parts;
            parts = GameObject.FindGameObjectsWithTag("RobotPart");
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject part in parts)
            {
                Vector3 diff = part.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = part;
                    distance = curDistance;
                }
            }
            if (closest != null && !closest.GetComponent<RobotPart>().GetOffOfConveyerStatus())
            {
                if (Vector3.Distance(position, closest.transform.position) < grabRadius)
                {
                    Vector3 direction = position - closest.transform.position;
                    if (Vector3.Dot(transform.forward, direction) < 0f)
                    {
                        heldPart = closest;
                        heldPart.GetComponent<RobotPart>().SetOffOfConveyerStatus();
                        heldPart.GetComponent<Collider>().enabled = false;
                        heldPart.transform.parent = transform;
                        heldPart.transform.position = _holdArea.transform.position;
                        holdingPart = true;
                    }
                }
            }
        }
        return;
    }

    void PlaceItem()
    {
        if (!holdingPart)
        {
            return;
        }

        GameObject robotPlatform = GameObject.FindGameObjectWithTag("RobotPlatform");
        GameObject trash = GameObject.FindGameObjectWithTag("Trash");
        Vector3 position = transform.position;
        if (Vector3.Distance(position, robotPlatform.transform.position) < dropRadius)
        {
            Vector3 direction = position - robotPlatform.transform.position;
            if (Vector3.Dot(transform.forward, direction) < 0f)
            {
                robotPlatform.GetComponent<RobotPlatform>().SetPiece(heldPart);
                heldPart = null;
                holdingPart = false;
            }
        }


        if (Vector3.Distance(position, trash.transform.position) < 5.0f)
        {
            Destroy(heldPart);
            heldPart = null;
            holdingPart = false;
        }
        return;
    }

    internal void IncreaseRobotMadeCount()
    {
        robotsMade++;
        display.UpdateRobotsMadeText(robotsMade);
    }

    void CheckForGameOver()
    {
        if(errors > 2)
        {
            gameOverUp = true;
            gameOverPanel.alpha = 1;
            gameOverPanel.interactable = true;
            gameOverPanel.blocksRaycasts = true;
            display.EditGameOverText(robotsMade);
            Time.timeScale = 0;
        }
    }
}
