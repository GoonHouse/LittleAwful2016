using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderSubscriber : MonoBehaviour {
	public Text textToChange;
	public Slider toReadFrom;

	public void ChangeText(){
		textToChange.text = toReadFrom.value.ToString ();
	}
}
