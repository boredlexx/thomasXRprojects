using UnityEngine;
using UnityEngine.XR;

//select objet to spawn
// where object spawns
//cool down
//input button
//hand

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab; //object to spawn
    public Transform spawnPoint; //where it spawns
    public XRNode controllerNode = XRNode.RightHand;
    public float spawnCooldown = 1.0f; //need a coroutine
    private bool canSpawn = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
