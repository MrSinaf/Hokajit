using Ratelite;
using Ratelite.Resources;
using Ratelite.UI;
using Ratelite.UI.Widgets;

namespace Hokajit.Scenes;

public class Menu : Scene
{
	private Canvas canvas = null!;
	
	public override void Init()
	{
		canvas = AddPlugin<Canvas>();
	}
	
	public override void Start()
	{
		var ui = Vault.GetAsset<Texture2D>("ui")!;
		
		canvas.root.padding = new Region(20);
		canvas.root.AddChild(new Label("Développé par MrSinaf | PurrVert")
		{
			pivotAndAnchors = new Vector2(0.5F, 0),
		});
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
		
		var layout = new Layout
		{
			orientation = Orientation.Horizontal,
			anchors = new Vector2(0.5F),
			pivot = new Vector2(0.5F, 1),
			position = new Vector2(0, -5),
			spacing = 10
		};
		layout.AddChild(new Button("Jouer", () => { }));
		layout.AddChild(new Button("Options", () => { }));
		layout.AddChild(new Button("Quitter", () => R.game.window.Close()));
		canvas.root.AddChild(layout);
	}
	
	public override void Update()
	{
		
	}
	
	public override void Render()
	{
		
	}
}