using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RosslerAttractor : MonoBehaviour, ISpeedByPositionCalculator
{
	[SerializeField]
	MonoBehaviour parameterChangeListener;
	[SerializeField]
	Vector3 ABC;

	IParameterChangeListener ParameterChangeListener =>
		(IParameterChangeListener)parameterChangeListener;
	public Vector3 CalculateSpeed(Vector3 position)
	{
		return
			new Vector3(
				-(position.y + position.z),
				position.x + ABC.x * position.y,
				ABC.y + position.x * position.z - ABC.z * position.z);
	}

	private void OnValidate()
	{
		parameterChangeListener =
			parameterChangeListener.GetComponent<IParameterChangeListener>()
			as MonoBehaviour;
		ParameterChangeListener.OnParameterChange();
	}
}
