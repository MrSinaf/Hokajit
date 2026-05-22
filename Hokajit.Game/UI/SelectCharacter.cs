using Hokajit.Objects;
using Hokajit.Scenes;
using Ratelite;
using Ratelite.UI;
using Ratelite.UI.Widgets;

namespace Hokajit.UI;

public sealed class SelectCharacter : ElementButton
{
	public readonly CharacterData data;
	
	public SelectCharacter(CharacterData data)
			: base(new Layout { orientation = Orientation.Horizontal }, () => { })
	{
		onClick += Click;
		
		captureCursorEvent = false;
		this.data = data;
		var layout = (Layout)element;
		layout.pivotAndAnchors = new Vector2(0, 0.5F);
		layout.padding = new Region(5);
		layout.alignment = 1;
		var icon = new UIElement { size = new Vector2(100), captureCursorEvent = false };
		icon.AddChild(new Image(data.texture)
		{
			captureCursorEvent = false,
			uv = data.uv, 
			size = data.size.aspect,
			scale = new Vector2(data.size.y <= 16 ? 50 : 100, data.size.y <= 16 ? 50 : 100), 
			pivotAndAnchors = new Vector2(0.5F, 0)
		});
		layout.AddChild(icon);
		layout.AddChild(new Label(data.name));
		anchorMin = Vector2.zero;
		anchorMax = Vector2.right;
		size = new Vector2(120);
	}
	
	public override void OnDestroy()
	{
		onClick -= Click;
	}
	
	private void Click()
	{
		RolePlay.m.DropCharacter(data);
	}
}