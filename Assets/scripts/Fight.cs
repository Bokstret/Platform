using System.Collections;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class Fight : MonoBehaviour
{
	static Fight instance;

    void Start()
    {
		instance = this;
	}


    static IEnumerator Attack(Collider2D enemy)
    {
		enemy.GetComponent<HP>().enabled = false;
		yield return new WaitForSecondsRealtime(0.75f);
		enemy.GetComponent<HP>().enabled = true;
	}

    public static void Hit(Vector2 point, float radius, int layerMask, float damage)
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius, 1 << layerMask);

		foreach (Collider2D hit in colliders)
		{
			if(hit.GetComponent<HP>().enabled == true)
            {
				if (hit.GetComponent<HP>().health > 0)
				{
					hit.GetComponent<HP>().health -= damage;
					if (hit.GetComponent<HP>().health > 0)
					{
						hit.GetComponent<Animator>().SetTrigger("IsHurt");
					}
				}
				instance.StartCoroutine(Attack(hit));

				if (hit.GetComponent<HP>().health <= 0)
				{
					if (hit.GetComponent<EnemiesAppear>() != null)
					{
						Destroy(hit.GetComponent<EnemiesAppear>());
					}
					hit.GetComponent<HP>().enabled = false;
					hit.GetComponent<Animator>().SetTrigger("Die");
					Destroy(hit.GetComponent<EnemiesMovement>());
					BoxCollider2D coll = hit.GetComponent<BoxCollider2D>();
					if (hit.name == "skeleton clothed" | hit.name == "skeleton")
					{
						coll.size = new Vector2(coll.size.x, coll.size.y * 2.5f);
					}

					if (hit.name == "skeleton shield")
					{
						coll.size = new Vector2(coll.size.x, coll.size.y - coll.size.y / 1.2f);
					}
					else
					{
						coll.size = new Vector2(coll.size.x, coll.size.y - coll.size.y / 1.5f);
					}
					Destroy(hit.gameObject, 1);
				}
			}	
	    }	
    }
}