using System;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBoundsRespawnVolume : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Names of scene objects to watch. Child matches are resolved up to the nearest Rigidbody/Grabbable root.")]
    string[] trackedObjectNames;

    [SerializeField]
    [Tooltip("How long an item can stay outside the bounds before it respawns.")]
    float outOfBoundsDelay = 3f;

    [SerializeField]
    [Tooltip("How often to check tracked objects.")]
    float checkInterval = 0.1f;

    BoxCollider boundsCollider;
    readonly List<TrackedObject> trackedObjects = new List<TrackedObject>();
    float checkTimer;

    static readonly string[] DefaultTrackedObjectNames =
    {
        "walkie",
        "LaserGunLevel2Green",
        "Card_Security",
        "Keypad",
        "Key Card Reader",
    };

    sealed class TrackedObject
    {
        public Transform Target;
        public Transform Parent;
        public Rigidbody Rigidbody;
        public Grabbable Grabbable;
        public Vector3 LocalPosition;
        public Quaternion LocalRotation;
        public Vector3 WorldPosition;
        public Quaternion WorldRotation;
        public float OutsideDuration;
    }

    void Awake()
    {
        EnsureDefaults();
        boundsCollider = GetComponent<BoxCollider>();

        if (boundsCollider == null)
        {
            Debug.LogError("OutOfBoundsRespawnVolume requires a BoxCollider on the same GameObject.", this);
            enabled = false;
            return;
        }

        BuildTrackedObjectList();
    }

    void Update()
    {
        if (!enabled)
        {
            return;
        }

        checkTimer -= Time.deltaTime;
        if (checkTimer > 0f)
        {
            return;
        }

        checkTimer = checkInterval;

        for (var i = 0; i < trackedObjects.Count; i++)
        {
            var tracked = trackedObjects[i];
            if (tracked.Target == null)
            {
                continue;
            }

            if (IsGrabbed(tracked) || IsWithinBounds(tracked.Target.position))
            {
                tracked.OutsideDuration = 0f;
                continue;
            }

            tracked.OutsideDuration += checkInterval;
            if (tracked.OutsideDuration >= outOfBoundsDelay)
            {
                Respawn(tracked);
            }
        }
    }

    void EnsureDefaults()
    {
        if (trackedObjectNames == null || trackedObjectNames.Length == 0)
        {
            trackedObjectNames = (string[])DefaultTrackedObjectNames.Clone();
        }

        if (outOfBoundsDelay <= 0f)
        {
            outOfBoundsDelay = 3f;
        }

        if (checkInterval <= 0f)
        {
            checkInterval = 0.1f;
        }
    }

    void BuildTrackedObjectList()
    {
        trackedObjects.Clear();

        foreach (var objectName in trackedObjectNames)
        {
            if (string.IsNullOrWhiteSpace(objectName))
            {
                continue;
            }

            var namedTransform = FindTransformByName(objectName);
            if (namedTransform == null)
            {
                Debug.LogWarning($"OutOfBoundsRespawnVolume could not find '{objectName}' in scene '{SceneManager.GetActiveScene().name}'.", this);
                continue;
            }

            var targetTransform = ResolveTrackedRoot(namedTransform);
            if (targetTransform == null)
            {
                Debug.LogWarning($"OutOfBoundsRespawnVolume could not resolve a respawn target for '{objectName}'.", this);
                continue;
            }

            var tracked = new TrackedObject
            {
                Target = targetTransform,
                Parent = targetTransform.parent,
                Rigidbody = targetTransform.GetComponent<Rigidbody>() ?? targetTransform.GetComponentInChildren<Rigidbody>(),
                Grabbable = targetTransform.GetComponent<Grabbable>() ?? targetTransform.GetComponentInChildren<Grabbable>(),
            };

            if (tracked.Parent != null)
            {
                tracked.LocalPosition = targetTransform.localPosition;
                tracked.LocalRotation = targetTransform.localRotation;
            }
            else
            {
                tracked.WorldPosition = targetTransform.position;
                tracked.WorldRotation = targetTransform.rotation;
            }

            trackedObjects.Add(tracked);
        }
    }

    Transform FindTransformByName(string objectName)
    {
        var roots = SceneManager.GetActiveScene().GetRootGameObjects();
        for (var i = 0; i < roots.Length; i++)
        {
            var match = FindTransformByNameRecursive(roots[i].transform, objectName);
            if (match != null)
            {
                return match;
            }
        }

        return null;
    }

    Transform FindTransformByNameRecursive(Transform current, string objectName)
    {
        if (string.Equals(current.name, objectName, StringComparison.Ordinal))
        {
            return current;
        }

        for (var i = 0; i < current.childCount; i++)
        {
            var match = FindTransformByNameRecursive(current.GetChild(i), objectName);
            if (match != null)
            {
                return match;
            }
        }

        return null;
    }

    Transform ResolveTrackedRoot(Transform candidate)
    {
        var current = candidate;
        while (current != null)
        {
            if (current.GetComponent<Grabbable>() != null || current.GetComponent<Rigidbody>() != null)
            {
                return current;
            }

            current = current.parent;
        }

        return candidate;
    }

    bool IsGrabbed(TrackedObject tracked)
    {
        return tracked.Grabbable != null && tracked.Grabbable.SelectingPointsCount > 0;
    }

    bool IsWithinBounds(Vector3 worldPosition)
    {
        var localPoint = transform.InverseTransformPoint(worldPosition) - boundsCollider.center;
        var halfSize = boundsCollider.size * 0.5f;

        return Mathf.Abs(localPoint.x) <= halfSize.x
            && Mathf.Abs(localPoint.y) <= halfSize.y
            && Mathf.Abs(localPoint.z) <= halfSize.z;
    }

    void Respawn(TrackedObject tracked)
    {
        if (tracked.Parent != null)
        {
            tracked.Target.SetLocalPositionAndRotation(tracked.LocalPosition, tracked.LocalRotation);
        }
        else
        {
            tracked.Target.SetPositionAndRotation(tracked.WorldPosition, tracked.WorldRotation);
        }

        if (tracked.Rigidbody != null)
        {
            tracked.Rigidbody.linearVelocity = Vector3.zero;
            tracked.Rigidbody.angularVelocity = Vector3.zero;
            tracked.Rigidbody.Sleep();
        }

        tracked.OutsideDuration = 0f;
    }

    void OnValidate()
    {
        EnsureDefaults();
    }
}
