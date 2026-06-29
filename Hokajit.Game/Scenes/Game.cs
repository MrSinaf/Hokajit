using Hokajit.GameMode;
using Ratelite;
using Ratelite.GO;

namespace Hokajit.Scenes;

public class Game : Scene
{
	private IGameMode gameMode = null!;
	private World world = null!;
	
	public override void Init()
	{
		world = AddPlugin<World>();
		
		// Pour le moment uniquement le RolePlay (>'-'<)
		gameMode = AddPlugin<RolePlay>();
		
		Cursor.SetTexture(0);
	}
}