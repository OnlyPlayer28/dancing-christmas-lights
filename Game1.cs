using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace christmas
{
    public class Particle
    {
        public Vector2 position { get; set; }
        public Vector2 startingPos { get; private set; }
        public float elapsed { get; set; }

        public Particle(Vector2 position, float elapsed)
        {
            this.elapsed = elapsed;
            this.startingPos = position;
        }
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D point;
        private Vector2 position;

        private Matrix scaleMatrix;
        private const float scale = 10f;
        private float angularSpeed = 10;
        private float radius = 10;
        private float amplitude = 5;

        private Vector2 centerOfRotation = new Vector2(25, 25);

        private List<Particle> christmasLights = new List<Particle>();
        private int numberOfParticles = 15;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            point = new Texture2D(GraphicsDevice, 1, 1);
            point.SetData<Color>(new Color[] { Color.White });

            scaleMatrix = new Matrix();
            scaleMatrix = Matrix.CreateScale(scale);

            for (int i = 0; i < numberOfParticles; i++)
            {
                christmasLights.Add(new Particle())
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }
        private float elapsed = 0f;
        protected override void Update(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds*5;

            /*position=new Vector2(25 + elapsed, 25+(10* MathF.Sin(elapsed)));*/

            position = new Vector2(25  + ((radius+ (amplitude * MathF.Sin(elapsed))) *(float)Math.Cos(MathHelper.ToRadians(angularSpeed*elapsed))), 
                                   25  + ((radius + (amplitude * MathF.Sin(elapsed))) * (float)Math.Sin(MathHelper.ToRadians(angularSpeed * elapsed)))
            );

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix:scaleMatrix,samplerState:SamplerState.PointClamp);
            _spriteBatch.Draw(point, position, Color.Red);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
