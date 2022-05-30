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
	int numberOfParticles = 10000;
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
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1000];
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
