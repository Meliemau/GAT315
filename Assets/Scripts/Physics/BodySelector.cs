using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySelector : MonoBehaviour
{
	[SerializeField] GameObject highlight;
    Body selectedBody;

    void Update()
    {
		Body body = Simulator.Instance.GetScreenToBody(Input.mousePosition);
		if (body != null || selectedBody != null)
		{
			body = (selectedBody != null) ? selectedBody : body;

			highlight.SetActive(true);
			highlight.transform.position = body.transform.position;
			highlight.transform.localScale = Vector2.one * body.shape.size * 1.5f;
			highlight.transform.rotation = Quaternion.AngleAxis(Time.time * 90, Vector3.forward);
		}
		else
		{
			highlight.SetActive(false);
		}

		if (selectedBody != null)
		{
			// static body - set at position
			if (selectedBody.bodyType == Body.eBodyType.STATIC)
			{
				selectedBody.position = Simulator.Instance.GetScreenToWorldPosition(Input.mousePosition);
			}
			// kinematic body - set at position, set velocity from position delta
			if (selectedBody.bodyType == Body.eBodyType.KINEMATIC)
			{
			}
			// dynamic body - use spring force to apply force towards position
			if (selectedBody.bodyType == Body.eBodyType.DYNAMIC)
			{
				Vector2 target = selectedBody.position = Simulator.Instance.GetScreenToWorldPosition(Input.mousePosition);
				Vector2 force = Spring.Force(target, selectedBody.position, 0, 25);

				selectedBody.ApplyForce(force, Body.eForceMode.FORCE);
			}
		}
	}

	public void OnPointerDown()
	{
		if (Input.GetMouseButton(2)) // center mouse button
		{
			selectedBody = Simulator.Instance.GetScreenToBody(Input.mousePosition);
		}
	}

	public void OnPointerUp()
	{
		selectedBody = null;
	}

	public void OnPointerExit()
	{
		selectedBody = null;
	}
}
