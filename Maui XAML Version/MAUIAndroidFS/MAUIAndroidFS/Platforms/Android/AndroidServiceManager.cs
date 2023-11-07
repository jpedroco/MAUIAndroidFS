using Android.Content;

namespace MAUIAndroidFS.Platforms.Android;

public static class AndroidServiceManager
{
    public static MainActivity MainActivity { get; set; }

    public static bool IsRunning { get; set; }

    // Mio
    public static Action Accion { get; set; }

    public static void AsignaAccion(Action action)
    {
        try
        {
            Accion = action;
        } catch (Exception ex) { }
    }
    public static void DesasignaAccion()
    {
        try
        {
            Accion = null;
        }
        catch (Exception ex) { }
    }

    public static void EjecutaAccion()
    {
        try
        {
            Accion?.Invoke();
        }
        catch (Exception ex) { }
    }

    // Fin Mio

    public static void StartMyService()
    {
        if (MainActivity == null) return;
        MainActivity.StartService();
    }

    public static void StopMyService()
    {
        if (MainActivity == null) return;
        MainActivity.StopService();
        IsRunning = false;
    }





}