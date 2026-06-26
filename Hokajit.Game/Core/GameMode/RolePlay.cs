using Hokajit.Objects;
using Ratelite;
using Ratelite.GO;

namespace Hokajit.GameMode;

public class RolePlay : IGameMode
{
	public World world { get; set; } = null!;
	
	public void Init()
	{
		world.AddObject(new Map { position = new Vector2(-50 * Map.TILE_SIZE) });
		world.camera.zoom = 3;
	}
	
	public void Update()
	{
		
	}
	
	public void Render()
	{
		
	}
	
	public void Destroy()
	{
		
	}
}