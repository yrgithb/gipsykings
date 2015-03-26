using UnityEngine;
using System.Collections;

public class BoulderScript : MonoBehaviour
{

	public enum BoulderType
	{
		Speed,
		Sniper,
		Shield,
		Jump,
		Dash,
	}

	public BoulderType type;
	public SpriteRenderer iconSpriteRenderer;
	public float damage = 5.1f;

	// Use this for initialization
	void Start()
	{

		// set image according to type
		SetImage();

		// force damage for sniper boulders
		if (type == BoulderType.Sniper)
		{
			damage = 1000.0f; // one shot
		}

	}

	// Update is called once per frame
	void Update()
	{

	}

	void SetImage()
	{

		string postfix = "sprite_boulder_";
		if (type == BoulderType.Speed)
		{
			postfix += "speed";
		}
		else if (type == BoulderType.Sniper)
		{
			postfix += "oneshot";
		}
		else if (type == BoulderType.Shield)
		{
			postfix += "shield";
		}
		else if (type == BoulderType.Jump)
		{
			postfix += "jump";
		}
		else if (type == BoulderType.Dash)
		{
			postfix += "dash";
		}
		string fullPath = "Sprites/Boulders/" + postfix;

		iconSpriteRenderer.sprite = Resources.Load<Sprite>(fullPath);
		
	}
}
