using UnityEngine;
using System.Collections;

public class Grass : MonoBehaviour {

	// Use this for initialization
	public GameObject grass;
	void Awake () 
	{
		Vector3 pos = Vector3.zero;
		for (int i = 0; i < 100; i++) 
		{
			pos.x = (i * 5);
			GameObject.Instantiate(grass, pos, Quaternion.identity);
		}
	}
	

}
