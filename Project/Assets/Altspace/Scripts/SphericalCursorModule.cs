﻿using UnityEngine;

/// <summary>
/// Module for controlling the spherical cursor.
/// </summary>
public class SphericalCursorModule : MonoBehaviour {
	// This is a sensitivity parameter that should adjust how sensitive the mouse control is.
	public float Sensitivity;

	// This is a scale factor that determines how much to scale down the cursor based on its collision distance.
	public float DistanceScaleFactor;

	// This is a toggle for whether we want spherical coordinate calculation for this example.
	public bool UseSphericalCalculation;
	
	// This is the layer mask to use when performing the ray cast for the objects.
	// The furniture in the room is in layer 8, everything else is not.
	private const int ColliderMask = (1 << 8);

	// This is the Cursor game object. Your job is to update its transform on each frame.
	private GameObject Cursor;

	// This is the Cursor mesh. (The sphere.)
	private MeshRenderer CursorMeshRenderer;

	// This is the scale to set the cursor to if no ray hit is found.
	private Vector3 DefaultCursorScale = new Vector3(10.0f, 10.0f, 10.0f);

	// Maximum distance to ray cast.
	private const float MaxDistance = 100.0f;

	// Sphere radius to project cursor onto if no raycast hit.
	private const float SphereRadius = 20.0f;

    void Awake() {
		Cursor = transform.Find("Cursor").gameObject;
		CursorMeshRenderer = Cursor.transform.GetComponentInChildren<MeshRenderer>();
        CursorMeshRenderer.GetComponent<Renderer>().material.color = new Color(0.0f, 0.8f, 1.0f);
    }	

	void Update()
	{
		// TODO: Handle mouse movement to update cursor position.
		GameObject cursor = GameObject.Find("Cursor");
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Vector3 direction = ray.direction.normalized;

		if (UseSphericalCalculation) {
			/* Spherical Coordinates Calculation From First Principles */	
			// Find the polar and azimuth angles from this ray direction
			float polar = Vector3.Angle(Vector3.up, direction); // polar won't go beyond 180 degrees
			Vector3 directionAzimuth = new Vector3(direction.x, 0f, direction.z);
			float azimuth = Vector3.Angle(Vector3.forward, directionAzimuth); // azimuth need to calculate in 360 degrees
			float sign = Mathf.Sign(Vector3.Dot(Vector3.up, Vector3.Cross(Vector3.forward, directionAzimuth)));
			azimuth = sign * azimuth;
			// Update Mouse Handler
			MouseHandler.Instance.UpdateSphericalCoordinates(SphereRadius, azimuth, polar);
			cursor.transform.position = transform.position + MouseHandler.Instance.SphericalToCartesian();
		} else {
			/* Regular Projection Method */
			cursor.transform.position = transform.position + direction * SphereRadius;
		}

		// TODO: Perform ray cast to find object cursor is pointing at.
		// TODO: Update cursor transform.
		var cursorHit = new RaycastHit();/* Your cursor hit code should set this properly. */;
		Physics.Raycast(transform.position, direction, out cursorHit, MaxDistance, ColliderMask);

		// Update highlighted object based upon the raycast.
		if (cursorHit.collider != null)
		{
			Selectable.CurrentSelection = cursorHit.collider.gameObject;

			// Modify the cursor size based on distance
			float distanceToObject = (cursorHit.point - transform.position).magnitude;
			float scaleFactor = (distanceToObject * DistanceScaleFactor + 1.0f) / 2.0f;
			cursor.transform.localScale = DefaultCursorScale * scaleFactor;
		}
		else
		{
			Selectable.CurrentSelection = null;
			cursor.transform.localScale = DefaultCursorScale;
		}
	}
}
