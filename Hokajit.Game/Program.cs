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
		 
		 await Vault.LoadAsyncResource<BitmapFont>(
			 "fonts/ari-w9500--display.ttf",
			 "big.font",
			 new BitmapFont.Config(new Vector2Int(512), 64)
		 );
		 
		 await Vault.LoadAsyncResource<BitmapFont>(
			 "fonts/ari-w9500--bold.ttf",
			 "normal.font",
			 new BitmapFont.Config(new Vector2Int(512), 18)
		 );
		 UIPrefab.Add<Button>(string.Empty, ButtonPrefab);
		 UIPrefab.Add<Button>("big", ButtonBigPrefab);
		 UIPrefab.Add<Label>(string.Empty, LabelPrefrab);
		 UIPrefab.Add<Label>("big", LabelBigPrefab);
		 progress.Report(0.5F);
		 
		 await Vault.LoadAsyncResource<Texture2D>("textures/purrvert.png", "purrvert");
		 await Vault.LoadAsyncResource<Texture2D>("textures/ui.png", "ui");
		 progress.Report(1);
	 }
 )
 .Run();

void LabelBigPrefab(Label e)
{
	var font = Vault.GetAsset<BitmapFont>("big.font")!;
	e.font = font.data;
	e.material = font.material;
}

void LabelPrefrab(Label e)
{
	var font = Vault.GetAsset<BitmapFont>("normal.font")!;
	e.font = font.data;
	e.material = font.material;
}

void ButtonBigPrefab(Button e)
{
	e.size = new Vector2(150, 64);
	
	UIPrefab.Apply("big", e.label);
	e.label.transform = Transform.Upper;
	e.label.pivotAndAnchors = new Vector2(0.5F);
	e.label.position = new Vector2(0, -10);
	
	e.cursorEnter += OnMouseEnterButton;
	e.cursorExit += OnMouseExitButton;
}

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
	
	e.cursorEnter += OnMouseEnterButton;
	e.cursorExit += OnMouseExitButton;
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
	
}

void OnMouseEnterButton(UIElement _) => Cursor.SetTexture(1);
void OnMouseExitButton(UIElement _) => Cursor.SetTexture(0);