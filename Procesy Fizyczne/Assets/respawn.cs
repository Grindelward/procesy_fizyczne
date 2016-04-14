using UnityEngine;
using System.Collections;

public class respawn : MonoBehaviour
{

    public Vector3 initPosition;
    public Terrain terrain;

    [Header("Respawn delay")]
    public int seconds = 2;

    private Rigidbody sphere;
    private move moveScript;
    private float maxX, minX;
    private float maxZ, minZ;

    // Use this for initialization
    void Start()
    {
        sphere = GetComponent<Rigidbody>(); ;
        maxX = terrain.transform.position.x + terrain.terrainData.size.x;
        minX = terrain.transform.position.x;
        maxZ = terrain.transform.position.z + terrain.terrainData.size.z;
        minZ = terrain.transform.position.z;
        moveScript = GetComponent<move>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sphere.transform.position.x < minX ||
            sphere.transform.position.x > maxX ||
            sphere.transform.position.z > maxZ ||
            sphere.transform.position.z < minZ)
        {
            moveScript.enabled = false;
            sphere.velocity = Vector3.zero;
            sphere.transform.rotation = Quaternion.identity;
            sphere.angularVelocity = Vector3.zero;
            sphere.transform.position = initPosition;
        }
    }
}