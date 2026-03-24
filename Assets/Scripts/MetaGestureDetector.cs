using UnityEngine;
using TMPro;

public class MetaGestureDetector : MonoBehaviour
{
    public OVRHand leftHand;
    public OVRSkeleton leftSkeleton;
    public OVRHand rightHand;
    public OVRSkeleton rightSkeleton;
    public TextMeshPro gestureMessage;

    private void Update()
    {
        if (rightHand != null && rightSkeleton != null && rightHand.IsTracked)
        {
            if (DetectGesture(rightHand, rightSkeleton, true))
                return;
        }

        if (leftHand != null && leftSkeleton != null && leftHand.IsTracked)
        {
            if (DetectGesture(leftHand, leftSkeleton, false))
                return;
        }

        if (gestureMessage != null)
            gestureMessage.text = "Waiting for gesture...";
    }

    private bool DetectGesture(OVRHand hand, OVRSkeleton skeleton, bool isRight)
    {
        bool thumbExtended = IsFingerExtended(skeleton, OVRSkeleton.BoneId.Hand_ThumbTip, OVRSkeleton.BoneId.Hand_Thumb1, 0.07f);
        bool indexExtended = IsFingerExtended(skeleton, OVRSkeleton.BoneId.Hand_IndexTip, OVRSkeleton.BoneId.Hand_Index1, 0.10f);
        bool middleExtended = IsFingerExtended(skeleton, OVRSkeleton.BoneId.Hand_MiddleTip, OVRSkeleton.BoneId.Hand_Middle1, 0.11f);
        bool ringExtended = IsFingerExtended(skeleton, OVRSkeleton.BoneId.Hand_RingTip, OVRSkeleton.BoneId.Hand_Ring1, 0.10f);
        bool pinkyExtended = IsFingerExtended(skeleton, OVRSkeleton.BoneId.Hand_PinkyTip, OVRSkeleton.BoneId.Hand_Pinky1, 0.09f);

        bool indexCurled = !indexExtended;
        bool middleCurled = !middleExtended;
        bool ringCurled = !ringExtended;
        bool pinkyCurled = !pinkyExtended;

        if (thumbExtended && indexCurled && middleCurled && ringCurled && pinkyCurled)
        {
            gestureMessage.text = isRight ? "Right hand thumbs up!" : "Left hand thumbs up!";
            return true;
        }

        if (!thumbExtended && indexCurled && middleCurled && ringCurled && pinkyCurled)
        {
            gestureMessage.text = isRight ? "Right hand fist!" : "Left hand fist!";
            return true;
        }

        bool indexPinch = hand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        float indexPinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);

        if (indexPinch && indexPinchStrength > 0.7f)
        {
            gestureMessage.text = isRight ? "Right-hand Pinch!" : "Left-hand Pinch!";
            return true;
        }

        return false;
    }

    private bool IsFingerExtended(OVRSkeleton skeleton, OVRSkeleton.BoneId tipId, OVRSkeleton.BoneId rootId, float threshold)
    {
        Transform tip = GetBoneTransform(skeleton, tipId);
        Transform root = GetBoneTransform(skeleton, rootId);

        if (tip == null || root == null)
            return false;

        return Vector3.Distance(tip.position, root.position) > threshold;
    }

    private Transform GetBoneTransform(OVRSkeleton skeleton, OVRSkeleton.BoneId boneId)
    {
        if (skeleton.Bones == null)
            return null;

        foreach (OVRBone bone in skeleton.Bones)
        {
            if (bone.Id == boneId)
                return bone.Transform;
        }

        return null;
    }
}