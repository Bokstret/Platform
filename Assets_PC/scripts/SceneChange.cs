using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{

	public static bool sceneEnd;
	public float fadeSpeed = 1.5f;
	private Image image;
	public static bool sceneStarting;
	public static int sceneId;

	void Awake()
	{
		image = GetComponent<Image>();
		image.enabled = true;
		sceneStarting = true;
		sceneEnd = false;
	}

	void Update()
	{
		if (sceneStarting)
		{
			StartScene();
		}

		if (sceneEnd)
		{
			EndScene(sceneId);
		}
	}

	void StartScene()
	{
		image.color = Color.Lerp(image.color, Color.clear, fadeSpeed * Time.deltaTime);

		if (image.color.a <= 0.1f)
		{
			image.color = Color.clear;
			image.enabled = false;
			sceneStarting = false;
		}
	}

	void EndScene(int id)
	{
		image.enabled = true;
		image.color = Color.Lerp(image.color, Color.black, fadeSpeed * Time.deltaTime);

		if (image.color.a >= 0.9f)
		{
			image.color = Color.black;
			SceneManager.LoadScene(id);
		}
	}
}