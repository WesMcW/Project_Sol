using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;
	float memory;
	[SerializeField]
	float maxMemory = 5;
	
	EnemyAstar Myself;
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visableTargets = new List<Transform>();

    void Start()
    {
		memory = maxMemory;
		Myself = GetComponentInParent<EnemyAstar>();
        StartCoroutine("FindTargetsWithDelay", .2f);
    }
	private void Update()
	{
		if (visableTargets.Count == 0 && Myself.encounter == true)
		{
			if (memory <= 0)
			{
				Myself.encounter = false;
				memory = maxMemory;
			}
			else
			{
				memory = memory - Time.deltaTime;
			}
		}
			
		
	}

	IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisableTargets();
        }
    }

    void FindVisableTargets()
    {
        visableTargets.Clear();
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        for(int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector2 dirToTarget = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).normalized;
            if(Vector3.Angle(dirToTarget, transform.right ) < viewAngle / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
					Myself.encounter = true;
                    visableTargets.Add(target);
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }
}
