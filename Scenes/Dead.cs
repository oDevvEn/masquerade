using Godot;
using System;

public partial class Dead : ColorRect
{
	public static double startTime;
	
	[Export] private Label timePlayedLabel;
	public override void _Ready()
	{
		var death = new AudioStreamPlayer();
		death.Stream = ResourceLoader.Load<AudioStream>("res://Assets/death.mp3");
		AddChild(death);
		death.Play();
			
		
		int[] times = new int[3];
		double timePlayed = Time.GetUnixTimeFromSystem() - startTime;
		times[0] = (int)(timePlayed % 60);
		times[1] = (int)(timePlayed / 60);
		times[2] = (int)(timePlayed / 3600);
		timePlayedLabel.Text = $"Survived for:\n{times[2]} hours, {times[1]} minutes, {times[0]} seconds";
	}

	private void MainMenuButtonPressed() 
	{
		GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
	}
}
