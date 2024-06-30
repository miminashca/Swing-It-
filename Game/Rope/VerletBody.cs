using System.Collections.Generic;
using System;
using GXPEngine;

public class VerletBody:GameObject{
    public List<VerletPoint> point;
    public List<VerletConstraint> constraint;
    public Vec2 position;
    public GameObject player;
    public VerletBody(GameObject pPlayer){
        point = new List<VerletPoint> ();
        constraint = new List<VerletConstraint> ();
        player = pPlayer;
        position = new Vec2(player.x, player.y);
    }

    public void AddPoint(VerletPoint newPoint) {
        point.Add(newPoint);
    }

    public void AddConstraint(int p1, int p2, float pStrength=1, bool pPush=true) {
        VerletConstraint c = new VerletConstraint (point [p1], point [p2],pStrength,pPush);
        constraint.Add (c);
    }

    public void AddAcceleration(Vec2 acceleration) {
        foreach (VerletPoint p in point) {
            p.acceleration += acceleration;
        }
    }

    public void UpdateVerlet() {
        foreach (VerletPoint p in point) {
            p.Step ();
        }
    }

    public void UpdateConstraints() {
        foreach (VerletConstraint c in constraint) {
            c.Apply ();
        }
    }

    public void Update()
    {
        x = position.x;
        y = position.y;
    }
}