using System;
using System.Numerics;
using Raylib_cs;

public class PlayerClass
{
    public Rectangle character = new(0, 60, 50, 50);
    public string direction = "right";
    public Texture2D avatarImage = Raylib.LoadTexture("jagare.png");
    public int directionInt = 1;
    public float PlayerMovementX(float characterx, float speed)
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            characterx += speed;
            direction = "right";
            directionInt = 1;
        }

        else if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            characterx -= speed;
            direction = "left";
            directionInt = -1;
        }
        return characterx;
    }
    public float PlayerMovementY(float charactery, float speed)
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            charactery += speed;
            direction = "down";
        }

        else if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            charactery -= speed;
            direction = "up";
        }
        return charactery;
    }

    public void DrawPlayer()
    {
        Raylib.DrawTextureRec(avatarImage, new Rectangle(0, 0, (50 * directionInt), 50), new Vector2(character.x, character.y), Color.WHITE);
    }
}