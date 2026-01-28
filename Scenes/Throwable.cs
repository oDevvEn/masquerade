using Godot;
using System;

public partial class Throwable : RigidBody2D
{
	public float speed = 400f;

	public void Throw(Vector2 targetPos)
	{
		LinearVelocity = targetPos * speed;
	}
}
