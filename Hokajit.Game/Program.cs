#if DEBUG
global using Ratelite.Debugs;
#endif

using Hokajit.Scenes;
using Ratelite;
using Ratelite.GO;
using Ratelite.Resources;
using Ratelite.Sounds;
using Ratelite.UI;
using Ratelite.UI.Widgets;

R.CreateGame("Hokajit")
 .SetIcon("assets/icon.png")
 .AddModule<SoundModule>()
 .AddModule<GOModule>()
 .AddModule<UIModule>()
#if DEBUG
 .AddModule<DebugModule>()
#endif
 .SetStartingScene<Splash>()
 .LoadingAssets(async progress =>
	 {
		 Cursor.AddTexture(
			 (await Vault.LoadResourceAsync<Texture2D>("textures/cursors/default.png")).AsRawImage()
		 );
		 Cursor.AddTexture(
			 (await Vault.LoadResourceAsync<Texture2D>("textures/cursors/pointer.png")).AsRawImage()
		 );
		 Cursor.AddTexture(
			 (await Vault.LoadResourceAsync<Texture2D>("textures/cursors/click.png")).AsRawImage(),
			 new Vector2Int(0, -2)
		 );
		 Cursor.AddTexture(
			 (await Vault.LoadResourceAsync<Texture2D>("textures/cursors/grab.png")).AsRawImage()
		 );
		 progress.Report(0.1F);
		 
		 var ui = await Vault.LoadResourceAsync<Texture2D>("textures/ui.png", "ui");
		 await Vault.LoadResourceAsync<BitmapFont>(
			 "fonts/ari-w9500--display.ttf",
			 "big.font",
			 new BitmapFont.Config(new Vector2Int(512), 64)
		 );
		 
		 await Vault.LoadResourceAsync<BitmapFont>(
			 "fonts/ari-w9500--bold.ttf",
			 "normal.font",
			 new BitmapFont.Config(new Vector2Int(512), 18)
		 );
		 Vault.AddAsset(
			 "button.mat", new MaterialUI(Vault.GetAsset<Shader>(UIModule.DEFAULT_SHADER))
						   .SetTexture(ui)
						   .SetNinePatch(new Region(3, 1, 2, 1), 5)
		 );
		 Vault.AddAsset(
			 "panel.mat", new MaterialUI(Vault.GetAsset<Shader>(UIModule.DEFAULT_SHADER))
						  .SetTexture(ui)
						  .SetNinePatch(new Region(3, 3, 1, 1), 5)
		 );
		 UIPrefab.Add<Button>(string.Empty, ButtonPrefab);
		 UIPrefab.Add<Button>("big", ButtonBigPrefab);
		 UIPrefab.Add<Label>(string.Empty, LabelPrefrab);
		 UIPrefab.Add<Label>("big", LabelBigPrefab);
		 UIPrefab.Add<Panel>(string.Empty, PanelPrefab);
		 UIPrefab.Add<Panel>("group", PanelGroupPrefab);
		 UIPrefab.Add<ElementButton>("icon", ElementButtonIconPrefab);
		 UIPrefab.Add<ScrollBar>(string.Empty, ScrollBarPrefab);
		 progress.Report(0.25F);
		 
		 const string unitsPath = "textures/units/";
		 await Vault.LoadResourceAsync<Texture2D>(unitsPath + "dratug.png", "units/dratug");
		 await Vault.LoadResourceAsync<Texture2D>(unitsPath + "mortiferi.png", "units/mortiferi");
		 await Vault.LoadResourceAsync<Texture2D>(unitsPath + "on-tek.png", "units/on-tek");
		 await Vault.LoadResourceAsync<Texture2D>(unitsPath + "orq.png", "units/orq");
		 progress.Report(0.5F);
		 
		 await Vault.LoadResourceAsync<Texture2D>("textures/purrvert.png", "purrvert");
		 progress.Report(0.75F);
		 
		 await Vault.LoadResourceAsync<Texture2D>("textures/characters/clan-sanlord.png");
		 await Vault.LoadResourceAsync<Texture2D>("textures/characters/clan-puuh.png");
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
	e.material = Vault.GetAsset<MaterialUI>("button.mat");
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
		e.uv = Vault.GetAsset<Texture2D>("ui").GetUVRegion(new RectInt(1, 7, 6, 5));
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


void ElementButtonIconPrefab(ElementButton e)
{
	ElementButton.DefaultPrefrab(e);
	e.size = new Vector2(48);
	e.cursorEnter += OnMouseEnterButton;
	e.cursorExit += OnMouseExitButton;
}

void PanelPrefab(Panel e)
{
	e.material = Vault.GetAsset<MaterialUI>("panel.mat");
	e.uv = Vault.GetAsset<Texture2D>("ui").GetUVRegion(new RectInt(9, 1, 6));
	e.mesh = Vault.GetAsset<Mesh>(UIModule.DEFAULT_MESH);
	e.size = new Vector2(300);
	e.padding = new Region(10);
}

void PanelGroupPrefab(Panel e)
{
	e.anchorMin = Vector2.zero;
	e.anchorMax = Vector2.one;
	e.padding = new Region(20);
	e.isInteractive = false;
}

void ScrollBarPrefab(ScrollBar e)
{
	e.mesh = Vault.GetAsset<Mesh>(UIModule.DEFAULT_MESH);
	e.material = Vault.GetAsset<MaterialUI>(UIModule.DEFAULT_MATERIAL);
	e.tint = new Color(0x41474F);
	e.cornerRadius = new Region(5);
	e.size = e.orientation == Orientation.Horizontal
			? new Vector2(150, 30)
			: new Vector2(30, 150);
	
	e.cursor.mesh = Vault.GetAsset<Mesh>(UIModule.DEFAULT_MESH);
	e.cursor.material = Vault.GetAsset<MaterialUI>(UIModule.DEFAULT_MATERIAL);
	e.cursor.anchorMin = Vector2.zero;
	e.cursor.anchorMax = Vector2.one;
	e.cursor.tint = new Color(0x26354A);
	e.cursor.cornerRadius = new Region(5);
}

void OnMouseEnterButton(UIElement _) => Cursor.SetTexture(1);
void OnMouseExitButton(UIElement _) => Cursor.SetTexture(0);