using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour {
	private static float BLINK_ANIMATION_DELAY = 0.5f;
	private static float EXPLOSION_ANIMATION_DELAY = 0.15f;
    private static float MULTIPLE_ANIMATION_DELAY = 0.5f;


	public IEnumerator BlinkAnimation(GameObject container, Material primary, Material secondary){
		while (true) {
			BlinkOn (container.transform, secondary);
			yield return new WaitForSeconds(BLINK_ANIMATION_DELAY);
			BlinkOff (container.transform, primary);
			yield return new WaitForSeconds(BLINK_ANIMATION_DELAY);
		}
	}

	public IEnumerator BlinkAllAnimation(GameObject container, Material primary, Material secondary){
		BlinkAllOff (container.transform, primary);
		yield return new WaitForSeconds (0.06f);
		BlinkAllOn (container.transform, secondary);
	}


	public IEnumerator MultipleAnimation(GameObject container, Material primary, Material secondary){
        int j = 0;
        int i = 0;
        while (true) {
			foreach (Transform step in container.transform) {
                if ((i+j) % 2 == 0)
                    BlinkOn(step, secondary);
                else
                    BlinkOff(step, primary);
                i++;
			}
            yield return new WaitForSeconds(MULTIPLE_ANIMATION_DELAY);
            j++;
		}
	}

	public IEnumerator ExplosionAnimation(GameObject container,Material primary, Material secondary){
		TurnOff (container.transform, primary);
		foreach (Transform step in container.transform) {
			BlinkOn (step, secondary);
			yield return new WaitForSeconds (EXPLOSION_ANIMATION_DELAY);
			BlinkOff (step, primary);
		}
	}

    void BlinkOn(Transform container, Material secondary) {
		Vector3 newPos = container.position;
		newPos.y += 0.1f;
		container.position = newPos;
		foreach (Transform child in container.transform) {
			child.GetComponent<Renderer>().material = secondary;
		}
    }

	void BlinkOff(Transform container, Material primary) {
		Vector3 newPos = container.position;
		newPos.y -= 0.1f;
		container.position = newPos;
		foreach (Transform child in container.transform) {
			child.GetComponent<Renderer>().material = primary;
		}
	}

    void BlinkAllOff(Transform container, Material primary) {
		Vector3 newPos = container.position;
		newPos.y -= 0.1f;
		container.position = newPos;
		Component[] array = container.transform.GetComponentsInChildren<Renderer> (); 
		foreach (Renderer child in array) {
			child.material = primary;
		}
    }

	void BlinkAllOn(Transform container, Material secondary) {
		Vector3 newPos = container.position;
		newPos.y += 0.1f;
		container.position = newPos;

		Component[] array = container.transform.GetComponentsInChildren<Renderer> (); 
		foreach (Renderer child in array) {
			child.material = secondary;
		}

	}
		
	void TurnOff(Transform container, Material primary){
		Transform[] children = container.GetComponentsInChildren<Transform> ();
		foreach (Transform drawable in children) {
			if (drawable.GetComponent<Renderer> () != null) {
				drawable.GetComponent<Renderer> ().material = primary;
			}
		}
	}
		
	public IEnumerator MoveTowards(GameObject obj, GameObject source, GameObject target, float speed){
//		while(Vector3.Distance(source.transform.position, target.transform.position) < 0.1f){
//			source.transform.position = Vector3.MoveTowards (source.transform.position, target.transform.position, speed * Time.deltaTime);
//			yield return new WaitForEndOfFrame ();
//		}
		float time = 0f;
		while (Vector3.Distance(obj.transform.position, target.transform.position) < 0.1f) {
			time += Time.deltaTime;
			obj.transform.position = Vector3.Lerp (obj.transform.position, target.transform.position, time);
			yield return null;
		}
//		obj.transform.position = Vector3.Lerp (source.transform.position, target.transform.position, Time.deltaTime * 2.0f);
//		obj.transform.position = opto.mid.transform.position;

	}

	public IEnumerator OptionAnimationIN(GameObject obj, GameObject source, GameObject midenter, GameObject mid){

		yield return StartCoroutine(MoveTowards(obj, source, midenter,5f));
		yield return new WaitForSeconds (1);
		yield return StartCoroutine(MoveTowards(obj, source, mid,2f));


	}

	public IEnumerator OptionAnimationOUT(GameObject obj, GameObject mid, GameObject midexit, GameObject exit){
		yield return StartCoroutine(MoveTowards(obj, mid, midexit,2f));
		yield return new WaitForSeconds (1);
		yield return StartCoroutine(MoveTowards(obj, mid, exit,5f));
	}
}
