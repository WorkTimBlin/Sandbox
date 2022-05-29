using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.Events;
using UnityEditor;

public class AttractorLine : MonoBehaviour, IParameterChangeListener
{
	[SerializeField]
	LineRenderer lineRenderer;
	[SerializeField]
	bool rebuildInstantly = true;
	[SerializeField]
	int segmentsNumber = 50000;
	[SerializeField]
	[Range(0,1)]
	float timeStep = 0.2f;
	[SerializeField]
	[Range(0f,2f)]
	float sizeCoefficent = 1;
	[SerializeField]
	Vector3 startPosition = Vector3.up;
	[SerializeReference]
	MonoBehaviour speedCalculator;

	public ISpeedByPositionCalculator speedByPositionCalculator;

	ISpeedByPositionCalculator SpeedCalculator => 
		(ISpeedByPositionCalculator)speedCalculator;

	private void OnValidate()
	{
		ValidateInterfaces();
		OnParameterChange();
	}

	public void OnParameterChange()
	{
		if(rebuildInstantly) RebuildLine();
	}

	[ContextMenu("Rebuild line")]
	public void RebuildLine()
	{
		lineRenderer.positionCount = segmentsNumber;
		lineRenderer.SetPositions(GetPositions().Take(segmentsNumber).ToArray());
	}

	IEnumerable<Vector3> GetPositions()
	{
		Vector3 currentPos = startPosition;
		while(true)
		{
			yield return currentPos += 
				SpeedCalculator.CalculateSpeed(currentPos * sizeCoefficent) * 
				timeStep / sizeCoefficent;
		}
	}

	void ValidateInterfaces()
	{
		speedCalculator = 
			(speedCalculator as MonoBehaviour).
			GetComponent<ISpeedByPositionCalculator>()
			as MonoBehaviour;
	}

}
