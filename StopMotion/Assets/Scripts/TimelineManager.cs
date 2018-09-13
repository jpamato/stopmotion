using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimelineManager : MonoBehaviour {

	public Texture2D currentFrame;
	public List<Frame> timeline;

	public GameObject btnContainer;
	public GameObject frameBtn;

	public RawImage monitor;

	public float speed;
	float time;

	int framesIdCount;
	int cabezal;

	public int selId;

	[Serializable]
	public class Frame{
		public Texture2D tex;
		public int id;

		public Frame(Texture2D t, int id_){
			tex = t;
			id = id_;
		}
	}

	public void SaveFrame(WebCamTexture webTex){
		currentFrame = new Texture2D(webTex.width,webTex.height);
		currentFrame.SetPixels(webTex.GetPixels());
		currentFrame.Apply ();
		timeline.Add(new Frame(currentFrame,framesIdCount));

		GameObject btn = Instantiate (frameBtn, btnContainer.transform);
		RectTransform btnFt = frameBtn.transform as RectTransform;
		if (framesIdCount * btnFt.sizeDelta.x > Screen.width*0.5f) {
			RectTransform rt = btnContainer.transform as RectTransform;
			rt.sizeDelta = new Vector2 (rt.sizeDelta.x + btnFt.sizeDelta.x, rt.sizeDelta.y);
		}

		FrameBtn fb = btn.GetComponent<FrameBtn> ();
		//Sprite sprite = new Sprite ();
		Sprite sprite = Sprite.Create (currentFrame, new Rect (0, 0, currentFrame.width, currentFrame.height), Vector2.zero);
		fb.Create (sprite, framesIdCount);

		framesIdCount++;

	}

	// Use this for initialization
	void Start () {
		Events.ShowFrame += ShowFrame;
		Events.OnKeyP+= Play;
		Events.OnKeyS+= Stop;
		Events.OnKeyD+= Delete;
		selId = -1;
	}

	void OnDestroy(){
		Events.ShowFrame -= ShowFrame;
		Events.OnKeyP-= Play;
		Events.OnKeyS-= Stop;
		Events.OnKeyD-= Delete;
	}
	
	// Update is called once per frame
	void Update () {
		if (Data.Instance.state == Data.States.playing) {
			if (time >= speed) {
				monitor.texture = timeline [cabezal].tex;
				cabezal++;
				if (cabezal >= timeline.Count)
					cabezal = 0;
				time = 0;
			} else {
				time += Time.deltaTime;
			}
		}
	}

	public void Play(){
		selId = -1;
		Data.Instance.state = Data.States.playing;
		cabezal = 0;
		time = 0;
		monitor.texture = timeline [cabezal].tex;
	}

	public void Stop(){
		selId = -1;
		Data.Instance.state = Data.States.live;	
	}

	public void ShowFrame(int id){
		selId = id;
		Data.Instance.state = Data.States.frame;
		monitor.texture = timeline.Find (x => x.id == id).tex;
	}

	public void Delete(){
		if (timeline.Count < 1)
			return;
		if (selId == -1) {		
			selId = timeline [timeline.Count - 1].id;
		}

		Frame f = timeline.Find (x => x.id == selId);
		timeline.Remove (f);

		FrameBtn[] btns = btnContainer.GetComponentsInChildren<FrameBtn> ();
		foreach(FrameBtn fb in btns){
			if (fb.id == selId)
				Destroy (fb.gameObject);
		}

		selId = -1;
	}

	public void DeleteAll(){		
		timeline.Clear ();
		selId = -1;
	}
}
