using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indy500.SceneManagement
{
    public abstract class Control
    {
        public abstract Vector2 DesiredSize { get; }
        public virtual Rectangle Bounds { get; set; }
        public abstract void Draw(SpriteBatch spriteBatch);
    }

    public abstract class Container : Control
    {
        private List<Control> children = new List<Control>();
        public IReadOnlyList<Control> Children => children;
        public virtual void AddChild(Control control)
        {
            children.Add(control);
        }
    }

    public class StackPanel : Container
    {
        public override Vector2 DesiredSize => new Vector2(Children.Max(c => c.DesiredSize.X), Children.Sum(c => c.DesiredSize.Y));

        public override void AddChild(Control control)
        {
            var currentHeight = Children.Sum(c => c.DesiredSize.Y);
            base.AddChild(control);

            control.Bounds = new Rectangle(Bounds.X + (Bounds.Width - (int)control.DesiredSize.X) / 2, (int)currentHeight, (int)control.DesiredSize.X, (int)control.DesiredSize.Y); 
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Control child in Children)
                child.Draw(spriteBatch);
        }
    }

    public class Placeholder : Control
    {
        public override Vector2 DesiredSize => new Vector2(Width, Height);

        public int Height { get; }
        public int Width { get; }

        public Placeholder(int height, int width)
        {
            Height = height;
            Width = width;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }

    public class TextBlock : Control
    {
        public SpriteFont Font { get; }
        public string Text { get; }
        public Color Color { get; set; } = Color.White;

        public TextBlock(SpriteFont font, string text)
        {
            Font = font;
            Text = text;
        }
        public override Vector2 DesiredSize => Font.MeasureString(Text);

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Text, new Vector2(Bounds.X + Bounds.Width / 2 - DesiredSize.X / 2, Bounds.Y + Bounds.Height / 2 - DesiredSize.Y / 2), Color);
        }
    }

    public class Image : Control
    {
        public Texture2D Texture { get; }

        public Image(Texture2D texture)
        {
            Texture = texture;
        }

        public override Rectangle Bounds { get; set; }

        public override Vector2 DesiredSize => new Vector2(Texture.Width, Texture.Height);

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Bounds, Color.White);
        }
    }

    public class MenuItem : Control // Not super flexible right now...
    {
        private Texture2D leftMarker;
        private Texture2D rightMarker;
        private string text;
        private SpriteFont font;

        public bool Active { get; set; }
        public Color ActiveColor { get; set; }
        public Color InactiveColor { get; set; }

        public MenuItem(Texture2D leftMarker, Texture2D rightMarker, SpriteFont font, string text)
        {
            this.leftMarker = leftMarker;
            this.rightMarker = rightMarker;
            this.font = font;
            this.text = text;
        }
        public override Rectangle Bounds { get; set; }

        public override Vector2 DesiredSize => new Vector2(leftMarker.Width + 40 * 2 + rightMarker.Width + font.MeasureString(text).X, Math.Max(Math.Max(leftMarker.Height, rightMarker.Height), font.MeasureString(text).Y));

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(Active)spriteBatch.Draw(leftMarker, new Rectangle(Bounds.X, Bounds.Y + 10, leftMarker.Width, leftMarker.Height), Active ? ActiveColor : InactiveColor);
            spriteBatch.DrawString(font, text, new Vector2(Bounds.X + 40 + leftMarker.Width, Bounds.Y + Bounds.Height / 2 - DesiredSize.Y / 2), Active ? ActiveColor : InactiveColor);
            if(Active)spriteBatch.Draw(rightMarker, new Rectangle(Bounds.X + Bounds.Width - rightMarker.Width, Bounds.Y + 10, rightMarker.Width, rightMarker.Height), Active ? ActiveColor : InactiveColor);
        }
    }
}
