using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableController : MonoBehaviour
{
    private const string targetTag = "Target";

    private GameObject currentTarget;
    private Vector3 initialPosition;
    private Vector3 initialScale;

    void Start()
    {
        initialPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        initialScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void Update()
    {
        if (currentTarget != null)
        {
            bool matchesTarget = MatchesTarget(currentTarget);
            TargetController targetController = currentTarget.GetComponent<TargetController>();
            if (matchesTarget)
            {
                targetController.OnTargetMatch();
            }
        }
    }

    IEnumerator OnDelayedTargetUnmatch(TargetController targetController)
    {
        yield return new WaitForSeconds(3);
        targetController.OnTargetUnmatch();
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;
        if (gameObject.CompareTag(targetTag))
        {
            currentTarget = gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameObject gameObject = other.gameObject;
        if (gameObject.CompareTag(targetTag))
        {
            currentTarget = null;
        }
    }

    bool MatchesTarget(GameObject target)
    {
        return MatchesTargetPosition(target) && MatchesTargetScale(target);
    }

    bool MatchesTargetPosition(GameObject target)
    {
        float distanceToCenter = Vector3.Distance(target.transform.position, transform.position);
        float targetSideSize = target.transform.localScale.x;
        float distanceRatio = distanceToCenter / targetSideSize;

        float ratioThreshold = 0.3f;
        return distanceRatio < ratioThreshold;
    }

    bool MatchesTargetScale(GameObject target)
    {
        float targetSideSize = target.transform.localScale.x;
        float thisSideSize = transform.localScale.x;
        float lengthRatio = thisSideSize / targetSideSize;

        float marginPercent = 0.1f;
        float upperLimit = 1 + marginPercent;
        float lowerLimit = 1 - marginPercent;

        return lengthRatio > lowerLimit && lengthRatio < upperLimit;
    }
}
