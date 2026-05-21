using Ratelite;
using Ratelite.Resources;
using Ratelite.Sounds;
using Ratelite.UI;
using Ratelite.UI.Widgets;
using Ratelite.Utils;

namespace Hokajit.Scenes;

public class Splash : Scene
{
	private Canvas canvas = null!;
	private Image image = null!;
	
	private AudioSource audioSource = null!;
	private bool firstLaunch;
	
	public override void Init()
	{
		R.game.windowColor = new Color(0x212830);
		R.game.backgroundColor = new Color(0x151B23);
		
		Cursor.SetTexture(0);
		canvas = AddPlugin<Canvas>();
	}
	
	public override async Task Load()
	{
		// TODO > Ratelite ne supporte pas le fait que les meshes puissent être créé dans le
		// SplashWindow, donc pour le moment on fait le ici...
		 MainThread.Enqueue(() =>
		 {
			 Vault.AddAsset("(16:16).mesh", MeshFactory.CreateQuad(new Vector2(Game.TILE_SIZE)));
			 Vault.AddAsset(
				 "(16:32).mesh",
				 MeshFactory.CreateQuad(new Vector2(Game.TILE_SIZE, Game.TILE_SIZE * 2))
			 );
		 });
		 await MainThread.Wait();
		audioSource = new AudioSource
		{
			audio = await Vault.LoadResourceAsync<AudioClip>("sounds/purrvert.wav"),
			volume = 0.15F
		};
	}
	
	public override void Start()
	{
		canvas.root.AddChild(
			image = new Image(Vault.GetAsset<Texture2D>("purrvert")!)
			{
				pivotAndAnchors = new Vector2(0.5F),
				scale = new Vector2(6),
				opacity = 0
			}
		);
	}
	
	public override void Update()
	{
		if (image.opacity < 1)
			image.opacity += Time.delta * 0.5F;
		else
		{
			if (audioSource.offset < 0.5F)
			{
				image.position = new Vector2(Random.Shared.Next(-5, 5), Random.Shared.Next(-5, 5));
				image.scale += Vector2.one * Time.delta;
			}
			
			if (!firstLaunch)
			{
				firstLaunch = true;
				audioSource.Play();
			}
			else if (!audioSource.isPlaying)
			{
				Stage.Load(new Menu());
			}
		}
	}
}