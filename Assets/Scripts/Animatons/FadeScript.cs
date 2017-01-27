using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// reference https://forum.unity3d.com/threads/fading-in-out-gui-text-with-c-solved.380822/

public class FadeScript : MonoBehaviour
{

	void Start(){
		StartCoroutine (Pulse ());
	}


	public IEnumerator Pulse(){
		while (true) {
			yield return StartCoroutine(FadeTextToFullAlpha(1f, GetComponent<TextMesh>()));
			yield return StartCoroutine(FadeTextToZeroAlpha(1f, GetComponent<TextMesh>()));
		}
	}


	public IEnumerator FadeTextToFullAlpha(float t, TextMesh i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
		while (i.color.a < 1.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
			yield return null;
		}
	}

	public IEnumerator FadeTextToZeroAlpha(float t, TextMesh i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
		while (i.color.a > 0.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
			yield return null;
		}
	}


}
