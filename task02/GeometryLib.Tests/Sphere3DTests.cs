namespace GeometryLib.Tests;
public class Sphere3DTests
{
    [Theory]
    [MemberData(nameof(PointsData))]
    public void Calculate_points_distance(Point3D a, Point3D b, double expected)
    {
        Assert.Equal(expected, a.DistanceTo(b));
        Assert.Equal(expected, b.DistanceTo(a));
    }

    public static TheoryData<Point3D, Point3D, double> PointsData()
    {
        return new TheoryData<Point3D, Point3D, double>
        {
            { new Point3D(3, 4, 0), new Point3D(0, 0, 0), 5 },
            { new Point3D(1, 2, 3), new Point3D(4, 6, 8), Math.Sqrt(50) },
            { new Point3D(-1, -2, -3), new Point3D(2, 3, 4), Math.Sqrt(83) },
        };
    }

    [Fact]
    public void Properties_test()
    {
        Sphere3D sphere = new Sphere3D(new Point3D(0, 0, 0), 2);
        Assert.Equal(sphere.Diameter(), 4);
        Assert.Equal(sphere.Area(), 16 * Math.PI);
        Assert.Equal(sphere.Volume(), (4 / 3) * Math.PI * 8);
    }

    public static TheoryData<Sphere3D, Point3D, double> DistanceToPointData()
    {
        return new TheoryData<Sphere3D, Point3D, double>
        {
            { new Sphere3D(new Point3D(0, 0, 0), 2), new Point3D(1, 0, 0), 1 },
            { new Sphere3D(new Point3D(0, 0, 0), 2), new Point3D(2, 0, 0), 0 },
            { new Sphere3D(new Point3D(0, 0, 0), 2), new Point3D(5, 0, 0), 3 },
        };
    }

    [Theory]
    [MemberData(nameof(DistanceToPointData))]
    public void Distance_to_point_test(Sphere3D sphere, Point3D point, double expected)
    {
        Assert.Equal(sphere.DistanceTo(point), expected);
    }

    public static TheoryData<Sphere3D, Point3D, bool> ContainsPointData()
    {
        return new TheoryData<Sphere3D, Point3D, bool>
        {
            { new Sphere3D(new Point3D(0, 0, 0), 2), new Point3D(1, 0, 0), true },
            { new Sphere3D(new Point3D(0, 0, 0), 2), new Point3D(2, 0, 0), false },
            { new Sphere3D(new Point3D(0, 0, 0), 2), new Point3D(5, 0, 0), false },
            { new Sphere3D(new Point3D(0, 0, 0), 2), new Point3D(0, 0, 0), true },
        };
    }

    [Theory]
    [MemberData(nameof(ContainsPointData))]
    public void Contains_point_test(Sphere3D sphere, Point3D point, bool expected)
    {
        Assert.Equal(sphere.Contains(point), expected);
    }

    public static TheoryData<Sphere3D, Sphere3D, double> DistanceToSphereData()
    {
        return new TheoryData<Sphere3D, Sphere3D, double>
        {
            { new Sphere3D(new Point3D(0, 0, 0), 2), new Sphere3D(new Point3D(5, 0, 0), 1), 2 },
            { new Sphere3D(new Point3D(0, 0, 0), 2), new Sphere3D(new Point3D(4, 0, 0), 2), 0 },
            { new Sphere3D(new Point3D(0, 0, 0), 3), new Sphere3D(new Point3D(4, 0, 0), 2), 0 },
        };
    }

    [Theory]
    [MemberData(nameof(DistanceToSphereData))]
    public void Distance_to_sphere_test(Sphere3D sphere1, Sphere3D sphere2, double expected)
    {
        Assert.Equal(sphere1.DistanceTo(sphere2), expected);
    }

    public static TheoryData<Sphere3D, Sphere3D, bool> IntersectsWithData()
    {
        return new TheoryData<Sphere3D, Sphere3D, bool>
        {
            { new Sphere3D(new Point3D(0, 0, 0), 2), new Sphere3D(new Point3D(5, 0, 0), 1), false },
            { new Sphere3D(new Point3D(0, 0, 0), 2), new Sphere3D(new Point3D(4, 0, 0), 2), false },
            { new Sphere3D(new Point3D(0, 0, 0), 3), new Sphere3D(new Point3D(4, 0, 0), 2), true },
            { new Sphere3D(new Point3D(0, 0, 0), 3), new Sphere3D(new Point3D(2, 0, 0), 1), false },
        };
    }

    [Theory]
    [MemberData(nameof(IntersectsWithData))]
    public void Intersects_with_test(Sphere3D sphere1, Sphere3D sphere2, bool expected)
    {
        Assert.Equal(sphere1.IntersectsWith(sphere2), expected);
    }

    public static TheoryData<Sphere3D, Sphere3D, bool> ContainsSphereData()
    {
        return new TheoryData<Sphere3D, Sphere3D, bool>
        {
            { new Sphere3D(new Point3D(0, 0, 0), 3), new Sphere3D(new Point3D(2, 0, 0), 1), true },
            { new Sphere3D(new Point3D(0, 0, 0), 2), new Sphere3D(new Point3D(0, 0, 0), 2), true },
            { new Sphere3D(new Point3D(0, 0, 0), 3), new Sphere3D(new Point3D(2, 0, 0), 2), false },
            { new Sphere3D(new Point3D(0, 0, 0), 3), new Sphere3D(new Point3D(5, 0, 0), 1), false },
            { new Sphere3D(new Point3D(0, 0, 0), 3), new Sphere3D(new Point3D(0, 0, 0), 5), false },
            { new Sphere3D(new Point3D(0, 0, 0), 3), new Sphere3D(new Point3D(0, 0, 0), 2), true },
        };
    }

    [Theory]
    [MemberData(nameof(ContainsSphereData))]
    public void Contains_sphere_test(Sphere3D sphere1, Sphere3D sphere2, bool expected)
    {
        Assert.Equal(sphere1.Contains(sphere2), expected);
    }

    [Fact]
    public void Zero_radius_test()
    {
        Assert.Throws<ArgumentException>(() => new Sphere3D(new Point3D(0, 0, 0), 0));
    }

    [Fact]
    public void Negative_radius_test()
    {
        Assert.Throws<ArgumentException>(() => new Sphere3D(new Point3D(0, 0, 0), -1));
    }

    [Fact]
    public void Min_radius_test()
    {
        Sphere3D sphere = new Sphere3D(new Point3D(0, 0, 0), double.Epsilon);
        Assert.True(sphere.GetRadius() > 0);
    }

    [Fact]
    public void Max_radius_test()
    {
        Sphere3D sphere = new Sphere3D(new Point3D(0, 0, 0), double.MaxValue);
        Assert.True(sphere.GetRadius() == double.MaxValue);
    }
}