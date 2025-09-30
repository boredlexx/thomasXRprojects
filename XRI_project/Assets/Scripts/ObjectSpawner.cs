using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem.XR; //for commnicating with quest controller

//select objet to spawn
// where object spawns
//cool down
//input button
//hand

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab; //object to spawn
    public Transform spawnPoint; //where it spawns
    public XRNode controllerNode = XRNode.RightHand; //assigning controller
    public float spawnCooldown = 1.0f; //need a coroutine
    private bool canSpawn = true; //time in seconds between spawns

    // Update is called once per frame
    void Update()
    {
        if (canSpawn && IsAButtonPressed())
        {
            StartCoroutine(SpawnObjectWithCooldown());
        }
    }

    bool IsAButtonPressed()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);
        bool buttonPressed = false;

        if (device.TryGetFeatureValue(CommonUsages.triggerButton, out buttonPressed) && buttonPressed) //primary button is "a' or "x" nutton on controller
        {
            return true;
        }

        return false;
    }

    IEnumerator SpawnObjectWithCooldown()
    {
        canSpawn = false; //prevents immediate rspawning
        SpawnObject();
        yield return new WaitForSeconds(spawnCooldown);
        canSpawn = true; //allow us to spawn again
    }

    void SpawnObject()
    {
        if(objectPrefab != null && spawnPoint)
        {
            GameObject spawnObject = Instantiate(objectPrefab, spawnPoint.position, spawnPoint.rotation);
        }

        else
        {
            Debug.LogError("Assign objectPrefab and spawnPoint in the Inspector");
        }

    }
}
