using System;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public static class QRExtensions
{
    public static bool HaveParentedActiveStatus(this Transform baseTransform)
    {
        List<Transform> parentList = new List<Transform>();
        Transform foundedTransform = baseTransform;

        while(foundedTransform != null)
        {
            parentList.Add(foundedTransform);
            foundedTransform = foundedTransform.parent;
        }

        bool somethingIsBlocked = parentList.Any(parent => !parent.gameObject.activeInHierarchy);

        return somethingIsBlocked;
    }

    public static T GetOrAddComponentExtension<T>(this Transform mainTransform, out T outputValue, Transform searchParent = null) where T : Component
    {
        Transform usedTransform = searchParent != null ? searchParent : mainTransform;
        outputValue = usedTransform.GetComponent<T>();
        if (!outputValue) { outputValue = usedTransform.gameObject.AddComponent<T>(); }
        if (!outputValue) { Debug.LogError("GetOrAddComponentExtension: " + usedTransform.name + " no logró encontrar un tipo de dato <T> " + outputValue.GetType().ToString()); }
        return outputValue;
    }
}
