using Ratelite;
using Ratelite.Resources;
using Ratelite.UI;
using Ratelite.UI.Widgets;

namespace Hokajit.Scenes;

public class Menu : Scene
{
	private Canvas canvas = null!;
	private Layout mainButtons = null!;
	private Layout modeButtons = null!;
	
	public override void Init()
	{
		canvas = AddPlugin<Canvas>();
	}
	
	public override void Start()
	{
		var ui = Vault.GetAsset<Texture2D>("ui")!;
		
		canvas.root.padding = new Region(20);
		canvas.root.AddChild(
			new Label("Développé par MrSinaf | PurrVert")
			{
				pivotAndAnchors = new Vector2(0.5F, 0),
			}
		);
		canvas.root.AddChild(
			new Image(ui)
			{
				uv = ui.GetUVRegion(new RectInt(0, 13, 76, 19)),
				size = new Vector2(76, 19),
				anchors = new Vector2(0.5F),
				pivot = new Vector2(0.5F, 0),
				position = new Vector2(0, 5),
				scale = new Vector2(12)
			}
		);
		
		mainButtons = new Layout
		{
			orientation = Orientation.Horizontal,
			anchors = new Vector2(0.5F),
			pivot = new Vector2(0.5F, 1),
			position = new Vector2(0, -5),
			spacing = 10
		};
		mainButtons.AddChild(
			new Button(
				"Jouer",
				() =>
				{
					mainButtons.active = false;
					modeButtons.active = true;
				}
			)
		);
		mainButtons.AddChild(
			new Button(
				"Options",
				() =>
				{ 
					// Ignore pour le moment... ¬_¬
				}
			)
		);
		mainButtons.AddChild(new Button("Quitter", () => R.game.window.Close()));
		canvas.root.AddChild(mainButtons);
		
		modeButtons = new Layout
		{
			pivot = new Vector2(0.5F, 1),
			anchorMin = new Vector2(0.5F, 0.5F),
			position = new Vector2(0, -42),
			active = false,
			alignment = 0.5F,
			spacing = 100
		};
		canvas.root.AddChild(modeButtons);
		var buttons = new Layout
		{
			orientation = Orientation.Horizontal,
			spacing = 200
		};
		modeButtons.AddChild(buttons);
		buttons.AddChild(new Button("JDR", () =>
		{
			Stage.Load(new RolePlay());
			Cursor.SetTexture(0);
		}, "big"));
		buttons.AddChild(new Button("RTS", () => { }, "big"));
		modeButtons.AddChild(
			new Button(
				"Retour",
				() =>
				{
					modeButtons.active = false;
					mainButtons.active = true;
				}
			)
		);
	}
}