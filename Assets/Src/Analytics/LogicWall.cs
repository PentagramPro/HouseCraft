
using System;

public class LogicWall
{
	public WallPoint Position;
	public bool Top=false,Bottom=false,Left=false,Right=false;
	public LogicWall (WallPoint point, bool top, bool bottom, bool left, bool right)
	{
		Position = point;
		Top = top;
		Bottom = bottom;
		Left = left;
		Right = right;
	}
}


