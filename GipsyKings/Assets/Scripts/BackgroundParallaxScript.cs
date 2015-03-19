using UnityEngine;
using System.Collections;

public class BackgroundParallaxScript : MonoBehaviour
{

	public float parallaxScale = 10.0f;

	private Camera mainCamera;
	private GameObject[] layers;
	private Vector3 lastPosition;

	void Start ()
	{

		mainCamera = Camera.main;
		lastPosition = mainCamera.transform.position;
		FindLayers ();

	}

	void Update ()
	{

		UpdateLayerPositions ();

	}

	void UpdateLayerPositions ()
	{

		Vector3 camPosition = mainCamera.transform.position;
		Vector3 difference = new Vector3 (lastPosition.x - camPosition.x, lastPosition.y - camPosition.y, 0.0f);
		lastPosition = camPosition;

		for (int i = 0; i < layers.Length; ++i) {
			GameObject layer = layers [i];
			SpriteRenderer renderer = layer.GetComponent<SpriteRenderer> ();
			int order = renderer.sortingOrder;
		
			Vector3 position = layer.transform.position;
			position.x += difference.x * order / parallaxScale;
			position.y += difference.y * order / parallaxScale;
			layer.transform.position = position;
		}

	}

	void FindLayers ()
	{

		SpriteRenderer[] renderers = this.gameObject.GetComponentsInChildren<SpriteRenderer> ();

		layers = new GameObject[renderers.Length];
		for (int i = 0; i < renderers.Length; ++i) {
			SpriteRenderer renderer = renderers[i];
			layers[i] = renderer.gameObject;
		}

	}

}
