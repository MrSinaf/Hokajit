using Hokajit.Scenes;
using Ratelite;
using Ratelite.Debugs;
using Ratelite.Resources;
using Ratelite.Sounds;
using Ratelite.UI;
using Ratelite.UI.Widgets;


R.CreateGame("Hokajit")
 .SetIcon("assets/icon.png")
 .AddModule<SoundModule>()
 .AddModule<UIModule>()
#if DEBUG
 .AddModule<DebugModule>()
#endif
 .SetStartingScene<Splash>()
 .LoadingAssets(async progress =>
	 {
		 Cursor.AddTexture(
			 (await Vault.LoadAsyncResource<Texture2D>("textures/cursors/default.png"))!
			 .AsRawImage()
		 );
		 Cursor.AddTexture(
			 (await Vault.LoadAsyncResource<Texture2D>("textures/cursors/pointer.png"))!
			 .AsRawImage()
		 );
		 Cursor.AddTexture(
			 (await Vault.LoadAsyncResource<Texture2D>("textures/cursors/click.png"))!
			 .AsRawImage(),
			 new Vector2Int(0, -2)
		 );
		 progress.Report(0.25F);
		 
		 UIPrefab.Add<Button>(string.Empty, ButtonPrefab);
		 progress.Report(0.5F);
		 
		 await Vault.LoadAsyncResource<Texture2D>("textures/purrvert.png", "purrvert");
		 await Vault.LoadAsyncResource<Texture2D>("textures/ui.png", "ui");
		 progress.Report(1);
	 }
 )
 .Run();

void ButtonPrefab(Button e)
{
	var ui = Vault.GetAsset<Texture2D>("ui");
	e.material = Vault.GetAsset<MaterialUI>(UIModule.DEFAULT_MATERIAL)
					  .SetTexture(ui)
					  .SetNinePatch(new Region(3, 1, 2, 1), 5);
	e.mesh = Vault.GetAsset<Mesh>(UIModule.DEFAULT_MESH);
	e.size = new Vector2(200, 30);
	
	e.label.transform = Transform.Upper;
	e.label.pivotAndAnchors = new Vector2(0.5F);
	
	e.cursorEnter += OnMouseEnter;
	e.cursorExit += OnMouseExit;
	e.onPressed += OnMousePressed;
	e.onReleased += OnMouseReleased;
	
	OnMouseReleased(e);
	
	void OnMousePressed(UIElement e)
	{
		e.uv = Vault.GetAsset<Texture2D>("ui").GetUVRegion(new RectInt(8, 1, 6, 5));
		((Button)e).label.position = new Vector2(-5, -1);
		Cursor.SetTexture(2);
	}
	
	void OnMouseReleased(UIElement e)
	{
		e.uv = Vault.GetAsset<Texture2D>("ui").GetUVRegion(new RectInt(1, 1, 6, 5));
		((Button)e).label.position = new Vector2(0, -1);
		Cursor.SetTexture(e.isCursorOver ? 1 : 0);
	}
	
	void OnMouseEnter(UIElement _) => Cursor.SetTexture(1);
	void OnMouseExit(UIElement _) => Cursor.SetTexture(0);
}