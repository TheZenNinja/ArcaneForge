using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    protected Transform target;
    protected bool looking = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FindTarget());
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
            transform.forward = (transform.position-target.position).normalized;
        else if (!looking)
            StartCoroutine(FindTarget());
    }

    IEnumerator FindTarget()
    {
        looking = true;

        float delay = 1f;

        while (looking)
        {
            var cam = StaticRefences.camController;
            if (cam)
            {
                target = cam.cam.transform;
                looking = false;
                break;
            }
            yield return new WaitForSecondsRealtime(delay);
            delay = Mathf.Clamp(delay + 1, 1, 30);
        }
    }
}
