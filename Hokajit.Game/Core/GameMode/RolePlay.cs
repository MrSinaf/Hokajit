using Hokajit.Components;
using Hokajit.Objects;
using Hokajit.Scenes;
using Ratelite;
using Ratelite.GO;
using Ratelite.Resources;

namespace Hokajit.GameMode;

public class RolePlay : IGameMode
{
	private Game game = null!;
	private World world = null!;
	private Map map = null!;
	
	private TokenData data = null!;
	
	public void Init()
	{
		if (Stage.current is Game m)
		{
			game = m;
			world = m.GetPlugin<World>();
		}
		else
			throw new Exception("Stage.current is not a Game instance (╬▔皿▔)╯");
		
		map = new Map(world, new Vector2Int(100));
		world.camera.zoom = 3;
		world.camera.position = new Vector2(0, 50 * Map.TILE_SIZE);
		
		game.components.AddComponent<CameraMovements>();
		
		R.game.window.mouseButtonPressed += OnMouseButtonPressed;
		
		data = new TokenData(
			1, Vault.GetAsset<Texture2D>("clan-sanlord")!, new RectInt(16, 0, 16), 8
		);
	}
	
	private void OnMouseButtonPressed(MouseButton button)
	{
		if (button != MouseButton.Left)
			return;
		
		world.AddObject(new Token(data)
		{
			position = world.camera.ScreenToWorldPosition(R.game.window.cursorPosition)
		});
	}
	
	public void Update()
	{
		map.cursorPosition = world.camera.ScreenToWorldPosition(R.game.window.cursorPosition).y;
	}
	
	public void Render() { }
	
	public void Destroy() { }
}