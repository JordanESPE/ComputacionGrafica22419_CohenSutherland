using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GraphicalProcessing
{
    public partial class CFrmCohenSutherlandVisualization : Form
    {
        private CLineClippingManager lineClippingManager;
        private bool isDrawingLineSegment = false;
        private Point lineStartVertex;
        private Point currentMousePosition;

        public CFrmCohenSutherlandVisualization()
        {
            InitializeComponent();
            lineClippingManager = new CLineClippingManager(CanvasDrawingSurface.ClientSize);
            ConfigureDataGridViews();

            CanvasDrawingSurface.MouseDown += CanvasDrawingSurface_MouseDown;
            CanvasDrawingSurface.MouseMove += CanvasDrawingSurface_MouseMove;
            CanvasDrawingSurface.MouseUp += CanvasDrawingSurface_MouseUp;
            CanvasDrawingSurface.Paint += CanvasDrawingSurface_Paint;
        }

        private void CanvasDrawingSurface_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawingLineSegment = true;
                lineStartVertex = e.Location;
                currentMousePosition = e.Location;
            }
        }

        private void CanvasDrawingSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawingLineSegment)
            {
                currentMousePosition = e.Location;
                CanvasDrawingSurface.Invalidate();
            }
        }

        private void CanvasDrawingSurface_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawingLineSegment)
            {
                isDrawingLineSegment = false;
                lineClippingManager.AddLineSegmentToCollection(lineStartVertex, e.Location);
                CanvasDrawingSurface.Invalidate();
                UpdateOriginalLineSegmentsGrid();
            }
        }

        private void CanvasDrawingSurface_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvasGraphics = e.Graphics;
            canvasGraphics.Clear(Color.White);

            lineClippingManager.RenderCanvasGrid(canvasGraphics, CanvasDrawingSurface.ClientSize);
            lineClippingManager.RenderViewportBoundary(canvasGraphics);
            lineClippingManager.RenderLineSegmentsOnCanvas(canvasGraphics);

            if (isDrawingLineSegment)
            {
                using (Pen previewLinePen = new Pen(Color.Green, 1))
                {
                    canvasGraphics.DrawLine(previewLinePen, lineStartVertex, currentMousePosition);
                }
            }

            RenderRegionOutcodes(canvasGraphics);
        }

        private void RenderRegionOutcodes(Graphics canvasGraphics)
        {
            var cohenSutherlandProcessor = lineClippingManager.GetCohenSutherlandProcessor();
            var viewportBoundary = lineClippingManager.GetClippingViewport();

            using (Font outcodeFont = new Font("Arial", 8))
            using (Brush outcodeBrush = new SolidBrush(Color.Black))
            {
                var regionPositions = new[]
                {
                    new { Position = new Point(viewportBoundary.Left - 50, viewportBoundary.Top - 20), Outcode = cohenSutherlandProcessor.ComputeRegionOutcode(new Point(viewportBoundary.Left - 50, viewportBoundary.Top - 20)) },
                    new { Position = new Point(viewportBoundary.Left + viewportBoundary.Width/2, viewportBoundary.Top - 20), Outcode = cohenSutherlandProcessor.ComputeRegionOutcode(new Point(viewportBoundary.Left + viewportBoundary.Width/2, viewportBoundary.Top - 20)) },
                    new { Position = new Point(viewportBoundary.Right + 20, viewportBoundary.Top - 20), Outcode = cohenSutherlandProcessor.ComputeRegionOutcode(new Point(viewportBoundary.Right + 20, viewportBoundary.Top - 20)) },
                    new { Position = new Point(viewportBoundary.Left - 50, viewportBoundary.Top + viewportBoundary.Height/2), Outcode = cohenSutherlandProcessor.ComputeRegionOutcode(new Point(viewportBoundary.Left - 50, viewportBoundary.Top + viewportBoundary.Height/2)) },
                    new { Position = new Point(viewportBoundary.Left + viewportBoundary.Width/2, viewportBoundary.Top + viewportBoundary.Height/2), Outcode = cohenSutherlandProcessor.ComputeRegionOutcode(new Point(viewportBoundary.Left + viewportBoundary.Width/2, viewportBoundary.Top + viewportBoundary.Height/2)) },
                    new { Position = new Point(viewportBoundary.Right + 20, viewportBoundary.Top + viewportBoundary.Height/2), Outcode = cohenSutherlandProcessor.ComputeRegionOutcode(new Point(viewportBoundary.Right + 20, viewportBoundary.Top + viewportBoundary.Height/2)) },
                    new { Position = new Point(viewportBoundary.Left - 50, viewportBoundary.Bottom + 20), Outcode = cohenSutherlandProcessor.ComputeRegionOutcode(new Point(viewportBoundary.Left - 50, viewportBoundary.Bottom + 20)) },
                    new { Position = new Point(viewportBoundary.Left + viewportBoundary.Width/2, viewportBoundary.Bottom + 20), Outcode = cohenSutherlandProcessor.ComputeRegionOutcode(new Point(viewportBoundary.Left + viewportBoundary.Width/2, viewportBoundary.Bottom + 20)) },
                    new { Position = new Point(viewportBoundary.Right + 20, viewportBoundary.Bottom + 20), Outcode = cohenSutherlandProcessor.ComputeRegionOutcode(new Point(viewportBoundary.Right + 20, viewportBoundary.Bottom + 20)) }
                };

                foreach (var regionData in regionPositions)
                {
                    string binaryOutcode = cohenSutherlandProcessor.GetBinaryOutcodeRepresentation(regionData.Outcode);
                    canvasGraphics.DrawString(binaryOutcode, outcodeFont, outcodeBrush, regionData.Position);
                }
            }
        }

        private void BtnExecuteClipping_Click(object sender, EventArgs e)
        {
            lineClippingManager.ExecuteCohenSutherlandClipping();
            CanvasDrawingSurface.Invalidate();
            UpdateClippedLineSegmentsGrid();
        }

        private void UpdateOriginalLineSegmentsGrid()
        {
            OriginalLineSegmentsGrid.Rows.Clear();
            var cohenSutherlandProcessor = lineClippingManager.GetCohenSutherlandProcessor();

            foreach (var lineSegment in lineClippingManager.GetOriginalLineSegments())
            {
                int startVertexOutcode = cohenSutherlandProcessor.ComputeRegionOutcode(lineSegment.startVertex);
                int endVertexOutcode = cohenSutherlandProcessor.ComputeRegionOutcode(lineSegment.endVertex);

                OriginalLineSegmentsGrid.Rows.Add(
                    $"({lineSegment.startVertex.X}, {lineSegment.startVertex.Y})",
                    $"({lineSegment.endVertex.X}, {lineSegment.endVertex.Y})",
                    cohenSutherlandProcessor.GetBinaryOutcodeRepresentation(startVertexOutcode),
                    cohenSutherlandProcessor.GetBinaryOutcodeRepresentation(endVertexOutcode)
                );
            }
        }

        private void UpdateClippedLineSegmentsGrid()
        {
            ClippedLineSegmentsGrid.Rows.Clear();

            foreach (var clippedSegment in lineClippingManager.GetClippedLineSegments())
            {
                ClippedLineSegmentsGrid.Rows.Add(
                    $"({clippedSegment.startVertex.X}, {clippedSegment.startVertex.Y})",
                    $"({clippedSegment.endVertex.X}, {clippedSegment.endVertex.Y})",
                    clippedSegment.isVisibleInViewport ? "Visible en Viewport" : "Fuera del Viewport",
                    clippedSegment.clippingResult
                );
            }
        }

        private void ConfigureDataGridViews()
        {
            OriginalLineSegmentsGrid.Columns.Clear();
            OriginalLineSegmentsGrid.Columns.Add("StartVertex", "Vertice Inicial");
            OriginalLineSegmentsGrid.Columns.Add("EndVertex", "Vertice Final");
            OriginalLineSegmentsGrid.Columns.Add("StartOutcode", "Outcode Inicio");
            OriginalLineSegmentsGrid.Columns.Add("EndOutcode", "Outcode Final");
            OriginalLineSegmentsGrid.AllowUserToAddRows = false;

            ClippedLineSegmentsGrid.Columns.Clear();
            ClippedLineSegmentsGrid.Columns.Add("ClippedStart", "Inicio Recortado");
            ClippedLineSegmentsGrid.Columns.Add("ClippedEnd", "Final Recortado");
            ClippedLineSegmentsGrid.Columns.Add("ViewportVisibility", "Visibilidad en Viewport");
            ClippedLineSegmentsGrid.Columns.Add("ClippingStatus", "Estado del Recorte");
            ClippedLineSegmentsGrid.AllowUserToAddRows = false;
        }

        private void BtnClearCanvas_Click(object sender, EventArgs e)
        {
            lineClippingManager.ClearAllLineSegments();
            OriginalLineSegmentsGrid.Rows.Clear();
            ClippedLineSegmentsGrid.Rows.Clear();
            CanvasDrawingSurface.Invalidate();
            isDrawingLineSegment = false;
        }

        private void BtnExitApplication_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CFrmCohenSutherlandVisualization_Load(object sender, EventArgs e)
        {
        }
    }
}