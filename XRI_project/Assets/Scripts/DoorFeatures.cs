using JetBrains.Annotations;
using System.Collections;
using System.Data;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class DoorFeatures : CoreFeatures //need to inherit our CoreValues script
{
    [Header("Door Configurations")]
    [SerializeField]
    private Transform doorPivot; //Specifically Y rotation

    [SerializeField]
    private float maxAngle = 90.0f; //probably will need to be less than 90

    [SerializeField]
    private bool reverseAngleDirection = false; //flips directon

    [SerializeField]
    private float doorSpeed = 2.0f;

    [SerializeField]
    private bool open = false;

    [SerializeField]
    private bool MakeKinematicOnOpen = false;

    [Header("Interactions Configuration")]
    [SerializeField]
    private XRSocketInteractor socketInteractor;

    [SerializeField]
    private XRSimpleInteractable simpleInteractable;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //when key gets clsoe to th socket, add a listner
        //s = shorthand, slectenterevnts
        socketInteractor?.selectEntered.AddListener((s) => //abstracion - hiding complexity
        {
            OpenDoor();
            PlayOnStart();
        });

        socketInteractor?.selectExited.AddListener((s) =>
        {
            PlayOnEnd();
            socketInteractor.socketActive = featureUsage == FeatureUsage.Once ? false : true; //reusability
            simpleInteractable?.selectEntered.AddListener((s) =>
            {
                OpenDoor();
            });

        });

        //testing only - delete me
        //OpenDoor();
    }

        //doors with simple interactirs may no require a akey. also good for cabinets and drawers
    public void OpenDoor()
    {
        PlayOnStart();
        open = true;
        StartCoroutine(ProcessMotion());
    }

    private IEnumerator ProcessMotion()
    {
        while (open)
        {
            var angle = doorPivot.localEulerAngles.y < 180 ? doorPivot.localEulerAngles.y : doorPivot.localEulerAngles.y - 360;

            angle = reverseAngleDirection ? Mathf.Abs(angle) : angle;

            if (angle <= maxAngle)
            {
                doorPivot?.Rotate(Vector3.up, doorSpeed * Time.deltaTime * (reverseAngleDirection ? -1: 1));
            }

            else
            {
                //when done wiht opening, turn off
                open = false;
                var featureRigidBody = GetComponent<Rigidbody>();
                if (featureRigidBody != null && MakeKinematicOnOpen) featureRigidBody.isKinematic = true;
            }

            yield return null;
        }
    }

 }
