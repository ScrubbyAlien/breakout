using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace breakout;

public class Paddle
{
    public const float Width = 100f;
    public const float Height = 25f;
    public const float Speed = 300f;
    
    public Sprite Sprite;
    public Vector2f size;

    public Paddle()
    {
        Sprite = new Sprite();
        Sprite.Texture = new Texture("assets/paddle.png");
        Sprite.Position = new Vector2f(Program.ScreenWidth / 2f, Program.ScreenHeight - 35);
        
        Vector2f paddleTextureSize = (Vector2f)Sprite.Texture.Size;
        Sprite.Origin = 0.5f  * paddleTextureSize;
        Sprite.Scale = new Vector2f(
            Width / paddleTextureSize.X,
            Height / paddleTextureSize.Y);

        size = new Vector2f(
            Sprite.GetGlobalBounds().Width,
            Sprite.GetGlobalBounds().Height);
    }

    public void Update(Ball ball, float deltaTime)
    {
        Vector2f newPos = Sprite.Position;
        if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
        {
            newPos.X += Speed * deltaTime;
        }
        if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
        {
            newPos.X -= Speed * deltaTime;
        }

        if (newPos.X + Width / 2f > Program.ScreenWidth)
        {
            newPos.X = Program.ScreenWidth - Width / 2f;
        } 
        else if (newPos.X - Width / 2f < 0)
        {
            newPos.X = 0 + Width / 2f;
        }

        if (Collision.CircleRectangle(
                ball.Sprite.Position, Ball.Radius, 
                Sprite.Position, size, out Vector2f hit))
        {
            ball.Sprite.Position += hit;
            ball.Reflect(hit.Normalized());
        }
        
        Sprite.Position = newPos;
    }

    public void Draw(RenderTarget target)
    {
        target.Draw(Sprite);
    }
}