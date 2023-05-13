using System;
using Raylib_cs;
using System.Numerics;


//"Public" används för att dem kan komma till användning i andra delar av programmet. Annars hade Private används om det enbart skulle behövas inom denna cs som Vector2 i Enemy.cs

//hela Bullet classen fungerar som en mall för alla skott som kallas för i program.cs varhe skott har en storlek av 15,15 men har variabler som ändrar dens riktning 
//bullets kommer alltid att ha en hastighet av 8f vilket betyder att den rör sig 8gånger i en viss riktning per frame
//"bulletTrajectory" får sina varden utifrån varje case vilket är riktningen och "shootingMechanics" är egentligen bulletrektanglens position som uppdateras efter de värden bulletTrajectory har
//Varje case är utifrån vilket riktning player är i och hur värdena ändras utifrån varje case. EX +x = bulletspeed och -x = -bulletspeed.
//längst ned så ritas det ut en texture över bullet

public class Bullet
{
    PlayerClass player;
    public Rectangle bulletRec;
    private float bulletSpeed = 8f;
    private Vector2 bulletTrajectory;
    public Bullet(PlayerClass pExtern)
    {
        player = pExtern;
        if (pExtern.directionInt == 1)//vart skottet ska ritas ut beroende på karaktärens riktning
        {
            bulletRec = new Rectangle(pExtern.character.x + 50, (pExtern.character.y + 15), 15, 15);
        }
        else
        {
            bulletRec = new Rectangle((pExtern.character.x - 15), (pExtern.character.y + 15), 15, 15);
        }
        switch (pExtern.direction)
        {
            case "right":
                bulletTrajectory = new Vector2(bulletSpeed, 0); //om skottet är x posetivt
                break;
            case "left":
                bulletTrajectory = new Vector2(-bulletSpeed, 0); //om skottet är x negativt
                break;
            case "down":
                bulletTrajectory = new Vector2(0, bulletSpeed); //om skottet är y posetivt 
                break;
            case "up":
                bulletTrajectory = new Vector2(0, -bulletSpeed);
                break;
        }
    }
    public void ShootingMechanics()
    {
        bulletRec.x += bulletTrajectory.X;//vart skottet går beroende på x vectorn
        bulletRec.y += bulletTrajectory.Y;//vart skottet går veronde på y vectorn
    }

    public void DrawBullet()
    {
        Raylib.DrawTexture(Textures.bulletTexture, (int)bulletRec.x, (int)bulletRec.y, Color.WHITE);
    }
}
