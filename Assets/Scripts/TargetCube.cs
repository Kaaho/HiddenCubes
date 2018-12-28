using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCube : MonoBehaviour {

    public GameObject[] startPositions;
    public GameObject[] shadowCubes;
    public GameObject[] doorPositions;

    public delegate void ReadyForNextLevel(int newlevel);
    public static event ReadyForNextLevel OnCubesReady;

    private Vector3 targetposition;
    public bool moving;
    public static int CubesInPlace = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (moving)
        {
            //Debug.Log(gameObject.name +  "is moving");
            transform.position = Vector3.MoveTowards(transform.position, targetposition, 3*Time.deltaTime);
            if ((transform.position - doorPositions[Dude.Level - 1].transform.position).magnitude < 0.1f)
            {
                targetposition = startPositions[Dude.Level - 1].transform.position;
                //Debug.Log(gameObject.name + " reached doorposition");
            }
            else if ((transform.position - startPositions[Dude.Level - 1].transform.position).magnitude < 0.1f)
            {
                //Debug.Log(gameObject.name + " reached startposition");
                moving = false;
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                CubesInPlace++;
                if (CubesInPlace == 3)
                {
                    CubesInPlace = 0;
                    if (OnCubesReady != null) OnCubesReady(Dude.Level);
                }
            }
        }
    }

    void OnEnable()
    {
        Dude.OnLevelUp += MoveTargetCube;
    }


    void OnDisable()
    {
        Dude.OnLevelUp -= MoveTargetCube;
    }



    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "DropPoint")
        {
            Vector3 position = shadowCubes[Dude.Level-1].transform.position;
            transform.position = position;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GameObject dude = GameObject.FindWithTag("dude");
            dude.GetComponent<Dude>().FoundCube();
            GameObject.Find("AudioPlayer").GetComponent<AudioController>().PlayCubeFound();
        }
    }

    private void MoveTargetCube(int newlevel)
    {
        moving = true;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        targetposition = doorPositions[Dude.Level - 1].transform.position;

    }

}
