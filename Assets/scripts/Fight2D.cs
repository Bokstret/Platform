using System.Security.Cryptography;
using UnityEngine;

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
		if (obj != null && obj.GetComponent<HP>().enabled == true)
		{
			if(obj.GetComponent<HP>().health > 0)
            {
				obj.GetComponent<HP>().health -= damage;
			}

			if (obj.GetComponent<HP>().health <= 0)
            {
				if (obj.GetComponent<EnemiesAppear>() != null)
				{
					Destroy(obj.GetComponent<EnemiesAppear>());
				}
				obj.GetComponent<HP>().enabled = false;
				obj.GetComponent<Animator>().SetTrigger("Die");
				Destroy(obj.GetComponent<EnemiesMovement>());
				BoxCollider2D coll = obj.GetComponent<BoxCollider2D>();
				if(obj.name == "skeleton clothed" | obj.name == "skeleton")
                {
					coll.size = new Vector2(coll.size.x, coll.size.y - coll.size.y / 3);
				}

				if(obj.name == "skeleton shield")
                {
					coll.size = new Vector2(coll.size.x, coll.size.y - coll.size.y / 1.2f);
				}

                else
                {
					coll.size = new Vector2(coll.size.x, coll.size.y - coll.size.y / 1.5f);
				}
				Destroy(obj, 1.5f);
			}	
        }
	}
}