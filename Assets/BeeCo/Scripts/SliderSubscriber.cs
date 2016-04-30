using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderSubscriber : MonoBehaviour {
	public Text textToChange;
	public Slider toReadFrom;
    public string format = "{0:P}";

	public void ChangeText(){
		textToChange.text = toReadFrom.value.ToString(format);
	}
}
