namespace SistemPerformansTakibi
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ProgressBar progressCpu;
        private System.Windows.Forms.Label labelCpuUsage;
        private System.Windows.Forms.ProgressBar progressRam;
        private System.Windows.Forms.Label labelRamUsage;
        private System.Windows.Forms.ProgressBar progressGpu;
        private System.Windows.Forms.Label labelgGpuUsage;
        private System.Windows.Forms.Label labelCpuTemp;
        private System.Windows.Forms.Label labelGpuTemp;
        private System.Windows.Forms.Label labelDiskUsage;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private LiveCharts.WinForms.CartesianChart cartesianChart2;
        private LiveCharts.WinForms.CartesianChart cartesianChart3;
        private LiveCharts.WinForms.CartesianChart cartesianChart4;
        private LiveCharts.WinForms.CartesianChart cartesianChart5;
        private LiveCharts.WinForms.CartesianChart cartesianChart6;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.progressCpu = new System.Windows.Forms.ProgressBar();
            this.labelCpuUsage = new System.Windows.Forms.Label();
            this.progressRam = new System.Windows.Forms.ProgressBar();
            this.labelRamUsage = new System.Windows.Forms.Label();
            this.progressGpu = new System.Windows.Forms.ProgressBar();
            this.labelgGpuUsage = new System.Windows.Forms.Label();
            this.labelCpuTemp = new System.Windows.Forms.Label();
            this.labelGpuTemp = new System.Windows.Forms.Label();
            this.labelDiskUsage = new System.Windows.Forms.Label();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.cartesianChart2 = new LiveCharts.WinForms.CartesianChart();
            this.cartesianChart3 = new LiveCharts.WinForms.CartesianChart();
            this.cartesianChart4 = new LiveCharts.WinForms.CartesianChart();
            this.cartesianChart5 = new LiveCharts.WinForms.CartesianChart();
            this.cartesianChart6 = new LiveCharts.WinForms.CartesianChart();
            this.SuspendLayout();
            // 
            // progressCpu
            // 
            this.progressCpu.Location = new System.Drawing.Point(0, 0);
            this.progressCpu.Name = "progressCpu";
            this.progressCpu.Size = new System.Drawing.Size(100, 23);
            this.progressCpu.TabIndex = 0;
            // 
            // labelCpuUsage
            // 
            this.labelCpuUsage.Location = new System.Drawing.Point(0, 0);
            this.labelCpuUsage.Name = "labelCpuUsage";
            this.labelCpuUsage.Size = new System.Drawing.Size(100, 23);
            this.labelCpuUsage.TabIndex = 1;
            // 
            // progressRam
            // 
            this.progressRam.Location = new System.Drawing.Point(0, 0);
            this.progressRam.Name = "progressRam";
            this.progressRam.Size = new System.Drawing.Size(100, 23);
            this.progressRam.TabIndex = 2;
            // 
            // labelRamUsage
            // 
            this.labelRamUsage.Location = new System.Drawing.Point(0, 0);
            this.labelRamUsage.Name = "labelRamUsage";
            this.labelRamUsage.Size = new System.Drawing.Size(100, 23);
            this.labelRamUsage.TabIndex = 3;
            // 
            // progressGpu
            // 
            this.progressGpu.Location = new System.Drawing.Point(0, 0);
            this.progressGpu.Name = "progressGpu";
            this.progressGpu.Size = new System.Drawing.Size(100, 23);
            this.progressGpu.TabIndex = 4;
            // 
            // labelgGpuUsage
            // 
            this.labelgGpuUsage.Location = new System.Drawing.Point(0, 0);
            this.labelgGpuUsage.Name = "labelgGpuUsage";
            this.labelgGpuUsage.Size = new System.Drawing.Size(100, 23);
            this.labelgGpuUsage.TabIndex = 5;
            // 
            // labelCpuTemp
            // 
            this.labelCpuTemp.Location = new System.Drawing.Point(0, 0);
            this.labelCpuTemp.Name = "labelCpuTemp";
            this.labelCpuTemp.Size = new System.Drawing.Size(100, 23);
            this.labelCpuTemp.TabIndex = 6;
            // 
            // labelGpuTemp
            // 
            this.labelGpuTemp.Location = new System.Drawing.Point(0, 0);
            this.labelGpuTemp.Name = "labelGpuTemp";
            this.labelGpuTemp.Size = new System.Drawing.Size(100, 23);
            this.labelGpuTemp.TabIndex = 7;
            // 
            // labelDiskUsage
            // 
            this.labelDiskUsage.Location = new System.Drawing.Point(0, 0);
            this.labelDiskUsage.Name = "labelDiskUsage";
            this.labelDiskUsage.Size = new System.Drawing.Size(100, 23);
            this.labelDiskUsage.TabIndex = 8;
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Location = new System.Drawing.Point(12, 266);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(370, 170);
            this.cartesianChart1.TabIndex = 9;
            this.cartesianChart1.ChildChanged += new System.EventHandler<System.Windows.Forms.Integration.ChildChangedEventArgs>(this.cartesianChart1_ChildChanged);
            // 
            // cartesianChart2
            // 
            this.cartesianChart2.Location = new System.Drawing.Point(392, 29);
            this.cartesianChart2.Name = "cartesianChart2";
            this.cartesianChart2.Size = new System.Drawing.Size(316, 170);
            this.cartesianChart2.TabIndex = 10;
            this.cartesianChart2.ChildChanged += new System.EventHandler<System.Windows.Forms.Integration.ChildChangedEventArgs>(this.cartesianChart2_ChildChanged);
            // 
            // cartesianChart3
            // 
            this.cartesianChart3.Location = new System.Drawing.Point(392, 259);
            this.cartesianChart3.Name = "cartesianChart3";
            this.cartesianChart3.Size = new System.Drawing.Size(362, 177);
            this.cartesianChart3.TabIndex = 11;
            // 
            // cartesianChart4
            // 
            this.cartesianChart4.Location = new System.Drawing.Point(790, 29);
            this.cartesianChart4.Name = "cartesianChart4";
            this.cartesianChart4.Size = new System.Drawing.Size(292, 170);
            this.cartesianChart4.TabIndex = 12;
            this.cartesianChart4.ChildChanged += new System.EventHandler<System.Windows.Forms.Integration.ChildChangedEventArgs>(this.cartesianChart4_ChildChanged);
            // 
            // cartesianChart5
            // 
            this.cartesianChart5.Location = new System.Drawing.Point(790, 248);
            this.cartesianChart5.Name = "cartesianChart5";
            this.cartesianChart5.Size = new System.Drawing.Size(321, 188);
            this.cartesianChart5.TabIndex = 13;
            this.cartesianChart5.ChildChanged += new System.EventHandler<System.Windows.Forms.Integration.ChildChangedEventArgs>(this.cartesianChart5_ChildChanged);
            // 
            // cartesianChart6
            // 
            this.cartesianChart6.Location = new System.Drawing.Point(12, 29);
            this.cartesianChart6.Name = "cartesianChart6";
            this.cartesianChart6.Size = new System.Drawing.Size(317, 170);
            this.cartesianChart6.TabIndex = 14;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1199, 600);
            this.Controls.Add(this.progressCpu);
            this.Controls.Add(this.labelCpuUsage);
            this.Controls.Add(this.progressRam);
            this.Controls.Add(this.labelRamUsage);
            this.Controls.Add(this.progressGpu);
            this.Controls.Add(this.labelgGpuUsage);
            this.Controls.Add(this.labelCpuTemp);
            this.Controls.Add(this.labelGpuTemp);
            this.Controls.Add(this.labelDiskUsage);
            this.Controls.Add(this.cartesianChart1);
            this.Controls.Add(this.cartesianChart2);
            this.Controls.Add(this.cartesianChart3);
            this.Controls.Add(this.cartesianChart4);
            this.Controls.Add(this.cartesianChart5);
            this.Controls.Add(this.cartesianChart6);
            this.Name = "Form1";
            this.ResumeLayout(false);

        }
    }
}
