using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace breakout;

internal static class Program
{
    public const int ScreenWidth = 500;
    public const int ScreenHeight = 700;

    public static bool GameOver;
    
    public static void Main(string[] args)
    {
        using var window = new RenderWindow(new VideoMode(ScreenWidth, ScreenHeight), "breakout");
        window.Closed += (o, e) => window.Close();
        
        
        Clock clock = new Clock();
        Ball ball = new Ball();
        Paddle paddle = new Paddle();
        Tiles tiles = new Tiles();
        
        while (window.IsOpen)
        {
            float deltaTime = clock.Restart().AsSeconds();
            window.DispatchEvents();
            
            // update objects
            ball.Update(deltaTime);
            paddle.Update(ball, deltaTime);
            tiles.Update(ball, deltaTime);
            
            
            
            
            window.Clear(new Color(131, 197, 235));
            // draw objects
            paddle.Draw(window);
            tiles.Draw(window);
            ball.Draw(window);
            
            if (ball.Health <= 0) GameOver = true; 
            if (GameOver)
            {
                Text finalScore = new Text();
                finalScore.Font = new Font("assets/future.ttf");
                finalScore.CharacterSize = 24;
                finalScore.DisplayedString = $"FINAL SCORE: {ball.Score}\nPress space to play again";
                finalScore.Position = new Vector2f(
                    (ScreenWidth - finalScore.GetGlobalBounds().Width) / 2,
                    (ScreenHeight - finalScore.GetGlobalBounds().Height) / 2);
                window.Draw(finalScore);
                window.Display();
                
                while (window.IsOpen) // wait for player input
                {
                    window.DispatchEvents(); // call event handler for window closer if user closes window
                    clock.Restart().AsSeconds(); // restart clock to not mess up deltaTime
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                    {
                        GameOver = false;
                        break;
                    }
                }
                
                ball.Reset();
                paddle.Reset();
                tiles.Reset();
                
                // Also reset the tiles.
            }
            
            
            window.Display();
        }
    }
}    



