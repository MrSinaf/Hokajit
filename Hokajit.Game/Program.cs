#if DEBUG
global using Ratelite.Debugs;
#endif

using Hokajit;
using Hokajit.Objects;
using Hokajit.Scenes;
using Ratelite;
using Ratelite.GO;
using Ratelite.Resources;
using Ratelite.Sounds;
using Ratelite.UI;
using Ratelite.UI.Widgets;

#if DEBUG
RDebug.showMenuBar = false;
#endif

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
		 UIPrefab.Add<ElementButton>("icon", ElementButtonIconPrefab);
		 progress.Report(0.25F);
		 
		 var t = (await Vault.LoadResourceAsync<Texture2D>(
			 "textures/units/dratug.png", "units/dratug"
		 ))!;
		 DataManager.characters.AddRange(
			 new TokenData(
				 "Travailleur - Dratug", new Vector2(16), t, t.GetUVRegion(new RectInt(0, 0, 16))
			 ),
			 new TokenData(
				 "Guerrier - Dratug", new Vector2(16), t, t.GetUVRegion(new RectInt(16, 0, 16))
			 ),
			 new TokenData(
				 "Tireur - Dratug", new Vector2(16), t, t.GetUVRegion(new RectInt(32, 0, 16))
			 ),
			 new TokenData(
				 "Spécial - Dratug", new Vector2(16), t, t.GetUVRegion(new RectInt(48, 0, 16))
			 )
		 );
		 t = (await Vault.LoadResourceAsync<Texture2D>(
			 "textures/units/mortiferi.png", "units/mortiferi"
		 ))!;
		 DataManager.characters.AddRange(
			 new TokenData(
				 "Travailleur - Mortiferi", new Vector2(16), t,
				 t.GetUVRegion(new RectInt(0, 16, 16))
			 ),
			 new TokenData(
				 "Guerrier - Mortiferi", new Vector2(16), t, t.GetUVRegion(new RectInt(16, 16, 16))
			 ),
			 new TokenData(
				 "Tireur - Mortiferi", new Vector2(16), t, t.GetUVRegion(new RectInt(32, 16, 16))
			 ),
			 new TokenData(
				 "Spécial - Mortiferi", new Vector2(16, 32), t,
				 t.GetUVRegion(new RectInt(48, 0, 16, 32))
			 )
		 );
		 t = (await Vault.LoadResourceAsync<Texture2D>(
			 "textures/units/on-tek.png", "units/on-tek"
		 ))!;
		 DataManager.characters.AddRange(
			 new TokenData(
				 "Travailleur - On-Tek", new Vector2(16), t,
				 t.GetUVRegion(new RectInt(0, 16, 16))
			 ),
			 new TokenData(
				 "Guerrier - On-Tek", new Vector2(16, 32), t, 
				 t.GetUVRegion(new RectInt(16, 0, 16, 32))
			 ),
			 new TokenData(
				 "Tireur - On-Tek", new Vector2(16), t, t.GetUVRegion(new RectInt(32, 16, 16))
			 ),
			 new TokenData("Spécial - On-Tek", new Vector2(16, 32), t,
				 t.GetUVRegion(new RectInt(48, 0, 16, 32))
			 )
		 );
		 t = (await Vault.LoadResourceAsync<Texture2D>(
			 "textures/units/orq.png", "units/orq"
		 ))!;
		 DataManager.characters.AddRange(
			 new TokenData(
				 "Travailleur - Orq", new Vector2(16), t,
				 t.GetUVRegion(new RectInt(0, 16, 16))
			 ),
			 new TokenData(
				 "Guerrier - Orq", new Vector2(16, 32), t, t.GetUVRegion(new RectInt(16, 0, 16, 32))
			 ),
			 new TokenData(
				 "Tireur - Orq", new Vector2(16, 32), t, t.GetUVRegion(new RectInt(32, 0, 16, 32))
			 ),
			 new TokenData("Spécial - Orq", new Vector2(16), t,
				 t.GetUVRegion(new RectInt(48, 16, 16))
			 )
		 );
		 t = (await Vault.LoadResourceAsync<Texture2D>(
			 "textures/units/sarka.png", "units/sarka"
		 ))!;
		 DataManager.characters.AddRange(
			 new TokenData(
				 "Travailleur - Sarka", new Vector2(16), t,
				 t.GetUVRegion(new RectInt(0, 16, 16))
			 ),
			 new TokenData(
				 "Guerrier - Sarka", new Vector2(16, 32), t, t.GetUVRegion(new RectInt(16, 0, 16, 32))
			 ),
			 new TokenData(
				 "Tireur - Sarka", new Vector2(16), t, t.GetUVRegion(new RectInt(32, 16, 16))
			 ),
			 new TokenData("Spécial - Sarka", new Vector2(16, 32), t,
				 t.GetUVRegion(new RectInt(48, 0, 16, 32))
			 )
		 );
		 progress.Report(0.5F);
		 
		 await Vault.LoadResourceAsync<Texture2D>("textures/tiles.png", "tiles");
		 await Vault.LoadResourceAsync<Texture2D>("textures/purrvert.png", "purrvert");
		 progress.Report(0.75F);
		 
		 t = await Vault.LoadResourceAsync<Texture2D>("textures/characters/clan-sanlord.png");
		 DataManager.characters.AddRange(
			 new TokenData(
				 "Sanl'ord", new Vector2(16), t, t.GetUVRegion(new RectInt(0, 0, 16))
			 ),
			 new TokenData(
				 "Snixk, le geôlier", new Vector2(16), t, t.GetUVRegion(new RectInt(16, 0, 16))
			 ),
			 new TokenData(
				 "Garde royal sanlord", new Vector2(16), t, t.GetUVRegion(new RectInt(0, 16, 16))
			 ),
			 new TokenData(
				 "Conseillé de sanlord", new Vector2(16), t, t.GetUVRegion(new RectInt(16, 16, 16))
			 )
		 );
		 t = await Vault.LoadResourceAsync<Texture2D>("textures/characters/clan-puuh.png");
		 DataManager.characters.AddRange(
			 new TokenData(
				 "Urh l'puant", new Vector2(16, 32), t, t.GetUVRegion(new RectInt(0, 0, 16, 32))
			 ),
			 new TokenData(
				 "Krok", new Vector2(16, 32), t, t.GetUVRegion(new RectInt(16, 0, 16, 32))
			 ),
			 new TokenData(
				 "Jigur", new Vector2(16), t, t.GetUVRegion(new RectInt(32, 16, 16))
			 ),
			 new TokenData(
				 "Passin-Ra", new Vector2(16), t, t.GetUVRegion(new RectInt(48, 16, 16))
			 )
		 );
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

void OnMouseEnterButton(UIElement _) => Cursor.SetTexture(1);
void OnMouseExitButton(UIElement _) => Cursor.SetTexture(0);