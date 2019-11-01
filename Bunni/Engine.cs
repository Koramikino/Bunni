﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Bunni.Resources.Modules;
using Bunni.Resources.Components;
using System;
using Bunni.Resources.Components.Collision;


//TODO:
    //check to see if entity already has component
    //hitboxes
        //box hitbox (done)
        //layers (done)
        //tags (done)
        //more hitbox types
    //Animator
        //animation atlas
        //animation track ?
    //physics
    //camera
    //particles
    //render
        //render layer

namespace Bunni
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Engine : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public player1 player;
        public Scene scene1;
        public Entity hitBox = new Entity();


        public Engine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Camera.Init(new Vector2(0, 0), graphics, 800, 480);
            Camera.UpdateWindow(1920, 1080);

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;
            // TODO: Add your initialization logic here
            hitBox.Tag = BniTypes.Tag.Player;
            Texture2D tex = Content.Load<Texture2D>("img");
            Texture2D rainbowAnimationTex = Content.Load<Texture2D>("rainbowbox");
            player = new player1(rainbowAnimationTex);
            Animation rainbowAnimation = new Animation(rainbowAnimationTex, 8);
            player.AddComponent(rainbowAnimation);
            rainbowAnimation.Loop = true;
            scene1 = new Scene();
            PositionVector nPsV = new PositionVector();
            Render nRen = new Render(tex);
            nPsV.Position = new Vector2(400, 200);
            Collider nHitbox = new Collider();
            nHitbox.CreateHitbox<BoxCollider>();
            nHitbox.CollisionLayer = BniTypes.CollisionLayer.Foreground;
            hitBox.AddComponent(nPsV);
            hitBox.AddComponent(nRen);
            hitBox.AddComponent(nHitbox);

            scene1.AddEntity(hitBox);
            scene1.AddEntity(player);
            rainbowAnimation.Play();
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

            // TODO: use this.Content to load your game content here
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

            scene1.PreUpdate(gameTime, scene1);

            if (player.GetComponent<Collider>().IntersectsWithTag(hitBox.GetComponent<Collider>()))
            {
                player.GetComponent<Render>().Color = Color.Red;
            }else
            {
                player.GetComponent<Render>().Color = Color.White;
            }



            scene1.Update(gameTime, scene1);
            scene1.PostUpdate(gameTime, scene1);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Camera.TransformMatrix());
            scene1.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}