using Godot;
using System;

public partial class MainMenu : TextureRect
{
	public void PlayButtonPressed()
	{
		Dead.startTime = Time.GetUnixTimeFromSystem();
		GetTree().ChangeSceneToFile("res://sampleroom.tscn");
	}

	public void QuitButtonPressed()
	{
		GetTree().Quit();
	}
}
