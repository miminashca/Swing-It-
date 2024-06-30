using System;
using System.Collections;
using GXPEngine;

class PointSprite : Sprite
{
    private VerletPoint point;
    private GameObject player;
    public PointSprite(VerletPoint p, GameObject pPlayer): base("Images/Rope_Beige.png", false, false)
    {
        point = p;
        player = pPlayer;
        // player = pPlayer;
        SetOrigin(width/2, height/2);
        // width = 13;
        // height = 13;
        
        x = point.x;
        y = point.y;
    }

    public void Update()
    {
        x = point.x-player.x;
        y = point.y-player.y;
    }
}