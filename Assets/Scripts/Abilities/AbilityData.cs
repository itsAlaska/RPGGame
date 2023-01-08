using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AbilityData
{
    private GameObject user;
    private Vector3 targetedPoint;
    private IEnumerable<GameObject> targets;

    public AbilityData(GameObject user)
    {
        this.user = user;
    }

    public IEnumerable<GameObject> GetTargets()
    {
        return targets;
    }

    public void SetTargets(IEnumerable<GameObject> targets)
    {
        this.targets = targets;
    }

    public void SetTargetedPoint(Vector3 targetedPoint)
    {
        this.targetedPoint = targetedPoint;
    }

    public Vector3 GetTargetedPoint()
    {
        return targetedPoint;
    }

    public GameObject GetUser()
    {
        return user;
    }

    public void StartCoroutine(IEnumerator coroutine)
    {
        user.GetComponent<MonoBehaviour>().StartCoroutine(coroutine);
    }
}
