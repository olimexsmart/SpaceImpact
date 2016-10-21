using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace SpaceImpact
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SpaceImpactGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Spaceship spaceship;
        Wallpaper wallpaper;
        Stopwatch update;
        Dust[] dust;
        int GameHeight = 600;
        int GameWidth = 960;
        int DustIntensity = 10;

        public SpaceImpactGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = GameWidth;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = GameHeight;   // set this value to the desired height of your window
            graphics.ApplyChanges();

            SpaceComponents.GameWidth = GameWidth;
            SpaceComponents.GameHeight = GameHeight;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            spaceship = new Spaceship(spriteBatch, Content.Load<Texture2D>("spaceship"), 50, 50, 100, 100); //Avoid asteroids in initial position
            wallpaper = new Wallpaper(spriteBatch, Content.Load<Texture2D>("spaceTexture"));

            dust = new Dust[DustIntensity];

            int i;
            for (i = 0; i < 2; i++)          
                dust[i] = new Dust(spriteBatch, Content.Load<Texture2D>("dust1"));

            for (; i < 5; i++)
                dust[i] = new Dust(spriteBatch, Content.Load<Texture2D>("dust2"));

            for (; i < 7; i++)
                dust[i] = new Dust(spriteBatch, Content.Load<Texture2D>("dust3"));

            for (; i < dust.Length; i++)
                dust[i] = new Dust(spriteBatch, Content.Load<Texture2D>("dust4"));


            //Delta T between updates
            update = new Stopwatch();
            update.Start();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            long dT = update.ElapsedMilliseconds; //Time differential

            spaceship.Move(dT);

            foreach (Dust d in dust)
            {
                d.Move(dT);
            }    

            base.Update(gameTime);

            update.Restart();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            wallpaper.Draw();
            spaceship.Draw();
            foreach (Dust d in dust)
            {
                d.Draw();
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
