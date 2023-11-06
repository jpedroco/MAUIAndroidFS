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

    private void MainPage_Unloaded(object sender, EventArgs e)
    {
#if ANDROID
        
#endif
    }

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

