using Hokajit.Objects;
using Hokajit.UI.Layouts;
using Ratelite;
using Ratelite.GO;
using Ratelite.UI;

namespace Hokajit.Scenes;

public class RolePlay : Scene
{
	public static RolePlay m { get; private set; }= null!;
	
	public World world = null!;
	public Canvas canvas = null!;
	private Camera camera = null!;
	
	public override void Init()
	{
		m = this;
		world = AddPlugin<World>();
		canvas = AddPlugin<Canvas>();
	}
	
	public override void Start()
	{
		canvas.root.AddChild(new MJLayout());
		
		camera = world.camera;
		world.AddObject(new Map(new Vector2Int(100)));
		camera.zoom = 5;
		camera.position = new Vector2(50 * Map.CELL_SIZE);
		
		R.game.window.mouseButtonPressed += OnMouseButtonPressed;
		R.game.window.mouseButtonReleased += OnMouseButtonReleased;
		R.game.window.scrolled += OnScrolled;
	}
	
	public override void Unload()
	{
		R.game.window.cursorMoved -= OnCursorMoved;
		R.game.window.mouseButtonPressed -= OnMouseButtonPressed;
		R.game.window.mouseButtonReleased -= OnMouseButtonReleased;
		R.game.window.scrolled -= OnScrolled;
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
	
	private void OnMouseButtonPressed(MouseButton button)
	{
#if DEBUG
		if (ImGui.GetIO().WantCaptureMouse)
			return;
#endif
		if (canvas.hasElementHovered)
			return;
		
		if (button == MouseButton.Right)
		{
			Cursor.SetTexture(3);
			R.game.window.cursorMoved += OnCursorMoved;
		}
		else if (button == MouseButton.Middle)
		{
			Stage.Load(new Menu());
		}
	}
	
	private void OnMouseButtonReleased(MouseButton button)
	{
		if (button == MouseButton.Right)
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