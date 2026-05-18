using Ratelite;
using Ratelite.Animations;
using Ratelite.Resources;
using Ratelite.UI;
using Ratelite.UI.Widgets;

namespace Hokajit.UI.Layouts;

public sealed class MJLayout : UIElement
{
	private readonly Panel sidePanel;
	public UIElement? selectedContainerPanel;
	public UIElement? oldContainerPanel;
	
	public MJLayout()
	{
		isInteractif = false;
		anchorMin = Vector2.zero;
		anchorMax = Vector2.one;
		
		var buttons = new Layout
		{
			position = new Vector2(0, 5),
			pivotAndAnchors = new Vector2(0.5F, 0),
			orientation = Orientation.Horizontal,
			spacing = 5
		};
		var ui = Vault.GetAsset<Texture2D>("ui")!;
		buttons.AddChild(new ElementButton(new Image(ui)
		{ // Characters
			size = new Vector2(8, 7),
			uv = ui.GetUVRegion(new RectInt(2, 37, 8, 7)),
			scale = new Vector2(4),
		}, () => { }, "icon"));
		buttons.AddChild(new ElementButton(new Image(ui)
		{ // Droits
			size = new Vector2(8, 7),
			uv = ui.GetUVRegion(new RectInt(12, 37, 8, 7)),
			scale = new Vector2(4),
		}, () => { }, "icon"));
		buttons.AddChild(new ElementButton(new Image(ui)
		{ // Dés
			size = new Vector2(7, 7),
			uv = ui.GetUVRegion(new RectInt(22, 37, 7, 7)),
			scale = new Vector2(4),
		}, () =>
		{
			selectedContainerPanel = ListingCharacter();
		}, "icon"));
		AddChild(buttons);
		
		AddChild(sidePanel = new Panel
		{
			position = new Vector2(-450, 0),
			size = new Vector2(450),
			anchorMin = Vector2.zero,
			anchorMax = new Vector2(0, 1),
			margin = new Region(0, 45, 0, 45)
		});
		var animator = sidePanel.components.AddComponent<Animator<Panel>>();
		var controller = new AnimationController<Panel>();
		var showAnimation = new AnimationTrack<Panel, float>(
			[
				new KeyFrame<float>(-sidePanel.size.x, 0, true),
				new KeyFrame<float>(20, 0.20F, true),
				new KeyFrame<float>(-5, 0.25F, true),
				new KeyFrame<float>(2, 0.26F, true),
				new KeyFrame<float>(0, 0.27F, true)
			],
			(panel, f) => panel.position = new Vector2(f, 0)
		);
		var hideAnimation = new AnimationTrack<Panel, float>(
			[
				new KeyFrame<float>(0, 0, true),
				new KeyFrame<float>(20, 0.2F, true),
				new KeyFrame<float>(-sidePanel.size.x, 0.35F)
			],
			(panel, f) => panel.position = new Vector2(f, 0)
		);
		
		controller.AddBlock("isHide", (animator, _) =>
		{
			if (selectedContainerPanel != null)
			{
				oldContainerPanel?.Destroy();
				selectedContainerPanel.active = true;
				oldContainerPanel = selectedContainerPanel;
				selectedContainerPanel = null;
				animator.SetBlock("show");
			}
		});
		controller.AddBlock("show", new Animation<Panel>().AddTrack(showAnimation), (animator, _) =>
		{
			if (!animator.isRunning)
				animator.SetBlock("isShow");
		});
		controller.AddBlock("isShow", (animator, _) =>
		{
			if (selectedContainerPanel != null)
				animator.SetBlock("hide");
		});
		controller.AddBlock("hide", new Animation<Panel>().AddTrack(hideAnimation), (animator, _) =>
		{
			if (!animator.isRunning)
				animator.SetBlock("isHide");
		});
		animator.SetController(sidePanel, controller);
		ListingCharacter();
	}
	
	public UIElement ListingCharacter()
	{
		var layout = new Layout();
		var dratug = Vault.GetAsset<Texture2D>("units/dratug")!;
		for (var i = 0; i < 100; i++)
		{
			layout.AddChild(new Image(dratug)
			{
				captureCursorEvent = false,
				size = new Vector2(16, 16),
				scale = new Vector2(5),
				uv = dratug.GetUVRegion(new RectInt(Random.Shared.Next(0, 4) * 16, 0, 16))
			});
		}
		var element = new ScrollView(layout, withHorizontal: false)
		{
			anchorMin = Vector2.zero, anchorMax = Vector2.one
		};
		element.active = false;
		sidePanel.AddChild(element);
		return element;
	}
}