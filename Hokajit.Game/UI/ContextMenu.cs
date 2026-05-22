using Ratelite;
using Ratelite.Animations;
using Ratelite.Resources;
using Ratelite.UI;
using Ratelite.UI.Widgets;

namespace Hokajit.UI;

public sealed class ContextMenu : UIElement
{
	private readonly Animator<ContextMenu> animator;
	
	public bool show
	{
		get;
		set
		{
			field = value;
			active = true;
			animator.SetBlock(value ? "show" : "hide");
		}
	}
	
	public ContextMenu()
	{
		var ui = Vault.GetAsset<Texture2D>("ui");
		mesh = Vault.GetAsset<Mesh>(UIModule.DEFAULT_MESH);
		// material = new MaterialUI().SetTexture(ui).SetNinePatch(new Region(2), 3);
		uv = ui.GetUVRegion(new RectInt(8, 8, 6, 4));
		size = new Vector2(200, 32);
		pivot = new Vector2(0.5F, 0);
		
		var mask = new Mask { anchorMin = Vector2.zero, anchorMax = Vector2.one };
		AddChild(mask);
		
		var layout = new Layout
		{
			orientation = Orientation.Horizontal, 
			spacing = 5, 
			pivotAndAnchors = new Vector2(0.5F)
		};
		mask.AddChild(layout);
		layout.AddChild(
			new ElementButton(new Image(ui, new RectInt(40, 37, 7))
			{
				scale = new Vector2(2.5F)
			}, () => { }) { size = new Vector2(32)}
		);
		layout.AddChild(
			new ElementButton(new Image(ui, new RectInt(49, 37, 7))
			{
				scale = new Vector2(2.5F)
			}, () => { }) { size = new Vector2(32)}
		);
		layout.AddChild(
			new ElementButton(new Image(ui, new RectInt(58, 37, 6, 7))
			{
				scale = new Vector2(2.5F)
			}, () => { }) { size = new Vector2(32)}
		);
		
		animator = components.AddComponent<Animator<ContextMenu>>();
		var controller = new AnimationController<ContextMenu>();
		var showAnim = new Animation<ContextMenu>();
		showAnim.AddTrack(new AnimationTrack<ContextMenu, Vector2>([
			new KeyFrame<Vector2>(Vector2.right, 0, true),
			new KeyFrame<Vector2>(Vector2.one, 0.1F)
		], (menu, value) => menu.scale = value));
		
		var hideAnim = new Animation<ContextMenu>();
		hideAnim.AddTrack(new AnimationTrack<ContextMenu, Vector2>([
			new KeyFrame<Vector2>(Vector2.right, 0),
		], (menu, value) => menu.scale = value));
		
		controller.AddBlock("show", showAnim);
		controller.AddBlock("hide", hideAnim);
		animator.SetController(this, controller);
	}
}