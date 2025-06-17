using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

public class CLineClippingManager
{
    private List<(Point startVertex, Point endVertex)> originalLineSegments;
    private List<(Point startVertex, Point endVertex, bool isVisibleInViewport, string clippingResult)> clippedLineSegments;
    private Rectangle clippingViewport;
    private Size drawingCanvasSize;
    private CCohenSutherlandClipper cohenSutherlandProcessor;

    public CLineClippingManager(Size canvasSize)
    {
        drawingCanvasSize = canvasSize;
        originalLineSegments = new List<(Point startVertex, Point endVertex)>();
        clippedLineSegments = new List<(Point startVertex, Point endVertex, bool isVisibleInViewport, string clippingResult)>();
        clippingViewport = new Rectangle(
            drawingCanvasSize.Width / 4,
            drawingCanvasSize.Height / 4,
            drawingCanvasSize.Width / 2,
            drawingCanvasSize.Height / 2
        );
        cohenSutherlandProcessor = new CCohenSutherlandClipper(clippingViewport);
    }

    public void AddLineSegmentToCollection(Point startVertex, Point endVertex)
    {
        originalLineSegments.Add((startVertex, endVertex));
    }

    public void ExecuteCohenSutherlandClipping()
    {
        clippedLineSegments = cohenSutherlandProcessor.ClipLineSegments(originalLineSegments);
    }

    public void ClearAllLineSegments()
    {
        originalLineSegments.Clear();
        clippedLineSegments.Clear();
    }

    public List<(Point startVertex, Point endVertex, bool isVisibleInViewport, string clippingResult)> GetClippedLineSegments()
    {
        return clippedLineSegments;
    }

    public List<(Point startVertex, Point endVertex)> GetOriginalLineSegments()
    {
        return originalLineSegments;
    }

    public Rectangle GetClippingViewport()
    {
        return clippingViewport;
    }

    public void RenderLineSegmentsOnCanvas(Graphics canvasGraphics)
    {
        using (Pen originalLinesPen = new Pen(Color.Gray, 1))
        using (Pen visibleLinesPen = new Pen(Color.Blue, 3))
        using (Pen hiddenLinesPen = new Pen(Color.Red, 1))
        {
            foreach (var originalSegment in originalLineSegments)
            {
                canvasGraphics.DrawLine(originalLinesPen, originalSegment.startVertex, originalSegment.endVertex);
            }

            foreach (var clippedSegment in clippedLineSegments)
            {
                if (clippedSegment.isVisibleInViewport)
                {
                    canvasGraphics.DrawLine(visibleLinesPen, clippedSegment.startVertex, clippedSegment.endVertex);
                }
                else
                {
                    canvasGraphics.DrawLine(hiddenLinesPen, clippedSegment.startVertex, clippedSegment.endVertex);
                }
            }
        }
    }

    public void RenderViewportBoundary(Graphics canvasGraphics)
    {
        using (Pen viewportBoundaryPen = new Pen(Color.Red, 2))
        {
            canvasGraphics.DrawRectangle(viewportBoundaryPen, clippingViewport);
        }
    }

    public void RenderCanvasGrid(Graphics canvasGraphics, Size canvasSize)
    {
        using (Pen gridLinesPen = new Pen(Color.LightGray, 1))
        {
            for (int xPosition = 0; xPosition <= canvasSize.Width; xPosition += 50)
            {
                canvasGraphics.DrawLine(gridLinesPen, xPosition, 0, xPosition, canvasSize.Height);
            }
            for (int yPosition = 0; yPosition <= canvasSize.Height; yPosition += 50)
            {
                canvasGraphics.DrawLine(gridLinesPen, 0, yPosition, canvasSize.Width, yPosition);
            }
        }
    }

    public CCohenSutherlandClipper GetCohenSutherlandProcessor()
    {
        return cohenSutherlandProcessor;
    }
}