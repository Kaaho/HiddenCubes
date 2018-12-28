using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStuffGenerator : MonoBehaviour {

    public int level;
    public int numberOfObjects;
    public GameObject[] objectsToCreate;
    public Material[] materials;
    public float radius;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnEnable()
    {
        TargetCube.OnCubesReady += CreateRandomStuff;
    }


    void OnDisable()
    {
        TargetCube.OnCubesReady -= CreateRandomStuff;
    }

    private void CreateRandomStuff(int newlevel)
    {
        if (level == newlevel)
        {
            //Debug.Log("Creating random stuff");
            for (int i = 0; i<numberOfObjects; i++)
            {
                GameObject go = Instantiate(objectsToCreate[Random.Range(0, objectsToCreate.Length)]);
                go.transform.SetParent(gameObject.transform);
                go.GetComponent<MeshRenderer>().material = materials[Random.Range(0, materials.Length)];
                go.transform.localPosition = new Vector3(Random.Range(-radius, radius), 2f, Random.Range(-radius, radius));
                go.transform.Rotate(new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)));
            }
            
        }
    }
}
