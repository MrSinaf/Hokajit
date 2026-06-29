using Ratelite;
using Ratelite.GO;
using Ratelite.UI;

namespace Hokajit.Components;

public class CameraMovements : IComponent
{
	public bool enable
	{
		get;
		set
		{
			if (value && Stage.current.TryGetPlugin(out World? world))
			{
				camera = world!.camera;
				Stage.current.TryGetPlugin(out canvas);
				R.game.window.mouseButtonPressed += OnMouseButtonPressed;
				R.game.window.mouseButtonReleased += OnMouseButtonReleased;
				R.game.window.scrolled += OnScrolled;
			}
			else
			{
				field = false;
				R.game.window.cursorMoved -= OnCursorMoved;
				R.game.window.mouseButtonPressed -= OnMouseButtonPressed;
				R.game.window.mouseButtonReleased -= OnMouseButtonReleased;
				R.game.window.scrolled -= OnScrolled;
			}
		}
	}
	
	public float minZoom
	{
		get;
		set
		{
			field = value;
			OnScrolled(Vector2Int.zero);
		}
	} = 3;
	
	public float maxZoom
	{
		get;
		set
		{
			field = value;
			OnScrolled(Vector2Int.zero);
		}
	} = 6;
	
	public Camera? camera { get; private set; }
	private Canvas? canvas;
	
	private void OnCursorMoved(Vector2 delta) => camera.position -= delta / camera.zoom;
	
	private void OnMouseButtonReleased(MouseButton button)
	{
		if (button == MouseButton.Right)
		{
			Cursor.SetTexture(0);
			R.game.window.cursorMoved -= OnCursorMoved;
		}
	}
	
	private void OnMouseButtonPressed(MouseButton button)
	{
#if DEBUG
		if (ImGui.GetIO().WantCaptureMouse)
			return;
#endif
		if (canvas is { hasElementHovered: true })
			return;
		
		if (camera != null && button == MouseButton.Right)
		{
			Cursor.SetTexture(3);
			R.game.window.cursorMoved += OnCursorMoved;
		}
	}
	
	private void OnScrolled(Vector2Int delta)
	{
#if DEBUG
		if (ImGui.GetIO().WantCaptureMouse)
			return;
#endif
		if (canvas is { hasElementHovered: true })
			return;
		
		camera.zoom = float.Clamp(camera.zoom += delta.y * Time.delta * 10, minZoom, maxZoom);
	}
}