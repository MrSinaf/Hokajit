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
		gameMode = AddPlugin<RolePlay>();
		world = gameMode.world = AddPlugin<World>();
	}
}