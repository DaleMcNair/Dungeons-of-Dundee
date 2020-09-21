using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
	Transform target;

	public float followSpeed = 1f;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		Vector3 dir = target.position - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * followSpeed);

	}
}
