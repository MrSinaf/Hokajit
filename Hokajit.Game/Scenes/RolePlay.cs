using Hokajit.Objects;
using Hokajit.UI.Layouts;
using Ratelite;
using Ratelite.GO;
using Ratelite.UI;

namespace Hokajit.Scenes;

public class RolePlay : Scene
{
	public static RolePlay m { get; private set; } = null!;
	
	public World world = null!;
	public Canvas canvas = null!;
	public MJLayout layout = null!;
	private Camera camera = null!;
	
	private CharacterData? selectCharacter;
	
	public override void Init()
	{
		m = this;
		world = AddPlugin<World>();
		canvas = AddPlugin<Canvas>();
	}
	
	public override void Start()
	{
		canvas.root.AddChild(layout = new MJLayout());
		
		camera = world.camera;
		world.AddObject(new Map(new Vector2Int(100)));
		camera.zoom = 5;
		camera.position = new Vector2(50 * Game.TILE_SIZE);
		
		R.game.window.keyPressed += OnKeyPressed;
		R.game.window.mouseButtonPressed += OnMouseButtonPressed;
		R.game.window.mouseButtonReleased += OnMouseButtonReleased;
		R.game.window.scrolled += OnScrolled;
	}
	
	public override void Unload()
	{
		R.game.window.keyPressed -= OnKeyPressed;
		R.game.window.cursorMoved -= OnCursorMoved;
		R.game.window.mouseButtonPressed -= OnMouseButtonPressed;
		R.game.window.mouseButtonReleased -= OnMouseButtonReleased;
		R.game.window.scrolled -= OnScrolled;
	}
	
	public void DropCharacter(CharacterData data)
	{
		selectCharacter = data;
	}
	
	private void OnScrolled(Vector2Int delta)
	{
#if DEBUG
		if (ImGui.GetIO().WantCaptureMouse)
			return;
#endif
		if (canvas.hasElementHovered)
			return;
		
		camera.zoom = float.Clamp(world.camera.zoom += delta.y * Time.delta * 10, 1, 10);
	}
	
	private static void OnKeyPressed(Key key, int _)
	{
		switch (key)
		{
			case Key.Escape:
				Stage.Load(new Menu());
				break;
		}
	}
	
	private void OnMouseButtonPressed(MouseButton button)
	{
#if DEBUG
		if (ImGui.GetIO().WantCaptureMouse)
			return;
#endif
		if (canvas.hasElementHovered)
			return;
		
		layout.contextMenu.active = false;
		if (button == MouseButton.Left)
		{
			if (selectCharacter != null)
			{
				world.AddObject(new Character(selectCharacter)
				{
					position = Map.WorldPositionToCell(
						camera.ScreenToWorldPosition(R.game.window.cursorPosition)
					)
				});
				selectCharacter = null;
			}
		}
		else if (button == MouseButton.Right)
		{
			layout.contextMenu.active = true;
			layout.contextMenu.position = R.game.window.cursorPosition;
		}
		else if (button == MouseButton.Middle)
		{
			Cursor.SetTexture(3);
			R.game.window.cursorMoved += OnCursorMoved;
		}
	}
	
	private void OnMouseButtonReleased(MouseButton button)
	{
		if (button == MouseButton.Middle)
		{
			Cursor.SetTexture(0);
			R.game.window.cursorMoved -= OnCursorMoved;
		}
	}
	
	private void OnCursorMoved(Vector2 delta)
	{
		camera.position -= delta / camera.zoom;
	}
}