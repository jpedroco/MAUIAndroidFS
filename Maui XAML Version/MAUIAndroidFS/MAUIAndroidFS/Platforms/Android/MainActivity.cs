using Android.App;
using Android.Content.PM;
using Android.Content;
using MAUIAndroidFS.Platforms.Android;

namespace MAUIAndroidFS;

//[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true,
//    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation
//    | ConfigChanges.UiMode | ConfigChanges.ScreenLayout
//    | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation
    | ConfigChanges.UiMode | ConfigChanges.ScreenLayout
    | ConfigChanges.SmallestScreenSize | ConfigChanges.Density,
    LaunchMode = LaunchMode.SingleTop)]
public class MainActivity : MauiAppCompatActivity
{
    public MainActivity()
    {
        AndroidServiceManager.MainActivity = this;
    }

    public void StartService()
    {
        var serviceIntent = new Intent(this, typeof(MyBackgroundService));
        serviceIntent.PutExtra("inputExtra", "Background Service");
        StartService(serviceIntent);
    }

    // Prueba. Pongo override de OnActivityResult para ver en qué momento se lanza y qué devuelve en requestCode
    protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
    {
        base.OnActivityResult(requestCode, resultCode, data);
    }

    public void StopService()
    {
        var serviceIntent = new Intent(this, typeof(MyBackgroundService));
        StopService(serviceIntent);
    }
}