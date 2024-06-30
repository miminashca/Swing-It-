using System;                   // System contains a lot of default C# libraries 
using GXPEngine;                // GXPEngine contains the engine

public class MyGame : Game {
	public MyGame() : base(1920, 1080, false, false)
	{
		SceneHandler scenehandler = new SceneHandler();
		AddChild(scenehandler);
		SoundHandler soundHandler = new SoundHandler();
		AddChild(soundHandler);
	}
	void Update()
	{
        //Console.WriteLine(targetFps + " || " + currentFps);
  }

	static void Main() {
		new MyGame().Start();
	}
}