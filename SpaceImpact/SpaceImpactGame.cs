using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections.Generic;

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
        Asteroid[] asteroid;
        IDrawable[] components; //Hold the components in their drawing order
        Texture2D mouseTexture;
        Texture2D bulletTexture;
        //Maybe this should stay in another class
        List<Bullet> flyingBullets;
        long fireRate = 250;
        long lastFire = 0;

        Vector2 mouse;
        int GameHeight = 600;
        int GameWidth = 960;
        int DustIntensity = 4;
        int AsteroidDensity = 25;

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
            SpaceComponents.SpriteBatch = spriteBatch;

            //Crating a unique container for these elements
            components = new IDrawable[2 + DustIntensity + AsteroidDensity];

            spaceship = new Spaceship(Content.Load<Texture2D>("spaceship")); //Avoid asteroids in initial position
            components[1] = spaceship;

            wallpaper = new Wallpaper(Content.Load<Texture2D>("spaceTexture"));
            components[0] = wallpaper;

            //Asteroids init
            int i;
            asteroid = new Asteroid[AsteroidDensity];
            Texture2D allFrames = Content.Load<Texture2D>("dynAsteroid");
            Texture2D[] frames = new Texture2D[64];
            for (i = 0; i < frames.Length; i++)
            {
                frames[i] = Crop(allFrames, new Rectangle(128 * (i % 8), 128 * (i / 8), 128, 128));
            }
            for (i = 0; i < asteroid.Length; i++)
            {
                asteroid[i] = new Asteroid(frames);
                components[i + 2] = asteroid[i];
            }


            //Dust nebulas init, TODO better this init part of the dust
            dust = new Dust[DustIntensity];
           
            //for (i = 0; i < 2; i++)          
            dust[0] = new Dust(Content.Load<Texture2D>("dust1"));

            //for (; i < 5; i++)
            dust[1] = new Dust(Content.Load<Texture2D>("dust2"));

            //for (; i < 7; i++)
            dust[2] = new Dust(Content.Load<Texture2D>("dust3"));

            //for (; i < dust.Length; i++)
            dust[3] = new Dust(Content.Load<Texture2D>("dust4"));
            i = 2 + AsteroidDensity;
            foreach (Dust d in dust)
            {
                components[i] = d;
                i++;
            }

            //Mouse pointer
            mouseTexture = Content.Load<Texture2D>("aim");
            mouse = new Vector2(0, 0);

            //Bullet texture
            bulletTexture = Crop(Content.Load<Texture2D>("bullet"), new Rectangle(177, 68, 18, 18));
            flyingBullets = new List<Bullet> { };

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
                d.Move(dT);

            foreach (Asteroid a in asteroid)
                a.Move(dT);            

            mouse.X = Mouse.GetState().X;
            mouse.Y = Mouse.GetState().Y;

            //Bullet management
            lastFire += dT;
            if (lastFire > fireRate) //Crate a new bullet
            {
                flyingBullets.Add(new Bullet(bulletTexture, spaceship.PosX, spaceship.PosY));
                lastFire = 0;
            }

            flyingBullets.RemoveAll(x => x.OutOfBounds); //MAMMA MIAAAAAAA SEE

            foreach (Bullet b in flyingBullets)            
                b.Move(dT);
            

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

            foreach (Asteroid a in asteroid)
                a.Draw();

            foreach (Bullet b in flyingBullets)
                b.Draw();

            foreach (Dust d in dust)
                d.Draw();
            

            //Mouse pointer draw
            spriteBatch.Draw(mouseTexture, new Rectangle((int)mouse.X - GameHeight / 20, (int)mouse.Y - GameHeight / 20, GameHeight / 10, GameHeight / 10), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private static Texture2D Crop(Texture2D source, Rectangle area)
        {
            if (source == null)
                return null;

            Texture2D cropped = new Texture2D(source.GraphicsDevice, area.Width, area.Height);
            Color[] data = new Color[source.Width * source.Height];
            Color[] cropData = new Color[cropped.Width * cropped.Height];

            source.GetData(data);

            int index = 0;
            for (int y = area.Y; y < area.Y + area.Height; y++)
            {
                for (int x = area.X; x < area.X + area.Width; x++)
                {
                    cropData[index] = data[x + (y * source.Width)];
                    index++;
                }
            }

            cropped.SetData(cropData);

            return cropped;
        }
    }
}
