using UnityEngine;
using System.Collections;

public class Cubes : MonoBehaviour {

	public GameObject circle;
	public GameObject cube;

	void Awake()
	{
		float random;
		float maxY = 300;
		float minY = 2;
		float minDistanceX = 1;
		float maxDistanceX  = 3;
		Vector3 pos = Vector3.zero;
		for (int i = 0; i < 500; i++) 
		{
			random = Random.Range(0f,1f);
			pos.x = Random.Range(minDistanceX, maxDistanceX) * i + 40;
			pos.y = Random.Range(minY, maxY);
			if(random > 0.5f)
			{
				GameObject.Instantiate(circle, pos, Quaternion.identity );
			}
			else
			{
				GameObject.Instantiate(cube, pos, Quaternion.identity );
			}
		}
	}
}
