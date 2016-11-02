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
        SpriteFont spriteFont;
        Spaceship spaceship;
        Wallpaper wallpaper;
        Stopwatch update;
        Dust[] dust;
        Asteroid[] asteroid;
        //IDrawable[] components; //Hold the components in their drawing order
        MousePointer mousePointer;
        BulletManager bulletManager;
        CollisionManager collisionManager;

        int GameHeight = 600;
        int GameWidth = 960;
        //int DustIntensity = 4;
        //Only even numbers or one is left null
        int AsteroidDensity = 26;

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

            SpaceComponents.WindowWidth = GameWidth;
            SpaceComponents.WindowHeight = GameHeight;

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
            spriteFont = Content.Load<SpriteFont>("FontArial");

            //Crating a unique container for these elements
            //components = new IDrawable[2 + DustIntensity + AsteroidDensity];

            spaceship = new Spaceship(Content.Load<Texture2D>("spaceship")); //Avoid asteroids in initial position
            //components[1] = spaceship;

            wallpaper = new Wallpaper(Content.Load<Texture2D>("spaceTexture"));
            //components[0] = wallpaper;

            int i;
            //Asteroids init, there are two series of frames, loading them in equal numbers
            asteroid = new Asteroid[AsteroidDensity];
            Texture2D allFrames = Content.Load<Texture2D>("dynAsteroid");
            Texture2D[] framesA = new Texture2D[31];
            Texture2D[] framesB = new Texture2D[31];            
            for (i = 32; i < framesA.Length + 32; i++)
            {
                framesA[i - 32] = Crop(allFrames, new Rectangle(128 * (i % 8), 128 * (i / 8), 128, 128));
            }
            for (i = 0; i < framesB.Length; i++)
            {
                framesB[i] = Crop(allFrames, new Rectangle(128 * (i % 8), 128 * (i / 8), 128, 128));
            }
            for (i = 0; i < asteroid.Length / 2; i++)
            {
                asteroid[i] = new Asteroid(framesA);
                asteroid[i + asteroid.Length / 2] = new Asteroid(framesB);
            }
            
            
            //Dust nebulas init, TODO better this init part of the dust
            dust = new Dust[4];                       
            dust[0] = new Dust(Content.Load<Texture2D>("dust1"));           
            dust[1] = new Dust(Content.Load<Texture2D>("dust2"));
            dust[2] = new Dust(Content.Load<Texture2D>("dust3"));
            dust[3] = new Dust(Content.Load<Texture2D>("dust4"));            
            
            //Mouse pointer
            mousePointer = new MousePointer(spriteBatch, Content.Load<Texture2D>("aim"), SpaceComponents.WindowHeight, SpaceComponents.WindowWidth);

            //Bullet texture
            bulletManager = new BulletManager(spaceship, Crop(Content.Load<Texture2D>("bullet"), new Rectangle(177, 68, 18, 18)));

            //Collision manager loading
            collisionManager = new CollisionManager();
            collisionManager.AddBulletManager(bulletManager);
            collisionManager.AddCollidableCollection(asteroid);
            collisionManager.AddCollidable(spaceship);

            //Explosion frames loading
            Explosion.spriteBatch = spriteBatch;
            Explosion.frames = new Texture2D[48];                        
            allFrames = Content.Load<Texture2D>("explosion");
            for (i = 0; i < Explosion.frames.Length; i++)
            {           
                Explosion.frames[i] = Crop(allFrames, new Rectangle(256 * (i % 8), 256 * (i / 8), 256, 256));
            }
                        
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
            bulletManager.MoveBullets(dT);
            mousePointer.MoveMouse();
            foreach (Dust d in dust)           
                d.Move(dT);
            foreach (Asteroid a in asteroid)
                a.Move(dT);

            collisionManager.DetectCollisions();

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
            bulletManager.DrawBullets();            
            foreach (Asteroid a in asteroid)
                a.Draw();
            foreach (Dust d in dust)
                d.Draw();            

            //Mouse pointer draw
            mousePointer.Draw();

            //Text outuput
            spriteBatch.DrawString(spriteFont, "Mouse positon: " + MousePointer.MousePositionX + ", " + MousePointer.MousePositionY, new Vector2(0, 0), Color.Red);
            
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
