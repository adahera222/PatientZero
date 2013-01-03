using UnityEngine;
using System.Collections;

public class GoalArea : MonoBehaviour {
	
	private ParticleSystem particleSystem;
	
	void Awake()
	{
		particleSystem = GetComponent<ParticleSystem>();
	}

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
