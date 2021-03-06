﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dude : MonoBehaviour {
    public GameObject player;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject[] wallText;
    public bool touchEnabled = true;
    public GameObject targetPosition;
    public GameObject possibleTargetPosition;
    public GameObject carryPoint;
    public GameObject carryObjects;
    public GameObject carryObject;
    RaycastHit hit = new RaycastHit();
    private int foundCubes = 0;
    private float wave = 0;

    public static int Level = 1;
    public static int Levels = 2;

    public delegate void NextLevel(int newlevel);
    public static event NextLevel OnLevelUp;

    public delegate void RoomEnter(int newlevel);
    public static event RoomEnter OnRoomEnter;

    private bool readyToCloseDoor = false;


    // Use this for initialization
    void Start ()
    {
    }

    // Update is called once per frame
    void Update () {
        if (wave > 0f) WaveHands();
        Vector3 newPosition = new Vector3(targetPosition.transform.position.x, transform.position.y, targetPosition.transform.position.z);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, newPosition, 0.5f*Time.deltaTime);
        gameObject.transform.LookAt(newPosition);
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0)), out hit, 100))
        {
            GameObject hitGameObject = hit.collider.gameObject;
            //Debug.Log(hitGameObject.name + " " + hit.point);
            if (hitGameObject.tag == "floor")
            {
                possibleTargetPosition.transform.position = new Vector3(hit.point.x, targetPosition.transform.position.y, hit.point.z);
            }
        }
        if (readyToCloseDoor)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                GameObject hitgo = hit.collider.gameObject;
                if (hitgo.tag == "floor" && hitgo.GetComponent<Floor>().level == Level)
                {
                    readyToCloseDoor = false;
                    Debug.Log("Entered room " + Level);
                    if (OnRoomEnter != null)
                    {
                        OnRoomEnter(Level);
                    }
                }
            }
        }
    }

    public void DudeClicked()
    {
        Debug.Log("DudeClicked");
        DropCarryObject();
    }

    private void DropCarryObject()
    {
        if (carryObject != null)
        {
            carryObject.GetComponent<Rigidbody>().useGravity = true;
            carryObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            carryObject.transform.SetParent(carryObjects.transform);
            carryObject = null;
            gameObject.transform.position += new Vector3(0.1f, 0, 0);
        }
        gameObject.layer = 2;
    }

    public void SetCarryObject(GameObject pickUp)
    {
        if (carryObject != null) DropCarryObject();
        carryObject = pickUp;
        pickUp.transform.SetParent(carryPoint.transform);
        pickUp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        transform.localRotation = Quaternion.Euler(pickUp.GetComponent<PickUpObject>().carryRotation);
        pickUp.transform.localPosition = new Vector3(0, 0, 0) - pickUp.GetComponent<PickUpObject>().carryPosition;
        gameObject.layer = 0;

    }

    public void FoundCube()
    {
        foundCubes++;
        if (foundCubes == 3)
        {
            wallText[Level-1].GetComponent<Text>().text = "Level " + Level + " completed. Congrats! \n (But where did those cubes go?)";
            leftHand.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
            rightHand.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
            wave = 6f;
            foundCubes = 0;
            Level++;
            if (Level > Levels)
            {
                wallText[Level - 2].GetComponent<Text>().text = "Level " + (Level-1)+" completed. Congrats! \n Game Completed!";
            }
            else if (OnLevelUp != null)
            {
                Debug.Log("Level up");
                OnLevelUp(Level);
            }
        }
        else
        {
            //wallText.GetComponent<Text>().text = (3 - foundCubes) + " cube(s) to be found...";
        }
    }

    void OnEnable()
    {
        AudioClicker.OnClicked += ChangeTargetPosition;
        if (!AndroidPermissionChecker.RuntimePermissionGranted(AndroidPermissionChecker.PERMISSION_RECORD_AUDIO))
        {
            wallText[Level - 1].GetComponent<Text>().text = "Please enable microphone for this app from settings and restart the app.";
        }

        
    }


    void OnDisable()
    {
        AudioClicker.OnClicked -= ChangeTargetPosition;
    }

        void ChangeTargetPosition()
    {
        if (hit.collider != null)
        {
            GameObject hitGameObject = hit.collider.gameObject;
            if (hitGameObject.tag == "floor")
            {
                targetPosition.transform.position = possibleTargetPosition.transform.position;
            }
        }
    }

    private void WaveHands()
    {
        wave -= Time.deltaTime;
        //Debug.Log("WaveHands");
        
        if (Mathf.FloorToInt(wave*2f)%2 == 0)
        {
            leftHand.transform.RotateAround(transform.position, transform.forward, 100 * Time.deltaTime);
            rightHand.transform.RotateAround(transform.position, transform.forward, -100 * Time.deltaTime);
        } else
        {
            leftHand.transform.RotateAround(transform.position, transform.forward, -100 * Time.deltaTime);
            rightHand.transform.RotateAround(transform.position, transform.forward, 100 * Time.deltaTime);
        }
    }

}
