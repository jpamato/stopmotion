using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameBtn : MonoBehaviour {

	public int id;
	public Image image;

	// Use this for initialization
	void Start () {
		//image = GetComponent<Image> ();
	}

	public void Create(Sprite s, int _id){
		if (image==null)
			image = GetComponent<Image> ();

		image.sprite = s;
		id = _id;
	}

	public void ShowFrame(){
		Events.ShowFrame (id);
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}

