using Hokajit.Plugins;
using Ratelite;
using Ratelite.GO;

namespace Hokajit.Scenes;

public class Game : Scene
{
	public World world = null!;
	
	public override void Init()
	{
		world = AddPlugin<World>();
		AddPlugin<RolePlay>();
	}
}