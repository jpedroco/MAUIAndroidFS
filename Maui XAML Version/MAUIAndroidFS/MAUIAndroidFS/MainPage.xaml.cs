namespace MAUIAndroidFS;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        this.Loaded += MainPage_Loaded;
        this.Unloaded += MainPage_Unloaded;
    }

    private void MainPage_Loaded(object sender, EventArgs e)
    {
#if ANDROID
        // Mio
        MiUdp.Envia("Loaded");
        MAUIAndroidFS.Platforms.Android.AndroidServiceManager.AsignaAccion(Evento);
        // Fin mio

        if (!MAUIAndroidFS.Platforms.Android.AndroidServiceManager.IsRunning)
        {
            MAUIAndroidFS.Platforms.Android.AndroidServiceManager.StartMyService();
            MessageLabel.Text = "Service has started";
        }
        else{
            MessageLabel.Text = "Service is running";
        }
#endif
    }

    // Mio
    private void MainPage_Unloaded(object sender, EventArgs e)
    {
        MiUdp.Envia("Unloaded");
#if ANDROID
        MAUIAndroidFS.Platforms.Android.AndroidServiceManager.DesasignaAccion();
#endif
    }
    // Fin mio

    private void StopButton_Clicked(object sender, EventArgs e)
    {
#if ANDROID
        MAUIAndroidFS.Platforms.Android.AndroidServiceManager.StopMyService();
        MessageLabel.Text = "Service is stopped";
#endif
    }

    private void Evento()
    {
        MiUdp.Envia("Activada página principal");
    }


}

