using Hokajit.Scenes;
using Ratelite;
using Ratelite.Animations;
using Ratelite.Resources;
using Ratelite.UI;
using Ratelite.UI.Widgets;

namespace Hokajit.UI.Layouts;

public sealed class MJLayout : UIElement
{
	private readonly Panel sidePanel;
	private readonly Animator<Panel> sidePanelAnimator;
	
	public UIElement? selectedContainerPanel;
	public UIElement? oldContainerPanel;
	
	public MJLayout()
	{
		isInteractive = false;
		anchorMin = Vector2.zero;
		anchorMax = Vector2.one;
		R.game.window.mouseButtonPressed += OnMouseButtonPressed;
		
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
		}, () => selectedContainerPanel = ListingCharacters(), "icon"));
		buttons.AddChild(new ElementButton(new Image(ui)
		{ // Objets
			size = new Vector2(7, 7),
			uv = ui.GetUVRegion(new RectInt(31, 37, 7)),
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
		}, () => { }, "icon"));
		AddChild(buttons);
		
		AddChild(sidePanel = new Panel
		{
			position = new Vector2(-450, 0),
			size = new Vector2(450),
			anchorMin = Vector2.zero,
			anchorMax = new Vector2(0, 1),
			margin = new Region(0, 45, 0, 45)
		});
		sidePanelAnimator = sidePanel.components.AddComponent<Animator<Panel>>();
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
		sidePanelAnimator.SetController(sidePanel, controller);
	}
	
	private void OnMouseButtonPressed(MouseButton button)
	{
		if (!RolePlay.m.canvas.hasElementHovered && sidePanelAnimator.block.name == "isShow")
			sidePanelAnimator.SetBlock("hide");
	}
	
	private UIElement ListingCharacters()
	{
		var layout = new Layout
		{
			anchorMin = Vector2.zero,
			anchorMax = Vector2.right,
			spacing = 5,
			padding = new Region(0, 0, 5, 0)
		};
		foreach (var tokenData in DataManager.characters)
			layout.AddChild(new SelectCharacter(tokenData));
		
		var element = new ScrollView(layout, withHorizontal: false)
		{
			anchorMin = Vector2.zero, anchorMax = Vector2.one, active = false, scrollSpeed = 25
		};
		sidePanel.AddChild(element);
		return element;
	}
}