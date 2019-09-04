using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using GeonBit.UI.Entities.TextValidators;
using GeonBit.UI.Utils;
// using GeonBit UI elements
using GeonBit.UI;
using GeonBit.UI.Entities;

namespace Boris_Online
{
    /// This is the main class for your game.
    public class Game1 : Game
    {
        // graphics and spritebatch
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Button login;
    
        int scrw = 1000;
        int scrh = 700;
        Panel topPanel = new Panel();
		/// Game constructor.
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// Allows the game to perform any initialization it needs to before starting to run.
        /// here we create and init the UI manager.
        protected override void Initialize()
        {
            // GeonBit.UI: Init the UI manager using the "hd" built-in theme
            UserInterface.Initialize(Content, BuiltinThemes.hd);

            // GeonBit.UI: tbd create your GUI layouts here..

            spriteBatch = new SpriteBatch(GraphicsDevice);

            int _ScreenWidth = scrw; //graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            int _ScreenHeight = scrh; // graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            graphics.PreferredBackBufferWidth = (int)_ScreenWidth;
            graphics.PreferredBackBufferHeight = (int)_ScreenHeight;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            // call base initialize func
            //base.Initialize();

            Start();

        }

        protected void Start()
        {

            //version
            Label label = new Label("Version: 0.0.0.1",Anchor.BottomRight, offset: new Vector2(20, 20));
            UserInterface.Active.AddEntity(label);

            //panel
            topPanel = new Panel(new Vector2((scrw/4), ((scrh/14)*5+80) ), PanelSkin.Default, Anchor.Center);
            topPanel.Padding = Vector2.Zero;
            UserInterface.Active.AddEntity(topPanel);
            //login btn
            login = new Button("Login", ButtonSkin.Default, Anchor.TopCenter, new Vector2(((scrw/5)), scrh/14),new Vector2(0, 20));
            login.OnClick = (Entity btn) => {  this.Login(); };
            topPanel.AddChild(login);
            //register btn
            login = new Button("Register", ButtonSkin.Default, Anchor.TopCenter, new Vector2(((scrw / 5)), scrh / 14), new Vector2(0, (scrh / 14)+30) );
            login.OnClick = (Entity btn) => { this.Register(); };
            topPanel.AddChild(login);
            //options btn
            login = new Button("Options", ButtonSkin.Default, Anchor.TopCenter, new Vector2(((scrw / 5)), scrh / 14), new Vector2(0, (scrh / 14)*2 + 40));
            login.OnClick = (Entity btn) => { this.Option(); };
            topPanel.AddChild(login);
            //about btn
            login = new Button("About", ButtonSkin.Default, Anchor.TopCenter, new Vector2(((scrw / 5)), scrh / 14), new Vector2(0, (scrh / 14)*3 + 50));
            login.OnClick = (Entity btn) => { this.About(); };
            topPanel.AddChild(login);
            //about btn
            login = new Button("Exit", ButtonSkin.Default, Anchor.TopCenter, new Vector2(((scrw / 5)), scrh / 14), new Vector2(0, (scrh / 14)*4 + 60));
            login.OnClick = (Entity btn) => { this.Exit(); };
            topPanel.AddChild(login);
        }


        protected void Login()
        {
            topPanel.Visible = false;

           Panel temp = new Panel(new Vector2((scrw / 2), ((scrh / 2))), PanelSkin.Default, Anchor.Center);
            temp.Padding = Vector2.Zero;
            UserInterface.Active.AddEntity(temp);

            Label label = new Label("Email:", offset: new Vector2 (20,20));
            temp.AddChild(label);

            TextInput email = new TextInput(false, size: new Vector2(scrw / 2 - 40, (scrh / 12)), offset: new Vector2(20, 0));
            temp.AddChild(email);

            Label label2 = new Label("Password:", offset: new Vector2(20, 0));
            temp.AddChild(label2);

            TextInput pass = new TextInput(false, size: new Vector2(scrw/2-40,(scrh/12)) , offset: new Vector2(20, 0));
            pass.HideInputWithChar = '*';
            pass.CharactersLimit = 10;
            pass.Validators.Add(new SlugValidator());
            temp.AddChild(pass);

            CheckBox check = new CheckBox("Remember login details" ,offset: new Vector2(20, 0));
            temp.AddChild(check);
            //login
            login = new Button("Login", ButtonSkin.Default, Anchor.BottomLeft, new Vector2(((scrw / 5)), scrh / 14), new Vector2(20,  20));
            login.OnClick = (Entity btn) => {
                try
                {
                     new System.Net.Mail.MailAddress(email.Value);
                }
                catch
                {
                    GeonBit.UI.Utils.MessageBox.ShowMsgBox("Email invalid!", "Please make sure you entered the correct email.\nEmail format is: example@domain.com.");
                }
                if (pass.Value.Length < 6)
                {
                    GeonBit.UI.Utils.MessageBox.ShowMsgBox("Password invalid!", "Password is too short!\nIt must be equal or greater than 6 characters.");
                }
                this.Connect(email.Value,pass.Value); };
            temp.AddChild(login);
            //back
            login = new Button("Back", ButtonSkin.Default, Anchor.BottomRight, new Vector2(((scrw / 5)), scrh / 14), new Vector2(20,  20));
            login.OnClick = (Entity btn) => { temp.RemoveFromParent(); topPanel.Visible = true; };
            temp.AddChild(login);



            // topPanel.Visible = true;
            //return;
        }
        protected void Register()
        {
            topPanel.Visible = false;

            Panel temp = new Panel(new Vector2((scrw / 2+20), ((scrh / 5)*4)), PanelSkin.Default, Anchor.Center);
            temp.Padding = Vector2.Zero;
            UserInterface.Active.AddEntity(temp);

            Label label = new Label("Enter your Email:", offset: new Vector2(20, 20));
            temp.AddChild(label);

            TextInput email = new TextInput(false, size: new Vector2(scrw / 2 - 40, (scrh / 12)), offset: new Vector2(20, 0));
            temp.AddChild(email);

            Label label2 = new Label("Enter your Password:", offset: new Vector2(20, 0));
            temp.AddChild(label2);

            TextInput pass = new TextInput(false, size: new Vector2(scrw / 2 - 40, (scrh / 12)), offset: new Vector2(20, 0));
            pass.HideInputWithChar = '*';
            pass.CharactersLimit = 10;
            pass.Validators.Add(new SlugValidator());
            temp.AddChild(pass);

            Label label3 = new Label("Re-Enter your Password:", offset: new Vector2(20, 0));
            temp.AddChild(label3);

            TextInput pass2 = new TextInput(false, size: new Vector2(scrw / 2 - 40, (scrh / 12)), offset: new Vector2(20, 0));
            pass2.HideInputWithChar = '*';
            pass2.CharactersLimit = 10;
            pass2.Validators.Add(new SlugValidator());
            temp.AddChild(pass2);

           


            Label label4 = new Label("     Read the terms and conditions", offset: new Vector2(20, 10));
            Icon icon = new Icon(IconType.Scroll, Anchor.CenterLeft, offset: new Vector2(-30, 0));
            label4.AddChild(icon, false);
            icon.OnClick = (Entity btn) => { terms(); };
            temp.AddChild(label4);

           

            CheckBox check = new CheckBox("Agree to the terms and conditions", offset: new Vector2(20, 10));
            temp.AddChild(check);

            //login
            login = new Button("Register", ButtonSkin.Default, Anchor.BottomLeft, new Vector2(((scrw / 5)), scrh / 14), new Vector2(20, 20));
            login.OnClick = (Entity btn) => { if (pass.Value != pass2.Value)
                {
                    GeonBit.UI.Utils.MessageBox.ShowMsgBox("Password dones not match!", "Please make sure you entered the same password.");
                }
            if (pass2.Value.Length < 6)
                {
                    GeonBit.UI.Utils.MessageBox.ShowMsgBox("Password is too short!", "The password must be equal or greater than 6 characters.");
                }
            if (check.Checked == false)
                {
                    GeonBit.UI.Utils.MessageBox.ShowMsgBox("Info", "Please agree to the terms and conditions first.");
                }
                try
                {
                     new System.Net.Mail.MailAddress(email.Value);
                }
                catch
                {
                    GeonBit.UI.Utils.MessageBox.ShowMsgBox("Email invalid!", "Please make sure you entered the correct email.\nEmail format is: example@domain.com.");
                }
                this.Reg(email.Value, pass2.Value); };
            temp.AddChild(login);
            //back
            login = new Button("Back", ButtonSkin.Default, Anchor.BottomRight, new Vector2(((scrw / 5)), scrh / 14), new Vector2(20, 20));
            login.OnClick = (Entity btn) => { temp.RemoveFromParent(); topPanel.Visible = true; };
            temp.AddChild(login); return;
        }


        protected void terms()
        {
            Panel panel = new Panel(new Vector2(500, -1));
            UserInterface.Active.AddEntity(panel);
            panel.AddChild(new Header("Locked Text Input"));
            panel.AddChild(new HorizontalLine());

            // inliner
            panel.AddChild(new Paragraph("Please Read the terms and conditions carefully.\nViolating the rules will result in punishment."));
            TextInput textMulti = new TextInput(true, new Vector2(0, 370));
            textMulti.Locked = true;
            textMulti.TextParagraph.Scale = 0.6f;
            textMulti.Value = @"The Cleric, Priest..";
            panel.AddChild(textMulti);

            login = new Button("OK", ButtonSkin.Default, Anchor.BottomCenter, new Vector2(((scrw / 5)), scrh / 14), new Vector2(20, 20));
            login.OnClick = (Entity btn) => { panel.RemoveFromParent(); };
            panel.AddChild(login);
            return;
        }

        protected void Connect(string adr, string pass)
        {

            
            //login handler

            return;
        }

        protected void Reg(string email, string pass)
        {

            //reg handler
            return;
        }

        protected void Option()
        {

        }
        protected void About()
        {

        }
       
        /// LoadContent will be called once per game and is the place to load.
        /// here we init the spriteBatch (this is code you probably already have).
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// Allows the game to run logic such as updating the world.
        /// here we call the UI manager update() function to update the UI.
        protected override void Update(GameTime gameTime)
        {
            // GeonBit.UIL update UI manager
            UserInterface.Active.Update(gameTime);

            // tbd add your own update() stuff here..

            // call base update
            base.Update(gameTime);
        }

        /// This is called when the game should draw itself.
        /// here we call the UI manager draw() function to render the UI.
        protected override void Draw(GameTime gameTime)
        {
            // clear buffer
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // GeonBit.UI: draw UI using the spriteBatch you created above
            UserInterface.Active.Draw(spriteBatch);

            // call base draw function
            base.Draw(gameTime);


        }
    }
}