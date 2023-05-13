using System;
using System.Numerics;
using Raylib_cs;

//"Public" används för att dem kan komma till användsning i andra delar av programmet. Annars hade Private används om det enbart skulle behövas inom denna cs som Vector2 i Enemy.cs
//En direktion string och direction int för vänster och höger alltså x=+/- men också upp och ned för då y=+/-
//String "direction" och directionINT används för att räkna ut vilket riktning som skottet som jägaren skjuter ska skjutas i anledningen till detta är för att göra skott riktningen senare enklare att räkna ut
//Det fungerar så att när players x,y kordinater ändrats posetivt eller negativt så ändras också direction. 
//alltså så krävs det bara att den går för x=100 till x=99 för att riktningen ska bli åt vänster. Samma för y axeln fast för dens kordinater. 
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