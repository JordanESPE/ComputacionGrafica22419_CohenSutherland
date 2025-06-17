using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

public class CCohenSutherlandClipper
{
    private Rectangle viewportWindow;
    private const int INSIDE_REGION = 0;
    private const int LEFT_REGION = 1;
    private const int RIGHT_REGION = 2;
    private const int BOTTOM_REGION = 4;
    private const int TOP_REGION = 8;

    public CCohenSutherlandClipper(Rectangle viewportWindow)
    {
        this.viewportWindow = viewportWindow;
    }

    public List<(Point startVertex, Point endVertex, bool isVisibleInViewport, string clippingResult)> ClipLineSegments(List<(Point startVertex, Point endVertex)> lineSegments)
    {
        var clippedSegments = new List<(Point startVertex, Point endVertex, bool isVisibleInViewport, string clippingResult)>();

        foreach (var lineSegment in lineSegments)
        {
            var clippingResult = ApplyCohenSutherlandAlgorithm(lineSegment.startVertex, lineSegment.endVertex);
            clippedSegments.Add((clippingResult.clippedStart, clippingResult.clippedEnd, clippingResult.isAccepted, clippingResult.algorithmStatus));
        }

        return clippedSegments;
    }

    private (Point clippedStart, Point clippedEnd, bool isAccepted, string algorithmStatus) ApplyCohenSutherlandAlgorithm(Point firstEndpoint, Point secondEndpoint)
    {
        int firstOutcode = ComputeRegionOutcode(firstEndpoint);
        int secondOutcode = ComputeRegionOutcode(secondEndpoint);
        bool lineAccepted = false;
        Point clippedStart = firstEndpoint;
        Point clippedEnd = secondEndpoint;

        while (true)
        {
            if ((firstOutcode | secondOutcode) == 0)
            {
                lineAccepted = true;
                return (clippedStart, clippedEnd, lineAccepted, "Aceptacion Trivial");
            }
            else if ((firstOutcode & secondOutcode) != 0)
            {
                return (firstEndpoint, secondEndpoint, false, "Rechazo Trivial");
            }
            else
            {
                int outsideOutcode = (firstOutcode != 0) ? firstOutcode : secondOutcode;
                Point intersectionPoint = CalculateViewportIntersection(clippedStart, clippedEnd, outsideOutcode);

                if (outsideOutcode == firstOutcode)
                {
                    firstOutcode = ComputeRegionOutcode(intersectionPoint);
                    clippedStart = intersectionPoint;
                }
                else
                {
                    secondOutcode = ComputeRegionOutcode(intersectionPoint);
                    clippedEnd = intersectionPoint;
                }
            }
        }
    }

    private Point CalculateViewportIntersection(Point lineStart, Point lineEnd, int regionOutcode)
    {
        double intersectionX = 0, intersectionY = 0;

        if ((regionOutcode & TOP_REGION) != 0)
        {
            intersectionX = lineStart.X + (double)(lineEnd.X - lineStart.X) * (viewportWindow.Top - lineStart.Y) / (lineEnd.Y - lineStart.Y);
            intersectionY = viewportWindow.Top;
        }
        else if ((regionOutcode & BOTTOM_REGION) != 0)
        {
            intersectionX = lineStart.X + (double)(lineEnd.X - lineStart.X) * (viewportWindow.Bottom - lineStart.Y) / (lineEnd.Y - lineStart.Y);
            intersectionY = viewportWindow.Bottom;
        }
        else if ((regionOutcode & RIGHT_REGION) != 0)
        {
            intersectionY = lineStart.Y + (double)(lineEnd.Y - lineStart.Y) * (viewportWindow.Right - lineStart.X) / (lineEnd.X - lineStart.X);
            intersectionX = viewportWindow.Right;
        }
        else if ((regionOutcode & LEFT_REGION) != 0)
        {
            intersectionY = lineStart.Y + (double)(lineEnd.Y - lineStart.Y) * (viewportWindow.Left - lineStart.X) / (lineEnd.X - lineStart.X);
            intersectionX = viewportWindow.Left;
        }

        return new Point((int)Math.Round(intersectionX), (int)Math.Round(intersectionY));
    }

    public int ComputeRegionOutcode(Point vertex)
    {
        int regionCode = INSIDE_REGION;

        if (vertex.X < viewportWindow.Left)
            regionCode |= LEFT_REGION;
        else if (vertex.X > viewportWindow.Right)
            regionCode |= RIGHT_REGION;

        if (vertex.Y < viewportWindow.Top)
            regionCode |= TOP_REGION;
        else if (vertex.Y > viewportWindow.Bottom)
            regionCode |= BOTTOM_REGION;

        return regionCode;
    }

    public string GetBinaryOutcodeRepresentation(int regionOutcode)
    {
        return Convert.ToString(regionOutcode, 2).PadLeft(4, '0');
    }

    public Rectangle GetViewportWindow()
    {
        return viewportWindow;
    }
}