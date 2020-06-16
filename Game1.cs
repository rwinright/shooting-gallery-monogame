using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootingGallery
{
    
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D target_sprite;
        Texture2D crosshair_sprite;
        Texture2D background_sprite;

        SpriteFont game_font;

        Vector2 targetPosition = new Vector2(300, 300);
        const int TARGET_RADIUS = 45;
        float mouseTargetDist;

        float timer = 10f;

        MouseState mState;

        int score = 0;
        bool mReleased = true;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            target_sprite = Content.Load<Texture2D>("target");
            crosshair_sprite = Content.Load<Texture2D>("crosshairs");
            background_sprite = Content.Load<Texture2D>("sky");

            game_font = Content.Load<SpriteFont>("galleryFont");
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(timer > 0)
                timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

             
            // TODO: Add your update logic here
            mState = Mouse.GetState();
            mouseTargetDist = Vector2.Distance(targetPosition, new Vector2(mState.X, mState.Y));

            if(mState.LeftButton == ButtonState.Pressed && mReleased)
            {

                if(mouseTargetDist < TARGET_RADIUS && timer > 0)
                {
                    score++;

                    Random rand = new Random();

                    targetPosition.X = rand.Next(TARGET_RADIUS, graphics.PreferredBackBufferWidth - TARGET_RADIUS +1 );
                    targetPosition.Y = rand.Next(TARGET_RADIUS, graphics.PreferredBackBufferHeight - TARGET_RADIUS + 1);


                }

                mReleased = false;
            }

            if (mState.LeftButton == ButtonState.Released) mReleased = true;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
                        
            spriteBatch.Draw(background_sprite, new Vector2(0, 0), Color.White);

            if(timer > 0)
            {
                spriteBatch.Draw(target_sprite, new Vector2(targetPosition.X - TARGET_RADIUS, targetPosition.Y - TARGET_RADIUS), Color.White);
            }

            spriteBatch.DrawString(game_font, $"Score: {score}", new Vector2(3, 3), Color.White);

            spriteBatch.DrawString(game_font, $"Time: {Math.Ceiling(timer)}", new Vector2(3, 40), Color.White);

            spriteBatch.Draw(crosshair_sprite, new Vector2(mState.X - crosshair_sprite.Width/2, mState.Y - crosshair_sprite.Height/2), Color.White);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
