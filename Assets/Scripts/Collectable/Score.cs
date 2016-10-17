using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public static class Score
{
	private static string scoreText;	// text to be displayed in front of score;
	private static int s = 0;	// Score
	private static Text t;	// Score text

	public static void Add(int i)
	{
		SetScore(s + i);
	}

	public static void Sub(int i)
	{
		SetScore(s - i);
	}

	private static void SetScore(int i)
	{
		if (t == null)
		{
			if (!FindText())
				throw new MissingComponentException ("Text missing on Score");
		}
		s = i;
		t.text = scoreText + s;
	}

	private static bool FindText()
	{
		t = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
		if (t != null)
			scoreText = t.text;
		return (t != null);
	}
}
