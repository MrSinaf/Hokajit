using Ratelite;
using Ratelite.Resources;
using Ratelite.UI;
using Ratelite.UI.Widgets;

namespace Hokajit.UI.Layouts;

public sealed class MJLayout : UIElement
{
	private readonly Panel characters;
	
	public MJLayout()
	{
		isInteractif = false;
		anchorMin = Vector2.zero;
		anchorMax = Vector2.one;
		
		var buttons = new Layout
		{
			pivotAndAnchors = new Vector2(0.5F, 0),
			orientation = Orientation.Horizontal,
			spacing = 10
		};
		buttons.AddChild(new Button("Ajouter", () => { }));
		buttons.AddChild(new Button("Contrôle", () => { }));
		buttons.AddChild(new Button("Jet de dé", () => { }));
		AddChild(buttons);
		
		AddChild(characters = new Panel
		{
			size = new Vector2(125),
			anchorMin = Vector2.zero,
			anchorMax = new Vector2(0, 1),
			margin = new Region(0, 0, 0, 45)
		});
		ListingCharacter();
	}
	
	public void ListingCharacter()
	{
		var layout = new Layout{alignment = 0.5F, pivotAndAnchors = new Vector2(0.5F, 0)};
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
		characters.AddChild(element);
	}
}