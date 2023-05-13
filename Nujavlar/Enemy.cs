using System;
using Raylib_cs;
using System.Numerics;


//"Public" används för att dem kan komma till användsning i andra delar av programmet. Annars hade Private används om det enbart skulle behövas inom denna cs som Vector2 i Enemy.cs
//eftersom att en del av den här koden är från Mickes workshop så kommer jag inte gå in på det. Jag förklarade också en lite bit i program.cs
//Random generatorn fungerar så att när en ny "Enemy" kallas för så kommer den att dyka upp mellan 1024, 768. Det kallas för en ny "Enemy" när K knappen trycks ned
//Public void MoveEnemy är Mickes kod. 
public class Enemy
{
    Random generator = new();
    PlayerClass player;
    public Rectangle rect;
    public Enemy(PlayerClass pExtern)
    {
        player = pExtern;
        rect = new Rectangle(generator.Next(1024), generator.Next(768), 50, 50);
        while (Raylib.CheckCollisionRecs(pExtern.character, rect))
        {
            rect.x = generator.Next(1024);
            rect.y = generator.Next(768);
        }
    }
    private Vector2 enemyMovement = new(1, 0);
    private float enemySpeed = 2.5f;

//MICKES KOD
    public void MoveEnemy()
    {
        Vector2 playerPos = new(player.character.x, player.character.y);
        Vector2 enemyPos = new(rect.x, rect.y);
        Vector2 diff = playerPos - enemyPos;
        Vector2 enemyDirection = Vector2.Normalize(diff);
        enemyMovement = enemyDirection * enemySpeed;
        rect.x += enemyMovement.X;
        rect.y += enemyMovement.Y;
    }

    public void DrawEnemy()
    {
        Raylib.DrawRectangle((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height, Color.RED);
    }
}
