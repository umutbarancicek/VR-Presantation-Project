using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;
using Unity.XR.CoreUtils;
using UnityEngine.InputSystem;

public class NetworkPlayer : MonoBehaviour
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;
    private PhotonView photonView;
    public HandsControllerScript rHandController;
    public HandsControllerScript lHandController;
    public Animator leftHandAnim;
    public Animator rightHandAnim;

    private Transform headRig, leftHandRig, rightHandRig;
    
    void Start()
    {

        photonView = GetComponent<PhotonView>();
        XROrigin rig = FindObjectOfType<XROrigin>();
        headRig= rig.transform.Find("Camera Offset/Main Camera");
        leftHandRig = rig.transform.Find("Camera Offset/LeftHand Controller");
        rightHandRig = rig.transform.Find("Camera Offset/RightHand Controller");

        
        if (photonView.IsMine)
        {

            //foreach (var item in GetComponentsInChildren<Renderer>())
            //{
             //   item.enabled = false;
            //}

           

            // Subscribe to events in HandsControllerScript
            lHandController.OnTriggerPressed += HandleLeftTriggerPressed;
            lHandController.OnGripPressed += HandleLeftGripPressed;
            rHandController.OnTriggerPressed += HandleRightTriggerPressed;
            rHandController.OnGripPressed += HandleRightGripPressed;
        }

    }

    void Update()
    {
        if(photonView.IsMine) 
        { 
           

            MapPosition(head, headRig);
            MapPosition(leftHand, leftHandRig);
            MapPosition(rightHand, rightHandRig);

        }
       

    }
    private void HandleRightTriggerPressed(float triggerValue)
    {
        rightHandAnim.SetFloat("Trigger", triggerValue);
    }

   
    private void HandleRightGripPressed(float gripValue)
    {
        rightHandAnim.SetFloat("Grip", gripValue);
    }
    private void HandleLeftTriggerPressed(float triggerValue)
    {
        leftHandAnim.SetFloat("Trigger", triggerValue);
    }

    private void HandleLeftGripPressed(float gripValue)
    {
        leftHandAnim.SetFloat("Grip", gripValue);
    }
    void MapPosition(Transform target, Transform rigTransform)
    {
       
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation; 

    }
    void OnDisable()
    {
        if (photonView.IsMine)
        {
            // Unsubscribe from events when the object is disabled
            lHandController.OnTriggerPressed -= HandleLeftTriggerPressed;
            lHandController.OnGripPressed -= HandleLeftGripPressed;
            rHandController.OnTriggerPressed -= HandleRightTriggerPressed;
            rHandController.OnGripPressed -= HandleRightGripPressed;
        }
    }
}
