using Photon.Pun;
using System.Runtime.InteropServices.ComTypes;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform ikTarget;
   
    public void Map()
    {
        ikTarget.position = vrTarget.position;
        ikTarget.rotation = vrTarget.rotation;

    }
}

public class IKTargetFollowVRRig : MonoBehaviour
{
    [Range(0, 1)]
    public float turnSmoothness = 0.1f;
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;

    public Vector3 headBodyPositionOffset;
    public float headBodyYawOffset;

    private PhotonView photonView;


    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        XROrigin rig = FindObjectOfType<XROrigin>();
        head.vrTarget = rig.transform.Find("Camera Offset/Main Camera/HeadVR Target");
        leftHand.vrTarget = rig.transform.Find("Camera Offset/LeftHand Controller/LeftHandVR Target");
        rightHand.vrTarget = rig.transform.Find("Camera Offset/RightHand Controller/RightHandVR Target");

        //XR rig ellerini kaldirmak icin, renderer kapatiliyor.
        if (photonView.IsMine)
        {

            foreach (var item in rig.gameObject.GetComponentsInChildren<Renderer>())
            {
                item.enabled = false;
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            transform.position = head.ikTarget.position + headBodyPositionOffset;
            float yaw = head.vrTarget.eulerAngles.y;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, yaw, transform.eulerAngles.z), turnSmoothness);
            head.Map();
            leftHand.Map();
            rightHand.Map();

        }

    }


}
