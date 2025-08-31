namespace GeometryLib;

public class Sphere3D
{
    private Point3D _center;
    private double _radius;

    public Sphere3D(Point3D center, double radius)
    {
        this._center = center;
        if (radius > 0)
        {
            this._radius = radius;
        }
        else
        {
            throw new ArgumentException("The radius cannot be less than or equal to zero.");
        }
    }

    public Point3D GetCenter()
    {
        return _center;
    }

    public double GetRadius()
    {
        return _radius;
    }

    public double Diameter()
    {
        return (double)(_radius * 2); // D = 2R
    }

    public double Area()
    {
        return (double)(4 * Math.PI * Math.Pow(_radius, 2)); // S = 4ПR^2
    }

    public double Volume()
    {
        return (double)((4 / 3) * Math.PI * Math.Pow(_radius, 3)); // V = (4/3)ПR^3
    }

    public double DistanceTo(Point3D point)
    {
        if (!Contains(point))
        {
            return _center.DistanceTo(point) - _radius;
        }

        return _radius - _center.DistanceTo(point);
    }

    public double DistanceTo(Sphere3D sphere)
    {
        if (!IntersectsWith(sphere))
        {
            if (!Contains(sphere))
            {
                return (double)(_center.DistanceTo(sphere.GetCenter()) - _radius - sphere.GetRadius());
            }
            else if (Contains(sphere))
            {
                return (double)(_radius - _center.DistanceTo(sphere.GetCenter()) - sphere.GetRadius());
            }
        }

        return 0; // пересекаются или касаются
    }

    public bool Contains(Point3D point)
    {
        return _center.DistanceTo(point) < _radius;
    }

    public bool IntersectsWith(Sphere3D sphere)
    {
        return (_center.DistanceTo(sphere.GetCenter()) + sphere.GetRadius() > _radius)
                && (_center.DistanceTo(sphere.GetCenter()) < _radius + sphere.GetRadius()); // L(Center1, Center2) + R2 > R1 AND L(Center1, Center2) < R1 + R2
    }

    public bool Contains(Sphere3D sphere)
    {
        return _center.DistanceTo(sphere.GetCenter()) + sphere.GetRadius() <= _radius; // L(Center1, Center2) + R2 <= R1
    }
}

public struct Point3D
{
    double x, y, z;

    public Point3D(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public double DistanceTo(Point3D point)
    {
        return Math.Sqrt(Math.Pow(point.x - x, 2) + Math.Pow(point.y - y, 2) + Math.Pow(point.z - z, 2));
    }
}
