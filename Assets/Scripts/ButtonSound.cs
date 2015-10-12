using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour ,IPointerEnterHandler, IPointerDownHandler {

	public AudioClip pressedSound;
	public AudioClip hoveredSound;

	private AudioSource audioSource { get { return GetComponent<AudioSource>(); } }
	private Button button { get { return GetComponent<Button>(); } }

	void Start()
	{
		gameObject.AddComponent<AudioSource> ();
		audioSource.playOnAwake = false;
	}

	public void OnPointerEnter( PointerEventData ped ) {
		audioSource.PlayOneShot (hoveredSound);
	}
	
	public void OnPointerDown( PointerEventData ped ) {
		audioSource.PlayOneShot (pressedSound);
	}   

}
