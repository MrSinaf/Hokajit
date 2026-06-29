using Hokajit.Components;
using Hokajit.Objects;
using Hokajit.Scenes;
using Ratelite;
using Ratelite.GO;

namespace Hokajit.GameMode;

public class RolePlay : IGameMode
{
	private Game game = null!;
	private World world = null!;
	private Map map = null!;
	
	public void Init()
	{
		if (Stage.current is Game m)
		{
			game = m;
			world = m.GetPlugin<World>();
		}
		else
			throw new Exception("Stage.current is not a Game instance (╬▔皿▔)╯");
		
		map = new Map(world);
		world.camera.zoom = 3;
		world.camera.position = new Vector2(50 * Map.TILE_SIZE);
		
		game.components.AddComponent<CameraMovements>();
	}
	
	public void Update() { }
	
	public void Render() { }
	
	public void Destroy() { }
}