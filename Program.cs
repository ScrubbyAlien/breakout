using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace breakout;

internal static class Program
{
    public const int ScreenWidth = 500;
    public const int ScreenHeight = 700;
    
    public static void Main(string[] args)
    {
        using var window = new RenderWindow(new VideoMode(ScreenWidth, ScreenHeight), "breakout");
        window.Closed += (o, e) => window.Close();
        
        Clock clock = new Clock();
        Ball ball = new Ball();
        Paddle paddle = new Paddle();
        
        while (window.IsOpen)
        {
            float deltaTime = clock.Restart().AsSeconds();
            window.DispatchEvents();
            
            // update objects
            ball.Update(deltaTime);
            paddle.Update(ball, deltaTime);    
            
            window.Clear(new Color(131, 197, 235));
            // draw objects
            ball.Draw(window);
            paddle.Draw(window);
            
            
            
            window.Display();
        }
    }
}    



