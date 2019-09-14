using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAstar : MonoBehaviour
{
	public Transform target;
    public bool followTargetGo = false;
	[SerializeField]
	float speed = 5;
	Vector3[] path;
	int targetIndex;
	Rigidbody2D rb;
	public bool encounter = false;
	Vector3 lastPosition;
	Vector3 velocity;
	Animator anim;
	[SerializeField]
	GameObject sight;
	float reactionTime = 3;
	private void Start()
	{
		lastPosition = transform.position;
		rb = GetComponent<Rigidbody2D>();
		//anim = GetComponent<Animator>();
	}
	private void Update()
	{
        if(followTargetGo)
		    PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
	}
	public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
	{
		if (pathSuccessful)
		{
			path = newPath;
			targetIndex = 0;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}
	//This needs to ne reworked, I have some ideas
	IEnumerator FollowPath()
	{
		Vector3 currentWaypoint = path[0];
		while (true)
		{
			if (transform.position == currentWaypoint)
			{
				targetIndex++;
				if (targetIndex >= path.Length)
				{
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}

			//rb.transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
			//print(rb.velocity.x);
			if (encounter == false)
			{
				if (lastPosition == transform.position)
				{
					transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
				}
				else
				{


					transform.position = Vector2.MoveTowards(transform.position,currentWaypoint, speed * Time.fixedDeltaTime);
					Vector3 direction = transform.position - lastPosition;
					Vector3 localVelocity = transform.InverseTransformDirection(direction);
					/*
					if (direction.x < 0)
					{
						anim.SetBool("LoR", true);
					}
					else
					{
						anim.SetBool("LoR", false);
					}
					*/
					//print(direction);
					float angle = Mathf.Atan2(localVelocity.y, localVelocity.x) * Mathf.Rad2Deg;
					Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
					//sight.transform.rotation = Quaternion.Slerp(sight.transform.rotation, rotation, reactionTime);
					//sight.transform.LookAt(Vector3.up+ sight.transform.rotation * direction);
					//anim.SetFloat("hVelocity", localVelocity.x);
					//anim.SetFloat("vVelocity", localVelocity.y);
					lastPosition = transform.position;
				}
				yield return null;

			}
			if (encounter == true)
			{
				if (lastPosition == transform.position)
				{
					transform.position = Vector2.MoveTowards(transform.position, playerBehavior.current.transform.position, speed * Time.fixedDeltaTime);
				}
				else
				{


					transform.position = Vector2.MoveTowards(transform.position, playerBehavior.current.transform.position, speed * Time.fixedDeltaTime);
					Vector3 direction = transform.position - lastPosition;
					Vector3 localVelocity = transform.InverseTransformDirection(direction);
					/*
					if (direction.x < 0)
					{
						anim.SetBool("LoR", true);
					}
					else
					{
						anim.SetBool("LoR", false);
					}
					*/
					print(direction);
					float angle = Mathf.Atan2(localVelocity.y, localVelocity.x) * Mathf.Rad2Deg;
					Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
					sight.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, reactionTime);
					//anim.SetFloat("hVelocity", direction.x);
					//anim.SetFloat("vVelocity", direction.y);
					lastPosition = transform.position;
				}
				yield return null;
			}
			
		}
	}
	IEnumerator lookAt(Vector3 direction)
	{
		while(true)
		{
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			Quaternion rotation = Quaternion.AngleAxis(angle, sight.transform.position);
			sight.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
			yield return null;
		}
	}
	public void OnDrawGizmos()
	{
		if (path != null)
		{
			for (int i = targetIndex; i < path.Length; i++)
			{
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one);
				

				if (i == targetIndex)
				{
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else
				{

					Gizmos.DrawLine(path[i - 1], path[i]);
				}
			}
		}
	}
    /***
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			//encounter = true;
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			//encounter = false;
		}
	}
    ***/
	
}
