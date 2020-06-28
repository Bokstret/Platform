using UnityEngine;
using System.Collections;

public class Fight2D : MonoBehaviour
{
	static GameObject NearTarget(Vector3 position, Collider2D[] array)
	{
		Collider2D current = null;
		float dist = Mathf.Infinity;

		foreach (Collider2D coll in array)
		{
			float curDist = Vector3.Distance(position, coll.transform.position);

			if (curDist < dist)
			{
				current = coll;
				dist = curDist;
			}
		}

		return (current != null) ? current.gameObject : null;
	}

	public static void Action(Vector2 point, float radius, int layerMask, float damage)
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius, 1 << layerMask);

		GameObject obj = NearTarget(point, colliders);
		if (obj != null && obj.GetComponent<HP>())
		{
			obj.GetComponent<HP>().health -= damage;
			if (obj.GetComponent<HP>().health <= 0)
            {
				Destroy(obj);
            }
	    }
	}
}