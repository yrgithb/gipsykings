using UnityEngine;
using System.Collections;

public class CameraFollowScript : MonoBehaviour
{

	private GameObject[] players;

	// autosizing
	public float minimumCameraSize = 5.0f;
	public float maximumCameraSize = 15.0f;
	private float smoothTime = 0.15f;
	private Vector3 dampingVelocity;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

		FindPlayerObjects(); 
		UpdateCameraSize();

	}

	void UpdateCameraSize()
	{

		// find point between players
		Vector2 middle = pointBetweenPlayers();

		Camera camera = Camera.main;

		float cameraX = middle.x;
		float cameraY = middle.y;

		// limit vertical camera movement
		float bottomMinimum = -5.0f;
		float currentBottom = cameraY - camera.orthographicSize;
		if (currentBottom < bottomMinimum)
		{
			float compensation = bottomMinimum - currentBottom;
			cameraY += compensation;
		}

		float cameraZ = camera.transform.position.z;

		Vector3 targetPosition = new Vector3(cameraX, cameraY, cameraZ);
		camera.transform.position = Vector3.SmoothDamp(camera.transform.position, targetPosition, ref dampingVelocity, smoothTime);

		float screenRatio = Screen.width / Screen.height;
		float minSizeX = minimumCameraSize * screenRatio;

		GameObject player1object = players[0];
		GameObject player2object = players[1];

		// multiplying by 0.5, because the ortographicSize is actually half the height
		float width = Mathf.Abs(player1object.transform.position.x - player2object.transform.position.x) * 0.5f;
		float height = Mathf.Abs(player1object.transform.position.y - player2object.transform.position.y) * 0.5f;

		//computing the size
		float camSizeX = Mathf.Max(width, minSizeX);
		camSizeX += 5.0f;

		float size = Mathf.Max(height + 2.0f, camSizeX * Screen.height / Screen.width, minimumCameraSize);
		size = Mathf.Min(size, maximumCameraSize);
		camera.orthographicSize = size;

	}

	void FindPlayerObjects()
	{

		if (players == null || players[0] == null || players[1] == null)
		{
			// find player objects
			players = new GameObject[2];
			players[0] = GameObject.Find("Player 1");
			players[1] = GameObject.Find("Player 2");
		}

	}

	Vector2 pointBetweenPlayers()
	{

		Vector2 result = new Vector2(0.0f, 0.0f);

		GameObject player1object = players[0];
		GameObject player2object = players[1];
		Transform player1 = player1object.transform;
		Transform player2 = player2object.transform;

		result.x = (player1.position.x + player2.position.x) * 0.5f;
		result.y = (player1.position.y + player2.position.y) * 0.5f;

		return result;

	}

}
