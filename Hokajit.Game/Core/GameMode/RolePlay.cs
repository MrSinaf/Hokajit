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
	
	public void Init()
	{
		if (Stage.current is Game m)
		{
			game = m;
			world = m.GetPlugin<World>();
		}
		else
			throw new Exception("Stage.current is not a Game instance (╬▔皿▔)╯");
		
		world.AddObject(new Map { position = new Vector2(-50 * Map.TILE_SIZE) });
		world.camera.zoom = 3;
		
		game.components.AddComponent<CameraMovements>();
	}
	
	public void Update() { }
	
	public void Render() { }
	
	public void Destroy() { }
}