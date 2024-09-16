using SFML.Graphics;
using SFML.System;

namespace breakout;

public class Ball 
{
    public const float Diameter = 20f; 
    public const float Radius = Diameter * .5f;
    private const float Speed = 400f;


    private int _health = 3;
    private int _score = 0;
    public Text Gui;
    
    public Sprite Sprite;
    public Vector2f direction = new Vector2f(1, 1).Normalized();
    
    
    public Ball()
    {
        Sprite = new Sprite();
        Sprite.Texture = new Texture("assets/ball.png");
        Sprite.Position = new Vector2f(250, 300);

        Vector2f ballTextureSize = (Vector2f)Sprite.Texture.Size;
        Sprite.Origin = 0.5f  * ballTextureSize;
        Sprite.Scale = new Vector2f(
            Diameter / ballTextureSize.X,
            Diameter / ballTextureSize.Y);

        Gui = new Text();
        Gui.CharacterSize = 24;
        Gui.Font = new Font("assets/future.ttf");
        
    }
    

    public void Update(float deltaTime)
    {
        Vector2f newPos = Sprite.Position;
        newPos += direction * Speed * deltaTime;
        
        // check bounce
        if (newPos.X > Program.ScreenWidth - Radius) // bounce off right wall
        {
            newPos.X = Program.ScreenWidth - Radius;
            Reflect(new Vector2f(-1, 0));
        } 
        else if (newPos.X < 0 + Radius) // bounce off left wall 
        {
            newPos.X = 0 + Radius;
            Reflect(new Vector2f(1, 0));
        }
        if (newPos.Y < 0 + Radius) // bounce off ceiling
        {
            newPos.Y = 0 + Radius;
            Reflect(new Vector2f(0, 1));
        }
        
        if (newPos.Y > Program.ScreenHeight - Radius) // bounce off floor
        {
            _health -= 1;
            newPos = new Vector2f(250, 300);
            direction = new Vector2f(new Random().Next() % 2, 1).Normalized();
        }
        
        Sprite.Position = newPos;
        
    }

    public void Draw(RenderTarget target)
    {
        target.Draw(Sprite);

        Gui.DisplayedString = $"Health: {_health}";
        Gui.Position = new Vector2f(12, 8);
        target.Draw(Gui);

        Gui.DisplayedString = $"Score: {_score}";
        Gui.Position = new Vector2f(Program.ScreenWidth - Gui.GetGlobalBounds().Width - 12, 8);
        target.Draw(Gui);
    }

    public void Reflect(Vector2f normal)
    {
        direction -= normal * 2 * (direction.X * normal.X +
                                   direction.Y * normal.Y);
    }
    
    
    
}    


