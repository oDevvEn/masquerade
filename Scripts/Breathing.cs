using Godot;
using System;

public partial class Breathing : ColorRect
{
	private TextureRect areaVisual;
	private ColorRect selector;
	private RandomNumberGenerator randamiser;
	private CharacterBody2D player;
	private Camera2D playerCamera;
	public Camera2D wardrobeCamera;
	public AudioStreamPlayer2D breathingAudio;
	
	
	public bool breathing = false;
	private float selectorVelocity = 500f;
	private float areaPos = 0f;
	private float areaWidth = 576f;

	
	public override void _Ready()
	{
		areaVisual = GetNode<TextureRect>("AreaVisual");
		selector = GetNode<ColorRect>("Selector");
		randamiser = new RandomNumberGenerator();
		player = (CharacterBody2D)GetParent().GetParent();
		selector.Position = new Vector2(randamiser.RandfRange(0f, 568f), 0f);
		playerCamera = player.GetNode<Camera2D>("Camera2D");
		breathingAudio = player.GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D2");
	}
	
	
	public override void _Process(double delta)
	{
		if (!breathing) return;

		selector.Position += new Vector2((float)delta * selectorVelocity, 0f);
		if (selector.Position.X > 568f || selector.Position.X < 0f)
		{
			selectorVelocity *= -1f;
			selector.Position = new Vector2(Mathf.Clamp(selector.Position.X, 1f, 567f), 0f);
		}
		
		if (Input.IsActionJustPressed("interact"))
		{
			float distance = Mathf.Abs(selector.Position.X + 5f - (areaPos + areaWidth / 2f)) / (areaWidth + 16f);
			GD.Print(distance);
			var audio = player.GetNode<CollisionShape2D>("AUDIOCUEarea/AUDIOCUE");
			if (distance > 1f)
			{
				Visible = false;
				player.Visible = true;
				breathing = false;
				wardrobeCamera.Enabled = false;
				playerCamera.Enabled = true;
				audio.Position = Vector2.Zero;
				breathingAudio.Stop();
				

			}
			else
			{
				// logic
				breathingAudio.Play();
				audio.Disabled = false;
				audio.Scale = new Vector2(50 * distance , 50 * distance);
				audio.GlobalPosition = GetTree().CurrentScene.GetNode<Sprite2D>("Wardrobe").GlobalPosition;

			}
			
			
			areaWidth = randamiser.RandfRange(64f, 256f);
			((GradientTexture1D)areaVisual.Texture).Width = (int)areaWidth;
			areaVisual.Size = new Vector2(areaWidth, 40f);
			areaPos = randamiser.RandfRange(0f, 576f - areaWidth);
			areaVisual.Position = new Vector2(areaPos, 0f);
		}
	}
}
