using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScoreScript : MonoBehaviour
{

	public enum OwningPlayer
	{
		Player1,
		Player2
	}
	public OwningPlayer player;

	private Image playerImage;
	private Text playerScoreText;

	// Use this for initialization
	void Start()
	{

		FindObjects();
		FillInfo();

	}

	// Update is called once per frame
	void Update()
	{

	}

	void FindObjects()
	{

		playerImage = this.GetComponentInChildren<Image>();
		playerScoreText = this.GetComponentInChildren<Text>();

	}

	void FillInfo()
	{

		// set icon
		string imageName = "sprite_icon_p";
		string postfix = "";
		if (player == OwningPlayer.Player1)
		{
			postfix = "1";
		}
		else
		{
			postfix = "2";
		}
		imageName += postfix;
		playerImage.sprite = Resources.Load<Sprite>("Sprites/HUD/Player icons/" + imageName);

		// set score
		string keyName = "scorePlayer";
		keyName += postfix;

		int score = 0;
		if (PlayerPrefs.HasKey(keyName) == true)
		{
			score = PlayerPrefs.GetInt(keyName);
		}
		string scoreText = "";
		scoreText += score;
		playerScoreText.text = scoreText;
		
	}

}
