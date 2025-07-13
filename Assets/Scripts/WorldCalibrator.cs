using UnityEngine;

public class WorldCalibrator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Press Space or B on controller to recenter
        if (Input.GetKeyDown(KeyCode.Space) || OVRInput.GetDown(OVRInput.Button.Two))
        {
            Debug.Log("[SimpleRecenterTest] Recentering pose...");
            OVRManager.display.RecenterPose();
        }
    }
}
