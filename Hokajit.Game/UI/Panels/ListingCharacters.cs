using Ratelite;
using Ratelite.UI;
using Ratelite.UI.Widgets;

namespace Hokajit.UI.Panels;

public sealed class ListingCharacters : UIElement
{
	public ListingCharacters()
	{
		anchorMin = Vector2.zero;
		anchorMax = Vector2.one;
		var layout = new Layout
		{
			anchorMin = Vector2.zero,
			anchorMax = Vector2.right,
			spacing = 5,
			padding = new Region(0, 0, 5, 0)
		};
		
		foreach (var tokenData in DataManager.characters)
			layout.AddChild(new SelectCharacter(tokenData));
		
		AddChild(new ScrollView(layout, withHorizontal: false)
		{
			anchorMin = Vector2.zero,
			anchorMax = Vector2.one,
			scrollSpeed = 25
		});
	}
}