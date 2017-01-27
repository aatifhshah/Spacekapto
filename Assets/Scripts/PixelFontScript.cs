using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelFontScript : MonoBehaviour {
	
	public Material primary;
	public Material secondary;
	public GameObject[] display;

	private static float BLINK_DURATION = 0.1f;
	private static float DIGIT_SPACING = 0.5f;
	private static float DIGIT_NEWLINE = 0.8f;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void DisplayParagraph(GameObject startDigit, int width, string val){
		char[] strArray = val.ToCharArray ();

		//create that many clones in proper order. add to display
		int charInOneLine = strArray.Length / width;
		int count = 1;
		Vector3 spawnPos = startDigit.transform.position;
		for (int i = 0; i < strArray.Length; i++) {
			//setcharval
			if (count <= charInOneLine) {
				//make new clone
				Transform clone = (Transform)Instantiate (startDigit.transform, spawnPos, startDigit.transform.rotation);
				//set val
				setCharVal (clone.gameObject, strArray [i]);
				//increment pos
				spawnPos.x = spawnPos.x + DIGIT_SPACING;
				count++;
			}
			else {
				count = 1;
				spawnPos.z = spawnPos.z - DIGIT_NEWLINE;
				spawnPos.z = spawnPos.x - ((charInOneLine - 1) * DIGIT_SPACING);

				//make new clone
				Transform clone = (Transform)Instantiate (startDigit.transform, spawnPos, startDigit.transform.rotation);
				//set val
				setCharVal (clone.gameObject, strArray [i]);

			}
		}

	}


		
	// Int Display function
	public void displayVal(int val)
	{
		int temp = val;
		for (int i = display.Length - 1; i >= 0; i--) {
			setVal (display [i], temp % 10);
			temp /= 10;
		}
	}

	// Set Digit to val
	private void setVal(GameObject obj, int val)
	{
		for (int i = 0; i < 15; i++) {
			obj.transform.GetChild (i).gameObject.GetComponent<Renderer> ().material = primary;
		}

		int[] pattern = getPattern(val);

		foreach (int i in pattern) {
			obj.transform.GetChild (i).gameObject.GetComponent<Renderer> ().material = secondary;
		}

	}

	// Display String
	public void displayStringVal(string val){
		val = val.ToUpper ();
		char[] valArray = val.ToCharArray ();
		if (display.Length <= valArray.Length) {
			for (int i = display.Length - 1; i >= 0; i--) {
				setCharVal (display[i], valArray[i]);
			}
		}
	}

	private void setCharVal(GameObject obj, char val){
		for (int i = 0; i < 15; i++) {
			obj.transform.GetChild (i).gameObject.GetComponent<Renderer> ().material = primary;
		}

		int[] pattern = getLetterPattern (val);

		foreach (int i in pattern) {
			obj.transform.GetChild (i).gameObject.GetComponent<Renderer> ().material = secondary;

		}
	}

	private int[] getPattern(int val){
		switch (val) {
		case 1:
			return PixelFont.One;
		case 2:
			return PixelFont.Two;
		case 3:
			return PixelFont.Three;
		case 4:
			return PixelFont.Four;
		case 5:
			return PixelFont.Five;
		case 6:
			return PixelFont.Six;
		case 7:
			return PixelFont.Seven;
		case 8:
			return PixelFont.Eight;
		case 9:
			return PixelFont.Nine;
		case 0:
			return PixelFont.Zero; 
		default:
			return null;
		}
	}

	private int[] getLetterPattern(char val){
		switch (val) {
		//Letters
			case 'A':
				return PixelFont.A;
			case 'B':
				return PixelFont.B;
			case 'C':
				return PixelFont.C;
			case 'D':
				return PixelFont.D;
			case 'E':
				return PixelFont.E;
			case 'F':
				return PixelFont.F;
			case 'G':
				return PixelFont.G;
			case 'H':
				return PixelFont.H;
			case 'I':
				return PixelFont.I;
			case 'J':
				return PixelFont.J;
			case 'K':
				return PixelFont.K;
			case 'L':
				return PixelFont.L;
			case 'M':
				return PixelFont.M;
			case 'N':
				return PixelFont.N;
			case 'O':
				return PixelFont.O;
			case 'P':
				return PixelFont.P;
			case 'Q':
				return PixelFont.Q;
			case 'R':
				return PixelFont.R;
			case 'S':
				return PixelFont.S;
			case 'T':
				return PixelFont.T;
			case 'U':
				return PixelFont.U;
			case 'V':
				return PixelFont.V;
			case 'W':
				return PixelFont.W;
			case 'X':
				return PixelFont.X;
			case 'Y':
				return PixelFont.Y;
			case 'Z':
				return PixelFont.Z;
			//Punctuation
			case ':':
				return PixelFont.Colon;
			case ',':
				return PixelFont.Comma;
			case '!':
				return PixelFont.Exclamation;
			case '.':
				return PixelFont.Period;
			case ' ':
				return PixelFont.Space;
			default:
				return PixelFont.Space;
		}
	}

	public IEnumerator blinkMaterial(Material blinkMaterial, string label){
		Material temp = secondary;
		secondary = blinkMaterial;
		displayStringVal (label);
		yield return new WaitForSeconds (BLINK_DURATION);
		secondary = temp;
		displayStringVal (label);

	}

	public void redrawWithMaterial(Material newMaterial, string label){
		secondary = newMaterial;
		displayStringVal (label);
	}
}

[System.Serializable]
static class PixelFont
{

	// Layout	
	//	10	9	0
	//	11	8	1
	//	12	7	2
	//	13	6	3
	//	14	5	4


	public static int[] One {get { return new int[]{0,1,2,3,4,8};}}
	public static int[] Two {get { return new int[]{10,9,0,1,2,7,12,13,14,5,4};}}
	public static int[] Three {get { return new int[]{10,9,0,1,2,7,12,3,14,5,4};}}
	public static int[] Four {get { return new int[]{10,11,12,7,2,3,4,1,0};}}
	public static int[] Five {get { return new int[]{0,9,10,11,12,7,2,3,4,5,14};}}
	public static int[] Six {get { return new int[]{0,9,10,11,12,7,2,3,4,5,14,13};}}
	public static int[] Seven {get { return new int[]{11,10,9,0,1,2,3,4};}}
	public static int[] Eight {get { return new int[]{0,9,10,11,12,7,2,3,4,5,14,1,13};}}
	public static int[] Nine {get { return new int[] {10,9,0,1,2,7,12,11,3,4,5,14};}}
	public static int[] Zero {get { return new int[]{0,1,2,3,4,5,9,10,11,12,13,14};}}

	public static int[] A { get { return new int[]{14,13,12,11,10,9,7,0,1,2,3,4}; } }
	public static int[] B { get { return new int[]{14,13,12,11,10,5,7,9,0,1,3,4}; } }
	public static int[] C { get { return new int[]{10,11,12,13,14,5,9,0,4}; } }
	public static int[] D { get { return new int[]{10,11,12,13,14,5,9,0,1,2,3}; } }
	public static int[] E { get { return new int[]{10,11,12,13,14,5,7,9,0,2,4}; } }
	public static int[] F { get { return new int[]{10,11,12,13,14,9,7,0,2}; } }
	public static int[] G { get { return new int[]{11,12,13,14,5,7,9,0,2,3,4}; } }
	public static int[] H { get { return new int[]{10,11,12,13,14,7,0,1,2,3,4}; } }
	public static int[] I { get { return new int[]{10,14,5,6,7,8,9,0,4}; } }
	public static int[] J { get { return new int[]{0,1,2,3,4,9,5,14,13}; } }
	public static int[] K { get { return new int[]{10,11,12,13,14,6,8,0,4}; } }
	public static int[] L { get { return new int[]{10,11,12,13,14,5,4}; } }
	public static int[] M { get { return new int[]{10,11,12,13,14,8,0,1,2,3,4}; } }
	public static int[] N { get { return new int[]{11,12,13,14,8,10,1,2,3,4}; } }
	public static int[] O { get { return new int[]{11,12,13,9,5,1,2,3}; } }
	public static int[] P { get { return new int[]{10,11,12,13,14,9,7,0,1,2}; } }
	public static int[] Q { get { return new int[]{10,11,12,13,9,6,0,1,2,4}; } }
	public static int[] R { get { return new int[]{10,11,12,13,14,9,7,0,1,3,4}; } }
	public static int[] S { get { return new int[]{10,11,12,14,5,7,9,0,2,3,4}; } }
	public static int[] T { get { return new int[]{10,5,6,7,8,9,0}; } }
	public static int[] U { get { return new int[]{10,11,12,13,14,5,0,1,2,3,4}; } }
	public static int[] V { get { return new int[]{10,11,12,13,5,0,1,2,3}; } }
	public static int[] W { get { return new int[]{10,11,12,13,14,6,0,1,2,3,4}; } }
	public static int[] X { get { return new int[]{10,11,13,14,7,0,1,3,4}; } }
	public static int[] Y { get { return new int[]{10,11,12,7,6,5,0,1,2}; } }
	public static int[] Z { get { return new int[]{10,13,14,5,7,9,0,1,4}; } }

	public static int[] Colon { get { return new int[]{ 9, 8, 6, 5 }; } }
	public static int[] Comma { get { return new int[]{ 6,14 }; } }
	public static int[] Exclamation { get { return new int[]{ 9,8,7,5 }; } }
	public static int[] Period { get { return new int[]{ 5 }; } }

	public static int[] Space { get { return new int[]{ 14, 5, 3 }; } }




}


	

