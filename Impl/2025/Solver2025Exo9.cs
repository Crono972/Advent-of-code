using System.Numerics;
using AdventOfCode.Shared;

public class Solver2025Exo9 : ISolver
{
    record Rectangle(long top, long left, long bottom, long right);
    IEnumerable<Rectangle> RectanglesOrderedByArea(Complex[] points) =>
        points.SelectMany(p1 => points, (p1, p2) => new { p1, p2 })
            .Select(pts =>  RectangleFromPoints(pts.p1, pts.p2) )
            .OrderByDescending(Area)
            .Select(r => r);

    IEnumerable<Rectangle> Boundary(Complex[] corners) =>
        from pair in corners.Zip(corners.Prepend(corners.Last()))
        select RectangleFromPoints(pair.First, pair.Second);

    Rectangle RectangleFromPoints(Complex p1, Complex p2)
    {
        var top = Math.Min(p1.Imaginary, p2.Imaginary);
        var bottom = Math.Max(p1.Imaginary, p2.Imaginary);
        var left = Math.Min(p1.Real, p2.Real);
        var right = Math.Max(p1.Real, p2.Real);
        return new Rectangle((long)top, (long)left, (long)bottom, (long)right);
    }

    Complex[] Parse(string[] lines) => lines
        .Select(line => new { line, parts = line.Split(",").Select(int.Parse).ToArray() })
        .Select(@t => @t.parts[0] + Complex.ImaginaryOne * @t.parts[1]).ToArray();

    long Area(Rectangle r) => (r.bottom - r.top + 1) * (r.right - r.left + 1);

    // see https://kishimotostudios.com/articles/aabb_collision/
    bool AabbCollision(Rectangle a, Rectangle b)
    {
        var aIsToTheLeft = a.right <= b.left;
        var aIsToTheRight = a.left >= b.right;
        var aIsAbove = a.bottom <= b.top;
        var aIsBelow = a.top >= b.bottom;
        return !(aIsToTheRight || aIsToTheLeft || aIsAbove || aIsBelow);
    }
    public string SolvePart1(string[] lines)
    {
        var points = Parse(lines);
        return (
            from r in RectanglesOrderedByArea(points)
            select Area(r)
        ).First().ToString();
    }

    public string SolvePart2(string[] lines)
    {
        var points = Parse(lines);
        var segments = Boundary(points).ToArray();
        return (
            from r in RectanglesOrderedByArea(points)
            where segments.All(s => !AabbCollision(r, s))
            select Area(r)
        ).First().ToString();
    }
}