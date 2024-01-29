using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HMDInfoManager : MonoBehaviour
{
    public GameObject xrDeviceSim;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Is device active " + XRSettings.isDeviceActive);
        Debug.Log("Device Name " + XRSettings.loadedDeviceName);

        if (!XRSettings.isDeviceActive)
        {
            Debug.Log("No headset plugged");
        }
        else if (XRSettings.isDeviceActive && (XRSettings.loadedDeviceName == "Mock HMD" || XRSettings.loadedDeviceName == "MockHMD Display"))
        {
            Debug.Log("Currently using the mock HMD");
            xrDeviceSim.SetActive(true);
        }
        else
        {
            Debug.Log("Loaded an original headset " + XRSettings.loadedDeviceName);
            xrDeviceSim.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
