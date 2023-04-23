using System;
using Raylib_cs;
using System.Numerics;

public class Enemy
{
    Random generator = new();
    PlayerClass player;
    public Rectangle rect;
    public Enemy(PlayerClass pExtern)
    {
        player = pExtern;
        rect = new Rectangle(generator.Next(1024),generator.Next(768),50,50);
        while (Raylib.CheckCollisionRecs(pExtern.character, rect))
        {
            rect.x = generator.Next(1024);
            rect.y = generator.Next(768);
        }
    }
    private Vector2 enemyMovement = new(1, 0);
    private float enemySpeed = 2.5f;
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
