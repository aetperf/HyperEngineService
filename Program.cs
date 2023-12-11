using System.Diagnostics;
using System.ServiceProcess;

// Wrapper for the HyperD service
// the program will start the HyperD.exe executable that will wait for connections
// The hyperd.exe executable file is localted in a specific folder defined with the HYPERENGINE_HOME environment variable
// The compiled service wrapper program HyperEngineService.exe should be located in the same folder as the hyperd.exe executable
// The service can be installed with the following command:
// $HYPERENGINE_HOME="D:\HyperEngine"
// sc create HyperEngineService binPath= "${HYPERENGINE_HOME}\HyperEngineService.exe" DisplayName="Hyper Database Engine" start=demand
// and uninstalled with the following command:
// sc delete HyperEngineService

public class HyperDService : ServiceBase
{
    private Process hyperdProcess = null;

    public HyperDService()
    {
        ServiceName = "HyperDService";
    }
    protected override void OnStart(string[] args)
    {
        base.OnStart(args);

        // Define the process to start
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            // The executable file is located in the same folder as the service wrapper HYPERENGINE_HOME
            FileName = "hyperd.exe",
            Arguments = "run --skip-license --no-password --listen-connection tab.tcp://localhost:8095 --init-user tableau_internal_user --restrict_dmv_access=0 --enable_hyper_event_logs_dmv=1 --external_table_sample_size_factor=0.005",
            UseShellExecute = false,
            CreateNoWindow = true
        };

        // Start the process
        hyperdProcess = Process.Start(startInfo);
    }
    protected override void OnStop()
    {
        if (hyperdProcess != null && !hyperdProcess.HasExited)
        {
            // Stop the process gracefully
            hyperdProcess.Kill();
        }

        base.OnStop();
    }
    public static void Main()
    {
        ServiceBase.Run(new HyperDService());
    }
}

