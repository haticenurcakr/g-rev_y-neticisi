using System;
using System.Data;
using System.Data.SqlClient; // SQL Server için gerekli
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using LibreHardwareMonitor.Hardware;
using LiveCharts;
using LiveCharts.Wpf;

namespace SistemPerformansTakibi
{
    public partial class Form1 : Form
    {
        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;
        private Computer computer;

        private Timer dbTimer; // Veritabanı kaydı için timer

        // 📌 **SQL Server Bağlantı Dizesi (Kendi Sunucana Göre Güncelle)**
        private string connectionString = "Server=HATICENUR;Database=Performance;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

        public Form1()
        {
            InitializeComponent();
            // Performans sayaçlarını başlat
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            // LibreHardwareMonitor başlat
            computer = new Computer
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true
            };
            computer.Open();

            // Timer başlat, her saniye verileri güncelle
            Timer timer = new Timer();
            timer.Interval = 1000; // 1 saniye
            timer.Tick += UpdateStats;
            timer.Start();

            // ✅ **10 saniyede bir SQL Server'a kayıt için yeni timer**
            dbTimer = new Timer();
            dbTimer.Interval = 10000; // 10 saniye
            dbTimer.Tick += SaveToDatabase;
            dbTimer.Start();

            // Grafik yüklemek için veritabanı verilerini al
            LoadData();
        }

        private void UpdateStats(object sender, EventArgs e)
        {
            // ✅ CPU Kullanımı
            float cpuUsage = cpuCounter.NextValue();
            progressCpu.Value = (int)cpuUsage;
            labelCpuUsage.Text = $"CPU Kullanımı: {cpuUsage:F1}%";

            // ✅ RAM Kullanımı
            float totalRam = 16000; // Bilgisayardaki toplam RAM (MB olarak)
            float usedRam = totalRam - ramCounter.NextValue();
            float ramUsage = (usedRam / totalRam) * 100;
            progressRam.Value = (int)ramUsage;
            labelRamUsage.Text = $"RAM: {usedRam:F1} MB / {totalRam} MB";

            // ✅ GPU Kullanımı
            float gpuUsage = GetGpuUsage();
            progressGpu.Value = (int)gpuUsage;
            labelgGpuUsage.Text = $"GPU Kullanımı: {gpuUsage:F1}%";

            // ✅ CPU ve GPU Sıcaklıkları
            float cpuTemp = GetTemperature(HardwareType.Cpu);
            float gpuTemp = GetTemperature(HardwareType.GpuNvidia) + GetTemperature(HardwareType.GpuAmd);

            labelCpuTemp.Text = $"CPU Sıcaklığı: {cpuTemp} °C";
            labelGpuTemp.Text = $"GPU Sıcaklığı: {gpuTemp} °C";

            // ✅ Disk Kullanımı
            DriveInfo drive = new DriveInfo("C");
            long usedDisk = drive.TotalSize - drive.AvailableFreeSpace;
            labelDiskUsage.Text = $"Disk: {usedDisk / 1024 / 1024 / 1024} GB / {drive.TotalSize / 1024 / 1024 / 1024} GB";
        }

        private float GetGpuUsage()
        {
            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.GpuNvidia || hardware.HardwareType == HardwareType.GpuAmd)
                {
                    hardware.Update();
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Load && sensor.Name.Contains("GPU Core"))
                        {
                            Console.WriteLine($"GPU Kullanımı: {sensor.Value.GetValueOrDefault()}%");
                            return sensor.Value.GetValueOrDefault();
                        }
                    }
                }
            }
            return -1; // -1 dönerse GPU kullanımı okunamamış demektir.
        }

        private float GetTemperature(HardwareType type)
        {
            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == type)
                {
                    hardware.Update(); // Sensörleri güncelle
                    foreach (var subHardware in hardware.SubHardware)
                    {
                        subHardware.Update(); // Alt donanımı güncelle
                    }
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            float? value = sensor.Value;
                            if (value.HasValue)
                            {
                                return value.Value; // Sıcaklık verisini döndür
                            }
                        }
                    }
                }
            }
            return -1; // Eğer sıcaklık değeri yoksa "-1" döndür
        }

        // ✅ **SQL Server'a her 10 saniyede bir veri kaydeden metod**
        private void SaveToDatabase(object sender, EventArgs e)
        {
            try
            {
                float cpuUsage = cpuCounter.NextValue();
                float totalRam = 16000;
                float usedRam = totalRam - ramCounter.NextValue();
                float ramUsage = (usedRam / totalRam) * 100;
                float gpuUsage = GetGpuUsage();
                float cpuTemp = GetTemperature(HardwareType.Cpu);
                float gpuTemp = GetTemperature(HardwareType.GpuNvidia) + GetTemperature(HardwareType.GpuAmd);
                DriveInfo drive = new DriveInfo("C");
                long usedDisk = drive.TotalSize - drive.AvailableFreeSpace;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO PerformanceData (Tarih, CPU_Kullanimi, RAM_Kullanimi, GPU_Kullanimi, CPU_Sicaklik, GPU_Sicaklik, Disk_Kullanimi) VALUES (@Tarih, @CPU_Kullanimi, @RAM_Kullanimi, @GPU_Kullanimi, @CPU_Sicaklik, @GPU_Sicaklik, @Disk_Kullanimi)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Tarih", DateTime.Now);
                        command.Parameters.AddWithValue("@CPU_Kullanimi", cpuUsage);
                        command.Parameters.AddWithValue("@RAM_Kullanimi", ramUsage);
                        command.Parameters.AddWithValue("@GPU_Kullanimi", gpuUsage);
                        command.Parameters.AddWithValue("@CPU_Sicaklik", cpuTemp);
                        command.Parameters.AddWithValue("@GPU_Sicaklik", gpuTemp);
                        command.Parameters.AddWithValue("@Disk_Kullanimi", usedDisk / 1024 / 1024 / 1024);

                        command.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("✅ Veri başarıyla kaydedildi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Hata: " + ex.Message);
            }
        }


        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Tarih, CPU_Kullanimi, RAM_Kullanimi, GPU_Kullanimi, CPU_Sicaklik, GPU_Sicaklik, Disk_Kullanimi FROM PerformanceData ORDER BY Tarih DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Verileri Grafiklere Ekle
                ChartValues<double> cpuValues = new ChartValues<double>();
                ChartValues<double> ramValues = new ChartValues<double>();
                ChartValues<double> gpuValues = new ChartValues<double>();
                ChartValues<double> cpuTempValues = new ChartValues<double>();
                ChartValues<double> gpuTempValues = new ChartValues<double>();
                ChartValues<double> diskValues = new ChartValues<double>();

                foreach (DataRow row in dt.Rows)
                {
                    cpuValues.Add(Convert.ToDouble(row["CPU_Kullanimi"]));
                    ramValues.Add(Convert.ToDouble(row["RAM_Kullanimi"]));
                    gpuValues.Add(Convert.ToDouble(row["GPU_Kullanimi"]));
                    cpuTempValues.Add(Convert.ToDouble(row["CPU_Sicaklik"]));
                    gpuTempValues.Add(Convert.ToDouble(row["GPU_Sicaklik"]));
                    diskValues.Add(Convert.ToDouble(row["Disk_Kullanimi"]));
                }

                // Grafik Serileri
                cartesianChart1.Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "CPU Kullanımı",
                        Values = cpuValues
                    }
                };

                cartesianChart2.Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "RAM Kullanımı",
                        Values = ramValues
                    }
                };

                cartesianChart3.Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "GPU Kullanımı",
                        Values = gpuValues
                    }
                };

                cartesianChart4.Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "CPU Sıcaklığı",
                        Values = cpuTempValues
                    }
                };

                cartesianChart5.Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "GPU Sıcaklığı",
                        Values = gpuTempValues
                    }
                };

                cartesianChart6.Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Disk Kullanımı",
                        Values = diskValues
                    }
                };
            }
        }

        private void cartesianChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void cartesianChart2_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void cartesianChart5_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void cartesianChart4_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
    }
}
