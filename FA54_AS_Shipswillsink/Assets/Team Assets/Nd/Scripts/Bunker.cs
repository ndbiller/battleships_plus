using UnityEngine;
using System.Collections;

public class Bunker : ScriptableObject {

	//public string name = "";
	public int length = 1;
	public int hitpoints = 1;
	public bool horizontal = true;
	public int x = 0;
	public int y = 0;
	public bool destroyed = false;

	public Bunker()
	{

	}

	public Bunker(string newName, int newLength)
	{
		name = newName;
		length = newLength;
		hitpoints = newLength;
	}

	public Bunker(string newName, int newLength, bool newHorizontal, int newX, int newY, bool newDestroyed)
	{
		name = newName;
		length = newLength;
		hitpoints = length;
		horizontal = newHorizontal;
		x = newX;
		y = newY;
		destroyed = newDestroyed;
	}
}