using UnityEngine;
using System.Collections;

/// <summary>
/// Class for handling mouse spherical calculations.
/// </summary>
public class MouseHandler {
	private static MouseHandler instance;
	public float r; // radial distance
	public float azimuth; // azimuthal angle
	public float polar; // polar angle

	/// <summary>
	/// Singleton. Gets the instance.
	/// </summary>
	public static MouseHandler Instance
	{
		get {
			if (instance == null)
			{
				instance = new MouseHandler();
			}
			return instance;
		}
	}

	public void UpdateSphericalCoordinates(float _radialDist, float _azimuthalAngle, float _polarAngle) {
		r = _radialDist;
		azimuth = _azimuthalAngle;
		polar = _polarAngle;
	}
	
	public Vector3 SphericalToCartesian() {
		// Convert to radians
		float polarR = polar * Mathf.Deg2Rad;
		float azimuthR = azimuth * Mathf.Deg2Rad;
		
		// Find cartesian coordinates
		float x = r * Mathf.Sin(polarR) * Mathf.Sin(azimuthR);
		float y = r * Mathf.Cos(polarR);
		float z = r * Mathf.Sin(polarR) * Mathf.Cos(azimuthR);
		
		return new Vector3(x, y, z);
	}
	
}