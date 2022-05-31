using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AttractorParticleSystem : MonoBehaviour
{

	[SerializeField]
	new ParticleSystem particleSystem;

	[Header("Parameters")]
	[SerializeField]
	[Range(0, 2)]
	float sizeCoefficent = 1;
	[SerializeField]
	[Range(-2, 2)]
	float timeCoefficent = 1;

	[SerializeField]
	MonoBehaviour speedCalculator;

	ISpeedByPositionCalculator SpeedCalculator =>
		(ISpeedByPositionCalculator)speedCalculator;

	// Update is called once per frame
	void Update()
	{
		ParticleSystem.Particle[] particles = 
			new ParticleSystem.Particle[particleSystem.main.maxParticles];
		particleSystem.GetParticles(particles);
		particles = particles.Select((particle) =>
		{
			particle.velocity = 
			SpeedCalculator.CalculateSpeed(particle.position * sizeCoefficent) * 
			timeCoefficent;
			return particle;
		}).ToArray();
		particleSystem.SetParticles(particles);
	}

	private void OnValidate()
	{
		speedCalculator = 
			speedCalculator.GetComponent<ISpeedByPositionCalculator>()
			as MonoBehaviour;
	}
}
