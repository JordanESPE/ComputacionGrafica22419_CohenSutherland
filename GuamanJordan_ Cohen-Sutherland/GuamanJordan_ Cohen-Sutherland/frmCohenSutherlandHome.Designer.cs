using System;

namespace GraphicalProcessing
{
    partial class CFrmCohenSutherlandVisualization
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.CanvasDrawingSurface = new System.Windows.Forms.PictureBox();
            this.BtnExecuteClipping = new System.Windows.Forms.Button();
            this.OriginalLineSegmentsGrid = new System.Windows.Forms.DataGridView();
            this.ClippedLineSegmentsGrid = new System.Windows.Forms.DataGridView();
            this.BtnClearCanvas = new System.Windows.Forms.Button();
            this.LblOriginalSegments = new System.Windows.Forms.Label();
            this.LblClippedSegments = new System.Windows.Forms.Label();
            this.LblCohenSutherlandProcess = new System.Windows.Forms.Label();
            this.BtnExitApplication = new System.Windows.Forms.Button();
            this.LblInstructions = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CanvasDrawingSurface)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OriginalLineSegmentsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClippedLineSegmentsGrid)).BeginInit();
            this.SuspendLayout();

            this.CanvasDrawingSurface.BackColor = System.Drawing.SystemColors.Window;
            this.CanvasDrawingSurface.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CanvasDrawingSurface.Location = new System.Drawing.Point(450, 12);
            this.CanvasDrawingSurface.Name = "CanvasDrawingSurface";
            this.CanvasDrawingSurface.Size = new System.Drawing.Size(800, 600);
            this.CanvasDrawingSurface.TabIndex = 0;
            this.CanvasDrawingSurface.TabStop = false;

            this.BtnExecuteClipping.Location = new System.Drawing.Point(12, 80);
            this.BtnExecuteClipping.Name = "BtnExecuteClipping";
            this.BtnExecuteClipping.Size = new System.Drawing.Size(120, 30);
            this.BtnExecuteClipping.TabIndex = 1;
            this.BtnExecuteClipping.Text = "Ejecutar Recorte";
            this.BtnExecuteClipping.UseVisualStyleBackColor = true;
            this.BtnExecuteClipping.Click += new System.EventHandler(this.BtnExecuteClipping_Click);

            this.OriginalLineSegmentsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OriginalLineSegmentsGrid.Location = new System.Drawing.Point(12, 150);
            this.OriginalLineSegmentsGrid.Name = "OriginalLineSegmentsGrid";
            this.OriginalLineSegmentsGrid.RowHeadersWidth = 51;
            this.OriginalLineSegmentsGrid.Size = new System.Drawing.Size(420, 200);
            this.OriginalLineSegmentsGrid.TabIndex = 2;

            this.ClippedLineSegmentsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ClippedLineSegmentsGrid.Location = new System.Drawing.Point(12, 390);
            this.ClippedLineSegmentsGrid.Name = "ClippedLineSegmentsGrid";
            this.ClippedLineSegmentsGrid.RowHeadersWidth = 51;
            this.ClippedLineSegmentsGrid.Size = new System.Drawing.Size(420, 200);
            this.ClippedLineSegmentsGrid.TabIndex = 3;

            this.BtnClearCanvas.Location = new System.Drawing.Point(150, 80);
            this.BtnClearCanvas.Name = "BtnClearCanvas";
            this.BtnClearCanvas.Size = new System.Drawing.Size(100, 30);
            this.BtnClearCanvas.TabIndex = 4;
            this.BtnClearCanvas.Text = "Limpiar Canvas";
            this.BtnClearCanvas.UseVisualStyleBackColor = true;
            this.BtnClearCanvas.Click += new System.EventHandler(this.BtnClearCanvas_Click);

            this.LblOriginalSegments.AutoSize = true;
            this.LblOriginalSegments.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.LblOriginalSegments.Location = new System.Drawing.Point(12, 130);
            this.LblOriginalSegments.Name = "LblOriginalSegments";
            this.LblOriginalSegments.Size = new System.Drawing.Size(180, 15);
            this.LblOriginalSegments.TabIndex = 5;
            this.LblOriginalSegments.Text = "Segmentos de Linea Originales";

            this.LblClippedSegments.AutoSize = true;
            this.LblClippedSegments.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.LblClippedSegments.Location = new System.Drawing.Point(12, 370);
            this.LblClippedSegments.Name = "LblClippedSegments";
            this.LblClippedSegments.Size = new System.Drawing.Size(190, 15);
            this.LblClippedSegments.TabIndex = 6;
            this.LblClippedSegments.Text = "Segmentos Procesados (Cohen-Sutherland)";

            this.LblCohenSutherlandProcess.AutoSize = true;
            this.LblCohenSutherlandProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.LblCohenSutherlandProcess.Location = new System.Drawing.Point(12, 50);
            this.LblCohenSutherlandProcess.Name = "LblCohenSutherlandProcess";
            this.LblCohenSutherlandProcess.Size = new System.Drawing.Size(180, 17);
            this.LblCohenSutherlandProcess.TabIndex = 7;
            this.LblCohenSutherlandProcess.Text = "Proceso Cohen-Sutherland";

            this.BtnExitApplication.Location = new System.Drawing.Point(270, 80);
            this.BtnExitApplication.Name = "BtnExitApplication";
            this.BtnExitApplication.Size = new System.Drawing.Size(100, 30);
            this.BtnExitApplication.TabIndex = 8;
            this.BtnExitApplication.Text = "Salir";
            this.BtnExitApplication.UseVisualStyleBackColor = true;
            this.BtnExitApplication.Click += new System.EventHandler(this.BtnExitApplication_Click);

            this.LblInstructions.AutoSize = true;
            this.LblInstructions.Location = new System.Drawing.Point(12, 12);
            this.LblInstructions.Name = "LblInstructions";
            this.LblInstructions.Size = new System.Drawing.Size(400, 15);
            this.LblInstructions.TabIndex = 9;
            this.LblInstructions.Text = "Instrucciones: Click izquierdo + arrastrar para dibujar lineas.";

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 620);
            this.Controls.Add(this.LblInstructions);
            this.Controls.Add(this.BtnExitApplication);
            this.Controls.Add(this.LblCohenSutherlandProcess);
            this.Controls.Add(this.LblClippedSegments);
            this.Controls.Add(this.LblOriginalSegments);
            this.Controls.Add(this.BtnClearCanvas);
            this.Controls.Add(this.ClippedLineSegmentsGrid);
            this.Controls.Add(this.OriginalLineSegmentsGrid);
            this.Controls.Add(this.BtnExecuteClipping);
            this.Controls.Add(this.CanvasDrawingSurface);
            this.Name = "CFrmCohenSutherlandVisualization";
            this.Text = "Visualizacion del Algoritmo Cohen-Sutherland para Recorte de Lineas";
            this.Load += new System.EventHandler(this.CFrmCohenSutherlandVisualization_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CanvasDrawingSurface)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OriginalLineSegmentsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClippedLineSegmentsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox CanvasDrawingSurface;
        private System.Windows.Forms.Button BtnExecuteClipping;
        private System.Windows.Forms.DataGridView OriginalLineSegmentsGrid;
        private System.Windows.Forms.DataGridView ClippedLineSegmentsGrid;
        private System.Windows.Forms.Button BtnClearCanvas;
        private System.Windows.Forms.Label LblOriginalSegments;
        private System.Windows.Forms.Label LblClippedSegments;
        private System.Windows.Forms.Label LblCohenSutherlandProcess;
        private System.Windows.Forms.Button BtnExitApplication;
        private System.Windows.Forms.Label LblInstructions;
    }
}