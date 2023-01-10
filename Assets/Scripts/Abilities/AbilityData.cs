using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;


public class AbilityData : IAction
{
    private GameObject user;
    private Vector3 targetedPoint;
    private IEnumerable<GameObject> targets;
    private bool canceled = false;

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

    public void Cancel()
    {
        canceled = true;
    }

    public bool IsCanceled()
    {
        return canceled;
    }
}
