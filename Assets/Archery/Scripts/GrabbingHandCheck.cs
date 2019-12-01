using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;

public class GrabbingHandCheck : MonoBehaviour
{
  

    public void CheckHand()
    {
        var hand = this.GetComponentInParent<TrackedPoseDriver>();
        switch (hand.poseSource)
        {
           
            case TrackedPoseDriver.TrackedPose.LeftPose:
                ArrowManager.Instance.currentgrabbingHand = grabbingHand.LeftHand;

                break;
            case TrackedPoseDriver.TrackedPose.RightPose:
                ArrowManager.Instance.currentgrabbingHand = grabbingHand.RightHand;

                break;
          
            default:
                break;
        }

    }
    

}
