using System;
using Raylib_cs;
using System.Numerics;


Raylib.InitWindow(1024, 768, "game");

Raylib.SetTargetFPS(60);

PlayerClass player = new();

//Här har jag en Bool(boolean) som har i uppgift att veta om skottet har prickat eller inte jag har också en float för karaktärens speed
//player speed
float speed = 4.5f;
bool hit = false; //har skottet prickat eller inte

List<Bullet> bullets = new(); //skapar bullets
List<Enemy> enemies = new(); //skapar enemies
//färg
Color myColor = new Color(0, 200, 30, 255);
//väggarna som sitter i katen på skärmen. Dock så stoppar dem inte spelaren från att gå utanför den synliga world border
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

    //Sålänge hit är false så kan spelaren röra på sig genom PlayerMovementX och PlayerMovementY. Dem är exakt som varandra bara att skillnaden är att direction ändras från left till right och -x,+x

    //------SKOTT MEKANIK------//
    //Själva skott mekaniken i spelet fungerar så att när space knappen trycks ned så kommer en instans av skott.cs skapas. Texten som förklarar hur allt fungerar finns i skott.cs men jag kan förklara den snabbt här
    //Space knappen trycks och då skapas det en "bullet". "bullet" har en satt texture,size och speed men riktningen kan ändras beroende på vilken sida playern är riktad åt
    //alltså om player är riktad åt vänster så ska skottet skjutas i -x från karaktärens riktning och om riktningen är nedåt så ska skottet skjtuas +y(posetiva y) från positionen karaktären stog i
    //"var bullet i bullets" kan man förklara enkalast såhär. Varje skott görs unikt beroende på dem ändrande variablerna inom skott.cs alltså så kommer två skott inte ha samma riktning och position om dem inte skjutits dirrekt efter varandra när player är stillaståendes. 
    //för att sammanfatta när space trycks ned så skapas en vector som en rect ska följa och den vectorn kan ändras beroende på om den är +x/-x, +y/-y

    //------Enemy------//
    //samma sak som med skott mekaniken så finns det en mall för exakt varje enemy. I den mallen ingår size,textur/utseende, hastighet
    //Det som ändras nu är att vi har en Random generator för hela skärmen alltså 1024, 768. Så barje gång K trycks ned så kommer en enemy att spawna någonstans mellan 1024, 768
    //"Enemy" fungerar så att dens x och y position ändras beroende på vart player är på skärmen. Om player är på höger sida så kommer "Enemy" x kordinat vara +x och om player är på vänster så är -x samma med y kordinaten fastupp och nedåt
    //Det skapas en vector mellan player och enemy som är längd 1 vilket gör att enemy rör sig med samma hastighet oavsätt vart player och enemy är. 
    //Enemy movement Vectorn är där för att Enemy ska röra sig med en viss hastighet varje frame.
    //(var enemy in enemies) fungerar som (var bullet in bullets) vilket egentligen är att varje buller och enemy blir sitt egna unika block och har sin egna instans(mall) av kod.

    //------Skott, player och Enemy------//
    //Enemy, Player och bullet är rects och om dem rectsens kordinater blir lika som varandra så händer något
    //Om bullet och Enemy kordinater blir lika som varandra så kommer Både bullet och enemy att blir borttagna. Alltså så har Enemy blivit skjuten och då försvinner kulan och Enemy är nu död. 
    //Men om Enemy och Players kordinater blir lika varandra så ska då hit=true och när hit är sann så ska END scenen spelas. Alltså så har du förlorat och kan spela spelet igen. 
    //Men hit kommer alltid att vara false tills Playerns x och y = Enemys x och y. Samma sak med bullet och Enemy, bullet kommer aldrig att tas bort förutom om den träffat en Enemy.
    //Det kan vara dåligt att skotten aldrig tars bort för om du spelat välldigt länge så tar det på prestandan om du har 1000 bullets som flyget iväg på skyhöga x och y positioner samtidigt. 



    if (currentscene == "Stage1")
    {
        hit = false;
        player.character.x = player.PlayerMovementX(player.character.x, speed);
        player.character.y = player.PlayerMovementY(player.character.y, speed);
        /*
        Karaktärens x position är lika med 
        */
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




