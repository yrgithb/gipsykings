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
	public float baseDamage = 5.1f;

	// inspector values for boulder type bonuses
	public float speedBoulderBoostPercent = 30.0f;
	public float sniperBoulderDamage = 1000.0f;
	public float shieldBoulderDamageReductionPercent = 30.0f;
	public float jumpBoulderBoostPercent = 30.0f;
	public float dashBoulderCooldownReductionPercent = 30.0f;

	// internal bonus values for boulder
	private float speedBonus = 0.0f;
	private float damageBonus = 0.0f;
	private float shieldBonus = 0.0f;
	private float jumpBonus = 0.0f;
	private float dashBonus = 0.0f;

	// Use this for initialization
	void Start()
	{

		ConfigureBoulder();

	}

	// Update is called once per frame
	void Update()
	{

	}

	void ConfigureBoulder()
	{

		string spriteName = "sprite_boulder_";
		if (type == BoulderType.Speed)
		{
			spriteName += "speed";
			speedBonus = speedBoulderBoostPercent;
		}
		else if (type == BoulderType.Sniper)
		{
			spriteName += "oneshot";
			damageBonus = 1000.0f; // some large value to ensure one-shotting
		}
		else if (type == BoulderType.Shield)
		{
			spriteName += "shield";
			shieldBonus = shieldBoulderDamageReductionPercent;
		}
		else if (type == BoulderType.Jump)
		{
			spriteName += "jump";
			jumpBonus = jumpBoulderBoostPercent;
		}
		else if (type == BoulderType.Dash)
		{
			spriteName += "dash";
			dashBonus = dashBoulderCooldownReductionPercent;
		}
		string fullPath = "Sprites/Boulders/" + spriteName;

		iconSpriteRenderer.sprite = Resources.Load<Sprite>(fullPath);

	}

	public float DamageBonus()
	{

		return damageBonus;

	}

	public float SpeedBonus()
	{

		return speedBonus;

	}
	public float ShieldBonus()
	{

		return shieldBonus;

	}
	public float JumpBonus()
	{

		return jumpBonus;

	}
	public float DashBonus()
	{

		return dashBonus;

	}

}
