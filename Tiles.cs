using System.Drawing;
using SFML.Graphics;
using SFML.System;

namespace breakout;

public class Tiles
{
    public Sprite Sprite;

    private string[] tileColorsPath =
    {
        "Blue",
        "Green",
        "Pink"
    };

    // collision is checked against every tile so total tiles should be small
    private const int TilesAcross = 5; 
    private const int TilesHigh = 4;
    
    private Vector2f[,] positions = new Vector2f[TilesAcross, TilesHigh];
    
    public const float Width = Program.ScreenWidth / TilesAcross;
    public const float Height = 30f;
    
    private Vector2f _size;
    
    public Tiles()
    {
        Sprite = new Sprite();
        Reset();
        
        Sprite.Texture = new Texture($"assets/tile{tileColorsPath[0]}.png");

        Vector2f spriteTextureSize = (Vector2f)Sprite.Texture.Size;
        Sprite.Scale = new Vector2f(
            Width / spriteTextureSize.X,
            Height / spriteTextureSize.Y);

        _size = new Vector2f(
            Sprite.GetGlobalBounds().Width,
            Sprite.GetGlobalBounds().Height);

    }

    public void Reset()
    {
        for (float i = 0; i < positions.GetLength(1); i++)
        {
            for (float j = 0; j < positions.GetLength(0); j++)
            {
                positions[(int)j, (int)i] = new Vector2f(j * Width, i * Height + 48);
            }
        }
    }

    public void Update(Ball ball, float deltaTime)
    {
        for (int i = 0; i < positions.GetLength(1); i++)
        {
            for (int j = 0; j < positions.GetLength(0); j++)
            {
                Vector2f pos = positions[j, i];

                if (pos.X >= 0) //don't collide with removed tiles
                {
                    Vector2f center = pos + new Vector2f(Width / 2, Height / 2);

                    if (Collision.CircleRectangle(ball.Sprite.Position, Ball.Radius, center, _size, out Vector2f hit))
                    {
                        ball.Sprite.Position += hit;
                        ball.Reflect(hit.Normalized());
                        positions[j, i] = new Vector2f(-1, -1);
                        ball.Score += 100 + ball.ScoreMultiplier * 100;
                        ball.ScoreMultiplier++;
                        i = 0;
                    }
                }
            }
            
            
        }
        bool isEmpty = true;
        for (int i = 0; i < positions.GetLength(1); i++)
        {
            for (int j = 0; j < positions.GetLength(0); j++)
            {
                if (positions[j, i].X >= 0) isEmpty = false;
            }
        }
        if (isEmpty) Program.GameOver = true;
    }

    public void Draw(RenderTarget target)
    {
        for (int i = 0; i < positions.GetLength(1); i++)
        {
            for (int j = 0; j < positions.GetLength(0); j++)
            {
                if (positions[j, i].X >= 0)
                {
                    Sprite.Position = positions[j, i];
                    Sprite.Texture = new Texture($"assets/tile{tileColorsPath[(j + i) % 3]}.png");
                    target.Draw(Sprite);
                }
            }
        }
    }
}