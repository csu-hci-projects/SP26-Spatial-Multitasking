using UnityEngine;
using TMPro;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Management;

public class XRITKGestureDetector : MonoBehaviour
{
    public TextMeshPro gestureMessage; // Assign your GestureMessage object here

    private XRHandSubsystem handSubsystem;

    void Start()
    {
        // Get the XR Hand Subsystem
        handSubsystem = XRGeneralSettings.Instance
            .Manager
            .activeLoader
            .GetLoadedSubsystem<XRHandSubsystem>();
    }

    void Update()
    {
        if (handSubsystem == null) return;

        // Check right hand first, then left hand
        if (CheckHand(handSubsystem.rightHand, true)) return;
        if (CheckHand(handSubsystem.leftHand, false)) return;

        gestureMessage.text = "Waiting for gesture...";
    }

    bool CheckHand(XRHand hand, bool isRight)
    {
        if (!hand.isTracked) return false;

        // Finger extension checks with slightly forgiving thresholds
        bool thumbExtended = IsFingerExtended(hand, XRHandJointID.ThumbTip, XRHandJointID.ThumbProximal, 0.03f);
        bool indexExtended = IsFingerExtended(hand, XRHandJointID.IndexTip, XRHandJointID.IndexProximal, 0.08f);
        bool middleExtended = IsFingerExtended(hand, XRHandJointID.MiddleTip, XRHandJointID.MiddleProximal, 0.08f);
        bool ringExtended = IsFingerExtended(hand, XRHandJointID.RingTip, XRHandJointID.RingProximal, 0.08f);
        bool pinkyExtended = IsFingerExtended(hand, XRHandJointID.LittleTip, XRHandJointID.LittleProximal, 0.08f);

        bool indexCurled = !indexExtended;
        bool middleCurled = !middleExtended;
        bool ringCurled = !ringExtended;
        bool pinkyCurled = !pinkyExtended;

        // 1. Pinch detection (thumb tip close to index tip)
        if (IsPinch(hand))
        {
            gestureMessage.text = isRight ? "Right hand pinch!" : "Left hand pinch!";
            return true;
        }

        // 3. Fist detection (all fingers curled, thumb not extended)
        if (IsFist(hand))
        {
            gestureMessage.text = isRight ? "Right hand fist!" : "Left hand fist!";
            return true;
        }

        // 2. Thumbs Up detection (thumb extended, other fingers curled)
        if (thumbExtended && IsThumbPointingUp(hand) &&
             indexCurled && middleCurled && ringCurled && pinkyCurled)
        {
            gestureMessage.text = isRight ? "Right hand thumbs up!" : "Left hand thumbs up!";
            return true;
        }

        return false;
    }

    // Check if a finger is extended based on distance from tip to base
    bool IsFingerExtended(XRHand hand, XRHandJointID tipID, XRHandJointID baseID, float threshold)
    {
        if (hand.GetJoint(tipID).TryGetPose(out Pose tipPose) &&
            hand.GetJoint(baseID).TryGetPose(out Pose basePose))
        {
            float distance = Vector3.Distance(tipPose.position, basePose.position);
            return distance > threshold;
        }
        return false;
    }

    // Detect a pinch by distance between thumb tip and index tip
    bool IsPinch(XRHand hand)
    {
        if (hand.GetJoint(XRHandJointID.ThumbTip).TryGetPose(out Pose thumbPose) &&
            hand.GetJoint(XRHandJointID.IndexTip).TryGetPose(out Pose indexPose))
        {
            float distance = Vector3.Distance(thumbPose.position, indexPose.position);
            return distance < 0.03f; // pinch threshold
        }
        return false;
    }
    bool IsThumbPointingUp(XRHand hand)
    {
        if (hand.GetJoint(XRHandJointID.ThumbTip).TryGetPose(out Pose tipPose) &&
            hand.GetJoint(XRHandJointID.ThumbProximal).TryGetPose(out Pose basePose))
        {
            Vector3 thumbDirection = (tipPose.position - basePose.position).normalized;
            return Vector3.Dot(thumbDirection, Vector3.up) > 0.3f;
        }

        return false;
    }
    bool IsFist(XRHand hand)
    {
        if (!hand.GetJoint(XRHandJointID.Wrist).TryGetPose(out Pose wristPose))
            return false;

        if (!hand.GetJoint(XRHandJointID.ThumbTip).TryGetPose(out Pose thumbTip)) return false;
        if (!hand.GetJoint(XRHandJointID.IndexTip).TryGetPose(out Pose indexTip)) return false;
        if (!hand.GetJoint(XRHandJointID.MiddleTip).TryGetPose(out Pose middleTip)) return false;
        if (!hand.GetJoint(XRHandJointID.RingTip).TryGetPose(out Pose ringTip)) return false;
        if (!hand.GetJoint(XRHandJointID.LittleTip).TryGetPose(out Pose pinkyTip)) return false;

        float thumbDistance = Vector3.Distance(thumbTip.position, wristPose.position);
        float indexDistance = Vector3.Distance(indexTip.position, wristPose.position);
        float middleDistance = Vector3.Distance(middleTip.position, wristPose.position);
        float ringDistance = Vector3.Distance(ringTip.position, wristPose.position);
        float pinkyDistance = Vector3.Distance(pinkyTip.position, wristPose.position);

        return thumbDistance < 0.12f &&
               indexDistance < 0.12f &&
               middleDistance < 0.12f &&
               ringDistance < 0.12f &&
               pinkyDistance < 0.12f;
    }
}