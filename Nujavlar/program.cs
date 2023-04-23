using System;
using Raylib_cs;
using System.Numerics;


Raylib.InitWindow(1024, 768, "game");

Raylib.SetTargetFPS(60);
1
PlayerClass player = new();

//player speed
float speed = 4.5f;
bool hit = false; //har skottet prickat eller inte

List<Bullet> bullets = new(); //skapar bullets
List<Enemy> enemies = new(); //skapar enemies
//färg
Color myColor = new Color(0, 200, 30, 255);
//väggar
Rectangle leftWall = new Rectangle(0, 0, 10, 768);
Rectangle rightWall = new Rectangle(1014, 0, 10, 768);
Rectangle topWall = new Rectangle(0, 0, 1024, 10);
Rectangle bottomWall = new Rectangle(0, 758, 1024, 10);

//scener
string currentscene = "start";


while (Raylib.WindowShouldClose() == false)
{
    if (currentscene == "start")
    { //startknapp
        if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
        {
            currentscene = "Stage1";
        }

    }


    if (currentscene == "Stage1")
    {
        hit = false;
        player.character.x = player.PlayerMovementX(player.character.x, speed);
        player.character.y = player.PlayerMovementY(player.character.y, speed);
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            bullets.Add(new Bullet(player));
        }
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_K))
        {
            enemies.Add(new Enemy(player));
        }
        foreach (var bullet in bullets)
        {
            bullet.ShootingMechanics();
        }
        foreach (var enemy in enemies)
        {
            enemy.MoveEnemy();
            if (Raylib.CheckCollisionRecs(player.character, enemy.rect))
            {
                currentscene = "END";
                break;
            }
            foreach (var bullet in bullets)
            {
                if (Raylib.CheckCollisionRecs(enemy.rect, bullet.bulletRec))
                {
                    enemies.Remove(enemy);//tar bort enemy och bullet och enemy blir prickad
                    bullets.Remove(bullet);
                    hit = true;
                    break;
                }
            }
            if (hit)
            {
                break;
            }
        }
    }

    if (currentscene == "END")
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_ESCAPE))
        {

        }
    }

    //GRAFIK FÖR FAAAAN
    if (currentscene == "start")
    {
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.DARKBLUE);

        Raylib.DrawRectangleRec(leftWall, Color.BLACK);
        Raylib.DrawRectangleRec(rightWall, Color.BLACK);
        Raylib.DrawRectangleRec(topWall, Color.BLACK);
        Raylib.DrawRectangleRec(bottomWall, Color.BLACK);

        Raylib.DrawText("Använd W,A,S,D för att röra dig I dem olika riktningarna", 212, 48, 22, Color.BLACK);
        Raylib.DrawText("Tryck på mellanslag för att skjuta ", 212, 100, 22, Color.BLACK);
        Raylib.DrawText("Tryck på Enter för att gå till spelet", 212, 130, 22, Color.BLACK);
    }

    else if (currentscene == "Stage1")
    {
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.WHITE);

        Raylib.DrawRectangleRec(leftWall, Color.BLACK);
        Raylib.DrawRectangleRec(rightWall, Color.BLACK);
        Raylib.DrawRectangleRec(topWall, Color.BLACK);
        Raylib.DrawRectangleRec(bottomWall, Color.BLACK);
        foreach (var bullet in bullets)
        {
            bullet.DrawBullet();
        }
        foreach (var enemy in enemies)
        {
            enemy.DrawEnemy();
        }
        player.DrawPlayer();
    }

    //END scene
    else if (currentscene == "END")
    {
        Raylib.DrawText("SORRY DU FÖRLORA", 312, 312, 42, Color.RED);
    }
    Raylib.EndDrawing();
}




