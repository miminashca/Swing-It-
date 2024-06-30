using System;
using GXPEngine; // Allows using Mathf functions

public struct Vec2
{
    public float x;
    public float y;

    public Vec2(float pX = 0, float pY = 0)
    {
        x = pX;
        y = pY;
    }

    public float Length()
    {
        return (float)Math.Sqrt(x * x + y * y);
    }

    public Vec2 Normalized()
    {
        return this / Length();
    }

    public void Normalize()
    {
        float l = Length();
        if (l > 0)
        {
            x /= l;
            y /= l;
        }
        else
        {
            Console.WriteLine("WARNING: trying to normalize zero vector!");
        }
    }

    public static float Deg2Rad(float deg)
    {
        return (deg * (Mathf.PI / 180));
    }

    public static float Rad2Deg(float rad)
    {
        return (rad * (180 / Mathf.PI));
    }

    public static Vec2 GetUnitVectorDeg(float deg)
    {
        float rad = Deg2Rad(deg);
        return GetUnitVectorRad(rad);
    }

    public static Vec2 GetUnitVectorRad(float rad)
    {
        return new Vec2(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    public static Vec2 RandomUnitVector()
    {
        return GetUnitVectorRad(Utils.Random(0, Mathf.PI * 2));
    }

    // Sets the angle of the current vector to the given angle (in degrees), without modifying the vector length
    public void SetAngleDegrees(float deg)
    {
        float rad = Deg2Rad(deg);
        SetAngleRadians(rad);
    }

    public void SetAngleRadians(float rad)
    {
        float l = Length();
        x = l * Mathf.Cos(rad);
        y = l * Mathf.Sin(rad);
    }

    public float GetAngleRadians()
    {
        return Mathf.Atan2(y, x);
    }

    public float GetAngleDegrees()
    {
        return Rad2Deg(Mathf.Atan2(y, x));
    }

    public void RotateDegrees(float deg)
    {
        float rad = Deg2Rad(deg);
        RotateRadians(rad);
    }

    public void RotateRadians(float rad)
    {
        float t = x * Mathf.Sin(rad) + y * Mathf.Cos(rad);
        x = x * Mathf.Cos(rad) - y * Mathf.Sin(rad);
        y = t;
    }

    public void RotateAroundDegrees(float deg, Vec2 point)
    {
        float rad = Deg2Rad(deg);
        RotateAroundRadians(rad, point);
    }
    public void RotateAroundRadians(float rad, Vec2 point)
    {
        this -= point;
        RotateRadians(rad);
        this += point;
    }

    public float Dot(Vec2 other)
    {
        return (x * other.x + y * other.y);
    }

    // Returns a unit normal, without modifying the vector
    public Vec2 Normal()
    {
        Vec2 unit = new Vec2(-y, x);
        return unit.Normalized();
    }

    public void Reflect(Vec2 pNormal, float pBounciness = 1)
    {
        Vec2 normal = pNormal.Normalized();
        this = this - (1 + pBounciness) * Dot(normal) * normal;
    }

    public void SetXY(float pX, float pY)
    {
        x = pX;
        y = pY;
    }

    public static Vec2 operator +(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x + right.x, left.y + right.y);
    }

    public static Vec2 operator -(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x - right.x, left.y - right.y);
    }

    public static Vec2 operator *(Vec2 left, float right)
    {
        return new Vec2(left.x * right, left.y * right);
    }

    public static Vec2 operator *(float right, Vec2 left)
    {
        return new Vec2(left.x * right, left.y * right);
    }

    public static Vec2 operator /(Vec2 left, float right)
    {
        return new Vec2(left.x / right, left.y / right);
    }

    public override string ToString()
    {
        return String.Format("({0},{1})", x, y);
    }
}