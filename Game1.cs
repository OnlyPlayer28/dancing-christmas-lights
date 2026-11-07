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
        public Color color { get; private set; }
        public Vector2 position { get; set; }
        public Vector2 startingPos { get; private set; }
        public float elapsed { get; set; }

        public Particle(Vector2 position, float elapsed)
        {
            this.elapsed = elapsed;
            this.startingPos = position;
            color = new Color(Game1.random.Next(0, 256), Game1.random.Next(0, 256), Game1.random.Next(0, 256));
        }
    }
    public class Game1 : Game
    {
        public static Random random;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D point;

        private Matrix scaleMatrix;
        private const float scale = 10f;
        private float angularSpeed = 10;
        private float radius = 10;
        private float amplitude = 5;

        private Vector2 centerOfRotation = new Vector2(25, 25);

        private List<Particle> christmasLights = new List<Particle>();
        private int numberOfParticles = 250;
        public Game1()
        {
            random = new Random();
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
                christmasLights.Add(new Particle(centerOfRotation, i / 5f));
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }
        protected override void Update(GameTime gameTime)
        {
            foreach (var item in christmasLights)
            {
                item.elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

                item.position = new Vector2(item.startingPos.X + ((radius + (amplitude * MathF.Sin(item.elapsed))) * (float)Math.Cos(MathHelper.ToRadians(angularSpeed * item.elapsed))),
                                       item.startingPos.Y + ((radius + (amplitude * MathF.Sin(item.elapsed))) * (float)Math.Sin(MathHelper.ToRadians(angularSpeed * item.elapsed)))
                );
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix:scaleMatrix,samplerState:SamplerState.PointClamp);
            foreach (var item in christmasLights)
            {
                _spriteBatch.Draw(point, item.position, item.color);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
