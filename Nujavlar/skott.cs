using System;
using Raylib_cs;
using System.Numerics;


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
