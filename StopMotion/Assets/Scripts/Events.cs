using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Events {
	//Input Manager
	public static System.Action<GameObject> OnMouseCollide = delegate { };
	public static System.Action OnKeyA = delegate { };
	public static System.Action OnKeyS = delegate { };
	public static System.Action OnKeyD = delegate { };
	public static System.Action OnKeyP = delegate { };

	public static System.Action<int> ShowFrame = delegate { };
}
