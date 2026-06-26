using Hokajit.GameMode;
using Ratelite;
using Ratelite.GO;

namespace Hokajit.Scenes;

public class Game : Scene
{
	public IGameMode gameMode = null!;
	public World world = null!;
	
	public override void Init()
	{
		world = AddPlugin<World>();
		gameMode = AddPlugin<RolePlay>();
		gameMode.world = world;
		
		Cursor.SetTexture(0);
	}
}