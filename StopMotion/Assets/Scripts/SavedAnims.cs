using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SavedAnims : MonoBehaviour {

	public List<Anim> anims;

	public int next2Play;
	public int next2Save;

	[Serializable]
	public class Anim{
		public string name;
		public List<TimelineManager.Frame> timeline;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Anim GetNextAnim(){
		int n = next2Play;
		next2Play++;
		if (next2Play >= anims.Count)
			next2Play = 0;

		return anims [n];
	}

	public void Save(List<TimelineManager.Frame> tl){

		if (anims == null)
			anims = new List<Anim> ();

		if (anims.Count>=Data.Instance.config.maxSavedAnims) {
			anims [next2Save].timeline = tl;
			next2Save++;
			if (next2Save >= Data.Instance.config.maxSavedAnims)
				next2Save = 0;			
		} else {
			print ("aca");
			Anim a = new Anim ();
			a.timeline = tl;
			anims.Add (a);
		}

		next2Play = 0;
	}
}
