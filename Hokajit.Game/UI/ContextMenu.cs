using Ratelite;
using Ratelite.Resources;
using Ratelite.UI;

namespace Hokajit.UI;

public sealed class ContextMenu : UIElement
{
	public ContextMenu()
	{
		var ui = Vault.GetAsset<Texture2D>("ui");
		mesh = Vault.GetAsset<Mesh>(UIModule.DEFAULT_MESH);
		material = new MaterialUI().SetTexture(ui).SetNinePatch(new Region(2), 3);
		uv = ui.GetUVRegion(new RectInt(8, 8, 6, 4));
		size = new Vector2(200, 32);
		pivot = new Vector2(0.5F, 0);
	}
	
	public void Show(Vector2 position)
	{
		
	}
	
}