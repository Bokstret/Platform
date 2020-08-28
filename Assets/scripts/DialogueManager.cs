using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
	StoneFall script;
	public Text nameText;
	public Text dialogueText;
	public Animator anim;
	private Queue<string> sentences;

    void Start()
	{
		script = GameObject.Find("Main Camera").GetComponent<StoneFall>();
		sentences = new Queue<string>();
	}

	public void StartDialogue(Dialogue dialogue)
	{
		anim.SetBool("IsOpen", true);
		nameText.text = dialogue.name;
		sentences.Clear();
		if (script)
		{
			script.enabled = false;
		}
		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}
		Invoke("DisplayNextSentence", 0.8f);
	}


	public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			ButtonsLevel.buttons.SetActive(true);
			if (HeroController.playing == true)
            {
				ButtonsLevel.HUD.SetActive(true);
				if (script)
                {
					script.enabled = true;
				}
			}
            else
            {
				Time.timeScale = 0;
				ButtonsLevel.menu.SetActive(true);
				ButtonsLevel.next.SetActive(true);
				GameObject.Find("Dialogue").SetActive(false);
            }

			return;
		}
		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		anim.SetBool("IsOpen", false);
		GameObject.Find("DialogueText").GetComponent<Text>().text = "";
	}
}