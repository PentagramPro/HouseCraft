using System.Collections.Generic;

public class NarrowCoridor : BaseRule, IRoomRule
{
	public NarrowCoridor(int a) : base(a) {}

	List<LogicCell> toProcess,processed;
	#region IRoomRule implementation

	public bool Process (Segmentator s, Room r)
	{
		processed = new List<LogicCell>();
		Dictionary<Door,LogicCell> doorCells = new Dictionary<Door, LogicCell>();
		foreach(Door d in r.Doors)
		{
			if(doorCells.ContainsKey(d))
				continue;
			LogicCell c = null;
			MapPoint p = null;

			p =new MapPoint(d.Position.X,d.Position.Y);
			if(r.LogicCells.TryGetValue(p.toInt(),out c) && !processed.Contains(c))
			{
				processed.Add(c);
				doorCells.Add(d,c);
				continue;
			}

			p =new MapPoint(d.Position.X-1,d.Position.Y);
			if(r.LogicCells.TryGetValue(p.toInt(),out c) && !processed.Contains(c))
			{
				processed.Add(c);
				doorCells.Add(d,c);
				continue;
			}

			p =new MapPoint(d.Position.X,d.Position.Y-1);
			if(r.LogicCells.TryGetValue(p.toInt(),out c) && !processed.Contains(c))
			{
				processed.Add(c);
				doorCells.Add(d,c);
				continue;
			}

			p =new MapPoint(d.Position.X-1,d.Position.Y-1);
			if(r.LogicCells.TryGetValue(p.toInt(),out c) && !processed.Contains(c))
			{
				processed.Add(c);
				doorCells.Add(d,c);
				continue;
			}


		}

		toProcess = new List<LogicCell>(doorCells.Values);
		if(toProcess.Count==0)
			return false;

		processed.Clear();

		Recursive(toProcess[0]);

		if(toProcess.Count>0)
			return true;

		return false;
	}



	#endregion

	void Recursive(LogicCell cell)
	{
		processed.Add(cell);
		bool wasWithDoor = toProcess.Remove(cell);

		if(toProcess.Count==0)
			return;

		if( (cell.ReachableCells.Count>2 && cell.AdjacentWalls<3) || (cell.ReachableCells.Count==2 && wasWithDoor))
		{
			foreach(LogicCell c in cell.ReachableCells )
			{
				if(processed.Contains(c))
					continue;

				Recursive(c);
			}
		}
	}

}


