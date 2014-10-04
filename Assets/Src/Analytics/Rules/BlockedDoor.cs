
public class BlockedDoor : BaseRule, IObjectRule
{
	public BlockedDoor(int a) : base(a) {}

	#region IRoomRule implementation

	public bool Process (Segmentator s, ILogicObject o)
	{
		bool res = false;
	
		o.ObjectRect.Foreach((MapPoint p) => {

			WallPoint wp;

			wp = new WallPoint(p.X,p.Y);
			if(s.Doors.ContainsKey(wp.toInt()))
				res = true;

			wp = new WallPoint(p.X+1,p.Y);
			if(s.Doors.ContainsKey(wp.toInt()))
				res = true;

			wp = new WallPoint(p.X,p.Y+1);
			if(s.Doors.ContainsKey(wp.toInt()))
				res = true;

			wp = new WallPoint(p.X+1,p.Y+1);
			if(s.Doors.ContainsKey(wp.toInt()))
				res = true;

		});
		return res;
	}


	#endregion


}


