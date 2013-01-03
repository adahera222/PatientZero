using UnityEngine;
using System.Collections;

public class GoalArea : MonoBehaviour {
	
	void OnTriggerEnter(Collider collider)
	{
		if(particleSystem != null)
			particleSystem.Play();
	}
	
	void OnTriggerStay(Collider colllider)
	{
		if(particleSystem != null)
		{
			if(particleSystem.isStopped)
			{
				particleSystem.Play();
			}
		}
	}
}
