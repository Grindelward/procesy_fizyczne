using UnityEngine;
using System.Collections;

public class Init : MonoBehaviour {

    public Vector3 globalPositionBest;
    public GameObject[] birds = new GameObject[5];

	// Use this for initialization
	void Start () {
        for(int i = 0; i < 5; i++){
            birds[i] = Instantiate(Resources.Load("Prefabs/BirdPrefab") as GameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
