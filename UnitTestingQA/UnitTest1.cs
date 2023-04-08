using System.Diagnostics;
using DataManagementQA;

namespace UnitTestingQA
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckProcesses_KillsProcess_AfterMaxLifetime()
        {
            // Arrange
            string processName = "notepad";
            int maxLifetime = 1;
            int frequency = 1;
            Process process = Process.Start(processName);

            // Act
            Timer timer = new Timer(Program.CheckProcesses, (processName, maxLifetime), TimeSpan.Zero, TimeSpan.FromMinutes(frequency));
            Thread.Sleep(TimeSpan.FromMinutes(maxLifetime + 1));

            // Assert
            Assert.IsTrue(process.HasExited);
        }



        [Test]
        public void Main_ThrowsException_WhenIncorrectParametersArePassed()
        {
            // Arrange
            string[] args = new string[] { "notepad", "abc", "1" };

            // Assert
            Assert.Throws<FormatException>(() => Program.Main(args));
        }

        [Test]
        public void CheckProcesses_KillsMultipleProcesses_AfterMaxLifetime()
        {
            // Arrange
            string processName1 = "notepad";
            string processName2 = "calc";
            int maxLifetime = 1;
            int frequency = 1;
            Process process1 = Process.Start(processName1);
            Process process2 = Process.Start(processName2);

            // Act
            Timer timer = new Timer(Program.CheckProcesses, (processName1, maxLifetime), TimeSpan.Zero, TimeSpan.FromMinutes(frequency));
            Timer timer2 = new Timer(Program.CheckProcesses, (processName2, maxLifetime), TimeSpan.Zero, TimeSpan.FromMinutes(frequency));
            Thread.Sleep(TimeSpan.FromMinutes(maxLifetime + 1));

            // Assert
            Assert.IsTrue(process1.HasExited);
            Assert.IsTrue(process2.HasExited);
        }
    }
}