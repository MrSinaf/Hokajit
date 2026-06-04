using Ratelite;
using Ratelite.Resources;
using Ratelite.UI;
using Ratelite.UI.Widgets;

namespace Hokajit.Scenes;

public class Menu : Scene
{
	private Canvas canvas = null!;
	
	private Panel mainMenu = null!;
	private Panel optionsMenu = null!;
	private Panel gameMenu = null!;
	
	public override void Init()
	{
		canvas = AddPlugin<Canvas>();
	}
	
	public override void Start()
	{
		var ui = Vault.GetAsset<Texture2D>("ui")!;
		
		//Main
		canvas.root.AddChild(mainMenu = new Panel("group"));
		mainMenu.AddChild(
			new Label("Développé par MrSinaf | PurrVert")
			{
				pivotAndAnchors = new Vector2(0.5F, 0),
			}
		);
		mainMenu.AddChild(
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
		
		var buttons = new Layout
		{
			orientation = Orientation.Horizontal,
			anchors = new Vector2(0.5F),
			pivot = new Vector2(0.5F, 1),
			position = new Vector2(0, -5),
			spacing = 10
		};
		buttons.AddChild(
			new Button(
				"Jouer",
				() =>
				{
					Stage.Load(new Game());
					
					mainMenu.active = false;
					gameMenu.active = true;
				}
			)
		);
		buttons.AddChild(
			new Button(
				"Options",
				() =>
				{
					mainMenu.active = false;
					optionsMenu.active = true;
				}
			)
		);
		buttons.AddChild(new Button("Quitter", () => R.game.window.Close()));
		mainMenu.AddChild(buttons);
		
		// Option
		canvas.root.AddChild(optionsMenu = new Panel("group"));
		// TODO > A implémenter <( _ _ )>
		
		// Game
		canvas.root.AddChild(gameMenu = new Panel("group"));
		
	}
}